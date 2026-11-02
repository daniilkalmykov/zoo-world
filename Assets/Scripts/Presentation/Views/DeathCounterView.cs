using System;
using System.Runtime.CompilerServices;
using Domain.Models.Animals;
using TMPro;
using UnityEngine;

[assembly: InternalsVisibleTo("Roots")]
namespace Presentation.Views
{
    internal sealed class DeathCounterView : MonoBehaviour, IDeathCounterView
    {
        [SerializeField] private TMP_Text _preyText;

        [SerializeField] private TMP_Text _predatorText;

        public void SetCount(AnimalType type, uint value)
        {
            switch (type)
            {
                case AnimalType.Prey:
                    _preyText.text = $"Dead prey: {value}";
                    break;
                case AnimalType.Predator:
                    _predatorText.text = $"Dead predators: {value}";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}