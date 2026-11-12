# Spec: Difficulty

Status: ready-for-agent

## Problem Statement

Every adventure plays at the same challenge level. A new or casual Player who loses to the Dragon repeatedly has no way to make the fight more forgiving, and an experienced Player who has bought the Magical Armor, the Magical Weapon, and a Healing Potion finds the fight too easy. Beyond the dice, nothing lets the Player tune how dangerous the Dragon is.

## Solution

At the start of each adventure, after choosing to create a character and before the Player is created, the user picks a Difficulty: Easy, Normal, or Hard. Difficulty changes only how much damage the Dragon deals to the Player: Easy halves each Dragon damage roll, Normal leaves it unchanged (the standard game), and Hard multiplies it by 1.5. Both round up. Protection is subtracted after the Difficulty is applied, exactly as today. The chosen Difficulty stays fixed for the rest of the adventure and appears on the Dragon's stats and the combat stats screen.

## User Stories

### Choosing a Difficulty

1. As a Player, I want to choose a Difficulty at the start of each adventure, so that I can set how dangerous the Dragon will be.
2. As a Player, I want to be asked for the Difficulty right after I choose to create a character and before I pick my race, so that the challenge is settled before I invest in a character.
3. As a Player, I want the Difficulty prompt to list Easy (1), Normal (2), and Hard (3), so that the choices are clear and stable.
4. As a Player, I want each option to say briefly what it does (the Dragon hits softer, normally, or harder), so that I can make an informed choice.
5. As a Player, I want to choose by typing `1`, `2`, or `3`, like the main menu and the Merchant menu, so that the controls feel consistent.
6. As a Player, I want extra spaces around my choice to be ignored, so that typing is forgiving.
7. As a Player playing in Spanish or French, I want to type the same `1`/`2`/`3`, so that numbered choices work the same in every language.
8. As a Player who types anything else, including nothing, I want to see the usual invalid message and be asked again, so that I never end up on a Difficulty I didn't pick.
9. As a Player, I want a confirmation of the Difficulty I chose, so that I know my choice was accepted.
10. As a Player who picks Normal, I want the game to play exactly as it did before this feature, so that the standard game is unchanged.

### Difficulty during the adventure

11. As a Player, I want the Difficulty to stay fixed for the whole adventure, so that the challenge can't change halfway through.
12. As a Player, I want to have no way to change Difficulty at the crossroads, at the Merchant, or in combat, so that the choice means something.
13. As a Player, I want my Difficulty to be chosen fresh for each new adventure, so that I can try a different challenge next time.
14. As a Player, I want the Merchant's prices, items, and Gold to be the same at every Difficulty, so that Difficulty only affects the fight.
15. As a Player, I want my rolled stats, Health, Max Health, and Gold to be the same at every Difficulty, so that character creation isn't affected.
16. As a Player, I want the Dragon's Strength, Agility, and Health to be the same at every Difficulty, so that only its damage changes.
17. As a Player, I want my own attacks against the Dragon to work the same at every Difficulty, so that Difficulty only changes how hard I'm hit.
18. As a Player, I want the Healing Potion to restore the same amount at every Difficulty, so that healing stays predictable.

### Dragon damage

19. As a Player on Easy, I want each Dragon hit to deal half its rolled damage, rounded up, so that the fight is more forgiving.
20. As a Player on Easy, I want a Dragon hit always to deal at least 1 damage before Protection, so that Easy still has some danger.
21. As a Player on Easy, I want Dragon damage to range from 1 to 6, so that I know what to expect.
22. As a Player on Normal, I want Dragon damage to range from 1 to 12, as it does today, so that the standard game is unchanged.
23. As a Player on Hard, I want each Dragon hit to deal one and a half times its rolled damage, rounded up, so that the fight is tougher.
24. As a Player on Hard, I want Dragon damage to range from 2 to 18, so that I know what to expect.
25. As a Player, I want the Dragon's chance to hit and my chance to defend to be the same at every Difficulty, so that only the size of a hit changes.
26. As a Player with Magical Armor, I want my Protection subtracted after the Difficulty is applied, so that armor removes the same flat amount at every Difficulty.
27. As a Player with Magical Armor on Easy, I want small hits fully absorbed by my armor to show the usual armor message, so that the existing feedback still works.
28. As a Player, I want the damage message to show the damage after Difficulty is applied, so that the number I see matches what happened.
29. As a Player, I want my Health never to drop below zero at any Difficulty, so that the existing floor still holds.
30. As a Player defeated on any Difficulty, I want the game to end in defeat as usual, so that the ending is the same.

### Seeing the Difficulty

31. As a Player, I want the Dragon's stats screen to show the Difficulty, so that I remember how hard it hits before I attack.
32. As a Player in combat, I want the combat stats screen to show the Difficulty, so that I can plan around it.
33. As a Player, I want the Dragon's Damage stat to keep showing its weapon's maximum damage, so that existing stat lines are unchanged.

### Languages

34. As a Spanish-speaking Player, I want the Difficulty prompt, option names, confirmation, and stat label in Spanish, so that the game stays playable in my language.
35. As a French-speaking Player, I want the same text in French, so that the game stays playable in my language.
36. As a Player in any language, I want the new text to follow the existing no-accent convention, so that it displays the same as the rest of the game.

## Implementation Decisions

