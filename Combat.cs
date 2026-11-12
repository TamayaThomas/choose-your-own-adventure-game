public class Combat
{
    private enum CombatChoice { Attack, Drink, Retreat }

    public Player player { get; set; }
    public Dragon dragon { get; set; }
    public Messages messages { get; set; }

    public bool PlayerRetreated { get; private set; }

    public Combat(Player player, Dragon dragon, Messages messages)
    {
        this.player = player;
        this.dragon = dragon;
        this.messages = messages;
        PlayerRetreated = false;
    }

    public bool StartCombat()
    {
        Console.WriteLine(GetCombatStatsDisplay());

        // Round order: Player action, Dragon attack, prompt. Drinking replaces the attack as the action.
        bool playerAttacks = true;

        while (true)
        {
            if (playerAttacks)
            {
                PlayerAttacksDragonSequence();

                if (dragon.HealthPoints <= 0)
                    return true;
            }

            DragonAttacksPlayerSequence();

            if (player.HealthPoints <= 0)
                return false;

            CombatChoice choice = PromptForCombatChoice();

            if (choice == CombatChoice.Retreat)
            {
                PlayerRetreated = true;
                Console.WriteLine(messages.GetMessage("retreat_combat"));
                return false;
            }

            playerAttacks = choice == CombatChoice.Attack;
        }
    }

    /// <summary>
    /// Asks for the next action until one uses the Player's turn or ends combat.
    /// Drink is returned only after a successful drink; a refused drink asks again.
    /// </summary>
    private CombatChoice PromptForCombatChoice()
    {
        while (true)
        {
            Console.WriteLine(messages.GetMessage("attack_prompt_with_drink"));
            string? input = Console.ReadLine()?.Trim().ToLower();

            if (input == "r" || input == "retreat")
                return CombatChoice.Retreat;

            if (input == "d" || input == "drink")
            {
                if (DrinkHealingPotionSequence())
                    return CombatChoice.Drink;
                continue;
            }

            if (input == "a" || input == "attack")
                return CombatChoice.Attack;

            // As before this feature: invalid input is reported and the Player attacks.
            Console.WriteLine(messages.GetMessage("invalid"));
            return CombatChoice.Attack;
        }
    }

    /// <summary>Drinks the Healing Potion and prints the result. Returns true if the Player's turn was used.</summary>
    private bool DrinkHealingPotionSequence()
    {
        DrinkResult result = player.DrinkHealingPotion();

        switch (result.Outcome)
        {
            case DrinkOutcome.Healed:
                Console.WriteLine(string.Format(messages.GetMessage("potion_healed"),
                    result.HealthRestored, player.HealthPoints, player.MaxHealth));
                return true;
            case DrinkOutcome.NoPotion:
                Console.WriteLine(messages.GetMessage("potion_none"));
                return false;
            default:
                Console.WriteLine(messages.GetMessage("potion_already_at_max_health"));
                return false;
        }
    }

    public string GetCombatStatsDisplay()
    {
        var output = new List<string>();

        output.Add(messages.GetMessage("combat_stats_header"));
        output.Add(messages.GetMessage("combat_player_header"));
        output.Add(messages.GetMessage("stats_name") + player.Name);
        output.Add(messages.GetMessage("stats_strength") + player.Strength);
        output.Add(messages.GetMessage("stats_agility") + player.Agility);
        output.Add(messages.GetMessage("stats_health") + player.HealthPoints);
        output.Add(messages.GetMessage("stats_protection") + player.Protection);
        output.Add(messages.GetMessage("stats_healing_potions") + player.HealingPotions);

        if (player.Weapon != null)
        {
            output.Add(messages.GetMessage("stats_weapon") + messages.TranslateWeaponForDisplay(player.Weapon.Type));
            output.Add(messages.GetMessage("stats_damage") + player.Weapon.MaxDamage);
        }

        output.Add("");
        output.Add(messages.GetMessage("combat_dragon_header"));
        output.Add(messages.GetMessage("stats_name") + dragon.Name);
        output.Add(messages.GetMessage("stats_strength") + dragon.Strength);
        output.Add(messages.GetMessage("stats_agility") + dragon.Agility);
        output.Add(messages.GetMessage("stats_health") + dragon.HealthPoints);
        output.Add(messages.GetMessage("stats_weapon") + messages.TranslateWeaponForDisplay(dragon.Weapon.Type));
        output.Add(messages.GetMessage("stats_damage") + dragon.Weapon.MaxDamage);

        return string.Join(Environment.NewLine, output);
    }

    public void PlayerAttacksDragonSequence()
    {
        Console.WriteLine(player.Attack(dragon, messages));
    }

    public void DragonAttacksPlayerSequence()
    {
        Console.WriteLine(dragon.Attack(player, messages));
    }
}