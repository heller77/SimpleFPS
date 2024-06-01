using UnityEngine;

namespace Players
{
    public class PlayerMono : MonoBehaviour
    {
        [SerializeField] private Transform hand;

        public Transform Hand => hand;
    }
}