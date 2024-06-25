using UnityEngine;

namespace Managers.Hits
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