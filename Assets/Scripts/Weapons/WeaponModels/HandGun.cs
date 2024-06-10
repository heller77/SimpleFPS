using R3;
using UnityEngine;
using Weapons.Bullets;

namespace Weapons
{
    public class HandGun : MonoBehaviour, IAttackable, IWeapon
    {
        public void Attack()
        {
            // Debug.Log("HandGun Attack!!");
            //弾を生成、
            BulletController._instance?.FireBullet(this.transform.position, Vector3.forward.normalized * 0.1f);
        }
    }
}