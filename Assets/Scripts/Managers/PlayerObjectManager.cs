using ObjectIDs;
using Players.HP;
using SimpleFPS.Weapons.Hits;
using UnityEngine;

namespace Managers
{
    public class PlayerObjectManager : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform generatePosition;

        public void GeneratePlayer()
        {
            var playerObj = Instantiate(playerPrefab, generatePosition);
            var playerHitTargetObjectIdentify = playerObj.GetComponent<HitTargetObjectIdentify>();
            var hpcontainer = playerObj.GetComponent<HpContaier>();
            var playerHitTargetHandler = new PlayerHitTargetHandler();
            playerHitTargetHandler.Initialize(hpcontainer);
            playerHitTargetObjectIdentify.Initialize(new ObjectID(), playerHitTargetHandler);
        }
    }
}