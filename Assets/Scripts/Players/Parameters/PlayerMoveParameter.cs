using UnityEngine;

namespace Players.Parameters
{
    [CreateAssetMenu(fileName = "PlayerMoveParameter",
        menuName = "ScriptableObjects/PlayerMoveParameterScriptableObject", order = 1)]
    public class PlayerMoveParameter : ScriptableObject
    {
        public float moveSpeed;
    }
}