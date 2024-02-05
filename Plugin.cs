using BepInEx;
using UnityEngine;

namespace LCMaxSoundFix
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            // Plugin startup logic

            var audioSettings = AudioSettings.GetConfiguration();
            // default - 40
            audioSettings.numRealVoices = 128;
            // default - 512
            audioSettings.numVirtualVoices = 1024;
            AudioSettings.SetConfiguration(audioSettings);

            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded, new audio settings are applied.");

        }
    }
}
