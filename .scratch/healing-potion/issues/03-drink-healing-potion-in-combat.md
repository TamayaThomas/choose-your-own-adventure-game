# 03: Drink a Healing Potion in combat

**What to build:** In combat with the Dragon, the prompt after each exchange offers attack, retreat, and drink. Typing `d` or `drink` (any case, extra spaces allowed, the same in every language) drinks the Healing Potion: it restores a d8 roll of Health, never above Max Health, uses up the potion, and tells the Player how much Health they regained and their Health against Max Health (e.g. 18/18). Drinking takes the place of the Player's attack: the Dragon still attacks that round, with Protection and the defense roll working as usual, and defeat ends the game as usual. If the Player has no Healing Potion, they're told so; if they're already at Max Health, they're told they're at full Health. Either refusal keeps the potion and the turn (no Dragon attack) and shows the prompt again. No Potion is reported first when both apply.

See the parent spec: `.scratch/healing-potion/spec.md`.

**Blocked by:** 01 (Max Health set by a public Health roll), 02 (The Merchant sells a Healing Potion).

**Status:** ready-for-agent

- [ ] The Player's drink operation returns an outcome (Healed, No Potion, or Already At Max Health) and the amount of Health actually restored. No Potion is checked before Already At Max Health. Refusals change nothing and restore 0.
- [ ] Healed: rolls a d8 with the Player's own die, raises Health by that amount but never above Max Health, and reduces the potion count by 1.
- [ ] The combat prompt offers attack, retreat, and drink in English, Spanish, and French.
- [ ] Combat-input validation accepts `d` and `drink`, ignoring case and surrounding whitespace, and still accepts attack and retreat and rejects anything else.
- [ ] On Healed, Combat prints the amount restored and Health against Max Health, then runs the Dragon's attack and the defeat check, skipping the Player's attack that round.
- [ ] On No Potion or Already At Max Health, Combat prints that refusal's message and shows the prompt again without a Dragon attack.
- [ ] After drinking, both stats screens show the reduced potion count.
- [ ] All new text (prompt, healed message, no-potion message, already-at-Max-Health message) exists in English, Spanish, and French, with no accents.
- [ ] Tests: with a count of 0, drinking returns No Potion and changes nothing, even when Health is below Max Health.
- [ ] Tests: at Max Health with a potion, drinking returns Already At Max Health, restores 0, and keeps the potion.
- [ ] Tests: a successful drink returns Healed, reduces the count by 1, and raises Health.
- [ ] Tests: starting at 17 of 18 Health over many trials, Health always ends at 18 and the amount restored is always 1.
- [ ] Tests: starting at low Health with a high Max Health over many trials, the amount restored is always 1 to 8.
- [ ] Tests: the returned amount always equals the actual change in Health.
- [ ] Tests: combat-input validation accepts `d`, `drink`, `D`, and ` Drink `.
- [ ] Tests: every new message key resolves (not wrapped in `[...]`) in all three languages.
- [ ] Verified by playing: buy a potion, go north, take a hit, drink; the Player doesn't attack that round and the Dragon does. A refused drink repeats the prompt with no Dragon attack.
