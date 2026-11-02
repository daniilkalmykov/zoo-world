using System;

namespace Domain.Interfaces
{
    public interface IDeathList<TKey>
    {
        event Action<TKey, uint> Changed;

        void InitializeTypes(TKey[] types);
        void Increase(TKey key);
    }
}