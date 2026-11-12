# 01: The Merchant sells a Healing Potion

**What to build:** The Merchant menu gains a Healing Potion for 25 Gold as option 3, with Leave moving to option 4. Buying it adds 1 to the Player's Healing Potion count and marks it Sold Out for the rest of the adventure. The count appears on the Player stats screen and the combat stats screen. Drinking comes in ticket 02.

See the parent spec: `.scratch/healing-potion/spec.md`.

**Blocked by:** none.

**Status:** ready-for-agent

- [ ] The Player has a Healing Potion count, starting at 0.
- [ ] The Merchant menu lists Magical Armor (1), Magical Weapon (2), Healing Potion with its benefit and price (3), and Leave (4).
- [ ] The Merchant's buy-Healing-Potion operation returns Purchased, Not Enough Gold, or Sold Out, checking Sold Out before Gold, with the existing refusal messages. A refused purchase changes nothing.
- [ ] Purchased: 25 Gold is deducted, the count becomes 1, and the menu marks the potion "(sold out)".
- [ ] The Player stats screen and the combat stats screen both show a "Healing Potions" line.
- [ ] All new text exists in English, Spanish, and French (no accents).
- [ ] Tests: buying with enough Gold deducts 25, returns Purchased, and sets the count to 1; 24 Gold gives Not Enough Gold with nothing changed; exactly 25 leaves 0; a second purchase is Sold Out with nothing changed, even when affordable.
- [ ] Tests: every new message key resolves in all three languages.
