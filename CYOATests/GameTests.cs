// CYOATests/GameTests.cs
[TestClass]
public class GameTests
{
    private Messages GetMessages(string language)
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage(language);
        messages.ReadDictionary();
        return messages;
    }

    [TestMethod]
    public void EndGame_DoesNotThrow_WhenPlayerWinsInEnglish()
    {
        Game game = new Game();
        game.EndGame(true, false, GetMessages("English"));
    }

    [TestMethod]
    public void EndGame_DoesNotThrow_WhenPlayerLosesInEnglish()
    {
        Game game = new Game();
        game.EndGame(false, false, GetMessages("English"));
    }

    [TestMethod]
    public void EndGame_DoesNotThrow_WhenPlayerWinsInSpanish()
    {
        Game game = new Game();
        game.EndGame(true, false, GetMessages("Spanish"));
    }

    [TestMethod]
    public void EndGame_DoesNotThrow_WhenPlayerWinsInFrench()
    {
        Game game = new Game();
        game.EndGame(true, false, GetMessages("French"));
    }

    [TestMethod]
    public void EndGame_DoesNotThrow_WhenPlayerLosesInSpanish()
    {
        Game game = new Game();
        game.EndGame(false, false, GetMessages("Spanish"));
    }

    [TestMethod]
    public void EndGame_DoesNotThrow_WhenPlayerLosesInFrench()
    {
        Game game = new Game();
        game.EndGame(false, false, GetMessages("French"));
    }

    [TestMethod]
    public void EndGame_DoesNotThrow_WhenPlayerRetreatsInEnglish()
    {
        Game game = new Game();
        game.EndGame(false, true, GetMessages("English"));
    }

    [TestMethod]
    public void EndGame_DoesNotThrow_WhenPlayerRetreatsInSpanish()
    {
        Game game = new Game();
        game.EndGame(false, true, GetMessages("Spanish"));
    }

    [TestMethod]
    public void EndGame_DoesNotThrow_WhenPlayerRetreatsInFrench()
    {
        Game game = new Game();
        game.EndGame(false, true, GetMessages("French"));
    }

    // --- IsValidMenuChoice ---

    [TestMethod]
    public void IsValidMenuChoice_ReturnsTrue_ForOne()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidMenuChoice("1"));
    }

    [TestMethod]
    public void IsValidMenuChoice_ReturnsTrue_ForTwo()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidMenuChoice("2"));
    }

    [TestMethod]
    public void IsValidMenuChoice_ReturnsFalse_ForThree()
    {
        Game game = new Game();
        Assert.IsFalse(game.IsValidMenuChoice("3"));
    }

    [TestMethod]
    public void IsValidMenuChoice_ReturnsFalse_ForNull()
    {
        Game game = new Game();
        Assert.IsFalse(game.IsValidMenuChoice(null));
    }

    [TestMethod]
    public void IsValidMenuChoice_ReturnsFalse_ForEmpty()
    {
        Game game = new Game();
        Assert.IsFalse(game.IsValidMenuChoice(""));
    }

    [TestMethod]
    public void IsValidMenuChoice_ReturnsFalse_ForLetters()
    {
        Game game = new Game();
        Assert.IsFalse(game.IsValidMenuChoice("abc"));
    }

    // --- IsValidPathChoice ---

    [TestMethod]
    public void IsValidPathChoice_ReturnsTrue_ForN()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidPathChoice("n"));
    }

    [TestMethod]
    public void IsValidPathChoice_ReturnsTrue_ForNorth()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidPathChoice("north"));
    }

    [TestMethod]
    public void IsValidPathChoice_ReturnsTrue_ForS()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidPathChoice("s"));
    }

    [TestMethod]
    public void IsValidPathChoice_ReturnsTrue_ForSouth()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidPathChoice("south"));
    }

    [TestMethod]
    public void IsValidPathChoice_ReturnsTrue_ForE()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidPathChoice("e"));
    }

    [TestMethod]
    public void IsValidPathChoice_ReturnsTrue_ForExit()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidPathChoice("exit"));
    }

    [TestMethod]
    public void IsValidPathChoice_ReturnsFalse_ForInvalidInput()
    {
        Game game = new Game();
        Assert.IsFalse(game.IsValidPathChoice("west"));
    }

    [TestMethod]
    public void IsValidPathChoice_ReturnsFalse_ForNull()
    {
        Game game = new Game();
        Assert.IsFalse(game.IsValidPathChoice(null));
    }

    [TestMethod]
    public void IsValidPathChoice_ReturnsFalse_ForEmpty()
    {
        Game game = new Game();
        Assert.IsFalse(game.IsValidPathChoice(""));
    }

    [TestMethod]
    public void IsValidPathChoice_ReturnsFalse_ForWhitespace()
    {
        Game game = new Game();
        Assert.IsFalse(game.IsValidPathChoice("   "));
    }

    // --- IsValidMenuChoice: padded input ---

    [TestMethod]
    public void IsValidMenuChoice_ReturnsTrue_ForPaddedOne()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidMenuChoice("  1  "));
    }

    [TestMethod]
    public void IsValidMenuChoice_ReturnsTrue_ForPaddedTwo()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidMenuChoice("  2  "));
    }

    // --- IsValidPathChoice: case insensitivity ---

    [TestMethod]
    public void IsValidPathChoice_ReturnsTrue_ForUppercaseN()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidPathChoice("N"));
    }

    [TestMethod]
    public void IsValidPathChoice_ReturnsTrue_ForUppercaseNorth()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidPathChoice("NORTH"));
    }

    [TestMethod]
    public void IsValidPathChoice_ReturnsTrue_ForUppercaseS()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidPathChoice("S"));
    }

    [TestMethod]
    public void IsValidPathChoice_ReturnsTrue_ForUppercaseE()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidPathChoice("E"));
    }

    // --- IsValidPathChoice: padded input ---

    [TestMethod]
    public void IsValidPathChoice_ReturnsTrue_ForPaddedNorth()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidPathChoice("  north  "));
    }

    // --- IsValidPathChoice: numbers should be invalid ---

    [TestMethod]
    public void IsValidPathChoice_ReturnsFalse_ForOne()
    {
        Game game = new Game();
        Assert.IsFalse(game.IsValidPathChoice("1"));
    }

    [TestMethod]
    public void IsValidPathChoice_ReturnsFalse_ForTwo()
    {
        Game game = new Game();
        Assert.IsFalse(game.IsValidPathChoice("2"));
    }

    [TestMethod]
    public void IsValidPathChoice_ReturnsFalse_ForThree()
    {
        Game game = new Game();
        Assert.IsFalse(game.IsValidPathChoice("3"));
    }

    // --- IsValidDifficultyChoice ---

    [DataTestMethod]
    [DataRow("1")]
    [DataRow("2")]
    [DataRow("3")]
    [DataRow(" 2 ")]
    public void IsValidDifficultyChoice_ReturnsTrue_ForNumberedChoices(string input)
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidDifficultyChoice(input));
    }

    [DataTestMethod]
    [DataRow("0")]
    [DataRow("4")]
    [DataRow("easy")]
    [DataRow("")]
    [DataRow("   ")]
    [DataRow(null)]
    public void IsValidDifficultyChoice_ReturnsFalse_ForAnythingElse(string? input)
    {
        Game game = new Game();
        Assert.IsFalse(game.IsValidDifficultyChoice(input));
    }

    // --- IsValidCombatChoice ---

    [TestMethod]
    public void IsValidCombatChoice_ReturnsTrue_ForA()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidCombatChoice("a"));
    }

    [TestMethod]
    public void IsValidCombatChoice_ReturnsTrue_ForAttack()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidCombatChoice("attack"));
    }

    [TestMethod]
    public void IsValidCombatChoice_ReturnsTrue_ForR()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidCombatChoice("r"));
    }

    [TestMethod]
    public void IsValidCombatChoice_ReturnsTrue_ForRetreat()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidCombatChoice("retreat"));
    }

    [TestMethod]
    [DataRow("d")]
    [DataRow("drink")]
    [DataRow("D")]
    [DataRow(" Drink ")]
    public void IsValidCombatChoice_ReturnsTrue_ForDrinkCommands(string input)
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidCombatChoice(input));
    }

    [TestMethod]
    public void IsValidCombatChoice_ReturnsFalse_ForInvalidInput()
    {
        Game game = new Game();
        Assert.IsFalse(game.IsValidCombatChoice("fight"));
    }

    [TestMethod]
    public void IsValidCombatChoice_ReturnsFalse_ForNull()
    {
        Game game = new Game();
        Assert.IsFalse(game.IsValidCombatChoice(null));
    }

    [TestMethod]
    public void IsValidCombatChoice_ReturnsFalse_ForEmpty()
    {
        Game game = new Game();
        Assert.IsFalse(game.IsValidCombatChoice(""));
    }

    [TestMethod]
    public void IsValidCombatChoice_ReturnsFalse_ForWhitespace()
    {
        Game game = new Game();
        Assert.IsFalse(game.IsValidCombatChoice("   "));
    }

    // --- IsValidCombatChoice: case insensitivity ---

    [TestMethod]
    public void IsValidCombatChoice_ReturnsTrue_ForUppercaseA()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidCombatChoice("A"));
    }

    [TestMethod]
    public void IsValidCombatChoice_ReturnsTrue_ForUppercaseAttack()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidCombatChoice("ATTACK"));
    }

    [TestMethod]
    public void IsValidCombatChoice_ReturnsTrue_ForUppercaseR()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidCombatChoice("R"));
    }

    [TestMethod]
    public void IsValidCombatChoice_ReturnsTrue_ForUppercaseRetreat()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidCombatChoice("RETREAT"));
    }

    // --- IsValidCombatChoice: numbers should be invalid (old prompt said "1. Attack 2. Retreat") ---

    [TestMethod]
    public void IsValidCombatChoice_ReturnsFalse_ForOne()
    {
        Game game = new Game();
        Assert.IsFalse(game.IsValidCombatChoice("1"));
    }

    [TestMethod]
    public void IsValidCombatChoice_ReturnsFalse_ForTwo()
    {
        Game game = new Game();
        Assert.IsFalse(game.IsValidCombatChoice("2"));
    }

    // --- IsValidCombatChoice: padded input ---

    [TestMethod]
    public void IsValidCombatChoice_ReturnsTrue_ForPaddedA()
    {
        Game game = new Game();
        Assert.IsTrue(game.IsValidCombatChoice("  a  "));
    }
}

