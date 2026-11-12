# Spec: Healing Potion

Status: ready-for-agent

## Problem Statement

Once the fight with the Dragon starts, the Player's only choices each round are to attack or to retreat. Health only ever goes down, so a Player who is losing has no way to recover and no real decision to make beyond "keep swinging or run away", and retreating ends the game. The Merchant on the south path sells only gear that helps before a hit lands (Protection) or makes attacks stronger (the Magical Weapon); nothing helps a Player who is already hurt. Gold left over after the gear also has no use.

## Solution

The Merchant sells a Healing Potion for 25 Gold, at most one per adventure. During combat with the Dragon, the prompt after each exchange gains a third option: drink the Healing Potion. Drinking restores a d8 roll of Health, never above the Player's Max Health (the Health they rolled at character creation), uses up the potion, and takes the place of the Player's next attack: the Dragon still attacks that round. If the Player has no Healing Potion, or is already at Max Health, drinking is refused with a message; the potion and the turn are both kept and the prompt is shown again.

## User Stories

### Buying from the Merchant

1. As a Player, I want the Merchant to sell a Healing Potion, so that I can prepare for a long fight with the Dragon.
2. As a Player, I want the Merchant's menu to list the Healing Potion with its benefit (restores up to 8 Health) and its price, so that I can decide whether it's worth buying.
3. As a Player, I want the Healing Potion to cost 25 Gold, so that I can afford it alongside one piece of gear.
4. As a Player with 100 Gold, I want to be able to buy one piece of gear and the Healing Potion, so that I can mix offense or defense with recovery.
5. As a Player with only enough Gold for the potion, I want buying it to be a real option, so that a poor starting Gold roll still lets me buy something.
6. As a Player with exactly 25 Gold, I want to be able to buy the potion and be left with 0 Gold, so that having exactly the price is enough.
7. As a Player with less than 25 Gold, I want to be told I don't have enough Gold, so that I understand why nothing happened.
8. As a Player who can't afford the potion, I want my Gold and potion count left unchanged, so that a failed purchase has no side effects.
9. As a Player, I want the price deducted from my Gold when I buy the potion, so that the purchase has a real cost.
10. As a Player, I want my potion count to go from 0 to 1 when I buy, so that I know I now own a Healing Potion.
11. As a Player, I want the Healing Potion marked "(sold out)" in the menu after I buy it, consistent with the Magical Armor and Magical Weapon.
12. As a Player who picks the Healing Potion again after buying it, I want to be told it's Sold Out with no Gold taken, so that I can't stack potions.
13. As a Player who picks a Sold Out potion while able to afford it, I want it still to be refused as Sold Out, so that the rule doesn't depend on my Gold.
14. As a Player, I want the menu to show my updated Gold after buying the potion, so that I can keep shopping or leave.
15. As a Player, I want the menu to number the items Magical Armor 1, Magical Weapon 2, Healing Potion 3, and Leave 4, so that the choices are clear and stable.
16. As a Player, I want invalid input in the Merchant menu still to show the usual invalid message and ask again, so that the new item doesn't break the existing controls.
17. As a Player, I want the potion to stay Sold Out on later trips south during the same adventure, so that the Merchant doesn't restock.
18. As a Player who creates a new character, I want my potion count to start at 0 and the Merchant to have the potion in stock again, so that each adventure starts fresh.

### Seeing what I have

19. As a Player, I want my stats screen to show how many Healing Potions I have, so that I know what I'm taking north.
20. As a Player in combat, I want the combat stats screen to show my Healing Potion count, so that I know whether drinking is an option.
21. As a Player without a potion, I want my stats to show 0 Healing Potions, so that the line is always present and consistent.
22. As a Player, I want my existing Health line in both stats screens to stay as it is, so that nothing I already know changes.

### Max Health

23. As a Player, I want my Max Health set to the Health I rolled at character creation, so that healing has a clear ceiling.
24. As a Player, I want my Max Health never to change during the adventure, so that the ceiling is predictable.
25. As a Player, I want Dragon hits to lower my Health but not my Max Health, so that a potion can bring me back toward where I started.

### Drinking in combat

