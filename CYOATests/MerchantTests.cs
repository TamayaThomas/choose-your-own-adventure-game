[TestClass]
public class MerchantTests
{
    private static Player PlayerWith(int gold)
    {
        return new Player { Gold = gold, Weapon = new Weapon("long sword", 12, "-)=====>") };
    }

    private static Offer OfferFor(Merchant merchant, MerchantItem item)
    {
        return merchant.Offers.Single(o => o.Item == item);
    }

    private static (int Protection, string? WeaponType, int? WeaponMaxDamage, int HealingPotions) GearOf(Player player)
    {
        return (player.Protection, player.Weapon?.Type, player.Weapon?.MaxDamage, player.HealingPotions);
    }

    // Offers

    [TestMethod]
    public void Offers_ListEveryItemInMenuOrderWithPricesAndAmounts()
    {
        Merchant merchant = new Merchant(PlayerWith(0));

        CollectionAssert.AreEqual(
            new[]
            {
                new Offer(MerchantItem.MagicalArmor, 75, 3, false),
                new Offer(MerchantItem.MagicalWeapon, 75, 4, false),
                new Offer(MerchantItem.HealingPotion, 25, 8, false),
            },
            merchant.Offers.ToArray());
    }

    [TestMethod]
    public void Offers_HealingPotionAmountIsPlayersMaxRestore()
    {
        Merchant merchant = new Merchant(PlayerWith(0));

        Assert.AreEqual(Player.HealingPotionMaxRestore, OfferFor(merchant, MerchantItem.HealingPotion).Amount);
    }

    // Shared purchase rules, for every item

    [DataTestMethod]
    [DataRow(MerchantItem.MagicalArmor, 75)]
    [DataRow(MerchantItem.MagicalWeapon, 75)]
    [DataRow(MerchantItem.HealingPotion, 25)]
    public void Buy_WithEnoughGold_TakesPriceAndSellsOut(MerchantItem item, int price)
    {
        Player player = PlayerWith(200);
        Merchant merchant = new Merchant(player);

        PurchaseResult result = merchant.Buy(item);

        Assert.AreEqual(PurchaseResult.Purchased, result);
        Assert.AreEqual(200 - price, player.Gold);
        Assert.IsTrue(OfferFor(merchant, item).IsSoldOut);
    }

    [DataTestMethod]
    [DataRow(MerchantItem.MagicalArmor, 75)]
    [DataRow(MerchantItem.MagicalWeapon, 75)]
    [DataRow(MerchantItem.HealingPotion, 25)]
    public void Buy_WithExactlyThePrice_SucceedsAndLeavesZero(MerchantItem item, int price)
    {
        Player player = PlayerWith(price);
        Merchant merchant = new Merchant(player);

        PurchaseResult result = merchant.Buy(item);

        Assert.AreEqual(PurchaseResult.Purchased, result);
        Assert.AreEqual(0, player.Gold);
    }

    [DataTestMethod]
    [DataRow(MerchantItem.MagicalArmor, 75)]
    [DataRow(MerchantItem.MagicalWeapon, 75)]
    [DataRow(MerchantItem.HealingPotion, 25)]
    public void Buy_OneGoldShort_ReturnsNotEnoughGoldAndChangesNothing(MerchantItem item, int price)
    {
        Player player = PlayerWith(price - 1);
        Merchant merchant = new Merchant(player);
        var gearBefore = GearOf(player);

        PurchaseResult result = merchant.Buy(item);

        Assert.AreEqual(PurchaseResult.NotEnoughGold, result);
        Assert.AreEqual(price - 1, player.Gold);
        Assert.AreEqual(gearBefore, GearOf(player));
        Assert.IsFalse(OfferFor(merchant, item).IsSoldOut);
    }

