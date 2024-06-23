using UnityEngine;

namespace ObjectIDs
{
    /// <summary>
    /// オブジェクトの識別子
    /// </summary>
    public class ObjectIdentify : MonoBehaviour
    {
        private ObjectIDs.ObjectID _idref;

        /// <summary>
        /// id取得
        /// </summary>
        /// <returns></returns>
        public ObjectID GetID()
        {
            return _idref;
        }
    }
}