[TestClass]
[DoNotParallelize]
public class GameMerchantScreenTests
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

    [DataTestMethod]
    [DataRow("English",
        "1. Magical Armor (Protection +3) - 75 Gold",
        "2. Magical Weapon (Damage +4) - 75 Gold",
        "3. Healing Potion (restores up to 8 Health) - 25 Gold",
        "4. Leave",
        " (sold out)")]
    [DataRow("Spanish",
        "1. Armadura Magica (Proteccion +3) - 75 de Oro",
        "2. Arma Magica (Dano +4) - 75 de Oro",
        "3. Pocion de Curacion (restaura hasta 8 puntos de Vida) - 25 de Oro",
        "4. Salir",
        " (agotado)")]
    [DataRow("French",
        "1. Armure Magique (Protection +3) - 75 Or",
        "2. Arme Magique (Degats +4) - 75 Or",
        "3. Potion de Soin (restaure jusqu'a 8 points de Vie) - 25 Or",
        "4. Partir",
        " (epuise)")]
    public void HandleMerchantEncounter_BuyArmorThenLeave_NumbersMenuAndMarksArmorSoldOut(
        string language, string armorLine, string weaponLine, string potionLine, string leaveLine, string soldOutTag)
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage(language);
        messages.ReadDictionary();

        Player player = new Player { Gold = 100, Weapon = new Weapon("long sword", 12, "-)=====>") };
        Merchant merchant = new Merchant(player);

        // "1" buys Magical Armor, "4" leaves.
        Console.SetIn(new StringReader("1\n4\n"));
        using StringWriter writer = new StringWriter();
        Console.SetOut(writer);

        new Game().HandleMerchantEncounter(player, merchant, messages);

        string[] lines = writer.ToString().Split(Environment.NewLine);

        // Before the purchase: items 1-3, then Leave last.
        int first = Array.IndexOf(lines, armorLine);
        Assert.IsTrue(first >= 0, $"Armor line not found before purchase.\n{writer}");
        Assert.AreEqual(weaponLine, lines[first + 1]);
        Assert.AreEqual(potionLine, lines[first + 2]);
        Assert.AreEqual(leaveLine, lines[first + 3]);

        // After the purchase: the armor line carries the Sold Out tag, the rest are unchanged.
        int second = Array.IndexOf(lines, armorLine + soldOutTag);
        Assert.IsTrue(second > first, $"Sold Out armor line not found after purchase.\n{writer}");
        Assert.AreEqual(weaponLine, lines[second + 1]);
        Assert.AreEqual(potionLine, lines[second + 2]);
        Assert.AreEqual(leaveLine, lines[second + 3]);

        Assert.AreEqual(25, player.Gold);
    }

    [DataTestMethod]
    [DataRow("01")]
    [DataRow("+1")]
    [DataRow("0")]
    [DataRow("5")]
    public void HandleMerchantEncounter_ChoiceNotOnTheMenu_IsInvalidAndBuysNothing(string choice)
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();

        Player player = new Player { Gold = 100 };
        Merchant merchant = new Merchant(player);

        // The bad choice, then "4" leaves.
        Console.SetIn(new StringReader(choice + "\n4\n"));
        using StringWriter writer = new StringWriter();
        Console.SetOut(writer);

        new Game().HandleMerchantEncounter(player, merchant, messages);

        StringAssert.Contains(writer.ToString(), messages.GetMessage("invalid"));
        Assert.AreEqual(100, player.Gold);
        Assert.AreEqual(0, player.Protection);
    }
}
