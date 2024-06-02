using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using R3;

namespace Weapons.Bullets
{
    /// <summary>
    /// 銃弾を管理
    /// </summary>
    public class BulletController
    {
        private BulletPool _bulletPool;
        private GameObject _bulletPrefab;

        private List<BulletMono> bulletList = new List<BulletMono>();

        public static BulletController _instance;

        public BulletController(BulletPool bulletPool, BulletMono bulletPrefab)
        {
            this._bulletPool = bulletPool;
            this._bulletPrefab = bulletPrefab.gameObject;
            this._bulletPool.Initialize(_bulletPrefab);
            _instance = this;
        }

        /// <summary>
        /// 弾発射
        /// </summary>
        public void FireBullet(Vector3 firePosition, Vector3 bulletMoveDir)
        {
            var bullet = _bulletPool.GetBullet();
            bullet.SetMoveDir(bulletMoveDir);
            bullet.transform.position = firePosition;

            bulletList.Add(bullet);


            bullet.destroyEvent.Subscribe(_ => { bulletList.Remove(bullet); }).AddTo(bullet);
        }

        /// <summary>
        /// 弾更新
        /// 毎フレーム
        /// </summary>
        public void UpdateBullet()
        {
            foreach (var bulletMono in bulletList)
            {
                bulletMono.UpdateMove();
            }
        }
    }
}