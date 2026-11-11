# 03: Merchant encounter with the Magical Weapon

**What to build:** Choosing south at the crossroads now leads to the Merchant. A new narrative replaces the old "nothing happens, you go home poor" text. The Merchant tells the Player their Gold and shows a numbered menu: Magical Weapon (Damage +4) for 75 Gold, and Leave. Buying enchants the Player's own weapon: same kind of weapon, maximum damage +4, shown under its magical name. The menu repeats with updated Gold until the Player picks Leave, then they return to the crossroads and can still go north. The Merchant appears on every trip south, but the Magical Weapon can be bought only once and is then marked Sold Out.

Shop rules live in a new Merchant module that never touches the console. The menu loop in the south branch is a thin console layer over it. See the parent spec: `.scratch/merchant-encounter/spec.md`.

**Blocked by:** 02 (Roll and display Gold).

**Status:** ready-for-agent

- [x] Choosing south shows the new Merchant narrative in all three languages; the old south narrative is gone.
- [x] The Merchant menu shows the Player's current Gold, the Magical Weapon with its benefit and price, and a Leave option.
- [x] The Merchant module's buy-weapon operation returns one of Purchased, Not Enough Gold, or Sold Out, checking Sold Out first.
- [x] Purchased: 75 Gold is deducted; the weapon's maximum damage increases by 4 and it is renamed to its magical form, keeping its art.
- [x] Not Enough Gold and Sold Out each show their own message and change nothing (Gold, weapon, Sold Out state).
- [x] After purchase the menu marks the Magical Weapon "(sold out)".
- [x] Invalid menu input shows the existing invalid message and asks again.
- [x] Leave returns the Player to the crossroads path prompt; going north afterwards fights the Dragon with the Magical Weapon.
- [x] The Merchant is created once per adventure, so Sold Out lasts across repeat trips south.
- [x] Magical weapon display names exist for all four weapons in English, Spanish, and French (e.g. magical long sword / espada larga magica / epee longue magique).
- [x] Tests: buying with enough Gold deducts 75 and returns Purchased; with 74 Gold returns Not Enough Gold and nothing changes; with exactly 75 succeeds and leaves 0; a second purchase returns Sold Out and nothing changes, even when the Player could afford it.
- [x] Tests: for each occupation (Fighter 12→16, Magician 12→16, Thief 6→10, Archer 8→12), the weapon keeps its kind and gets its magical name.
- [x] Tests: every new message key and magical weapon name resolves in all three languages.