26. As a Player in combat, I want a "drink potion" option at the prompt after each exchange, alongside attack and retreat, so that I can choose to heal.
27. As a Player, I want to type `d` or `drink` to drink, just as I type `a`/`attack` or `r`/`retreat`, so that the controls feel consistent.
28. As a Player, I want the drink commands to work in upper or lower case and with extra spaces, so that typing is forgiving, like the other commands.
29. As a Player playing in Spanish or French, I want to type the same `d`/`drink` commands, so that typed commands stay the same as attack and retreat in every language.
30. As a Player, I want drinking to restore between 1 and 8 Health, so that healing is part of the game's dice luck.
31. As a Player, I want healing never to raise my Health above my Max Health, so that a potion can't become a free stat boost.
32. As a Player who is only a little hurt, I want to regain only the Health I'm missing even if the roll is higher, so that the cap is applied fairly.
33. As a Player, I want to be told how much Health I actually regained, so that I can see what the potion did.
34. As a Player, I want to see my Health against my Max Health after drinking (for example 18/18), so that I understand the cap.
35. As a Player, I want the potion used up after a successful drink, so that it's single-use.
36. As a Player, I want my potion count to go back to 0 after drinking, so that the stats stay accurate.
37. As a Player, I want drinking to use up my turn, so that I don't also attack that round.
38. As a Player who drinks, I want the Dragon still to attack me that round, so that healing is a real trade against dealing damage.
39. As a Player who drinks and then takes a hit, I want my Protection and defense roll to work as usual, so that drinking doesn't change how Dragon hits are handled.
40. As a Player who drinks and is then defeated by the Dragon's attack, I want the game to end in defeat as usual, so that the potion is not a guarantee of survival.
41. As a Player who survives after drinking, I want the prompt to appear again, so that I can attack, retreat, or try to drink again.

### Refused drinks

42. As a Player with no Healing Potion, I want to be told I have none when I choose to drink, so that I understand why nothing happened.
43. As a Player with no Healing Potion, I want a refused drink not to use my turn, so that the Dragon doesn't get a free attack.
44. As a Player already at Max Health, I want to be told I'm at full Health when I choose to drink, so that I don't waste the potion.
45. As a Player at Max Health, I want to keep my potion and my turn after a refused drink, so that the mistake costs nothing.
46. As a Player with neither a potion nor any missing Health, I want to be told I have no potion, so that the more basic reason is reported.
47. As a Player whose drink was refused, I want the prompt shown again, so that I can pick attack or retreat instead.

### Languages

48. As a Spanish-speaking Player, I want the Merchant's potion line, the drink option, all drink messages, and the potion stat label in Spanish, so that the game stays playable in my language.
49. As a French-speaking Player, I want the same text in French, so that the game stays playable in my language.
50. As a Player in any language, I want the new text to follow the existing no-accent convention, so that it displays the same as the rest of the game.

## Implementation Decisions

- **Player gains Max Health.** It is set to the same value as Health whenever Health is rolled, and not changed afterward.
- **The Health roll becomes a public Player operation**, like the existing Gold roll. It takes the strength modifier and sets both Health and Max Health. Character creation's "roll" prompt calls it. This is the only new public surface added for testing.
- **Player gains a Healing Potion count.** It is a plain number, starting at 0, like Gold and Protection. There is no general inventory.
- **Player gains a drink operation.** It returns an outcome and the amount of Health actually restored. The outcomes are Healed, No Potion, and Already At Max Health.
  - No Potion is checked first, then Already At Max Health. Either refusal changes nothing and restores 0.
  - Healed rolls a d8 with the Player's own die, raises Health by that amount but never above Max Health, reduces the count by 1, and returns the amount actually restored. This follows the same pattern as take-damage, which returns the damage actually taken.
- **Merchant gains a buy-Healing-Potion operation.** It returns the existing purchase outcome (Purchased, Not Enough Gold, Sold Out). The price is 25 Gold. It checks Sold Out before Gold. Purchased deducts 25 Gold, adds 1 to the potion count, and marks the potion Sold Out for the rest of the adventure. A refused purchase changes nothing. The Merchant also reports whether the potion is Sold Out, for the menu display. The Merchant still never touches the console.
- **Merchant menu.** It lists Magical Armor (1), Magical Weapon (2), Healing Potion (3), and Leave (4). Leave moves from 3 to 4. The Healing Potion line shows its benefit and price, and the Sold Out tag when bought. It reuses the existing refusal messages.
- **Combat.** The attack/retreat prompt gains a drink option. Combat stays a thin console layer, and the Player holds the rules.
  - On Healed, Combat prints the healed message with the amount restored and Health against Max Health. It then runs the Dragon's attack and checks for defeat, skipping the Player's attack that round. The round order stays Player action, then Dragon attack, then prompt, with drinking replacing the attack as the Player's action.
  - On either refusal, Combat prints that refusal's message and shows the prompt again, without a Dragon attack.
