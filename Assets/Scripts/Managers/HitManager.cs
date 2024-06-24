using System;
using System.Collections.Generic;
using ObjectIDs;
using UnityEditor.Build.Content;
using UnityEngine;

namespace Managers
{
    public class HitInfo
    {
        public GameObject Bullet { get; }
        public GameObject HitObject { get; }
        public Vector3 HitPoint { get; }
        public Vector3 HitNormal { get; }

        public HitInfo(GameObject bullet, GameObject hitObject, Vector3 hitPoint, Vector3 hitNormal)
        {
            Bullet = bullet;
            HitObject = hitObject;
            HitPoint = hitPoint;
            HitNormal = hitNormal;
        }
    }

    /// <summary>
    /// 当たった時の処理を書く
    /// </summary>
    public interface IHitTargetHandler
    {
        void HandleHit(HitInfo hitInfo);
    }

    public class HitTargetDatabase
    {
        private Dictionary<ObjectID, IHitTargetHandler> _hitTargetHandlers =
            new Dictionary<ObjectID, IHitTargetHandler>();

        private static HitTargetDatabase _instance;

        public static HitTargetDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HitTargetDatabase();
                }

                return _instance;
            }
        }

        public IHitTargetHandler GetHitTarget(ObjectID id)
        {
            return this._hitTargetHandlers[id];
        }

        public void AddHitTarget(ObjectID objectID, IHitTargetHandler hitTargetHandler)
        {
            this._hitTargetHandlers.Add(objectID, hitTargetHandler);
        }
    }

    public class HitManager
    {
        private static int generateNum = 0;

        public HitManager()
        {
            if (generateNum >= 1)
            {
                throw new Exception("HitManagerが二つ生成されています。");
            }

            generateNum++;
        }

        /// <summary>
        /// 攻撃を実行する
        /// </summary>
        /// <param name="hitInfo"></param>
        public void ProcessHit(HitInfo hitInfo)
        {
            // ObjectIdentify 
            var hitID = hitInfo.HitObject.GetComponent<HitTargetObjectIdentify>();
            var hitHandler = HitTargetDatabase.Instance.GetHitTarget(hitID.GetID());

            if (hitHandler != null)
            {
                hitHandler.HandleHit(hitInfo);
            }
            else
            {
            }
        }
    }
}