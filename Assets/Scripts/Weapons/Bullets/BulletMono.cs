using System;
using R3;
using UnityEngine;

namespace Weapons.Bullets
{
    /// <summary>
    ///     unity世界の銃弾
    /// </summary>
    public class BulletMono : MonoBehaviour
    {
        /// <summary>
        ///     弾をオブジェクトプールに返す時に使う
        /// </summary>
        private IDisposable _disposable;

        private readonly Subject<Unit> destroy = new Subject<Unit>();

        /// <summary>
        ///     銃弾が消えた時に発火
        /// </summary>
        public Observable<Unit> destroyEvent
        {
            get { return destroy; }
        }

        private Vector3 movedir;

        public void SetMoveDir(Vector3 movedir)
        {
            this.movedir = movedir;
        }

        /// <summary>
        ///     初期化。
        /// </summary>
        /// <param name="disposable">弾を使わなくなったらpoolの参照がいるので設定</param>
        public void Initialize(IDisposable disposable)
        {
            _disposable = disposable;
        }

        /// <summary>
        ///     弾を使わなくなったら呼ぶ。
        ///     弾をオブジェクトプールに返す時に使う
        /// </summary>
        public void Destroy()
        {
            _disposable.Dispose();
        }

        /// <summary>
        ///     毎フレーム位置を計算するもの。外部から呼ばれる
        /// </summary>
        public void UpdateMove()
        {
            transform.position += movedir;
        }
    }
}