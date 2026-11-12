public class Game
{
    public void StartGame()
    {
        // Title screen
        Console.WriteLine("=====================================");
        Console.WriteLine("Welcome to Choose Your Adventure Game");
        Console.WriteLine("=====================================\n");

        // Instantiate Die first (per sequence diagram)
        Die die = new Die();

        // Language selection shown before Messages dictionary is loaded
        Console.WriteLine("Select language / Selecciona idioma / Choisissez la langue:");
        Console.WriteLine("1. English  2. Espanol  3. Francais");
        Console.Write("Enter choice: ");
        string? langInput = Console.ReadLine()?.Trim();
        string selectedLanguage = langInput switch { "2" => "Spanish", "3" => "French", _ => "English" };

        // Initialize Messages with selected language (per sequence diagram)

        Messages messages = new Messages();
        messages.SetCurrentLanguage(selectedLanguage);
        messages.ReadDictionary();
        // Display welcome message
        Console.WriteLine();
        Console.WriteLine(messages.GetMessage("welcome"));
        Console.WriteLine();

        // Main menu loop
        bool running = true;
        while (running)
        {
            Console.WriteLine(messages.GetMessage("menu"));
            Console.Write(messages.GetMessage("enter_choice"));
            string? choice = Console.ReadLine()?.Trim();

            if (choice == "1")
            {
                // Chosen before the Player exists, and fixed for the adventure.
                Difficulty difficulty = PromptForDifficulty(messages);

                Player player = new Player();
                player.CreateCharacter(messages);

                // Once per adventure, so Sold Out lasts across repeat trips south.
                Merchant merchant = new Merchant(player);

                // Once per adventure, so the same Dragon is met however many trips south come first.
                Dragon dragon = new Dragon(die, difficulty);

                bool inAdventureMenu = true;
                while (inAdventureMenu)
                {
                    string selectedPath = PromptForPath(messages);

                    if (selectedPath == "exit")
                    {
                        Console.WriteLine(messages.GetMessage("adventure_ends"));
                        running = false;
                        inAdventureMenu = false;
                    }
                    else if (selectedPath == "south")
                    {
                        HandleMerchantEncounter(player, merchant, messages);
                    }
                    else if (selectedPath == "north")
                    {
                        HandleDragonEncounter(player, dragon, messages);

                        running = false;
                        inAdventureMenu = false;
                    }
                }
            }
            else if (choice == "2")
            {
                Console.WriteLine(messages.GetMessage("goodbye"));
                running = false;
            }
            else
            {
                Console.WriteLine(messages.GetMessage("invalid"));
            }
        }
    }

    public void EndGame(bool playerWon, bool playerRetreated, Messages messages)
    {
        if (playerRetreated)
            Console.WriteLine(messages.GetMessage("retreat"));
        else if (playerWon)
            Console.WriteLine(messages.GetMessage("victory"));
        else
            Console.WriteLine(messages.GetMessage("defeat"));
    }

    public bool IsValidMenuChoice(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;
        string trimmed = input.Trim();
        return trimmed == "1" || trimmed == "2";
    }

    public bool IsValidPathChoice(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;
        string trimmed = input.Trim().ToLower();
        return trimmed == "n" || trimmed == "north" ||
               trimmed == "s" || trimmed == "south" ||
               trimmed == "e" || trimmed == "exit";
    }

    public bool IsValidDifficultyChoice(string? input)
    {
        return ParseDifficultyChoice(input) != null;
    }

    // The numbers are the same in every language; only the prompt is translated.
    private static Difficulty? ParseDifficultyChoice(string? input) => input?.Trim() switch
    {
        "1" => Difficulty.Easy,
        "2" => Difficulty.Normal,
        "3" => Difficulty.Hard,
        _ => null
    };

    public bool IsValidCombatChoice(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;
        string trimmed = input.Trim().ToLower();
        return trimmed == "a" || trimmed == "attack" ||
               trimmed == "r" || trimmed == "retreat" ||
               trimmed == "d" || trimmed == "drink";
    }

    private Difficulty PromptForDifficulty(Messages messages)
    {
        while (true)
        {
            Console.WriteLine(messages.GetMessage("difficulty_prompt"));
            Console.Write(messages.GetMessage("enter_choice"));
            Difficulty? choice = ParseDifficultyChoice(Console.ReadLine());

            if (choice is Difficulty difficulty)
            {
                Console.WriteLine(string.Format(messages.GetMessage("difficulty_chosen"),
                    messages.TranslateDifficultyForDisplay(difficulty)));
                return difficulty;
            }

            Console.WriteLine(messages.GetMessage("invalid"));
        }
    }

    private string PromptForPath(Messages messages)
    {
        while (true)
        {
            Console.WriteLine(messages.GetMessage("path_prompt_full"));
            Console.Write(messages.GetMessage("enter_choice"));
            string? input = Console.ReadLine()?.Trim().ToLower();

            if (input == "north" || input == "n")
                return "north";

            if (input == "south" || input == "s")
                return "south";

            if (input == "exit" || input == "e")
                return "exit";

            Console.WriteLine(messages.GetMessage("path_invalid"));
        }
    }

    internal void HandleMerchantEncounter(Player player, Merchant merchant, Messages messages)
    {
        Console.WriteLine(messages.GetMessage("merchant_narrative"));

        while (true)
        {
            Console.WriteLine(string.Format(messages.GetMessage("merchant_gold"), player.Gold));

            // The menu is numbered here, not in the translations, so it always matches the Offers.
            IReadOnlyList<Offer> offers = merchant.Offers;
            for (int i = 0; i < offers.Count; i++)
                Console.WriteLine($"{i + 1}. {FormatOfferLine(offers[i], player, messages)}");
            int leaveChoice = offers.Count + 1;
            Console.WriteLine($"{leaveChoice}. {messages.GetMessage("merchant_menu_leave")}");

            Console.Write(messages.GetMessage("enter_choice"));
            string? input = Console.ReadLine()?.Trim();

            // Only the exact menu numbers count, as on every other screen ("01" or "+1" is invalid).
            int choice = Enumerable.Range(1, leaveChoice).FirstOrDefault(n => input == n.ToString());

            if (choice == 0)
            {
                Console.WriteLine(messages.GetMessage("invalid"));
            }
            else if (choice == leaveChoice)
            {
                Console.WriteLine(messages.GetMessage("merchant_leave"));
                return;
            }
            else
            {
                MerchantItem item = offers[choice - 1].Item;
                PurchaseResult result = merchant.Buy(item);
                if (result == PurchaseResult.Purchased)
                    Console.WriteLine(ItemText(item, player, messages).PurchasedMessage());
                else
                    PrintPurchaseRefusal(result, messages);
            }
        }
    }

    private static string FormatOfferLine(Offer offer, Player player, Messages messages)
    {
        string menuKey = ItemText(offer.Item, player, messages).MenuKey;
        string line = string.Format(messages.GetMessage(menuKey), offer.Amount, offer.Price);
        if (offer.IsSoldOut)
            line += messages.GetMessage("merchant_sold_out_tag");
        return line;
    }

    /// <summary>
    /// The one place that maps an item to its text: its menu-line key and its purchased message,
    /// built only after the purchase so it shows the Player's new gear.
    /// </summary>
    private static (string MenuKey, Func<string> PurchasedMessage) ItemText(
        MerchantItem item, Player player, Messages messages) => item switch
    {
        MerchantItem.MagicalArmor => ("merchant_menu_armor",
            () => string.Format(messages.GetMessage("merchant_armor_purchased"), player.Protection)),
        MerchantItem.MagicalWeapon => ("merchant_menu_weapon",
            () => string.Format(messages.GetMessage("merchant_weapon_purchased"),
                messages.TranslateWeaponForDisplay(player.Weapon?.Type ?? ""))),
        MerchantItem.HealingPotion => ("merchant_menu_potion",
            () => messages.GetMessage("merchant_potion_purchased")),
        _ => throw new ArgumentOutOfRangeException(nameof(item), item, "Not an item the Merchant sells."),
    };

    private static void PrintPurchaseRefusal(PurchaseResult result, Messages messages)
    {
        if (result == PurchaseResult.NotEnoughGold)
            Console.WriteLine(messages.GetMessage("merchant_not_enough_gold"));
        else if (result == PurchaseResult.SoldOut)
            Console.WriteLine(messages.GetMessage("merchant_sold_out"));
    }

    private void HandleDragonEncounter(Player player, Dragon dragon, Messages messages)
    {
        Console.WriteLine(messages.GetMessage("north_path_narrative"));

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(string.Format(messages.GetMessage("dragon_intro"), dragon.Name));
        Console.ResetColor();

        Console.WriteLine(messages.GetMessage("dragon_stats_intro"));
        dragon.DisplayStats(messages);
        while (true)
        {
            Console.WriteLine(messages.GetMessage("dragon_encounter_prompt"));
            Console.Write(messages.GetMessage("enter_choice"));
            string? input = Console.ReadLine()?.Trim().ToLower();

            if (input == "a" || input == "attack")
            {
                Combat combat = new Combat(player, dragon, messages);
                bool playerWon = combat.StartCombat();

                if (combat.PlayerRetreated)
                {
                    return; //combat already handled retreat message
                }
                else
                {
                   EndGame(playerWon, false, messages); 
                }
                
                return;
            }

        if (input == "r" || input == "retreat")
        {
            Console.WriteLine(messages.GetMessage("retreat"));
            return;
        }

        Console.WriteLine(messages.GetMessage("invalid"));
    }
}
}
