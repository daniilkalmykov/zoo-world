using Domain.Models.Animals;

namespace Presentation.Views
{
    public interface IDeathCounterView
    {
        void SetCount(AnimalType type, uint value);
    }
}