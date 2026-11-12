# Spec: Healing Potion

Status: ready-for-agent

## Problem Statement

Once the fight with the Dragon starts, the Player's only choices each round are to attack or retreat. Health only ever goes down, so a Player who is losing has no way to recover and no decision to make beyond "keep swinging or run". Gold left over after the Merchant has no use.

## Solution

The Merchant sells a Healing Potion for 25 Gold, at most one per adventure. During combat with the Dragon, the prompt after each exchange gains a third option: drink the Healing Potion. Drinking restores a d8 roll of health, never above the Player's Max Health (the health they rolled at character creation), and takes the place of the Player's next attack: the Dragon still attacks that round. Drinking is refused, without using the potion or the turn, when the Player has no potion or is already at Max Health.

## User Stories

### Buying

1. As a Player, I want the Merchant to sell a Healing Potion, so that I can prepare for a long fight with the Dragon.
2. As a Player, I want the Healing Potion to cost 25 Gold, so that I can afford it alongside one piece of gear.
3. As a Player, I want the menu to list the Healing Potion with its benefit and price, so that I can decide whether to buy it.
4. As a Player who can't afford the potion, I want to be told I don't have enough Gold, with nothing changed.
5. As a Player, I want the Healing Potion to be marked "(sold out)" after I buy it, and refused with no Gold taken if I pick it again, consistent with the other Merchant items.
6. As a Player, I want to see how many Healing Potions I have on my stats screen and the combat stats screen, so that I know whether I can drink one.

### Drinking in combat

7. As a Player in combat, I want a "drink potion" option at the prompt after each exchange, alongside attack and retreat.
8. As a Player, I want to type `d` or `drink` to drink, just as I type `a`/`attack` or `r`/`retreat`.
9. As a Player, I want drinking to restore between 1 and 8 health, so that healing is part of the game's dice luck.
10. As a Player, I want healing never to raise my health above my Max Health, so that a potion can't become a free stat boost.
11. As a Player, I want to be told how much health I actually regained and my health against my Max Health (e.g. 18/18), so that I understand the cap.
12. As a Player, I want drinking to use up my turn, so that the Dragon still attacks me that round and I don't attack.
13. As a Player with no Healing Potion, I want to be told I have none, without losing my turn, and be asked again.
14. As a Player already at Max Health, I want to be told I'm at full health, keeping my potion and my turn, and be asked again.
15. As a Player, I want the potion to be used up after a successful drink, so that it's single-use.

### Languages

16. As a Spanish- or French-speaking Player, I want all new Healing Potion text in my language.

## Implementation Decisions

- **Player gains Max Health.** It is set to the rolled health when health is rolled at character creation. The health roll becomes a public operation (like `RollGold`), taking the strength modifier and setting both health and Max Health; the existing "roll" prompt calls it.
- **Player gains a Healing Potion count.** It starts at 0. Not a general inventory.
- **Player gains a drink operation.** It returns an outcome: Healed, No Potion, or Already At Max Health, and the amount actually restored. On Healed it rolls a d8, raises health by that amount capped at Max Health, and reduces the count by 1. On No Potion or Already At Max Health nothing changes. No Potion is checked before Already At Max Health.
- **Merchant gains a buy-Healing-Potion operation.** Price 25. It returns the existing `PurchaseResult`, checks Sold Out before Gold, adds 1 to the Player's potion count, and is Sold Out after one purchase for the rest of the adventure. A refused purchase changes nothing.
- **Merchant menu:** Magical Armor is 1, Magical Weapon is 2, Healing Potion is 3, and Leave moves from 3 to 4.
- **Combat:** the attack/retreat prompt gains the drink option. Healed prints the healed message and goes straight to the Dragon's attack, skipping the Player's attack that round. The two refusals print their message and repeat the prompt with no turn used. Combat keeps the console I/O; Player keeps the rules.
- **`Game.IsValidCombatChoice`** accepts `d` and `drink`, ignoring case and whitespace. Typed commands stay English in every language; only the prompt text is translated.
- **Stats display:** both the Player stats screen and the combat stats screen add a "Healing Potions" line. Existing Health lines are unchanged; Max Health appears only in the healed message.
- **Localization:** every new message key goes into the English, Spanish, and French dictionaries, following the no-accent convention.

## Testing Decisions

- Tests check behavior through public operations and visible state, not console output, following the merchant spec.
- **Merchant:** buying the potion deducts 25, returns Purchased, and makes the count 1. 24 Gold gives Not Enough Gold with nothing changed. Exactly 25 Gold leaves 0. A second purchase returns Sold Out with nothing changed, even when affordable.
- **Player, drinking:** with no potion the result is No Potion and nothing changes. At Max Health the result is Already At Max Health and the potion is kept. A successful drink uses up the potion and raises health. Healing never goes above Max Health (e.g. health 17 of 18, many trials, always ends at 18). From low health over many trials the amount healed is always 1 to 8. The returned amount equals the actual change in health.
- **Player, Max Health:** after the health roll, Max Health equals health.
- **Game:** `IsValidCombatChoice` accepts `d`, `drink`, `D`, ` Drink `.
- **Messages:** every new key resolves (not wrapped in `[...]`) in all three languages.
- **Not directly tested:** the combat and Merchant console loops, as today.

## Out of Scope

- A general inventory, or any item other than the Healing Potion.
- Buying more than one Healing Potion.
- Drinking outside combat, including after retreating. Retreat still ends the game.
- Changing retreat to return to the crossroads.
- Difficulty settings (separate feature, even though this branch name mentions it).
- Any change to how starting health is rolled.

## Further Notes

- Domain terms follow `CONTEXT.md`: Health, Max Health, Healing Potion, Merchant, Gold, Sold Out. Use "drink" for using a potion.
- The drink option first appears after the Dragon's first attack, because the prompt follows each exchange. That's fine: the Player is at Max Health before then.
- **Known existing bug, separate ticket:** a Halfling can roll starting health of 0 or below (d20 of 1 minus a strength modifier of up to 4). Max Health would then be 0 or below, so a potion could never heal. Not caused by this feature.
- Affordability: Gold comes in steps of 10, so the potion needs 30 or more (d20 of 3+), which is 90% of Players. 100 Gold buys one gear item plus the potion.
