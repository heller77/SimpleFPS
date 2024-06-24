using Managers;
using R3;
using UnityEngine;
using Weapons.Bullets;

namespace Weapons
{
    public class HandGun : MonoBehaviour, IWeapon
    {
        private HitManager _hitManager;

        public void SetHitManager(HitManager hitManager)
        {
            _hitManager = hitManager;
        }

        public void Attack()
        {
            // Debug.Log("HandGun Attack!!");
            //弾を生成、
            var bulletMono =
                BulletController._instance?.FireBullet(this.transform.position, Vector3.forward.normalized * 0.1f);
            //銃弾があたったら、HitManagerに
            bulletMono?.HitEvent.Subscribe(HitInfo => { _hitManager.ProcessHit(HitInfo); });
        }
    }
}