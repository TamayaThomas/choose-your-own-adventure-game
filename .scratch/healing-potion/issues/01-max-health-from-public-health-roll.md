# 01: Max Health set by a public Health roll

**What to build:** A prefactor with no visible change to play. The Player's Health roll becomes a public operation, like the existing Gold roll. It takes the strength modifier and sets both Health and the new Max Health to the rolled value. Character creation's "roll" prompt for Health calls it, so creating a character works exactly as before. Max Health doesn't change for the rest of the adventure; Dragon hits lower Health only.

See the parent spec: `.scratch/healing-potion/spec.md`.

**Blocked by:** None (can start immediately).

**Status:** ready-for-agent

- [ ] The Player has a Max Health, distinct from Health.
- [ ] The Health roll is a public Player operation taking the strength modifier; it sets Health to a d20 roll plus the modifier and sets Max Health to the same value.
- [ ] Character creation's Health prompt calls the public operation; prompts, order, and displayed stats are unchanged.
- [ ] Taking damage lowers Health and leaves Max Health unchanged.
- [ ] Tests: after the Health roll, Max Health equals Health (sampled over many rolls, with positive, zero, and negative modifiers).
- [ ] Tests: after taking damage, Max Health is unchanged.
- [ ] All existing tests still pass.
