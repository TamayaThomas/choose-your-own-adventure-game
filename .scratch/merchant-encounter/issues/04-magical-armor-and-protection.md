# 04: Magical Armor and Protection

**What to build:** The Merchant menu gains Magical Armor (Protection +3) for 75 Gold, listed alongside the Magical Weapon and Leave. Buying it sets the Player's Protection to 3, so every Dragon hit that gets past the Player's defense roll does 3 less damage, never below 0. When the armor absorbs the whole hit, the Player sees an "armor absorbs the blow" message; when it absorbs part, they see both the Dragon's damage and the amount absorbed. Protection appears on the Player stats screen and the combat stats screen. A Player with 150 Gold or more can buy both items.

See the parent spec: `.scratch/merchant-encounter/spec.md`.

**Blocked by:** 01 (Route Dragon damage through Player.TakeDamage), 03 (Merchant encounter with the Magical Weapon).

**Status:** ready-for-agent

- [x] The Merchant menu lists Magical Armor with its benefit and price, as option 1, with the Magical Weapon as option 2 and Leave as option 3.
- [x] The Merchant module's buy-armor operation returns Purchased, Not Enough Gold, or Sold Out, with the same rules and messages as the weapon.
- [x] Purchased: 75 Gold is deducted and Protection becomes 3; the menu then marks Magical Armor "(sold out)".
- [x] Dragon hits apply Protection through the take-damage operation. Full absorption shows the "armor absorbs the blow" message; partial absorption shows the raw damage and the amount absorbed; with no armor the existing damage message is unchanged.
- [x] The Player stats screen and the combat stats screen both show Protection (0 without armor).
- [x] All new text exists in English, Spanish, and French.
- [x] Tests: buying armor with enough Gold deducts 75 and sets Protection to 3; refusals change nothing; buying both items works with 150 Gold or more.
- [x] Tests: with Protection 3, take-damage reduces damage by 3; damage of 3 or less becomes 0; health never goes below 0; the return value equals the damage actually taken. *(Already covered by ticket 01 in `CYOATests/PlayerTests.cs`. `TakeDamage` returns damage after Protection, not capped by remaining health; use raw minus returned to get the amount absorbed.)*
- [x] Tests: every new message key resolves in all three languages.
