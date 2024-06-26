using UnityEngine;
using UnityEngine.Serialization;

namespace ObjectIDs
{
    public class RefHitTargetObjectIdentify : MonoBehaviour
    {
        [SerializeField] private HitTargetObjectIdentify hitTargetObjectIdentify;

        public ObjectID GetID()
        {
            return hitTargetObjectIdentify.GetID();
        }
    }
}