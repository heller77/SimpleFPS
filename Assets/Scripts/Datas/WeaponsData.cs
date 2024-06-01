using UnityEngine;
using Weapons;

namespace Datas
{
    [CreateAssetMenu(fileName = "WeaponsData", menuName = "Battle/WeaponsData", order = 0)]
    public class WeaponsData : ScriptableObject
    {
        [SerializeField] private GameObject _handGun;

        public GameObject HandGun => _handGun;
    }
}