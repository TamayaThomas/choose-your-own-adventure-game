# Spec: Merchant Encounter on the South Path

Status: ready-for-agent

## Problem Statement

When the Player takes the south path at the mountain crossroads, nothing happens: the narrative says they get bored and go home poor, and they are dropped back at the crossroads. The south path offers no reason to take it, and the Player has no way to prepare before facing the Dragon on the north path. Every fight against the Dragon uses exactly the gear the Player's occupation started with.

## Solution

The south path leads to a Merchant who sells two pieces of magical gear for Gold: Magical Armor, which gives the Player Protection that reduces damage from Dragon hits, and a Magical Weapon, which enchants the Player's own weapon to deal more damage. The Player rolls their starting Gold during character creation, so how much they can buy depends on luck. They can buy either item, both, or neither, then return to the crossroads and choose north to fight the Dragon with whatever they bought. The Merchant appears on every trip south, but each item can be bought only once.

## User Stories

### Gold

1. As a Player, I want to roll my starting Gold during character creation, so that my purchasing power is part of my character's luck like my other stats.
2. As a Player, I want the Gold roll to use the same "roll" prompt as my other stats, so that character creation feels consistent.
3. As a Player, I want my Gold to be between 10 and 200 in steps of 10, so that amounts are easy to read and compare to prices.
4. As a Player, I want to see my Gold when I view my stats, so that I know what I can afford before heading south.
5. As a Player, I want my unspent Gold to remain visible in my stats, so that I know what I have left.

### Meeting the Merchant

6. As a Player, I want choosing south at the crossroads to lead me to a Merchant, so that the south path is worth taking.
7. As a Player, I want a narrative introducing the Merchant, so that the encounter feels part of the story rather than a bare menu.
8. As a Player, I want the old "nothing happens, you go home poor" text removed, so that the story doesn't contradict the Merchant encounter.
9. As a Player, I want the Merchant to tell me how much Gold I have, so that I can decide what to buy.
10. As a Player, I want to see each item's name, its benefit, and its price, so that I can make an informed choice.
11. As a Player, I want a numbered menu like the main menu, so that the controls are familiar.
12. As a Player, I want a Leave option, so that I can walk away without buying anything.
13. As a Player, I want to return to the crossroads after leaving the Merchant, so that I can still choose to go north and fight the Dragon.
14. As a Player, I want the Merchant to appear every time I go south, so that I can come back if I left without buying.

### Buying

15. As a Player, I want to buy Magical Armor for 75 Gold, so that Dragon attacks hurt me less.
16. As a Player, I want to buy a Magical Weapon for 75 Gold, so that my attacks deal more damage.
17. As a Player with at least 150 Gold, I want to buy both items, so that I'm as prepared as possible.
18. As a Player with exactly 75 Gold, I want to be able to buy one item, ending with 0 Gold, so that having exactly the price is enough.
19. As a Player, I want the price deducted from my Gold when I buy, so that purchases have a real cost.
20. As a Player, I want the menu to reappear with my updated Gold after each purchase, so that I can keep shopping or leave.
21. As a Player who can't afford an item, I want to be told I don't have enough Gold, so that I understand why nothing happened.
22. As a Player who can't afford an item, I want my Gold and gear left unchanged, so that a failed purchase has no side effects.
23. As a Player, I want an item I've already bought to be marked "(sold out)" in the menu, so that I can see what I already own.
24. As a Player who picks a Sold Out item, I want to be told I already bought it, with no Gold taken, so that I can't waste Gold or stack bonuses.
25. As a Player, I want invalid menu input to show the usual invalid message and ask again, so that a typo doesn't kick me out of the shop.

### Magical Weapon

26. As a Player, I want the Magical Weapon to be my own weapon enchanted, so that my occupation's identity is preserved.
27. As a Fighter, I want my long sword to become a magical long sword with maximum damage raised from 12 to 16.
28. As a Magician, I want my lightning bolt spell to become a magical lightning bolt spell with maximum damage raised from 12 to 16.
29. As a Thief, I want my dagger to become a magical dagger with maximum damage raised from 6 to 10.
30. As an Archer, I want my long bow to become a magical long bow with maximum damage raised from 8 to 12.
31. As a Player, I want my stats and combat screen to show my Magical Weapon's name and new maximum damage, so that I can see the upgrade.
32. As a Player, I want my attacks against the Dragon to use the Magical Weapon's higher maximum damage, so that the purchase actually matters in combat.

### Magical Armor and Protection

33. As a Player, I want to start with Protection 0, so that armor is a clear upgrade.
34. As a Player, I want Magical Armor to give me Protection 3, so that every Dragon hit that gets through does 3 less damage.
35. As a Player, I want Protection to never reduce damage below 0, so that armor can't heal me.
36. As a Player, I want a message that my armor absorbed the blow when it blocks all the damage, so that I understand why I took no damage.
37. As a Player, I want to see both the Dragon's damage and how much my armor absorbed when it blocks only part, so that I can see the armor working.
38. As a Player, I want my Protection to apply only after a hit gets past my Agility-based defense roll, so that armor and defense are separate layers.
39. As a Player, I want my health never to drop below 0, so that health displays stay sensible.
40. As a Player, I want my stats and combat screen to show my Protection, so that I know how well protected I am going into the fight.

