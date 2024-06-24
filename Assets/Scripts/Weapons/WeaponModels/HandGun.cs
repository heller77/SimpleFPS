using Managers;
using R3;
using UnityEngine;
using Weapons.Bullets;

namespace Weapons
{
    public class HandGun : MonoBehaviour, IAttackable, IWeapon
    {
        private HitManager _hitManager;

        public void Initialize(HitManager hitManager)
        {
            this._hitManager = hitManager;
        }

        public void Attack()
        {
            // Debug.Log("HandGun Attack!!");
            //弾を生成、
            var bulletMono =
                BulletController._instance?.FireBullet(this.transform.position, Vector3.forward.normalized * 0.1f);
            bulletMono?.HitEvent.Subscribe(HitInfo => { _hitManager.ProcessHit(HitInfo); });
        }
    }
}