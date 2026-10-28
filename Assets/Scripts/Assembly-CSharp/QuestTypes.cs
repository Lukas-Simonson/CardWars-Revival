public class QuestTypes
{
	public const string Main = "main";

	public const string FCDemo = "fc_demo";

	public const string FC = "fc";

	public const string Multiplayer = "deck_wars";

	public const string Dungeon = "dungeon";

	public const string TreasureChestCat = "tcat";

	public const string ElFisto = "elfisto";

	// Quests that follow the story rewards: a gem for every star earned, and better drops once all three stars are held.
	public static bool UsesStoryRewards(string questType)
	{
		return questType == "main" || questType == "fc";
	}

	public static bool AutoUnlockQuests(string questType)
	{
		if (questType != "tcat" && questType != "elfisto")
		{
			return true;
		}
		return false;
	}
}
