# 01: Merchant Offers and a single Buy

**What to build:** A refactor with no visible change to play. Replace the Merchant's per-item interface (three Buy methods, three Sold Out properties, public price and bonus constants) with `Offers` and `Buy(MerchantItem)`. The Sold Out-before-Gold rule is written once, and each item's effect lives in one private switch. Game's merchant screen switches to the new interface but keeps its current hand-written menu lines, so the screen looks exactly as it does today.

See the parent spec: `.scratch/merchant-offers/spec.md`.

**Blocked by:** None (can start immediately).

**Status:** ready-for-agent

- [ ] `MerchantItem` enumeration: MagicalArmor, MagicalWeapon, HealingPotion, in menu order.
- [ ] `Offer(MerchantItem Item, int Price, int Amount, bool IsSoldOut)` readonly record struct.
- [ ] Merchant exposes `IReadOnlyList<Offer> Offers` (menu order, current Sold Out state) and `PurchaseResult Buy(MerchantItem)`.
- [ ] `Buy` checks Sold Out, then Gold; a refused purchase changes nothing; an undefined item throws `ArgumentOutOfRangeException`.
- [ ] The old `Buy…` methods and `Is…SoldOut` properties are deleted; the price and bonus constants are private; `Merchant.HealingPotionMaxRestore` is deleted and the potion's Amount comes from `Player.HealingPotionMaxRestore`.
- [ ] Game's merchant screen reads prices, amounts, and Sold Out state from `Offers` and buys through `Buy`; its output is unchanged.
- [ ] Merchant tests are rewritten against the new interface: data-driven tests over every item for the shared rules, plus separate tests for each item's effect, the Offers list, the both-items Gold edges, and an undefined item (see the spec's Testing Decisions).
- [ ] All other existing tests still pass.
