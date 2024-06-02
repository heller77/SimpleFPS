using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Weapons.Bullets
{
    /// <summary>
    ///     銃弾のオブジェクトプール
    /// </summary>
    public class BulletPool : MonoBehaviour
    {
        private IObjectPool<BulletMono> _objectPool;

        /// <summary>
        ///     初期化
        /// </summary>
        /// <param name="bulletPrefab">銃弾のprefabを設定</param>
        /// <param name="poolDefaultCapacity">デフォルトの容量</param>
        /// <param name="poolMaxSize">maxの容量</param>
        /// <exception cref="Exception">bulletPrefabがBulletMonoでは無かったら投げられる</exception>
        public void Initialize(GameObject bulletPrefab, int poolDefaultCapacity = 10, int poolMaxSize = 100)
        {
            if (bulletPrefab.TryGetComponent(out BulletMono bullet))
            {
                _objectPool = new ObjectPool<BulletMono>(
                    createFunc: () => Instantiate(bulletPrefab).GetComponent<BulletMono>(), // プールが空のときに新しいインスタンスを生成する処理
                    actionOnGet: target => target.gameObject.SetActive(true), // プールから取り出されたときの処理 
                    actionOnRelease: target => target.gameObject.SetActive(false), // プールに戻したときの処理
                    actionOnDestroy: target => Destroy(target), // プールがmaxSizeを超えたときの処理
                    collectionCheck: true, // 同一インスタンスが登録されていないかチェックするかどうか
                    defaultCapacity: poolDefaultCapacity, // デフォルトの容量
                    maxSize: poolMaxSize);
            }
            else
            {
                throw new Exception("bulletprefabがBullet型のコンポーネントを持っていません。");
            }
        }

        /// <summary>
        ///     弾を生成する
        /// </summary>
        /// <returns></returns>
        public BulletMono GetBullet()
        {
            var dispose = _objectPool.Get(out BulletMono bullet);
            bullet.Initialize(dispose);

            return bullet;
        }
    }
}