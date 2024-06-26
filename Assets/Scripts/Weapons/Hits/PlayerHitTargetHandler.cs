using Managers;
using Players.HP;
using UnityEngine;

namespace SimpleFPS.Weapons.Hits
{
    public class PlayerHitTargetHandler : IHitTargetHandler
    {
        private HpContaier _hpContaier;

        public void Initialize(HpContaier hpContaier)
        {
            this._hpContaier = hpContaier;
        }

        public void HandleHit(HitInfo hitInfo)
        {
            Debug.Log("hit!! ");
            _hpContaier.Attack(new AttackPowerInfo(0, hitInfo.Damage));
        }
    }
}