using Dalamud.Game.ClientState;
using Dalamud.Game.Gui;
using Dalamud.IoC;
using Dalamud.Plugin;

namespace SamplePlugin
{
    public sealed class Plugin : IDalamudPlugin
    {
        public string Name => "Fungah";

        private DalamudPluginInterface PluginInterface { get; init; }
        private PluginUI PluginUi { get; init; }

        public Plugin(
            [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
            ClientState clientState,
            GameGui gameGui)
        {
            this.PluginUi = new PluginUI(clientState, gameGui);
            this.PluginInterface = pluginInterface;
            this.PluginInterface.UiBuilder.Draw += DrawUI;
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
