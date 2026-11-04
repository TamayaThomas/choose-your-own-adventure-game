// CYOATests/MessagesTests.cs
[TestClass]
public class MessagesTests
{
    [TestMethod]
    public void Constructor_DefaultLanguageIsEnglish()
    {
        Messages messages = new Messages();
        Assert.AreEqual("English", messages.CurrentLanguage);
    }

    [TestMethod]
    public void SetCurrentLanguage_SetsLanguageToSpanish()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("Spanish");
        Assert.AreEqual("Spanish", messages.CurrentLanguage);
    }

    [TestMethod]
    public void SetCurrentLanguage_SetsLanguageToFrench()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("French");
        Assert.AreEqual("French", messages.CurrentLanguage);
    }

    [TestMethod]
    public void SetCurrentLanguage_SetsLanguageToEnglish()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("Spanish");
        messages.SetCurrentLanguage("English");
        Assert.AreEqual("English", messages.CurrentLanguage);
    }

    [TestMethod]
    public void ReadDictionary_LoadsEnglishWelcomeMessage()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        string result = messages.GetMessage("welcome");
        Assert.AreEqual("Welcome to Choose Your Adventure Game", result);
    }

    [TestMethod]
    public void ReadDictionary_LoadsSpanishWelcomeMessage()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("Spanish");
        messages.ReadDictionary();
        string result = messages.GetMessage("welcome");
        Assert.AreEqual("Bienvenido al Juego de Elige Tu Aventura", result);
    }

    [TestMethod]
    public void ReadDictionary_LoadsFrenchWelcomeMessage()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("French");
        messages.ReadDictionary();
        string result = messages.GetMessage("welcome");
        Assert.AreEqual("Bienvenue dans Choisissez Votre Aventure", result);
    }

    [TestMethod]
    public void GetMessage_ReturnsFallbackForUnknownKey()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        string result = messages.GetMessage("unknown_key");
        Assert.AreEqual("[unknown_key]", result);
    }

    [TestMethod]
    public void GetMessage_ReturnsDifferentMessageAfterLanguageSwitch()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        string english = messages.GetMessage("welcome");

        messages.SetCurrentLanguage("Spanish");
        messages.ReadDictionary();
        string spanish = messages.GetMessage("welcome");

        Assert.AreNotEqual(english, spanish);
    }

    [TestMethod]
    public void ReadDictionary_LoadsGoodbyeMessageInEachLanguage()
    {
        Messages messages = new Messages();

        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.AreEqual("Adventure ends. Bye.", messages.GetMessage("goodbye"));

        messages.SetCurrentLanguage("Spanish");
        messages.ReadDictionary();
        Assert.AreEqual("Adios!", messages.GetMessage("goodbye"));

        messages.SetCurrentLanguage("French");
        messages.ReadDictionary();
        Assert.AreEqual("Au revoir!", messages.GetMessage("goodbye"));
    }

    [TestMethod]
    public void ReadDictionary_AllRequiredKeysExistInEnglish()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();

        string[] requiredKeys = { "welcome", "menu",
                                   "choose_path", "path_north", "path_south", "attack_prompt",
                                   "victory", "defeat", "retreat", "invalid", "goodbye" };

        foreach (string key in requiredKeys)
        {
            string result = messages.GetMessage(key);
            Assert.AreNotEqual($"[{key}]", result, $"Key '{key}' is missing from English dictionary.");
        }
    }

    [TestMethod]
    public void ReadDictionary_AllRequiredKeysExistInSpanish()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("Spanish");
        messages.ReadDictionary();

        string[] requiredKeys = { "welcome", "menu",
                                   "choose_path", "path_north", "path_south", "attack_prompt",
                                   "victory", "defeat", "retreat", "invalid", "goodbye" };

        foreach (string key in requiredKeys)
        {
            string result = messages.GetMessage(key);
            Assert.AreNotEqual($"[{key}]", result, $"Key '{key}' is missing from Spanish dictionary.");
        }
    }

    [TestMethod]
    public void ReadDictionary_AllRequiredKeysExistInFrench()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("French");
        messages.ReadDictionary();

        string[] requiredKeys = { "welcome", "menu",
                                   "choose_path", "path_north", "path_south", "attack_prompt",
                                   "victory", "defeat", "retreat", "invalid", "goodbye" };

        foreach (string key in requiredKeys)
        {
            string result = messages.GetMessage(key);
            Assert.AreNotEqual($"[{key}]", result, $"Key '{key}' is missing from French dictionary.");
        }
    }

    // --- IsValidRace ---

    [TestMethod]
    public void IsValidRace_ReturnsTrue_ForHuman()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.IsTrue(messages.IsValidRace("human"));
    }

    [TestMethod]
    public void IsValidRace_ReturnsTrue_CaseInsensitive()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.IsTrue(messages.IsValidRace("HUMAN"));
    }

    [TestMethod]
    public void IsValidRace_ReturnsFalse_ForInvalidRace()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.IsFalse(messages.IsValidRace("orc"));
    }

    [TestMethod]
    public void IsValidRace_ReturnsFalse_ForNull()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.IsFalse(messages.IsValidRace(null));
    }

    [TestMethod]
    public void NormalizeRace_ReturnsCanonical_ForHuman()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.AreEqual("Human", messages.NormalizeRace("human"));
    }

    [TestMethod]
    public void NormalizeRace_ReturnsCanonical_CaseInsensitive()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.AreEqual("Human", messages.NormalizeRace("HUMAN"));
    }

    // --- IsValidOccupation ---

    [TestMethod]
    public void IsValidOccupation_ReturnsTrue_ForFighter()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.IsTrue(messages.IsValidOccupation("fighter"));
    }

    [TestMethod]
    public void IsValidOccupation_ReturnsTrue_CaseInsensitive()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.IsTrue(messages.IsValidOccupation("FIGHTER"));
    }

    [TestMethod]
    public void IsValidOccupation_ReturnsFalse_ForInvalidOccupation()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.IsFalse(messages.IsValidOccupation("wizard"));
    }

    [TestMethod]
    public void IsValidOccupation_ReturnsFalse_ForNull()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.IsFalse(messages.IsValidOccupation(null));
    }

    [TestMethod]
    public void NormalizeOccupation_ReturnsCanonical_ForFighter()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.AreEqual("Fighter", messages.NormalizeOccupation("fighter"));
    }

    [TestMethod]
    public void NormalizeOccupation_ReturnsCanonical_CaseInsensitive()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.AreEqual("Fighter", messages.NormalizeOccupation("FIGHTER"));
    }

    // ----- Tests for the constructor and ReadDictionary fixes -----

    // Writes JSON to a temp file and returns its path, for tests that need a custom language file.
    private static string WriteTempLanguageFile(string json)
    {
        string path = Path.Combine(Path.GetTempPath(), $"cyoa_lang_{Guid.NewGuid():N}.json");
        File.WriteAllText(path, json);
        return path;
    }

    [TestMethod]
    public void TranslateRaceForDisplay_BeforeReadDictionary_ReturnsInputInsteadOfCrashing()
    {
        Messages messages = new Messages();
        Assert.AreEqual("Elf", messages.TranslateRaceForDisplay("Elf"));
    }

    [TestMethod]
    public void TranslateOccupationForDisplay_BeforeReadDictionary_ReturnsInputInsteadOfCrashing()
    {
        Messages messages = new Messages();
        Assert.AreEqual("Thief", messages.TranslateOccupationForDisplay("Thief"));
    }

    [TestMethod]
    public void TranslateWeaponForDisplay_BeforeReadDictionary_ReturnsInputInsteadOfCrashing()
    {
        Messages messages = new Messages();
        Assert.AreEqual("dagger", messages.TranslateWeaponForDisplay("dagger"));
    }

    [TestMethod]
    public void ReadDictionary_UnknownLanguage_FallsBackToEnglish()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("German");
        messages.ReadDictionary();
        Assert.AreEqual("English", messages.CurrentLanguage);
        Assert.AreEqual("Welcome to Choose Your Adventure Game", messages.GetMessage("welcome"));
    }

    [TestMethod]
    public void ReadDictionary_EmptyLanguage_FallsBackToEnglish()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("");
        messages.ReadDictionary();
        Assert.AreEqual("English", messages.CurrentLanguage);
    }

    [TestMethod]
    public void ReadDictionary_LanguageNameIgnoresCase()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("spanish");
        messages.ReadDictionary();
        Assert.AreEqual("Spanish", messages.CurrentLanguage);
        Assert.AreEqual("Bienvenido al Juego de Elige Tu Aventura", messages.GetMessage("welcome"));
    }

    [TestMethod]
    public void ReadDictionary_MissingFile_ThrowsFileNotFoundWithClearMessage()
    {
        string missingPath = Path.Combine(Path.GetTempPath(), $"does_not_exist_{Guid.NewGuid():N}.json");
        Messages messages = new Messages(missingPath);
        var ex = Assert.ThrowsException<FileNotFoundException>(() => messages.ReadDictionary());
        StringAssert.Contains(ex.Message, "language_data.json");
    }

    [TestMethod]
    public void ReadDictionary_NoMatchingLanguageAndNoEnglish_ThrowsInvalidData()
    {
        string path = WriteTempLanguageFile("{ \"Spanish\": { \"dictionary\": { \"welcome\": \"Hola\" } } }");
        try
        {
            Messages messages = new Messages(path);
            messages.SetCurrentLanguage("German");
            Assert.ThrowsException<InvalidDataException>(() => messages.ReadDictionary());
        }
        finally
        {
            File.Delete(path);
        }
    }

    [TestMethod]
    public void ReadDictionary_MissingTables_LoadsWhatExistsWithoutCrashing()
    {
        // Only "dictionary" is present; the five other tables are missing.
        string path = WriteTempLanguageFile("{ \"English\": { \"dictionary\": { \"welcome\": \"Hi\" } } }");
        try
        {
            Messages messages = new Messages(path);
            messages.ReadDictionary();
            Assert.AreEqual("Hi", messages.GetMessage("welcome"));
            Assert.IsFalse(messages.IsValidRace("elf"));
            Assert.IsFalse(messages.IsValidOccupation("thief"));
            Assert.AreEqual("claws", messages.TranslateWeaponForDisplay("claws"));
        }
        finally
        {
            File.Delete(path);
        }
    }

    [TestMethod]
    public void ReadDictionary_LoadsAllSixTables()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("Spanish");
        messages.ReadDictionary();
        Assert.AreNotEqual("[welcome]", messages.GetMessage("welcome"));          // dictionary
        Assert.IsTrue(messages.IsValidRace("elfo"));                               // raceMap
        Assert.IsTrue(messages.IsValidOccupation("mago"));                         // occupationMap
        Assert.AreNotEqual("Elf", messages.TranslateRaceForDisplay("Elf"));        // displayRaceMap
        Assert.AreNotEqual("Magician", messages.TranslateOccupationForDisplay("Magician")); // displayOccupationMap
        Assert.AreNotEqual("claws", messages.TranslateWeaponForDisplay("claws"));  // displayWeaponMap
    }

    [TestMethod]
    public void ReadDictionary_CalledAgainAfterLanguageChange_ReplacesOldText()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        messages.SetCurrentLanguage("French");
        messages.ReadDictionary();
        Assert.IsFalse(messages.IsValidRace("elf"));
        Assert.IsTrue(messages.IsValidRace("elfe"));
    }

    // ----- Italian -----

    [TestMethod]
    public void ReadDictionary_LoadsItalianWelcomeMessage()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("Italian");
        messages.ReadDictionary();
        Assert.AreEqual("Italian", messages.CurrentLanguage);
        Assert.AreEqual("Benvenuto nel Gioco Scegli la Tua Avventura", messages.GetMessage("welcome"));
    }

    [TestMethod]
    public void ReadDictionary_EveryLanguageHasEveryEnglishKey()
    {
        string jsonPath = Path.Combine(AppContext.BaseDirectory, "language_data.json");
        using var doc = System.Text.Json.JsonDocument.Parse(File.ReadAllText(jsonPath));
        var english = doc.RootElement.GetProperty("English").GetProperty("dictionary");

        foreach (string language in new[] { "Spanish", "French", "Italian" })
        {
            Messages messages = new Messages();
            messages.SetCurrentLanguage(language);
            messages.ReadDictionary();

            foreach (var entry in english.EnumerateObject())
                Assert.AreNotEqual($"[{entry.Name}]", messages.GetMessage(entry.Name),
                    $"Key '{entry.Name}' is missing from the {language} dictionary.");
        }
    }

    [TestMethod]
    public void IsValidRace_AcceptsItalianRaceNames()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("Italian");
        messages.ReadDictionary();
        foreach (string race in new[] { "umano", "Elfo", "NANO", "halfling" })
            Assert.IsTrue(messages.IsValidRace(race), $"'{race}' should be a valid Italian race.");
        Assert.IsFalse(messages.IsValidRace("human"));
    }

    [TestMethod]
    public void NormalizeRace_ItalianInput_ReturnsEnglishCanonicalName()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("Italian");
        messages.ReadDictionary();
        Assert.AreEqual("Dwarf", messages.NormalizeRace("Nano"));
    }

    [TestMethod]
    public void NormalizeOccupation_ItalianInput_ReturnsEnglishCanonicalName()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("Italian");
        messages.ReadDictionary();
        Assert.AreEqual("Fighter", messages.NormalizeOccupation("guerriero"));
        Assert.AreEqual("Magician", messages.NormalizeOccupation("Mago"));
        Assert.AreEqual("Thief", messages.NormalizeOccupation("LADRO"));
        Assert.AreEqual("Archer", messages.NormalizeOccupation("arciere"));
    }

    [TestMethod]
    public void TranslateForDisplay_ItalianShowsItalianNames()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("Italian");
        messages.ReadDictionary();
        Assert.AreEqual("Umano", messages.TranslateRaceForDisplay("Human"));
        Assert.AreEqual("Arciere", messages.TranslateOccupationForDisplay("Archer"));
        Assert.AreEqual("spada lunga", messages.TranslateWeaponForDisplay("long sword"));
        Assert.AreEqual("artigli", messages.TranslateWeaponForDisplay("claws"));
    }

    [TestMethod]
    public void GetMessage_ItalianFormatStringsKeepPlaceholders()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("Italian");
        messages.ReadDictionary();
        string result = string.Format(messages.GetMessage("dragon_attack_intro"), "Smolderfang", "Marco", "artigli");
        Assert.AreEqual("Smolderfang attacca Marco con artigli!", result);
    }
}
