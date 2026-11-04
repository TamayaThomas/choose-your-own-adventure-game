using System.Text.Json;

public class Messages
{
    public const string DefaultLanguage = "English";

    public string CurrentLanguage { get; set; }
    private readonly string _jsonPath;
    private Dictionary<string, string> _dictionary;
    private Dictionary<string, string> _raceMap;
    private Dictionary<string, string> _occupationMap;
    private Dictionary<string, string> _displayRaceMap;
    private Dictionary<string, string> _displayOccupationMap;
    private Dictionary<string, string> _displayWeaponMap;

    public Messages()
        : this(Path.Combine(AppContext.BaseDirectory, "language_data.json"))
    {
    }

    // Lets tests point at a different language file.
    public Messages(string jsonPath)
    {
        _jsonPath = jsonPath;
        CurrentLanguage = DefaultLanguage;
        _dictionary = new Dictionary<string, string>();
        _raceMap = new Dictionary<string, string>();
        _occupationMap = new Dictionary<string, string>();
        _displayRaceMap = new Dictionary<string, string>();
        _displayOccupationMap = new Dictionary<string, string>();
        _displayWeaponMap = new Dictionary<string, string>();
    }

    public void SetCurrentLanguage(string language)
    {
        CurrentLanguage = language;
    }

    public void ReadDictionary()
    {
        if (!File.Exists(_jsonPath))
            throw new FileNotFoundException(
                $"Language file not found. Make sure language_data.json is copied to the output folder.", _jsonPath);

        string jsonText = File.ReadAllText(_jsonPath);
        using JsonDocument doc = JsonDocument.Parse(jsonText);

        JsonElement langSection = FindLanguageSection(doc.RootElement);

        _dictionary = ReadSection(langSection, "dictionary");
        _raceMap = ReadSection(langSection, "raceMap");
        _occupationMap = ReadSection(langSection, "occupationMap");
        _displayRaceMap = ReadSection(langSection, "displayRaceMap");
        _displayOccupationMap = ReadSection(langSection, "displayOccupationMap");
        _displayWeaponMap = ReadSection(langSection, "displayWeaponMap");
    }

    // Finds the section for CurrentLanguage (ignoring case). If that language
    // is not in the file, falls back to English and updates CurrentLanguage.
    private JsonElement FindLanguageSection(JsonElement root)
    {
        foreach (JsonProperty language in root.EnumerateObject())
        {
            if (string.Equals(language.Name, CurrentLanguage, StringComparison.OrdinalIgnoreCase))
            {
                CurrentLanguage = language.Name;
                return language.Value;
            }
        }

        if (root.TryGetProperty(DefaultLanguage, out JsonElement fallback))
        {
            CurrentLanguage = DefaultLanguage;
            return fallback;
        }

        throw new InvalidDataException(
            $"Language file has no '{CurrentLanguage}' section and no '{DefaultLanguage}' section to fall back to.");
    }

    // Reads one table from a language section. A missing table becomes empty.
    private static Dictionary<string, string> ReadSection(JsonElement langSection, string sectionName)
    {
        var result = new Dictionary<string, string>();

        if (!langSection.TryGetProperty(sectionName, out JsonElement section))
            return result;

        foreach (JsonProperty entry in section.EnumerateObject())
            result[entry.Name] = entry.Value.GetString() ?? string.Empty;

        return result;
    }

    public string GetMessage(string key)
    {
        return _dictionary.TryGetValue(key, out string? message) ? message : $"[{key}]";
    }

    public bool IsValidRace(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;
        return _raceMap.ContainsKey(input.Trim().ToLower());
    }

    public string NormalizeRace(string input)
    {
        return _raceMap.TryGetValue(input.Trim().ToLower(), out string? canonical) ? canonical : input;
    }

    public bool IsValidOccupation(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;
        return _occupationMap.ContainsKey(input.Trim().ToLower());
    }

    public string NormalizeOccupation(string input)
    {
        return _occupationMap.TryGetValue(input.Trim().ToLower(), out string? canonical) ? canonical : input;
    }

    public string TranslateRaceForDisplay(string race)
    {
        return _displayRaceMap.TryGetValue(race, out string? translated) ? translated : race;
    }

    public string TranslateOccupationForDisplay(string occupation)
    {
        return _displayOccupationMap.TryGetValue(occupation, out string? translated) ? translated : occupation;
    }

    public string TranslateWeaponForDisplay(string weaponType)
    {
        return _displayWeaponMap.TryGetValue(weaponType, out string? translated) ? translated : weaponType;
    }
}
