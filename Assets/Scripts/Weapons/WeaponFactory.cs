using Datas;
using Managers;
using UnityEngine;

namespace Weapons
{
    public class WeaponFactory : MonoBehaviour
    {
        public static IWeapon GenerateWeapon(GameObject weaponPrefab, Transform generateParent,
            HitManager hitManager)
        {
            Debug.Log("generate weapon !!");
            var weaponInstance = Instantiate(weaponPrefab, generateParent, false);
            var weapon = weaponInstance.GetComponent<IWeapon>();
            weapon.SetHitManager(hitManager);
            return weapon;
        }
    }
}