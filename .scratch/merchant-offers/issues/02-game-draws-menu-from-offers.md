# 02: Game draws the merchant menu from the Offers

**What to build:** A refactor with no visible change to play. Game's merchant screen loops over the Merchant's Offers instead of hand-writing one line and one branch per item. It numbers each line itself, puts Leave at offers + 1, and maps each item to its text with one switch. The menu numbers come out of the translated strings in all three languages. One console-scripted test covers the screen.

See the parent spec: `.scratch/merchant-offers/spec.md`.

**Blocked by:** 01

**Status:** done

- [x] `HandleMerchantEncounter` loops over `Offers`: it prints `"{n}. "` + the item's formatted menu line (+ the Sold Out tag when Sold Out), then `"{offers + 1}. "` + the Leave text.
- [x] Input `1`..`offers` buys that Offer's item; `offers + 1` leaves; anything else prints the usual invalid message.
- [x] One switch in Game maps each `MerchantItem` to its menu-line key and purchased message (armor: Protection; weapon: translated weapon name; potion: no arguments).
- [x] The leading `"1. "`–`"4. "` are removed from `merchant_menu_armor`, `merchant_menu_weapon`, `merchant_menu_potion`, `merchant_menu_leave` in English, Spanish, and French; the rest of the wording is unchanged.
- [x] The menu reads exactly as before in all three languages.
- [x] `HandleMerchantEncounter` is `internal`; the game project declares `InternalsVisibleTo` for `CYOATests`.
- [x] Test: with a Player holding known Gold, scripted input buys Magical Armor then leaves; output shows lines numbered 1–4 with Leave last, and the armor line carries the Sold Out tag after purchase. Console in/out are restored afterwards.
- [x] Any Messages test that asserted the leading menu number is updated; all other existing tests still pass.
