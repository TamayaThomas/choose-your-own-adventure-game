# ADR 0001: Add Italian as a supported language

- **Status:** Accepted
- **Date:** 2026-11-04
- **Decider:** Tamaya Thomas

## Context

The game supports English, Spanish and French. All player-facing text lives in
`language_data.json`, one section per language, and `Messages.cs` loads the section
for the language the player picks at startup. Each section has six tables:

- `dictionary`: every message the game prints
- `raceMap` and `occupationMap`: the words a player may type, mapped to the English
  names the game logic uses (`Human`, `Fighter`, ...)
- `displayRaceMap`, `displayOccupationMap` and `displayWeaponMap`: English names
  mapped back to translated names for display

Because of this design, a new language is mostly data. The only code involved is the
language menu in `Game.cs`, which has the choices written into it.

The goal is to offer a larger variety of languages to the people playing the game.
More languages let more players play in a language they are comfortable with.

## Decision

Add Italian as a fourth language, menu option `4. Italiano`, as follows:

1. **Add the translation as data, not code.** Add an `Italian` section to
   `language_data.json` with all six tables. In `Game.cs`, the only change is the
   menu text and mapping `"4"` to `"Italian"`.
2. **Leave out accents.** Write the Italian text without accented letters,
   as the Spanish and French text already is. Where dropping an accent would read
   badly or change the meaning (for example *è* "is" against *e* "and"), reword
   instead ("Come si chiama il tuo personaggio?" rather than "Qual è il nome...").
3. **Keep the typed commands in English.** `roll`, `n`/`s`/`e` and `a`/`r` are
   written into the code for every language. The Italian prompts tell players to
   type these same commands rather than translated ones.
4. **Players type Italian race and occupation names**: `umano`, `elfo`, `nano`,
   `halfling`; `guerriero`, `mago`, `ladro`, `arciere`. These map to the existing
   English names, so the game logic doesn't change.
5. **Keep "Halfling" in English**, as Italian fantasy writing often does, instead
   of *mezzuomo*.
6. **Add tests** for loading Italian, Italian input and display names, and the
   placeholders in Italian messages. Also add a test that every non-English language
   has every English message key.

## Alternatives considered

| Option | Why not chosen |
|---|---|
| Use accented letters | The Spanish and French text already leaves accents out. Accented letters can show up garbled on Windows consoles whose code page differs from the file's encoding. |
| Translate the typed commands (e.g. `tira` for `roll`) | The commands are written into `Game.cs`, `Player.cs` and `Combat.cs`. Translating them would change code in three classes for one language, and no other language does it. |
| Use *mezzuomo* for Halfling | Both are correct. "Halfling" is easier to recognize, and it is the same word English players type. |
| Add a language without a full set of keys | Missing keys show up as `[key]` in the game. The new all-keys test prevents this. |

## Consequences

**Positive**

- Italian-speaking players can play the whole game in Italian.
- The change shows that a new language needs almost no code: one JSON section and
  one menu line.
- The all-keys test now protects every language. A key added to English without
  translations will fail the test suite.

**Negative / risks**

- The translation was not written by a native speaker. A native speaker should
  review it.
- Text without accents is correct enough to read but not fully correct Italian.
- English commands such as `roll` are mixed into Italian prompts.
- The language menu in `Game.cs` is still written by hand. Each new language
  still needs a code change there and an update to the menu text.

**Follow-ups (optional)**

- Have a native speaker review the Italian text.
- Build the language menu from the sections in `language_data.json`, so a new language
  needs no code change.
- Set `Console.OutputEncoding = Encoding.UTF8` and start using accents in all languages.
