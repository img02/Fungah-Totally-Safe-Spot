using Dalamud.Game.ClientState;
using Dalamud.Game.Gui;
using Dalamud.Interface;
using ImGuiNET;
using System;
using System.Numerics;

namespace SamplePlugin
{
    // It is good to have this be disposable in general, in case you ever need it
    // to do any cleanup
    public unsafe class PluginUI : IDisposable
    {
        private readonly ClientState _clientState;
        private readonly GameGui _gameGui;

        // south of stage : <70.78049, -4.472919, -21.072674>
        // east of stage  : <85.45761, -4.4729047, -36.12376>
        // north of stage : <70.41719, -4.4729795, -50.762985>
        // west of stage  : <55.68961, -4.4729276, -35.709606>
        // 'Safe' spot    : <66.96, -4.48, -24.69> 

        private const float StageNorth = -50.76f;
        private const float StageSouth = -21f;
        private const float StageEast = 85.45f;
        private const float StageWest = 55.6f;
        private readonly Vector3 _safeSpot = new Vector3(66.96f, -4.48f, -24.69f);
        private readonly uint _goldSaucerMapID = 144;
        private readonly uint _red = ImGui.GetColorU32(ImGui.ColorConvertFloat4ToU32(new Vector4(1, 0, 0, 1f)));


        public PluginUI(ClientState clientState, GameGui gameGui)
        {
            _clientState = clientState;
            _gameGui = gameGui;
        }


        public void Dispose()
        {
        }

        public void Draw()
        {
            if (PlayerOnStage()) DrawCircleWindow();
        }

        private void DrawCircleWindow()
        {
            Vector2 circlePos;
            _gameGui.WorldToScreen(_safeSpot, out circlePos);

            var winPos = new Vector2(circlePos.X - 15, circlePos.Y - 15);

            ImGuiHelpers.SetNextWindowPosRelativeMainViewport(winPos);
            ImGui.SetNextWindowSize(new Vector2(5, 5));
            if (ImGui.Begin("Pointer", ImGuiWindowFlags.NoBackground | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoResize))
            {
                DrawCircle(circlePos);
                ImGui.End();
            }
        }

        private void DrawCircle(Vector2 pos) => ImGui.GetWindowDrawList().AddCircleFilled(pos, 5, _red);

        private bool PlayerAtGoldSaucer() => _clientState.TerritoryType == _goldSaucerMapID;

        private bool PlayerOnStage()
        {
            if (_clientState.LocalPlayer == null || !PlayerAtGoldSaucer()) return false;
            var pos = _clientState.LocalPlayer.Position;
            return ((pos.X is > StageWest and < StageEast) && (pos.Z is < StageSouth and > StageNorth));
        }
    }
}

