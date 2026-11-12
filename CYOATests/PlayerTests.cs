[TestClass]
public class PlayerTests
{
    [TestMethod]
    public void NewPlayer_ShouldHaveDefaultValues()
    {
        Player player = new Player();

        Assert.AreEqual("", player.Name);
        Assert.AreEqual("", player.Race);
        Assert.AreEqual("", player.Occupation);

        Assert.AreEqual(0, player.Strength);
        Assert.AreEqual(0, player.Agility);
        Assert.AreEqual(0, player.HealthPoints);
        Assert.AreEqual(0, player.MaxHealth);
        Assert.AreEqual(0, player.Protection);

        Assert.IsNull(player.Weapon);
    }

    [TestMethod]
    public void TakeDamage_WithNoProtection_AppliesFullDamage()
    {
        Player player = new Player { HealthPoints = 20 };

        int taken = player.TakeDamage(7);

        Assert.AreEqual(7, taken);
        Assert.AreEqual(13, player.HealthPoints);
    }

    [TestMethod]
    public void TakeDamage_MoreThanHealth_FloorsHealthAtZeroAndReturnsDamageAfterProtection()
    {
        Player player = new Player { HealthPoints = 4 };

        int taken = player.TakeDamage(10);

        Assert.AreEqual(10, taken);
        Assert.AreEqual(0, player.HealthPoints);
    }

    [TestMethod]
    public void TakeDamage_WithProtection_ReducesDamageByProtection()
    {
        Player player = new Player { HealthPoints = 20, Protection = 3 };

        int taken = player.TakeDamage(7);

        Assert.AreEqual(4, taken);
        Assert.AreEqual(16, player.HealthPoints);
    }

    [TestMethod]
    public void RollGold_AlwaysGivesMultipleOfTenFrom10To200()
    {
        Player player = new Player();

        for (int i = 0; i < 1000; i++)
        {
            player.RollGold();

            Assert.IsTrue(player.Gold >= 10 && player.Gold <= 200, $"Gold {player.Gold} out of range.");
            Assert.AreEqual(0, player.Gold % 10, $"Gold {player.Gold} is not a multiple of 10.");
        }
    }

    [TestMethod]
    [DataRow(3)]
    [DataRow(0)]
    [DataRow(-4)]
    public void RollHealth_SetsMaxHealthEqualToRolledHealth(int strengthModifier)
    {
        Player player = new Player();

        for (int i = 0; i < 1000; i++)
        {
            player.RollHealth(strengthModifier);

            Assert.IsTrue(
                player.HealthPoints >= 1 + strengthModifier && player.HealthPoints <= 20 + strengthModifier,
                $"Health {player.HealthPoints} out of range for modifier {strengthModifier}.");
            Assert.AreEqual(player.HealthPoints, player.MaxHealth);
        }
    }

    [TestMethod]
    public void TakeDamage_LowersHealthButLeavesMaxHealthUnchanged()
    {
        Player player = new Player();
        // A large modifier keeps rolled Health (26..45) above the 7 damage, so Health never floors at 0.
        player.RollHealth(25);
        int maxHealthBefore = player.MaxHealth;

        player.TakeDamage(7);

        Assert.AreEqual(maxHealthBefore - 7, player.HealthPoints);
        Assert.AreEqual(maxHealthBefore, player.MaxHealth);
    }

    [TestMethod]
    public void TakeDamage_AtOrBelowProtection_TakesNoDamage()
    {
        Player player = new Player { HealthPoints = 20, Protection = 3 };

        int taken = player.TakeDamage(2);

        Assert.AreEqual(0, taken);
        Assert.AreEqual(20, player.HealthPoints);
    }
}
