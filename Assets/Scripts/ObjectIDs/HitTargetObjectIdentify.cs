using System;
using Managers;
using Managers.Hits;
using UnityEngine;
using UnityEngine.Serialization;

namespace ObjectIDs
{
    /// <summary>
    /// IHitTargetHandlerを設定する種類
    /// </summary>
    public enum SetTypeforHitTargetType
    {
        /// <summary>
        /// スクリプトからIHitTargetHandlerを設定
        /// </summary>
        Script,
        /// <summary>
        /// エディタからIHitTargetHandlerを設定
        /// </summary>
        SerializeField
    }

    /// <summary>
    /// 攻撃があたったオブジェクトの識別子
    /// </summary>
    public class HitTargetObjectIdentify : MonoBehaviour
    {
        private ObjectIDs.ObjectID _idref;

        [Tooltip("このコンポーネント内部でオブジェクトidを生成するかどうか(外部のスクリプトから設定したい場合はoff)")] [SerializeField]
        private bool generateIDMySelf = true;

        [FormerlySerializedAs("hitTargetType")] [SerializeField] private SetTypeforHitTargetType setTypeforHitTargetType;

        public SetTypeforHitTargetType SetTypeforHitTargetType
        {
            get { return setTypeforHitTargetType; }
        }

        [SerializeField] private GameObject hitTargetHandlerGameObject;
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (hitTargetHandlerGameObject != null)
            {
                if (!hitTargetHandlerGameObject.TryGetComponent(out IHitTargetHandler hitTargetHandler))
                {
                    hitTargetHandlerGameObject = null;
                    Debug.Log("hitTargetHandlerGameObject にIHitTargetHandlerを継承したコンポーネントがありません");
                }
            }
        }
#endif

        private void Awake()
        {
            if (generateIDMySelf)
            {
                if (SetTypeforHitTargetType == SetTypeforHitTargetType.SerializeField)
                {
                    hitTargetHandlerGameObject.TryGetComponent<IHitTargetHandler>(out var hitTargetHandler);
                    Initialize(new ObjectID(), hitTargetHandler);
                }
                else if (SetTypeforHitTargetType == SetTypeforHitTargetType.Script)
                {
                }
            }
        }

        public void Initialize(ObjectID objectID, IHitTargetHandler hitTargetHandler)
        {
            this._idref = objectID;
            HitTargetDatabase.Instance.AddHitTarget(this._idref, hitTargetHandler);
        }

        /// <summary>
        /// id取得
        /// </summary>
        /// <returns>id</returns>
        public ObjectID GetID()
        {
            return _idref;
        }
    }
}