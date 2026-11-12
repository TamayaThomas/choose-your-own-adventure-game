# 03: Show the Difficulty on the stats screens

**What to build:** The Player can see the adventure's Difficulty when sizing up the Dragon. The Dragon's stats screen (shown when the Dragon appears) and the Dragon section of the combat stats screen each gain a Difficulty line showing the translated Difficulty name (Easy, Normal, or Hard). Existing lines are unchanged, including the Dragon's Damage line, which keeps showing the claws' maximum of 12.

See the parent spec: `.scratch/difficulty/spec.md`.

**Blocked by:** 02 (Choose a Difficulty that scales Dragon damage).

**Status:** done

- [x] The Dragon's stats screen shows a Difficulty line with the translated Difficulty name.
- [x] The combat stats screen's Dragon section shows the same line.
- [x] All existing stat lines are unchanged, including the Dragon's Damage line.
- [x] The Difficulty stat label exists in English, Spanish, and French, with no accents.
- [x] Tests: the combat stats display includes the Difficulty label and the chosen Difficulty's translated name.
- [x] Tests: the stat label resolves (not `[key]`) in all three languages.

## Comments

Done in 297082f (Show the Difficulty on the stats screens).
