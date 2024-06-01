using R3;
using UnityEngine;

namespace Weapons
{
    public class HandGun : MonoBehaviour, IAttackable, IWeapon
    {
        public HandGun(MyInputs.MyInput input)
        {
            input.Attack.Subscribe(trigger => { Attack(); });
        }

        public void Attack()
        {
            Debug.Log("HandGun Attack!!");
        }
    }
}