using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
[TestClass]
public class DragonTests
{
    private const int DamageTrials = 5000;
    private const int AttackTrials = 500;

    private Messages GetMessages(string language = "English")
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage(language);
        messages.ReadDictionary();
        return messages;
    }

    [TestMethod]
    public void NewDragon_InitializesRequiredProperties()
    {
        Die die = new Die();
        Dragon dragon = new Dragon(die);

        Assert.AreEqual("Smolderfang", dragon.Name);
        Assert.IsTrue(dragon.Strength >= 1 && dragon.Strength <= 20);
        Assert.IsTrue(dragon.Agility >= 1 && dragon.Agility <= 20);
        Assert.IsTrue(dragon.HealthPoints >= 1 && dragon.HealthPoints <= 20);

        Assert.IsNotNull(dragon.Weapon);
        Assert.AreEqual("claws", dragon.Weapon.Type);
        Assert.AreEqual(12, dragon.Weapon.MaxDamage);
        Assert.IsFalse(string.IsNullOrWhiteSpace(dragon.Weapon.AsciiArt));
    }

    [TestMethod]
    public void NewDragon_WithoutDifficulty_IsNormal()
    {
        Dragon dragon = new Dragon(new Die());

        Assert.AreEqual(Difficulty.Normal, dragon.Difficulty);
    }

    [DataTestMethod]
    [DataRow(Difficulty.Easy, 1, 6)]
    [DataRow(Difficulty.Normal, 1, 12)]
    [DataRow(Difficulty.Hard, 2, 18)]
    public void RollDamage_StaysWithinDifficultyRange(Difficulty difficulty, int min, int max)
    {
        Dragon dragon = new Dragon(new Die(), difficulty);

        for (int i = 0; i < DamageTrials; i++)
        {
            int damage = dragon.RollDamage();
            Assert.IsTrue(damage >= min && damage <= max,
                $"{difficulty} damage {damage} is outside {min}-{max}.");
        }
    }

    private static HashSet<int> SampleDamage(Difficulty difficulty)
    {
        Dragon dragon = new Dragon(new Die(), difficulty);
        HashSet<int> seen = new();
        for (int i = 0; i < DamageTrials; i++)
            seen.Add(dragon.RollDamage());
        return seen;
    }

    [TestMethod]
    public void RollDamage_OnEasy_ReachesSix()
    {
        CollectionAssert.Contains(SampleDamage(Difficulty.Easy).ToList(), 6);
    }

    [TestMethod]
    public void RollDamage_OnHard_ReachesTwoAndEighteen()
    {
        List<int> seen = SampleDamage(Difficulty.Hard).ToList();
        CollectionAssert.Contains(seen, 2);
        CollectionAssert.Contains(seen, 18);
    }

    [TestMethod]
    public void RollDamage_OnHard_NeverTakesUnreachableValues()
    {
        HashSet<int> seen = SampleDamage(Difficulty.Hard);
        foreach (int impossible in new[] { 4, 7, 10, 13, 16 })
            Assert.IsFalse(seen.Contains(impossible), $"Hard damage should never be {impossible}.");
    }

    [DataTestMethod]
    [DataRow(Difficulty.Easy)]
    [DataRow(Difficulty.Normal)]
    [DataRow(Difficulty.Hard)]
    public void Attack_OnPlayerWithProtection_NeverRaisesHealthOrDropsBelowZero(Difficulty difficulty)
    {
        Messages messages = GetMessages();

        for (int i = 0; i < AttackTrials; i++)
        {
            Dragon dragon = new Dragon(new Die(), difficulty);
            Player player = new Player
            {
                Name = "TestHero",
                Agility = 0,
                HealthPoints = 10,
                Protection = 3
            };

            int before = player.HealthPoints;
            dragon.Attack(player, messages);

            Assert.IsTrue(player.HealthPoints <= before, "Dragon attack raised Health.");
            Assert.IsTrue(player.HealthPoints >= 0, "Dragon attack dropped Health below zero.");
        }
    }

    [TestMethod]
    public void GetRandomDefenseTaunt_ReturnsExpectedTaunt()
    {
        Die die = new Die();
        Dragon dragon = new Dragon(die);
        Messages messages = GetMessages();

        string taunt = dragon.GetRandomDefenseTaunt(messages);

        string[] validTaunts =
        {
        messages.GetMessage("dragon_defense_taunt_1"),
        messages.GetMessage("dragon_defense_taunt_2"),
        messages.GetMessage("dragon_defense_taunt_3"),
        messages.GetMessage("dragon_defense_taunt_4")
    };

        CollectionAssert.Contains(validTaunts, taunt);
    }

    [TestMethod]
    public void GetRandomDamageReply_ReturnsExpectedReply()
    {
        Die die = new Die();
        Dragon dragon = new Dragon(die);
        Messages messages = GetMessages();

        string reply = dragon.GetRandomDamageReply(messages);

        string[] validReplies =
        {
        messages.GetMessage("dragon_damage_reply_1"),
        messages.GetMessage("dragon_damage_reply_2"),
        messages.GetMessage("dragon_damage_reply_3"),
        messages.GetMessage("dragon_damage_reply_4")
    };

        CollectionAssert.Contains(validReplies, reply);
    }

    [TestMethod]
    public void Attack_ReturnsNonEmptyResultString()
    {
        Die die = new Die();
        Dragon dragon = new Dragon(die);

        Player player = new Player
        {
            Name = "TestHero",
            Agility = 10,
            HealthPoints = 30
        };

        string result = dragon.Attack(player, GetMessages());

        Assert.IsFalse(string.IsNullOrWhiteSpace(result));
    }

    [TestMethod]
    public void Attack_DoesNotIncreasePlayerHealth()
    {
        Die die = new Die();
        Dragon dragon = new Dragon(die);

        Player player = new Player
        {
            Name = "TestHero",
            Agility = 10,
            HealthPoints = 30
        };

        int startingHealth = player.HealthPoints;

        string result = dragon.Attack(player, GetMessages());

        Assert.IsTrue(player.HealthPoints <= startingHealth);
    }

    [TestMethod]
    public void Attack_KeepsPlayerHealthNonNegative()
    {
        Die die = new Die();
        Dragon dragon = new Dragon(die);

        Player player = new Player
        {
            Name = "TestHero",
            Agility = 0,
            HealthPoints = 1
        };

        string result = dragon.Attack(player, GetMessages());

        Assert.IsTrue(player.HealthPoints >= 0);
    }
}

