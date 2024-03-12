using BepInEx;
using BepInEx.Logging;

namespace LCMaxSoundFix
{
    [BepInPlugin("." + PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    internal class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger { get; private set; } = null;

        protected Plugin() : base()
        {
            Logger = base.Logger;
        }

        protected void Awake()
        {
            Logger?.LogInfo($"Plugin {PluginInfo.PLUGIN_NAME} is loaded!");

            Logger?.LogDebug("Binding audio config...");
            AudioConfig.Bind(this);
        }
    }
}