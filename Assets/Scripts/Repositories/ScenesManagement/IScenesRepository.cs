using VContainer.Unity;

namespace Repositories.ScenesManagement
{
    public interface IScenesRepository
    {
        public LifetimeScope GetScopePrefab(string sceneName);
    }
}