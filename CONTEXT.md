# Choose Your Own Adventure

A console adventure in which a created character chooses a path at a mountain crossroads, can trade with a merchant to the south, and faces a dragon to the north.

## Glossary

### Characters

**Player**:
The character the user creates (race, occupation, rolled stats) and controls through the adventure.
_Avoid_: Hero, user, character (when meaning the player specifically)

**Dragon**:
The hostile creature met on the north path, fought in combat.
_Avoid_: Monster, enemy, boss

**Merchant**:
The trader met on the south path who sells magical gear to the player in exchange for gold.
_Avoid_: Shopkeeper, vendor, trader, shop

### Economy

**Gold**:
The player's currency, rolled at character creation and spent only at the merchant.
_Avoid_: Money, coins, currency

**Offer**:
An item the merchant sells, with its price and whether it is Sold Out. The merchant's menu lists its offers in a fixed order.
_Avoid_: Listing, product, stock

**Sold Out**:
The state of a merchant item that the player has already bought; each item can be bought at most once.
_Avoid_: Out of stock, unavailable

### Gear

**Weapon**:
The item the player attacks with; its maximum damage sets the die rolled for damage. Each occupation starts with its own weapon.

**Magical Weapon**:
The player's own weapon after the merchant enchants it: same kind of weapon, higher maximum damage.
_Avoid_: Enchanted weapon, new weapon, upgraded weapon

**Magical Armor**:
Armor sold by the merchant that gives the player protection. The player starts with no armor.
_Avoid_: Shield, defense gear

**Protection**:
The amount subtracted from each dragon hit that gets through, never reducing damage below zero. Zero when the player has no armor.
_Avoid_: Armor class, defense (defense means the agility-based block roll)

### Health

**Health**:
The player's current hit points. Dragon hits lower it (never below zero) and a Healing Potion raises it (never above Max Health).
_Avoid_: HP, life, hit points

**Max Health**:
The health the player rolled at character creation; healing can never raise health above it.
_Avoid_: Full health, health cap, starting health

**Healing Potion**:
A single-use item sold by the merchant that the player can drink during combat with the dragon to restore health. The player holds a count of them, not a general inventory.
_Avoid_: Health potion, potion of healing, elixir, inventory item

### Adventure

**Difficulty**:
The challenge level chosen at the start of each adventure, before the player is created: Easy, Normal, or Hard. It changes only how much damage the dragon deals to the player, and stays fixed for the rest of the adventure. Normal is the standard game.
_Avoid_: Mode, game mode, level
