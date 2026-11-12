# 02: Drink a Healing Potion in combat

**What to build:** The combat prompt after each exchange gains a drink option (`d`/`drink`). Drinking restores a d8 of health, capped at the Player's Max Health (the health rolled at character creation), uses up the potion, and takes the place of the Player's attack: the Dragon still attacks that round. With no potion, or at Max Health, drinking is refused with a message, nothing is used, and the prompt repeats.

See the parent spec: `.scratch/healing-potion/spec.md`.

**Blocked by:** 01 (The Merchant sells a Healing Potion). Tests can set the potion count directly.

**Status:** ready-for-agent

- [ ] The health roll becomes a public Player operation taking the strength modifier; it sets health and Max Health to the same value. Character creation's "roll" prompt calls it.
- [ ] The Player's drink operation returns Healed, No Potion, or Already At Max Health, plus the amount actually restored. No Potion is checked first. Refusals change nothing.
- [ ] Healed: rolls a d8, raises health by that amount but never above Max Health, and reduces the potion count by 1.
- [ ] The combat prompt offers attack, retreat, and drink in all three languages; `d`/`drink` are accepted in any case.
- [ ] Healed prints the amount restored and health against Max Health (e.g. "18/18"), then the Dragon attacks; the Player does not attack that round.
- [ ] No Potion and Already At Max Health print their own message and repeat the prompt without a Dragon attack.
- [ ] `Game.IsValidCombatChoice` accepts `d` and `drink`.
- [ ] All new text exists in English, Spanish, and French (no accents).
- [ ] Tests: after the health roll, Max Health equals health.
- [ ] Tests: No Potion with count 0 changes nothing; Already At Max Health keeps the potion; a successful drink uses up the potion and raises health; healing never exceeds Max Health (health 17 of 18, many trials); from low health the amount healed is always 1 to 8 (many trials); the returned amount equals the actual change in health.
- [ ] Tests: `IsValidCombatChoice` accepts `d`, `drink`, `D`, ` Drink `.
- [ ] Tests: every new message key resolves in all three languages.
