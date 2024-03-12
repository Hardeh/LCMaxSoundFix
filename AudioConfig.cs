using BepInEx.Configuration;
using BepInEx;
using System;
using UnityEngine;

namespace LCMaxSoundFix
{
    internal static class AudioConfig
    {
        private static ConfigEntry<int> _numRealVoices = null;
        private static ConfigEntry<int> _numVirtualVoices = null;

        private static readonly int[] _validNumVoices =
        {
            1, 2, 4, 8, 16, 32, 50, 64, 100, 128, 256, 512, 1024
        };

        public static void Bind(BaseUnityPlugin plugin)
        {
            if (plugin is null)
            {
                throw new ArgumentNullException(nameof(plugin));
            }

            Unbind();

            _numRealVoices = plugin.Config.Bind("Audio Settings", "Real Voices", 128, new ConfigDescription("The current maximum number of simultaneously audible sounds in the game. Game default: 40", new AcceptableValueListSorted<int>(_validNumVoices)));
            _numVirtualVoices = plugin.Config.Bind("Audio Settings", "Virtual Voices", 1024, new ConfigDescription("The maximum number of managed sounds in the game. Beyond this limit sounds will simply stop playing. Game default: 512", new AcceptableValueListSorted<int>(_validNumVoices)));

            _numRealVoices.SettingChanged += OnSettingChanged;
            _numVirtualVoices.SettingChanged += OnSettingChanged;

            ApplyChanges();
        }

        public static void Unbind()
        {
            if (_numRealVoices != null)
            {
                _numRealVoices.SettingChanged -= OnSettingChanged;
                _numRealVoices.ConfigFile.Remove(_numRealVoices.Definition);
                _numRealVoices = null;
            }

            if (_numVirtualVoices != null)
            {
                _numVirtualVoices.SettingChanged -= OnSettingChanged;
                _numVirtualVoices.ConfigFile.Remove(_numVirtualVoices.Definition);
                _numVirtualVoices = null;
            }
        }

        private static void OnSettingChanged(object sender, EventArgs e)
        {
            ApplyChanges();
        }

        private static void ApplyChanges()
        {
            Plugin.Logger?.LogDebug("Applying audio settings...");
            AudioConfiguration audioConfig = AudioSettings.GetConfiguration();
            audioConfig.numRealVoices = _numRealVoices.Value;
            audioConfig.numVirtualVoices = _numVirtualVoices.Value;
            AudioSettings.Reset(audioConfig);
            Plugin.Logger?.LogInfo($"Audio settings (numRealVoices={_numRealVoices.Value}; numVirtualVoices={_numVirtualVoices.Value}) are applied.");
        }
    }
}