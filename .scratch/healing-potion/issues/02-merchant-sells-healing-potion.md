# 02: The Merchant sells a Healing Potion

**What to build:** On the south path, the Merchant's menu gains a Healing Potion for 25 Gold, listed with its benefit (restores up to 8 Health). The menu becomes Magical Armor (1), Magical Weapon (2), Healing Potion (3), Leave (4). Buying it deducts 25 Gold, gives the Player one Healing Potion, and marks it Sold Out for the rest of the adventure. The Player's Healing Potion count appears on the Player stats screen and the combat stats screen. Drinking comes in ticket 03; after this ticket the potion can be bought and seen but not yet used.

See the parent spec: `.scratch/healing-potion/spec.md`.

**Blocked by:** None (can start immediately).

**Status:** ready-for-agent

- [ ] The Player has a Healing Potion count, starting at 0 for every new character.
- [ ] The Merchant menu lists Magical Armor (1), Magical Weapon (2), Healing Potion with its benefit and price (3), and Leave (4); invalid input still shows the usual invalid message.
- [ ] The Merchant's buy-Healing-Potion operation returns the existing purchase outcomes (Purchased, Not Enough Gold, Sold Out), checking Sold Out before Gold. A refused purchase changes nothing. The Merchant still never touches the console.
- [ ] Purchased: 25 Gold is deducted, the count becomes 1, a purchase confirmation is shown, and the menu marks the Healing Potion "(sold out)". Refusals reuse the existing Not Enough Gold and Sold Out messages.
- [ ] The Merchant reports whether the Healing Potion is Sold Out, and that state lasts across repeat trips south.
- [ ] The Player stats screen and the combat stats screen each show a Healing Potions line (0 when none); existing lines are unchanged.
- [ ] All new text (menu line, purchase confirmation, stat label) exists in English, Spanish, and French, with no accents.
- [ ] Tests: with enough Gold, buying deducts 25, returns Purchased, and sets the count to 1.
- [ ] Tests: with 24 Gold the result is Not Enough Gold and Gold, count, and Sold Out are unchanged; with exactly 25 Gold the purchase succeeds and leaves 0.
- [ ] Tests: a second purchase returns Sold Out with nothing changed, even when affordable.
- [ ] Tests: buying the Healing Potion doesn't change the Sold Out state of the Magical Armor or Magical Weapon.
- [ ] Tests: every new message key resolves (not wrapped in `[...]`) in all three languages.