    [DataTestMethod]
    [DataRow(MerchantItem.MagicalArmor)]
    [DataRow(MerchantItem.MagicalWeapon)]
    [DataRow(MerchantItem.HealingPotion)]
    public void Buy_SecondTime_ReturnsSoldOutAndChangesNothingEvenWhenAffordable(MerchantItem item)
    {
        Player player = PlayerWith(200);
        Merchant merchant = new Merchant(player);
        merchant.Buy(item);
        int goldBefore = player.Gold;
        var gearBefore = GearOf(player);

        PurchaseResult result = merchant.Buy(item);

        Assert.AreEqual(PurchaseResult.SoldOut, result);
        Assert.AreEqual(goldBefore, player.Gold);
        Assert.AreEqual(gearBefore, GearOf(player));
        Assert.IsTrue(OfferFor(merchant, item).IsSoldOut);
    }

    [DataTestMethod]
    [DataRow(MerchantItem.MagicalArmor)]
    [DataRow(MerchantItem.MagicalWeapon)]
    [DataRow(MerchantItem.HealingPotion)]
    public void Buy_DoesNotSellOutTheOtherItems(MerchantItem item)
    {
        Merchant merchant = new Merchant(PlayerWith(200));

        merchant.Buy(item);

        foreach (Offer offer in merchant.Offers.Where(o => o.Item != item))
            Assert.IsFalse(offer.IsSoldOut, $"{offer.Item} should still be on sale after buying {item}.");
    }

    // What each item gives the Player

    [TestMethod]
    public void Buy_MagicalArmor_SetsProtectionTo3()
    {
        Player player = PlayerWith(75);
        Merchant merchant = new Merchant(player);

        merchant.Buy(MerchantItem.MagicalArmor);

        Assert.AreEqual(3, player.Protection);
    }

    [DataTestMethod]
    [DataRow("long sword", 12, "-)=====>", "magical long sword", 16)]                       // Fighter
    [DataRow("lightning bolt spell", 12, "zap~~~~~~", "magical lightning bolt spell", 16)]  // Magician
    [DataRow("dagger", 6, "-)==>", "magical dagger", 10)]                                   // Thief
    [DataRow("long bow", 8, "}    -->", "magical long bow", 12)]                            // Archer
    public void Buy_MagicalWeapon_EnchantsOwnWeapon(string type, int maxDamage, string art,
                                                    string expectedType, int expectedMaxDamage)
    {
        Player player = new Player { Gold = 75, Weapon = new Weapon(type, maxDamage, art) };
        Merchant merchant = new Merchant(player);

        merchant.Buy(MerchantItem.MagicalWeapon);

        Assert.AreEqual(expectedType, player.Weapon!.Type);
        Assert.AreEqual(expectedMaxDamage, player.Weapon.MaxDamage);
        Assert.AreEqual(art, player.Weapon.AsciiArt);
    }

    [TestMethod]
    public void Buy_HealingPotion_AddsOnePotion()
    {
        Player player = PlayerWith(25);
        Merchant merchant = new Merchant(player);

        merchant.Buy(MerchantItem.HealingPotion);

        Assert.AreEqual(1, player.HealingPotions);
    }

    // Buying both Magical Armor and the Magical Weapon

    [TestMethod]
    public void BuyBothItems_WithExactly150Gold_BothSucceedAndLeaveZero()
    {
        Player player = PlayerWith(150);
        Merchant merchant = new Merchant(player);

        Assert.AreEqual(PurchaseResult.Purchased, merchant.Buy(MerchantItem.MagicalArmor));
        Assert.AreEqual(PurchaseResult.Purchased, merchant.Buy(MerchantItem.MagicalWeapon));

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

        Assert.AreEqual(PurchaseResult.Purchased, merchant.Buy(MerchantItem.MagicalWeapon));
        Assert.AreEqual(PurchaseResult.NotEnoughGold, merchant.Buy(MerchantItem.MagicalArmor));

        Assert.AreEqual(74, player.Gold);
        Assert.AreEqual(0, player.Protection);
    }

    // Undefined item

    [TestMethod]
    public void Buy_UndefinedItem_Throws()
    {
        Merchant merchant = new Merchant(PlayerWith(200));

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => merchant.Buy((MerchantItem)99));
    }
}
