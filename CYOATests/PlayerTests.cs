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
    public void DrinkHealingPotion_WithNoPotion_ReturnsNoPotionAndChangesNothing()
    {
        Player player = new Player { HealthPoints = 5, MaxHealth = 18, HealingPotions = 0 };

        DrinkResult result = player.DrinkHealingPotion();

        Assert.AreEqual(DrinkOutcome.NoPotion, result.Outcome);
        Assert.AreEqual(0, result.HealthRestored);
        Assert.AreEqual(5, player.HealthPoints);
        Assert.AreEqual(0, player.HealingPotions);
    }

    [TestMethod]
    public void DrinkHealingPotion_WhenHurtWithPotion_HealsAndUsesUpThePotion()
    {
        Player player = new Player { HealthPoints = 5, MaxHealth = 18, HealingPotions = 1 };

        DrinkResult result = player.DrinkHealingPotion();

        Assert.AreEqual(DrinkOutcome.Healed, result.Outcome);
        Assert.AreEqual(0, player.HealingPotions);
        Assert.IsTrue(player.HealthPoints > 5, $"Health {player.HealthPoints} did not rise.");
    }

    [TestMethod]
    public void DrinkHealingPotion_AtMaxHealthWithPotion_ReturnsAlreadyAtMaxHealthAndKeepsPotion()
    {
        Player player = new Player { HealthPoints = 18, MaxHealth = 18, HealingPotions = 1 };

        DrinkResult result = player.DrinkHealingPotion();

        Assert.AreEqual(DrinkOutcome.AlreadyAtMaxHealth, result.Outcome);
        Assert.AreEqual(0, result.HealthRestored);
        Assert.AreEqual(18, player.HealthPoints);
        Assert.AreEqual(1, player.HealingPotions);
    }

    [TestMethod]
    public void DrinkHealingPotion_AtMaxHealthWithNoPotion_ReportsNoPotionFirst()
    {
        Player player = new Player { HealthPoints = 18, MaxHealth = 18, HealingPotions = 0 };

        DrinkResult result = player.DrinkHealingPotion();

        Assert.AreEqual(DrinkOutcome.NoPotion, result.Outcome);
        Assert.AreEqual(18, player.HealthPoints);
    }

    [TestMethod]
    public void DrinkHealingPotion_OneBelowMaxHealth_AlwaysCapsAtMaxHealthAndRestoresOne()
    {
        for (int i = 0; i < 1000; i++)
        {
            Player player = new Player { HealthPoints = 17, MaxHealth = 18, HealingPotions = 1 };

            DrinkResult result = player.DrinkHealingPotion();

            Assert.AreEqual(18, player.HealthPoints);
            Assert.AreEqual(1, result.HealthRestored);
        }
    }

    [TestMethod]
    public void DrinkHealingPotion_FarBelowMaxHealth_AlwaysRestoresOneToEight()
    {
        for (int i = 0; i < 1000; i++)
        {
            Player player = new Player { HealthPoints = 1, MaxHealth = 40, HealingPotions = 1 };

            DrinkResult result = player.DrinkHealingPotion();

            Assert.IsTrue(result.HealthRestored >= 1 && result.HealthRestored <= 8,
                $"Restored {result.HealthRestored} out of range.");
        }
    }

    [TestMethod]
    [DataRow(1, 40)]
    [DataRow(12, 18)]
    [DataRow(17, 18)]
    public void DrinkHealingPotion_ReturnedAmountEqualsActualChangeInHealth(int health, int maxHealth)
    {
        for (int i = 0; i < 1000; i++)
        {
            Player player = new Player { HealthPoints = health, MaxHealth = maxHealth, HealingPotions = 1 };

            DrinkResult result = player.DrinkHealingPotion();

            Assert.AreEqual(health + result.HealthRestored, player.HealthPoints);
        }
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
