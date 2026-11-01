using UnityEngine;

namespace Roots
{
    public sealed class GameWorldRoot : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _animalsRoot;
        
        public Camera Camera => _camera;
        public Transform AnimalsRoot => _animalsRoot;
    }
}