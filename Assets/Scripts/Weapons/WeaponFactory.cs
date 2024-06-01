using Datas;
using UnityEngine;

namespace Weapons
{
    public class WeaponFactory : MonoBehaviour
    {
        public static IAttackable GenerateWeapon(GameObject weaponPrefab, Transform generateParent)
        {
            Debug.Log("generate weapon !!");
            var weaponInstance = Instantiate(weaponPrefab, generateParent, false);
            return weaponInstance.GetComponent<IAttackable>();
        }
    }
}