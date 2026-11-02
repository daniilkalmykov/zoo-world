namespace Domain.Interfaces
{
    public interface IMortal
    {
        public bool IsAlive { get; }
        public bool TryDie();
    }
}