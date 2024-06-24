﻿using System;
using Managers;
using UnityEngine;

namespace ObjectIDs
{
    /// <summary>
    /// オブジェクトの識別子
    /// </summary>
    public class HitTargetObjectIdentify : MonoBehaviour , IHitTargetHandler
    {
        private ObjectIDs.ObjectID _idref;

        [Tooltip("このコンポーネント内部でオブジェクトidを生成するかどうか")] [SerializeField]
        private bool generateIDMySelf = true;

        private void Awake()
        {
            if (generateIDMySelf)
            {
                Initialize();
            }
        }

        public void Initialize()
        {
            Initialize(new ObjectID());
        }

        public void Initialize(ObjectID objectID)
        {
            this._idref = objectID;
            HitTargetDatabase.Instance.AddHitTarget(this._idref,this);
        }

        /// <summary>
        /// id取得
        /// </summary>
        /// <returns></returns>
        public ObjectID GetID()
        {
            return _idref;
        }

        public void HandleHit(HitInfo hitInfo)
        {
            
        }
    }
}