- **New Difficulty type.** A plain enumeration with three values: Easy, Normal, Hard. There is no settings object and no "mode".
- **The Dragon holds the Difficulty.** The Dragon is constructed with a Difficulty, which defaults to Normal so existing callers and tests are unaffected. It is exposed read-only and never changes after construction.
- **The Dragon's damage roll applies the Difficulty.** The existing public damage roll rolls the weapon's die as today, then scales it: Easy is the roll divided by 2, rounded up; Normal is unchanged; Hard is the roll times 1.5, rounded up. The resulting ranges with the d12 claws are Easy 1–6, Normal 1–12, Hard 2–18. The scaling is integer arithmetic, not floating point (Easy is `(roll + 1) / 2`, Hard is `(roll * 3 + 1) / 2`).
- **The Dragon's attack is otherwise unchanged.** The hit roll, the Player's defense roll, take-damage (which subtracts Protection and floors Health at zero), the absorbed and partial-absorption messages, and the defeat check all work on the scaled roll exactly as they do on the raw roll today. The damage message shows the scaled roll.
- **The Dragon is created once per adventure, after the Difficulty is chosen.** Today it is created once per run of the game, before the main menu. It moves into the adventure, after the Difficulty choice, alongside the Player and the Merchant, so all three share the adventure's lifetime.
- **Game owns the Difficulty prompt.** When the user chooses to create a character, Game shows the Difficulty prompt (Easy 1, Normal 2, Hard 3, each with a one-line description), reads input, and repeats with the usual invalid message until it gets a valid choice. It then prints a confirmation naming the chosen Difficulty and continues to character creation. There is no default on empty input.
- **Game gains a Difficulty-choice validator,** alongside the existing menu, path, and combat-choice validators. It accepts `1`, `2`, and `3`, ignoring surrounding whitespace, and rejects everything else, including null and blank input. Typed choices are the same in every language; only the prompt text is translated.
- **Stats display.** The Dragon's stats screen and the combat stats screen's Dragon section each add a Difficulty line showing the translated Difficulty name. Existing lines, including the Dragon's Damage line, are unchanged.
- **Localization.** New message keys are added to the English, Spanish, and French dictionaries, following the no-accent convention: the Difficulty prompt, the three Difficulty names, the confirmation, and the Difficulty stat label.
- **Glossary.** Use the CONTEXT.md term Difficulty. Don't call it mode, game mode, or level.

## Testing Decisions

- **What makes a good test:** it calls public operations and asserts on outcomes and visible state (damage rolled, Health, validator results, resolved messages). It doesn't assert on private fields or console output. Dice-driven results are checked by sampling many trials and asserting on the range, as the Gold roll, Health roll, and Healing Potion tests already do.
- **Seams:** three existing modules, with no new seam in Combat or Player.
- **Dragon (main seam):**
  - A Dragon built without a Difficulty has Difficulty Normal.
  - Over many trials, the damage roll is always 1 to 6 on Easy, 1 to 12 on Normal, and 2 to 18 on Hard.
  - Over many trials, Easy reaches its maximum of 6 and Hard reaches both its minimum of 2 and its maximum of 18, so the rounding is right at the edges.
  - Over many trials on Hard, the damage roll never takes a value that 1.5 times a d12 roll, rounded up, can't produce (for example 4, 7, 10, 13, 16).
  - Through the Dragon's attack on a Player with Protection, Health never drops below zero and never rises, at every Difficulty.
- **Game:** the Difficulty-choice validator accepts `1`, `2`, `3`, and ` 2 `, and rejects `0`, `4`, `easy`, an empty string, whitespace, and null.
- **Messages:** every new key resolves in English, Spanish, and French. A missing key shows up as `[key]`, so tests assert the result is not wrapped in brackets.
- **Prior art:** the Dragon tests (construction, attack keeps Health non-negative and never raises it), the Player Gold and Health roll tests (ranges by sampling), the menu, path, and combat-choice validator tests in the Game tests, and the multi-language key checks in the Messages tests.
- **Not directly tested:** the Difficulty prompt loop and the move of Dragon creation into the adventure, which own console input and output, as today. These are verified by playing the game.

## Out of Scope

- Difficulty affecting anything other than Dragon damage: Dragon stats, Dragon hit or defense chances, the Player's attacks, starting stats, Gold, Merchant prices, or Healing Potion strength.
- Changing the Difficulty after it is chosen, or remembering it between adventures.
- More than three Difficulty levels, or custom multipliers.
- Typing Difficulty names (such as `easy` or `facil`) instead of numbers.
- A default Difficulty on empty input.
- Showing the scaled damage range in the Dragon's Damage stat.
- Fixing the known low-Health bug noted in the Healing Potion spec (a Halfling can roll starting Health of 0 or below).

## Further Notes

- Domain terms follow the glossary: Difficulty, Dragon, Player, Health, Protection, Healing Potion, Merchant.
- **Why scale before Protection:** the glossary defines Protection as an amount subtracted from each Dragon hit that gets through. Scaling the hit first keeps that definition true at every Difficulty. As a consequence, Magical Armor is relatively stronger on Easy (it fully absorbs more hits) and relatively weaker on Hard.
- **Feel of the numbers:** with the d12 claws, the average hit before Protection is 3.5 on Easy, 6.5 on Normal, and 10.0 on Hard. Starting Health is a d20 plus the strength modifier, so a Hard hit of 18 can defeat most fresh Players in one blow. That is intended for Hard.
- **Implementation tickets:** 01 is a prefactor that creates the Dragon once per adventure. 02 adds choosing a Difficulty and the scaled Dragon damage, and depends on 01. 03 shows the Difficulty on the stats screens, and depends on 02.
