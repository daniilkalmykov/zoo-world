using Bootstrap.Roots;
using UnityEngine;
using VContainer.Unity;

namespace Bootstrap
{
    internal sealed class GameSceneEntryPoint : IStartable
    {
        private readonly Camera _camera;
        private readonly GameUIRoot _ui;

        public GameSceneEntryPoint(GameUIRoot ui, GameWorldRoot world)
        {
            _camera = world.Camera;
            _ui = ui;
        }

        public void Start()
        {
            _ui.Initialize(_camera);
        }
    }
}