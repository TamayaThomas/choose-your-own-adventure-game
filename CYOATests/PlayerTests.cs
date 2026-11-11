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
    public void TakeDamage_AtOrBelowProtection_TakesNoDamage()
    {
        Player player = new Player { HealthPoints = 20, Protection = 3 };

        int taken = player.TakeDamage(2);

        Assert.AreEqual(0, taken);
        Assert.AreEqual(20, player.HealthPoints);
    }
}