- **Combat-input validation** in Game accepts `d` and `drink`, ignoring case and surrounding whitespace, alongside the existing attack and retreat commands. Typed commands stay the same in every language; only the prompt text is translated.
- **Stats display.** The Player stats screen and the combat stats screen each add a Healing Potions line. Existing lines are unchanged. Max Health appears only in the healed message.
- **Localization.** Every new message key is added to the English, Spanish, and French dictionaries, following the no-accent convention. The new keys are the shop line, the purchase confirmation, the combat prompt text with the drink option, the healed message, the no-potion message, the already-at-Max-Health message, and the potion stat label.
- **Lifetime.** The potion count lives on the Player and the Sold Out state on the Merchant. Both are created once per adventure, so both reset when a new character is created.

## Testing Decisions

- **What makes a good test:** it calls public operations and asserts on outcomes and visible state (Gold, Health, Max Health, potion count, Sold Out, resolved messages). It doesn't assert on private fields or console output. Dice-driven results are checked by sampling many trials and asserting on the range, as the Gold roll tests already do.
- **Seams:** four existing modules: Merchant, Player, Game's combat-input validation, and Messages. Only one surface is new: the Health roll becomes public. No new seam is added in Combat.
- **Merchant (main seam for buying):**
  - Buying with enough Gold deducts 25, returns Purchased, and sets the potion count to 1.
  - 24 Gold returns Not Enough Gold, and Gold, the count, and Sold Out are unchanged.
  - Exactly 25 Gold succeeds and leaves 0.
  - A second purchase returns Sold Out with nothing changed, even when the Player can afford it.
  - Buying the potion doesn't affect the Sold Out state of the Magical Armor or Magical Weapon.
- **Player (main seam for drinking and Max Health):**
  - After the Health roll, Max Health equals Health.
  - With a count of 0, drinking returns No Potion and changes nothing, including when Health is below Max Health.
  - At Max Health with a potion, drinking returns Already At Max Health, restores 0, and keeps the potion.
  - A successful drink returns Healed, reduces the count by 1, and raises Health.
  - Starting at 17 of 18 Health over many trials, Health always ends at exactly 18 and the amount restored is always 1.
  - Starting at low Health with a high Max Health over many trials, the amount restored is always 1 to 8.
  - The returned amount always equals the actual change in Health.
- **Game:** combat-input validation accepts `d`, `drink`, `D`, and ` Drink `, still accepts attack and retreat, and still rejects other input.
- **Messages:** every new key resolves in English, Spanish, and French. A missing key shows up as `[key]`, so tests assert the result is not wrapped in brackets.
- **Prior art:** the Merchant purchase tests (outcomes, refusals changing nothing, Sold Out even when affordable), the Player take-damage and Gold roll tests (returned amounts, floors and ranges by sampling), the combat-choice validation tests in the Game tests, and the multi-language key checks in the Messages tests.
- **Not directly tested:** the Combat loop and the Merchant menu loop, which own console input and output, as today. That means two things are verified only by playing the game: drinking skips the Player's attack while the Dragon still attacks, and refused drinks repeat the prompt.

## Out of Scope

- A general inventory, or any item other than the Healing Potion.
- Buying more than one Healing Potion, or potions that restore different amounts.
- Drinking outside combat: at the crossroads, at the Merchant, or after retreating.
- Changing retreat so it returns the Player to the crossroads; retreat still ends the game.
- Drinking before the first exchange of combat.
- Raising Max Health, or any other effect that changes it.
- Difficulty settings, even though this branch's name mentions difficulty. That's a separate feature.
- Any change to how starting Health is rolled, including the known low-Health bug below.
- Translating typed combat commands.

## Further Notes

- Domain terms follow the glossary: Health, Max Health, Healing Potion, Merchant, Gold, Sold Out, Protection. Use "drink" for using a Healing Potion. Don't call it a "health potion" or an inventory item.
- The drink option first appears after the Dragon's first attack, because the prompt follows each exchange. That's acceptable: the Player is at Max Health before then.
- **Known existing bug, for a separate ticket:** a Halfling can roll starting Health of 0 or below (a d20 roll of 1 minus a strength modifier of up to 4). Max Health would then also be 0 or below, so a potion could never heal. This feature doesn't cause it.
- **Affordability:** Gold comes in steps of 10, so the potion needs 30 Gold or more (a d20 of 3 or higher), which 90% of Players have. 100 Gold buys one gear item plus the potion. Both gear items plus the potion need 175, effectively 180, which about 15% of Players have.
- **Implementation tickets:** 01 is a prefactor that adds Max Health and makes the Health roll public. 02 has the Merchant sell the Healing Potion and shows the count. 01 and 02 are independent. 03 adds drinking in combat and depends on both.
