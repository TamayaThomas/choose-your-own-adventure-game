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
    public const int MagicalArmorPrice = 75;
    public const int MagicalArmorProtection = 3;

    private readonly Player _player;

    public Merchant(Player player)
    {
        _player = player;
    }

    public bool IsMagicalWeaponSoldOut { get; private set; }

    public bool IsMagicalArmorSoldOut { get; private set; }

    /// <summary>
    /// Gives the Player Protection, which reduces each Dragon hit that gets past their defense roll.
    /// Checks Sold Out before Gold; a refused purchase changes nothing.
    /// </summary>
    public PurchaseResult BuyMagicalArmor()
    {
        if (IsMagicalArmorSoldOut)
            return PurchaseResult.SoldOut;

        if (_player.Gold < MagicalArmorPrice)
            return PurchaseResult.NotEnoughGold;

        _player.Gold -= MagicalArmorPrice;
        _player.Protection = MagicalArmorProtection;
        IsMagicalArmorSoldOut = true;
        return PurchaseResult.Purchased;
    }

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
