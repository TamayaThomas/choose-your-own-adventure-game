[TestClass]
public class MerchantTests
{
    private static Player PlayerWith(int gold)
    {
        return new Player { Gold = gold, Weapon = new Weapon("long sword", 12, "-)=====>") };
    }

    [TestMethod]
    public void BuyMagicalWeapon_WithEnoughGold_DeductsPriceAndReturnsPurchased()
    {
        Player player = PlayerWith(100);
        Merchant merchant = new Merchant(player);

        PurchaseResult result = merchant.BuyMagicalWeapon();

        Assert.AreEqual(PurchaseResult.Purchased, result);
        Assert.AreEqual(25, player.Gold);
    }

    [TestMethod]
    public void BuyMagicalWeapon_With74Gold_ReturnsNotEnoughGoldAndChangesNothing()
    {
        Player player = PlayerWith(74);
        Merchant merchant = new Merchant(player);

        PurchaseResult result = merchant.BuyMagicalWeapon();

        Assert.AreEqual(PurchaseResult.NotEnoughGold, result);
        Assert.AreEqual(74, player.Gold);
        Assert.AreEqual("long sword", player.Weapon!.Type);
        Assert.AreEqual(12, player.Weapon.MaxDamage);
        Assert.IsFalse(merchant.IsMagicalWeaponSoldOut);
    }

    [TestMethod]
    public void BuyMagicalWeapon_WithExactly75Gold_SucceedsAndLeavesZero()
    {
        Player player = PlayerWith(75);
        Merchant merchant = new Merchant(player);

        PurchaseResult result = merchant.BuyMagicalWeapon();

        Assert.AreEqual(PurchaseResult.Purchased, result);
        Assert.AreEqual(0, player.Gold);
    }

    [TestMethod]
    public void BuyMagicalWeapon_SecondTime_ReturnsSoldOutAndChangesNothingEvenWhenAffordable()
    {
        Player player = PlayerWith(200);
        Merchant merchant = new Merchant(player);
        merchant.BuyMagicalWeapon();

        PurchaseResult result = merchant.BuyMagicalWeapon();

        Assert.AreEqual(PurchaseResult.SoldOut, result);
        Assert.AreEqual(125, player.Gold);
        Assert.AreEqual(16, player.Weapon!.MaxDamage);
        Assert.IsTrue(merchant.IsMagicalWeaponSoldOut);
    }

    [DataTestMethod]
    [DataRow("long sword", 12, "-)=====>", "magical long sword", 16)]                       // Fighter
    [DataRow("lightning bolt spell", 12, "zap~~~~~~", "magical lightning bolt spell", 16)]  // Magician
    [DataRow("dagger", 6, "-)==>", "magical dagger", 10)]                                   // Thief
    [DataRow("long bow", 8, "}    -->", "magical long bow", 12)]                            // Archer
    public void BuyMagicalWeapon_EnchantsOwnWeapon(string type, int maxDamage, string art,
                                                   string expectedType, int expectedMaxDamage)
    {
        Player player = new Player { Gold = 75, Weapon = new Weapon(type, maxDamage, art) };
        Merchant merchant = new Merchant(player);

        merchant.BuyMagicalWeapon();

        Assert.AreEqual(expectedType, player.Weapon!.Type);
        Assert.AreEqual(expectedMaxDamage, player.Weapon.MaxDamage);
        Assert.AreEqual(art, player.Weapon.AsciiArt);
    }

    [TestMethod]
    public void BuyMagicalArmor_WithEnoughGold_DeductsPriceSetsProtectionAndReturnsPurchased()
    {
        Player player = PlayerWith(100);
        Merchant merchant = new Merchant(player);

        PurchaseResult result = merchant.BuyMagicalArmor();

        Assert.AreEqual(PurchaseResult.Purchased, result);
        Assert.AreEqual(25, player.Gold);
        Assert.AreEqual(3, player.Protection);
        Assert.IsTrue(merchant.IsMagicalArmorSoldOut);
    }

    [TestMethod]
    public void BuyMagicalArmor_With74Gold_ReturnsNotEnoughGoldAndChangesNothing()
    {
        Player player = PlayerWith(74);
        Merchant merchant = new Merchant(player);

        PurchaseResult result = merchant.BuyMagicalArmor();

        Assert.AreEqual(PurchaseResult.NotEnoughGold, result);
        Assert.AreEqual(74, player.Gold);
        Assert.AreEqual(0, player.Protection);
        Assert.IsFalse(merchant.IsMagicalArmorSoldOut);
    }

    [TestMethod]
    public void BuyMagicalArmor_WithExactly75Gold_SucceedsAndLeavesZero()
    {
        Player player = PlayerWith(75);
        Merchant merchant = new Merchant(player);

        PurchaseResult result = merchant.BuyMagicalArmor();

        Assert.AreEqual(PurchaseResult.Purchased, result);
        Assert.AreEqual(0, player.Gold);
        Assert.AreEqual(3, player.Protection);
    }

    [TestMethod]
    public void BuyMagicalArmor_SecondTime_ReturnsSoldOutAndChangesNothingEvenWhenAffordable()
    {
        Player player = PlayerWith(200);
        Merchant merchant = new Merchant(player);
        merchant.BuyMagicalArmor();

        PurchaseResult result = merchant.BuyMagicalArmor();

        Assert.AreEqual(PurchaseResult.SoldOut, result);
        Assert.AreEqual(125, player.Gold);
        Assert.AreEqual(3, player.Protection);
        Assert.IsTrue(merchant.IsMagicalArmorSoldOut);
    }

    [TestMethod]
    public void BuyBothItems_WithExactly150Gold_BothSucceedAndLeaveZero()
    {
        Player player = PlayerWith(150);
        Merchant merchant = new Merchant(player);

        Assert.AreEqual(PurchaseResult.Purchased, merchant.BuyMagicalArmor());
        Assert.AreEqual(PurchaseResult.Purchased, merchant.BuyMagicalWeapon());

        Assert.AreEqual(0, player.Gold);
        Assert.AreEqual(3, player.Protection);
        Assert.AreEqual("magical long sword", player.Weapon!.Type);
        Assert.AreEqual(16, player.Weapon.MaxDamage);
    }

    [TestMethod]
    public void BuyBothItems_With149Gold_SecondPurchaseIsRefused()
    {
        Player player = PlayerWith(149);
        Merchant merchant = new Merchant(player);

        Assert.AreEqual(PurchaseResult.Purchased, merchant.BuyMagicalWeapon());
        Assert.AreEqual(PurchaseResult.NotEnoughGold, merchant.BuyMagicalArmor());

        Assert.AreEqual(74, player.Gold);
        Assert.AreEqual(0, player.Protection);
    }
}
