using Managers;
using UnityEngine;

namespace SimpleFPS.Weapons.Hits
{
    public class DestructionHitTargetHandler : MonoBehaviour, IHitTargetHandler
    {
        [SerializeField] private GameObject target;

        public void HandleHit(HitInfo hitInfo)
        {
            this.target.SetActive(false);
        }
    }
}