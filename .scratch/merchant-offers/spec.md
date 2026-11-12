# Spec: Merchant Offers

Status: ready-for-agent

## Problem Statement

Every item the Merchant sells is wired in by hand in several places. Each one has its own price and bonus constants, its own Sold Out flag, and its own Buy method, which repeats the same "Sold Out before Gold, a refused purchase changes nothing" rule. Game's merchant screen repeats the list again: one hand-written menu line, Sold Out tag, and numbered branch per item. The menu numbers ("1. ", "2. ", … "4. Leave") are part of the translated text in all three languages. The Healing Potion's restore amount is defined twice, on Player and on Merchant. Adding, removing, or reordering an item means editing all of these, and the purchase rule is tested three times over.

## Solution

A pure refactor with no visible change to play. The Merchant gets a small interface: its list of Offers, in menu order, and one Buy operation that takes the item to buy. The purchase rule is written once, inside the Merchant. Game draws the merchant menu by looping over the Offers. It numbers the lines itself and puts "Leave" after the last Offer, so the translations hold only each item's wording.

## User Stories

1. As a Player, I want the Merchant's menu to show the same items, in the same order, at the same prices, with the same wording in every language, so that this change is invisible to me.
2. As a Player, I want the menu numbered 1 to 3 for the items and 4 for Leave, as today, so that the controls don't change.
3. As a Player, I want items I've already bought still marked Sold Out, so that I know what's left.
4. As a Player, I want buying, refusals for Sold Out and not enough Gold, and the purchase messages to work exactly as today, so that trading feels the same.
5. As a maintainer, I want an item's price, bonus, and effect defined in one place, so that changing an item is one edit.
6. As a maintainer, I want the menu to be drawn from the Merchant's Offers, so that the menu can't drift from what the Merchant sells.
7. As a maintainer, I want the Sold Out and Gold rules tested once for every item, so that a new item is covered by adding one data row.

## Implementation Decisions

- **Pure refactor.** Same items, prices, bonuses, menu order, and wording. Sold Out is still checked before Gold, each item can still be bought at most once (including the Healing Potion), and a refused purchase still changes nothing.
- **Items are named by an enumeration,** `MerchantItem { MagicalArmor, MagicalWeapon, HealingPotion }`, declared in that order, which is the menu order.
- **The Merchant's interface is two members:**
  - `IReadOnlyList<Offer> Offers`: one Offer per item, in menu order, with the current Sold Out state.
  - `PurchaseResult Buy(MerchantItem item)`: checks Sold Out, then Gold, then takes the Gold, applies the item's effect, and marks it Sold Out. What each item gives the Player lives in one private switch: Magical Armor sets Protection, the Magical Weapon enchants the Player's own weapon (higher maximum damage, "magical " name prefix, same art), and the Healing Potion adds one to the Player's potion count. An undefined `MerchantItem` value throws `ArgumentOutOfRangeException`.
- **Offer is a readonly record struct,** `Offer(MerchantItem Item, int Price, int Amount, bool IsSoldOut)`. `Amount` is the number shown on the item's menu line: Protection for Magical Armor (3), damage bonus for the Magical Weapon (4), maximum restore for the Healing Potion (8).
- **Constants.** The price and bonus constants become private to the Merchant. `Merchant.HealingPotionMaxRestore` is deleted. The potion's `Amount` comes from `Player.HealingPotionMaxRestore`, which stays the single owner because the drink rule uses it.
- **Removed interface.** `BuyMagicalArmor`, `BuyMagicalWeapon`, `BuyHealingPotion`, `IsMagicalArmorSoldOut`, `IsMagicalWeaponSoldOut`, and `IsHealingPotionSoldOut` are deleted. Nothing keeps them around as shortcuts.
- **The Merchant still never touches the console.**
- **Game keeps the merchant screen.** `HandleMerchantEncounter` loops over the Offers. For each one it prints `"{n}. "` followed by the formatted menu line for that item (`{0}` = Amount, `{1}` = Price), plus the Sold Out tag when it applies. Then it prints `"{offers + 1}. "` followed by the Leave text. Input `1`..`offers` buys that Offer's item, `offers + 1` leaves, and anything else prints the usual invalid message.
- **Game maps items to text.** A switch in Game maps each `MerchantItem` to its menu-line key and its purchased message: armor shows the Player's Protection, the weapon shows the translated weapon name, and the potion has no arguments. Refusal messages are unchanged.
- **Localization.** The leading `"1. "`, `"2. "`, `"3. "`, `"4. "` are removed from `merchant_menu_armor`, `merchant_menu_weapon`, `merchant_menu_potion`, and `merchant_menu_leave` in English, Spanish, and French. All other wording and placeholders are unchanged.
- **Test access to the screen.** `HandleMerchantEncounter` becomes `internal`, and the game project declares `InternalsVisibleTo` for `CYOATests`.
- **Glossary.** Use the CONTEXT.md terms Offer, Merchant, Sold Out, Gold, Magical Armor, Magical Weapon, Healing Potion, Protection.

## Testing Decisions

- **What makes a good test:** it calls the Merchant's interface (`Offers`, `Buy`) and asserts on the result and on visible Player and Offer state. It doesn't reach into private fields.
- **Merchant (main seam), data-driven over every `MerchantItem`:**
  - With enough Gold: returns Purchased, takes the price, and the Offer is now Sold Out.
  - With exactly the price: succeeds and leaves 0 Gold.
  - With one Gold short: returns NotEnoughGold and changes nothing (Gold, Offer Sold Out state, the Player's gear).
  - A second purchase, even when affordable: returns SoldOut and changes nothing.
  - Buying one item doesn't sell out the others.
- **Merchant, one test per item effect:** armor sets Protection to 3; the weapon enchants the Player's own weapon for each occupation's weapon (existing data rows); the potion adds one to the potion count.
- **Merchant, Offers:** in menu order (armor, weapon, potion), with prices 75, 75, 25 and amounts 3, 4, 8 as literal numbers, a deliberate double-check against the private constants. The potion's amount equals `Player.HealingPotionMaxRestore`.
- **Merchant, both-items tests:** exactly 150 Gold buys armor and weapon; 149 Gold refuses the second.
- **Merchant, undefined item:** `Buy((MerchantItem)99)` throws `ArgumentOutOfRangeException`.
- **Game, one console-scripted test:** with a Player holding a known amount of Gold, scripted input buys Magical Armor and then leaves. The output shows the lines numbered 1–4 with Leave last, and after the purchase the armor line carries the Sold Out tag. It uses `Console.SetIn`/`SetOut` and restores both, like the existing Dragon encounter test.
- **Messages:** the existing merchant key tests still pass. Any test that asserted the leading number inside a `merchant_menu_*` string is updated.
- **Prior art:** the existing Merchant tests (Sold Out before Gold, exact-Gold edges, enchanting data rows) and the Dragon encounter test's console scripting.

## Out of Scope

- Any rule change: buying more than one Healing Potion, new items, price changes, selling items back.
- Moving the merchant screen out of Game or putting the console behind a seam.
- Moving item text lookup into Messages.

## Further Notes

- **Why an enumeration, not offer objects with their own effect:** there are three fixed items, none come from data, and there's one Merchant. Letting each item carry its own effect would add a seam with only one adapter. The private switch keeps the Merchant as the one place that knows what each item does.
- **Implementation tickets:** 01 replaces the Merchant's per-item interface with Offers and Buy, and points Game at it with the menu unchanged. 02 has Game draw and number the menu from the Offers, removes the numbers from the translations, and adds the menu test. It depends on 01.
