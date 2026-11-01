using UnityEngine;

namespace Bootstrap.Roots
{
    internal sealed class GameWorldRoot : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        public Camera Camera => _camera;
    }
}