using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Objects;
using Dalamud.Game.Gui;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Fungah;

namespace SamplePlugin
{
    public sealed class Plugin : IDalamudPlugin
    {
        public string Name => "Fungah";

        private IDalamudPluginInterface PluginInterface { get; init; }
        private PluginUI PluginUi { get; init; }

        public Plugin(
            IDalamudPluginInterface pluginInterface,
            IClientState clientState,
            IGameGui gameGui,
            IObjectTable objectTable,
            IPluginLog pluginLog)
        {
            this.PluginUi = new PluginUI(clientState, gameGui, objectTable);
            this.PluginInterface = pluginInterface;
            this.PluginInterface.UiBuilder.Draw += DrawUI;
            PluginLog.Logger = pluginLog;
        }

        public void Dispose()
        {
            this.PluginUi.Dispose();
        }

        private void DrawUI()
        {
            this.PluginUi.Draw();
        }

    }
}
