# 01: Route Dragon damage through Player.TakeDamage

**What to build:** When the Dragon lands a hit that gets past the Player's defense roll, the damage is applied through a new Player take-damage operation instead of being subtracted from health inside the Dragon's attack. The Player gains a Protection value that starts at 0, so combat behaves exactly as it does today. This prefactor lets Magical Armor (ticket 04) plug in without touching the Dragon's attack logic again.

See the parent spec: `.scratch/merchant-encounter/spec.md`.

**Blocked by:** None (can start immediately).

**Status:** ready-for-agent

- [x] The Player has a Protection value that starts at 0.
- [x] The Player's take-damage operation subtracts Protection from the raw damage (never below 0), lowers health (never below 0), and returns the damage actually taken.
- [x] The Dragon's attack uses the take-damage operation; it no longer changes the Player's health directly.
- [x] Combat output and outcomes are unchanged from today (same messages, same damage, health still floors at 0).
- [x] Tests: with Protection 0, full damage is applied; health never goes below 0; the return value equals the damage actually taken.
- [x] All existing tests still pass.
