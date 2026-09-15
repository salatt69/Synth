using ProjectSynth.Character.Synth.Achievements;
using ProjectSynth.Modules;

namespace ProjectSynth.Character.Synth.Content
{
    public static class SynthTokens
    {
        public static void Init()
        {
            AddSynthTokens();

            ////uncomment this to spit out a lanuage file with all the above tokens that people can translate
            ////make sure you set Language.usingLanguageFolder and printingEnabled to true
            //Language.PrintOutput("Henry.txt");
            ////refer to guide on how to build and distribute your mod with the proper folders
        }

        public static void AddSynthTokens()
        {
            string prefix = SynthSurvivor.SYNTH_PREFIX;

            string name = "Synth";

            string desc = "Pop pipop pipop pop pip poo\r\nPop pipop pipop pop pip poo\r\nPop pipop pipop pop pip poo\r\nPop pipop pipop pop pip poo\r\n"
                + "Pop pipop pipop pop pip poo\r\nPop pipop pipop pop pip poo\r\nPop pipop pipop pop pip poo\r\nPop pipop pipop pop pip poo";

            string subtitle = "Pop pipop pipop pop pip poo\r\nPop pipop pipop pop pip poo\r\nPop pipop pipop pop pip poo\r\nPop pipop pipop pop pip poo\r\n"
                + "Pop pipop pipop pop pip poo\r\nPop pipop pipop pop pip poo\r\nPop pipop pipop pop pip poo\r\nPop pipop pipop pop pip poo";

            string outro = "..the world is hers or whatever.";
            string outroFailure = "..sadly, she was replaced by AI.";

            string lore = "later to be added...";

            Language.Add(prefix + "NAME", name);
            Language.Add(prefix + "DESCRIPTION", desc);
            Language.Add(prefix + "SUBTITLE", subtitle);
            Language.Add(prefix + "LORE", lore);
            Language.Add(prefix + "OUTRO_FLAVOR", outro);
            Language.Add(prefix + "OUTRO_FAILURE", outroFailure);

            #region Skins
            Language.Add(prefix + "MASTERY_SKIN_NAME", "Alternate");
            #endregion

            #region Passive
            string metroDesc = "fih";
            Language.Add(prefix + "PASSIVE_METRO_NAME", "M1K-U");
            Language.Add(prefix + "PASSIVE_METRO_DESCRIPTION", metroDesc);

            string anotherDesc = "tf you looking at";
            Language.Add(prefix + "PASSIVE_ANOTHER_NAME", "M1K-U v2.0");
            Language.Add(prefix + "PASSIVE_ANOTHER_DESCRIPTION", anotherDesc);
            #endregion

            #region Primary
            string tnmDesc = "?";
            Language.Add(prefix + "PRIMARY_THIRTY_NINE_MUSIC_NAME", "39 Music!");
            Language.Add(prefix + "PRIMARY_THIRTY_NINE_MUSIC_DESCRIPTION", tnmDesc);
            #endregion

            #region Secondary
            string divaDesc = "idk";
            Language.Add(prefix + "SECONDARY_VIRTUAL_DEVIATION_NAME", "Virtual Deviation");
            Language.Add(prefix + "SECONDARY_VIRTUAL_DEVIATION_DESCRIPTION", divaDesc);
            #endregion

            #region Utility
            string rollingGirlDesc = "rolling rolling rolling";
            Language.Add(prefix + "UTILITY_ROLLING_GIRL_NAME", "Rolling Girl");
            Language.Add(prefix + "UTILITY_ROLLING_GIRL_DESCRIPTION", rollingGirlDesc);

            string pumpedUpDesc = "can't be bothered to explain what it does, sorry";
            Language.Add(prefix + "UTILITY_PUMPED_UP_NAME", "PumpedUp");
            Language.Add(prefix + "UTILITY_PUMPED_UP_DESCRIPTION", pumpedUpDesc);
            #endregion

            #region Special
            string mikuBeamDesc = "beaming";
            Language.Add(prefix + "SPECIAL_MIKU_BEAM_NAME", "Miku Miku Beam!");
            Language.Add(prefix + "SPECIAL_MIKU_BEAM_DESCRIPTION", mikuBeamDesc);
            #endregion

            #region Achievements
            Language.Add(Tokens.GetAchievementNameToken(SynthMasteryAchievement.identifier), "Synth: Mastery");
            Language.Add(Tokens.GetAchievementDescriptionToken(SynthMasteryAchievement.identifier), "As Synth, beat the game or obliterate on Monsoon.");
            #endregion

            #region Keywords
            string followTheRhythmDesc = "Allows the skill to be shaped in a special way when used in sync with the rhythm of M1K-U technology.";
            Language.Add(prefix + "KEYWORD_FOLLOW_THE_RHYTHM", Tokens.KeywordText("Follow The Rhythm (rename maybe?)", followTheRhythmDesc));
            #endregion
        }
    }
}
