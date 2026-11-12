public enum PurchaseResult
{
    Purchased,
    NotEnoughGold,
    SoldOut
}

/// <summary>The items the Merchant sells, declared in menu order.</summary>
public enum MerchantItem
{
    MagicalArmor,
    MagicalWeapon,
    HealingPotion
}

/// <summary>
/// One item on the Merchant's menu. Amount is the number its menu line shows: Protection for
/// Magical Armor, the damage bonus for the Magical Weapon, the maximum restore for a Healing Potion.
/// </summary>
public readonly record struct Offer(MerchantItem Item, int Price, int Amount, bool IsSoldOut);

/// <summary>Trading rules for the Merchant on the south path. Never touches the console.</summary>
public class Merchant
{
    private const int MagicalArmorPrice = 75;
    private const int MagicalArmorProtection = 3;
    private const int MagicalWeaponPrice = 75;
    private const int MagicalWeaponDamageBonus = 4;
    private const int HealingPotionPrice = 25;

    private readonly Player _player;
    private readonly HashSet<MerchantItem> _soldOut = new();

    public Merchant(Player player)
    {
        _player = player;
    }

    /// <summary>One Offer per item, in menu order, with its current Sold Out state.</summary>
    public IReadOnlyList<Offer> Offers => Enum.GetValues<MerchantItem>().Select(OfferFor).ToList();

    /// <summary>
    /// Sells the item to the Player at most once. Checks Sold Out before Gold; a refused purchase
    /// changes nothing. Throws <see cref="ArgumentOutOfRangeException"/> for an undefined item.
    /// </summary>
    public PurchaseResult Buy(MerchantItem item)
    {
        Offer offer = OfferFor(item);

        if (offer.IsSoldOut)
            return PurchaseResult.SoldOut;

        if (_player.Gold < offer.Price)
            return PurchaseResult.NotEnoughGold;

        _player.Gold -= offer.Price;
        GiveToPlayer(item);
        _soldOut.Add(item);
        return PurchaseResult.Purchased;
    }

    private Offer OfferFor(MerchantItem item)
    {
        bool isSoldOut = _soldOut.Contains(item);
        return item switch
        {
            MerchantItem.MagicalArmor => new Offer(item, MagicalArmorPrice, MagicalArmorProtection, isSoldOut),
            MerchantItem.MagicalWeapon => new Offer(item, MagicalWeaponPrice, MagicalWeaponDamageBonus, isSoldOut),
            MerchantItem.HealingPotion => new Offer(item, HealingPotionPrice, Player.HealingPotionMaxRestore, isSoldOut),
            _ => throw new ArgumentOutOfRangeException(nameof(item), item, "Not an item the Merchant sells."),
        };
    }

    private void GiveToPlayer(MerchantItem item)
    {
        switch (item)
        {
            case MerchantItem.MagicalArmor:
                // Protection reduces each Dragon hit that gets past the Player's defense roll.
                _player.Protection = MagicalArmorProtection;
                break;
            case MerchantItem.MagicalWeapon:
                // Enchants the Player's own weapon: higher maximum damage and its magical name, same art.
                if (_player.Weapon != null)
                {
                    _player.Weapon.MaxDamage += MagicalWeaponDamageBonus;
                    _player.Weapon.Type = "magical " + _player.Weapon.Type;
                }
                break;
            case MerchantItem.HealingPotion:
                _player.HealingPotions += 1;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(item), item, "Not an item the Merchant sells.");
        }
    }
}
