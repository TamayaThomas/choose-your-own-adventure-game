public enum PurchaseResult
{
    Purchased,
    NotEnoughGold,
    SoldOut
}

/// <summary>Trading rules for the Merchant on the south path. Never touches the console.</summary>
public class Merchant
{
    public const int MagicalWeaponPrice = 75;
    public const int MagicalWeaponDamageBonus = 4;

    private readonly Player _player;

    public Merchant(Player player)
    {
        _player = player;
    }

    public bool IsMagicalWeaponSoldOut { get; private set; }

    /// <summary>
    /// Enchants the Player's own weapon: higher maximum damage and its magical name, same art.
    /// Checks Sold Out before Gold; a refused purchase changes nothing.
    /// </summary>
    public PurchaseResult BuyMagicalWeapon()
    {
        if (IsMagicalWeaponSoldOut)
            return PurchaseResult.SoldOut;

        if (_player.Gold < MagicalWeaponPrice)
            return PurchaseResult.NotEnoughGold;

        _player.Gold -= MagicalWeaponPrice;
        if (_player.Weapon != null)
        {
            _player.Weapon.MaxDamage += MagicalWeaponDamageBonus;
            _player.Weapon.Type = "magical " + _player.Weapon.Type;
        }
        IsMagicalWeaponSoldOut = true;
        return PurchaseResult.Purchased;
    }
}
