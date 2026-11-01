using System.Threading;
using Cysharp.Threading.Tasks;

namespace Infrastructure.SceneManagement
{
    public interface ISceneLoader
    {
        public UniTask LoadAsync(string sceneName, CancellationToken cancellationToken = default);

        public UniTask UnloadAsync(string sceneName, CancellationToken cancellationToken = default);

        public UniTask SwitchAsync(string sceneName, CancellationToken cancellationToken = default);
    }
}