# 01: Create the Dragon once per adventure

**What to build:** A prefactor. Today the Dragon is created once, when the game starts, before the main menu. Move its creation into the adventure so it's created right after the user chooses to create a character, next to the Player and the Merchant. All three then last for the same adventure, which gives the next ticket a place to build the Dragon with the chosen Difficulty. Play is unchanged: the Dragon has the same name, rolled stats, and claws, and the north path works exactly as before.

See the parent spec: `.scratch/difficulty/spec.md`.

**Blocked by:** None (can start immediately).

**Status:** ready-for-agent

- [ ] The Dragon is created inside the adventure, after the user chooses to create a character, not at game start.
- [ ] The Dragon is created once per adventure, so the same Dragon is met however many times the Player visits the Merchant before going north.
- [ ] The Dragon's encounter, stats screen, and combat behave exactly as before.
- [ ] All existing tests still pass.
