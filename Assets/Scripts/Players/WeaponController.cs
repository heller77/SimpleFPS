using MyInputs;
using R3;
using UnityEngine;
using Weapons;

namespace Players
{
    public class WeaponController
    {
        private IAttackable _weapon;

        public WeaponController(MyInput myInput)
        {
            myInput.Attack.Subscribe(_ => { Attack(); });
        }

        public void SetWeapon(IAttackable weapon)
        {
            this._weapon = weapon;
        }

        /// <summary>
        /// 管理している武器で攻撃
        /// </summary>
        private void Attack()
        {
            // Debug.Log("weaponattack");
            _weapon?.Attack();
        }
    }
}