### Languages

41. As a Spanish-speaking Player, I want all Merchant dialogue, item names, messages, and stat labels in Spanish, so that the game stays playable in my language.
42. As a French-speaking Player, I want all Merchant dialogue, item names, messages, and stat labels in French, so that the game stays playable in my language.
43. As a Player in any language, I want Magical Weapon names translated consistently with the existing weapon names, so that the game reads naturally.

## Implementation Decisions

- **New Merchant module.** It holds the shop rules and never reads or writes the console. It is created for a Player, keeps track of which items are Sold Out, and exposes two purchase operations: buy Magical Armor and buy Magical Weapon. Each operation returns one of three outcomes: Purchased, Not Enough Gold, or Sold Out. It also reports whether each item is Sold Out, for the menu display.
- **Prices and bonuses:** each item costs 75 Gold. Magical Armor sets Protection to 3. The Magical Weapon adds 4 to the Player's current weapon's maximum damage and renames it to its magical form (for example "long sword" becomes "magical long sword"), keeping its art.
- **Checking order:** a purchase checks Sold Out before Gold. A refused purchase changes neither Gold, gear, nor Sold Out state.
- **Sold Out state lasts the whole adventure:** the Merchant instance must be created once per adventure (alongside the Player), not once per trip south. Otherwise items would restock on every trip.
- **Player gains Gold and Protection.** Gold is rolled as d20 × 10 through a public roll operation (like the existing attack roll) and is called during character creation right after health, behind the same "roll" prompt pattern. Protection starts at 0.
- **Player gains a take-damage operation.** It accepts the raw damage, subtracts Protection (never going below 0), lowers health (never below 0), and returns the damage actually taken. The Dragon's attack uses it instead of subtracting health inline, and chooses the full-absorption, partial-absorption, or plain damage message from the raw and actual amounts.
- **Game south branch:** it shows the new Merchant narrative, then runs a thin console menu loop: show Gold and the three options (marking Sold Out items), read input, call the Merchant, print the message for the outcome, and repeat until Leave. Afterwards control returns to the crossroads path prompt as today.
- **Stats display:** the Player stats screen adds Gold and Protection lines. The combat stats screen adds Protection but not Gold.
- **Localization:** every new message key is added to the English, Spanish, and French dictionaries in the language data file. The four Magical Weapon names are added to each language's weapon display map. Spanish and French text follows the file's existing no-accent convention.
- **Unspent Gold** has no other use in this feature.

## Testing Decisions

- **Good tests check behavior, not internals:** they call public operations and assert on outcomes and visible state (Gold, Protection, weapon type and maximum damage, health, resolved messages). They don't assert on private fields or console output formatting.
- **Merchant (main seam):**
  - Buying deducts 75 Gold and returns Purchased.
  - With 74 Gold the result is Not Enough Gold, and nothing changes.
  - With exactly 75 Gold the purchase succeeds and leaves 0.
  - A second purchase of the same item returns Sold Out, and nothing changes.
  - Buying both with 150 Gold or more succeeds.
  - The Magical Weapon adds 4 to maximum damage and renames to the magical form for each of the four occupations.
  - Magical Armor sets Protection to 3.
  - A Sold Out item still reports Sold Out when the Player can afford it again.
- **Player:**
  - Taking damage with Protection 0 applies the full amount.
  - With Protection 3 it reduces damage by 3.
  - Damage at or below Protection becomes 0.
  - Health never goes below 0.
  - The return value equals the damage actually taken.
  - Rolling Gold always gives a value from 10 to 200 that is a multiple of 10 (sampled over many rolls).
- **Messages:** every new message key and every Magical Weapon display name resolves in English, Spanish, and French. A missing key shows up as `[key]`, so tests assert the result is not wrapped in brackets.
- **Prior art:** the existing pure-method tests in the Player and Dragon test files (e.g. attack-hit checks), the multi-language checks in the Messages and Game test files, and the Weapon tests for weapon construction.
- **Not directly tested:** the console menu loop in Game and the Dragon attack's console output, consistent with how the Dragon encounter and combat loop are treated today.

## Out of Scope

- Any other use for Gold (score, other shops, rewards for defeating the Dragon).
- More Merchant items, haggling, selling items back, or prices that change.
- Armor or upgrades for the Dragon.
- Changes to the Agility-based defense roll or to hit chances.
- Refactoring console I/O across the game beyond the Merchant menu.
- Saving state across adventures; Gold and purchases reset when a new character is created.

## Further Notes

- Domain terms follow `CONTEXT.md`: Merchant, Gold, Sold Out, Magical Weapon, Magical Armor, Protection. "Protection" (armor reducing damage) is distinct from "defense" (the Agility block roll).
- Retreating from or finishing the Dragon fight ends the game today, so the Merchant can only be visited before going north.
- Probability check with d20 × 10 Gold and 75 per item: about 65% of Players can afford at least one item and about 30% can afford both.
