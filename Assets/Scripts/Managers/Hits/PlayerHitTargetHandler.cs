using Players.HP;
using UnityEngine;

namespace Managers.Hits
{
    public class PlayerHitTargetHandler : IHitTargetHandler
    {
        private HpContaier hpContaier;
        public void HandleHit(HitInfo hitInfo)
        {
            Debug.Log("hit!! ");
        }
    }
}