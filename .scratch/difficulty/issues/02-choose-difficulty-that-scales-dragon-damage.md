# 02: Choose a Difficulty that scales Dragon damage

**What to build:** After the user chooses to create a character, and before they pick a race, the game asks for a Difficulty: Easy (1), Normal (2), or Hard (3). Each option has a one-line description (the Dragon hits softer, normally, or harder). The user types `1`, `2`, or `3`; surrounding spaces are ignored, and the numbers are the same in every language. Anything else, including blank input, shows the usual invalid message and asks again. There is no default. The game confirms the chosen Difficulty and goes on to character creation. The Dragon for that adventure is built with that Difficulty, which can't change afterward.

Difficulty changes only the Dragon's damage roll. The d12 claw roll is scaled: Easy is half, rounded up (1–6); Normal is unchanged (1–12); Hard is 1.5 times, rounded up (2–18). Use integer arithmetic: Easy is `(roll + 1) / 2` and Hard is `(roll * 3 + 1) / 2`. Protection is subtracted after scaling, through the existing take-damage. The hit roll, defense roll, armor messages, Health floor, and defeat check are unchanged, and the damage message shows the scaled number. Normal plays exactly as the game does today.

See the parent spec: `.scratch/difficulty/spec.md`.

**Blocked by:** 01 (Create the Dragon once per adventure).

**Status:** done

- [x] A Difficulty type with values Easy, Normal, and Hard.
- [x] The Dragon is built with a Difficulty (Normal if none is given) and exposes it read-only.
- [x] The Dragon's public damage roll applies the scaling above; nothing else about the Dragon's attack changes.
- [x] Game asks for the Difficulty after "Create Character" and before race selection, repeats on invalid input with the usual invalid message, and confirms the choice.
- [x] Game's Difficulty-choice validator accepts `1`, `2`, and `3` (ignoring surrounding whitespace) and rejects everything else, including null and blank.
- [x] The prompt, the three Difficulty names, and the confirmation exist in English, Spanish, and French, with no accents.
- [x] Tests: a Dragon built without a Difficulty is Normal.
- [x] Tests: over many trials, damage is always 1–6 on Easy, 1–12 on Normal, and 2–18 on Hard; Easy reaches 6, and Hard reaches 2 and 18.
- [x] Tests: over many trials on Hard, damage is never 4, 7, 10, 13, or 16.
- [x] Tests: the Dragon's attack on a Player with Protection never raises Health or drops it below zero, at every Difficulty.
- [x] Tests: the validator accepts `1`, `2`, `3`, and ` 2 `, and rejects `0`, `4`, `easy`, empty, whitespace, and null.
- [x] Tests: every new message key resolves (not `[key]`) in all three languages.

## Comments

Done in ee12211 (Choose a Difficulty that scales Dragon damage).
