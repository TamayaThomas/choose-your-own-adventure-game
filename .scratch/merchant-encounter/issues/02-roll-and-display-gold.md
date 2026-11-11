# 02: Roll and display Gold

**What to build:** During character creation, after rolling health, the Player is asked to type "roll" for their Gold, using the same prompt pattern as the other stats. Gold is d20 × 10 (10 to 200). The Player stats screen shows Gold. All new text is available in English, Spanish, and French. Gold has no use yet; the Merchant (ticket 03) spends it.

See the parent spec: `.scratch/merchant-encounter/spec.md`.

**Blocked by:** None (can start immediately).

**Status:** ready-for-agent

- [x] Character creation shows a Gold "roll" prompt right after the health roll; invalid input shows the existing roll-invalid message and asks again.
- [x] Gold is set to d20 × 10 through a public roll operation on the Player.
- [x] The Player stats screen shows a Gold line. The combat stats screen does not show Gold.
- [x] The Gold roll prompt and Gold stat label exist in English, Spanish, and French (Spanish and French without accents, following the existing language data).
- [x] Tests: the Gold roll always gives a value from 10 to 200 that is a multiple of 10 (sampled over many rolls).
- [x] Tests: every new message key resolves in all three languages (the result is not `[key]`).
