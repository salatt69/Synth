using BepInEx.Configuration;
using ProjectSynth.Mod;
using RiskOfOptions;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ProjectSynth.Modules
{
    public static class Config
    {
        public static ConfigFile MyConfig = SynthPlugin.instance.Config;

        /// <summary>
        /// Very specific function for very specific testing purposes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="section"></param>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <param name="defaultValue"></param>
        /// <param name="onChanged"></param>
        /// <returns></returns>
        public static ConfigEntry<T> BindAndReasignPassedValueOnChange<T>(string section, string name, string desc, T defaultValue, Action<T> onChanged)
        {
            var entry = Config.BindOption(section, name, desc, defaultValue);
            onChanged(entry.Value);
            entry.SettingChanged += (sender, args) =>
            {
                Log.Warning($"[{section}/{name}] SettingChanged fired, new value = {entry.Value}");
                onChanged(entry.Value);
            };
            return entry;
        }

        private static ConfigEntry<T> BindOption<T>(string section, string name, string description, T defaultValue, float min = 0, float max = 0, bool restartRequired = false)
        {
            if (string.IsNullOrEmpty(description))
            {
                description = name;
            }

            if (restartRequired)
            {
                description += " (restart required)";
            }
            ConfigEntry<T> configEntry = MyConfig.Bind(section, name, defaultValue, description);

            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.rune580.riskofoptions"))
            {
                TryRegisterOption(configEntry, min, max, restartRequired);
            }

            return configEntry;
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static void TryRegisterOption<T>(ConfigEntry<T> entry, float min, float max, bool restartRequired)
        {
            bool isSlider = min != max;

            if (entry is ConfigEntry<float>)
            {
                if (isSlider)
                {
                    ModSettingsManager.AddOption(new SliderOption(entry as ConfigEntry<float>, new SliderConfig() { min = min, max = max, FormatString = "{0:0.00}", restartRequired = restartRequired }));
                }
                else
                {
                    ModSettingsManager.AddOption(new FloatFieldOption(entry as ConfigEntry<float>));
                }
            }
            if (entry is ConfigEntry<int>)
            {
                if (isSlider)
                {
                    ModSettingsManager.AddOption(new IntSliderOption(entry as ConfigEntry<int>, new IntSliderConfig() { min = (int)min, max = (int)max, restartRequired = restartRequired }));
                }
                else
                {
                    ModSettingsManager.AddOption(new IntFieldOption(entry as ConfigEntry<int>));
                }
            }
            if (entry is ConfigEntry<bool>)
            {
                ModSettingsManager.AddOption(new CheckBoxOption(entry as ConfigEntry<bool>, restartRequired));
            }
            if (entry is BepInEx.Configuration.ConfigEntry<KeyboardShortcut>)
            {
                ModSettingsManager.AddOption(new KeyBindOption(entry as ConfigEntry<KeyboardShortcut>, restartRequired));
            }
        }

        //Taken from https://github.com/ToastedOven/CustomEmotesAPI/blob/main/CustomEmotesAPI/CustomEmotesAPI/CustomEmotesAPI.cs
        public static bool GetKeyPressed(KeyboardShortcut entry)
        {
            foreach (var item in entry.Modifiers)
            {
                if (!Input.GetKey(item))
                {
                    return false;
                }
            }
            return Input.GetKeyDown(entry.MainKey);
        }
    }
}