[TestClass]
[DoNotParallelize]
public class GamePbi18Tests
{
    private TextReader? _originalIn;
    private TextWriter? _originalOut;

    [TestInitialize]
    public void Setup()
    {
        _originalIn = Console.In;
        _originalOut = Console.Out;
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (_originalIn != null)
            Console.SetIn(_originalIn);

        if (_originalOut != null)
            Console.SetOut(_originalOut);
    }

    [TestMethod]
    public void DragonDisplayStats_UsesLocalizedLabels()
    {
        Die die = new Die();
        Dragon dragon = new Dragon(die);

        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();

        using StringWriter writer = new StringWriter();
        Console.SetOut(writer);

        dragon.DisplayStats(messages);

        string output = writer.ToString();

        StringAssert.Contains(output, messages.GetMessage("dragon_stats_header"));
        StringAssert.Contains(output, messages.GetMessage("stats_name"));
        StringAssert.Contains(output, messages.GetMessage("stats_strength"));
        StringAssert.Contains(output, messages.GetMessage("stats_agility"));
        StringAssert.Contains(output, messages.GetMessage("stats_health"));
        StringAssert.Contains(output, messages.GetMessage("stats_weapon"));
        StringAssert.Contains(output, messages.GetMessage("stats_damage"));
    }

    [TestMethod]
    public void HandleDragonEncounter_AttackPath_DisplaysEncounterFlowText()
    {
        Game game = new Game();
        Die die = new Die();
        Dragon dragon = new Dragon(die);

        Player player = new Player
        {
            Name = "TestHero",
            Race = "Human",
            Occupation = "Fighter",
            Strength = 20,
            Agility = 20,
            HealthPoints = 25,
            Weapon = new Weapon("long sword", 12, "-)=====>")
        };

        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();

        // "a" enters combat, then "r" retreats from Combat immediately
        Console.SetIn(new StringReader("a\nr\n"));

        using StringWriter writer = new StringWriter();
        Console.SetOut(writer);

        MethodInfo? method = typeof(Game).GetMethod(
            "HandleDragonEncounter",
            BindingFlags.NonPublic | BindingFlags.Instance
        );

        Assert.IsNotNull(method, "HandleDragonEncounter method was not found.");

        method!.Invoke(game, new object[] { player, dragon, messages });

        string output = writer.ToString();

        StringAssert.Contains(output, messages.GetMessage("north_path_narrative"));
        StringAssert.Contains(output, string.Format(messages.GetMessage("dragon_intro"), dragon.Name));
        StringAssert.Contains(output, messages.GetMessage("dragon_stats_intro"));
        StringAssert.Contains(output, messages.GetMessage("dragon_encounter_prompt"));
    }

    [TestMethod]
    public void DragonEncounter_MessageKeys_AreLoaded()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();

        Assert.AreNotEqual("[north_path_narrative]", messages.GetMessage("north_path_narrative"));
        Assert.AreNotEqual("[dragon_intro]", messages.GetMessage("dragon_intro"));
        Assert.AreNotEqual("[dragon_stats_intro]", messages.GetMessage("dragon_stats_intro"));
        Assert.AreNotEqual("[dragon_encounter_prompt]", messages.GetMessage("dragon_encounter_prompt"));
        Assert.AreNotEqual("[dragon_stats_header]", messages.GetMessage("dragon_stats_header"));
    }
}
