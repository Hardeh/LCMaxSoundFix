using BepInEx;
using UnityEngine;

namespace LCMaxSoundFix
{
    [BepInPlugin("." + PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
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
            AudioSettings.Reset(audioSettings);

            Logger.LogInfo($"Plugin LCMaxSoundsFix is loaded, new audio settings are applied.");

        }
    }
}
