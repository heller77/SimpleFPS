using Players.HP;
using UnityEngine;

namespace Enemys
{
    public class EnemyMono : MonoBehaviour
    {
        [SerializeField] private HpContaier _hpContaier;

        public void Attack(float damage)
        {
            Debug.Log("damage");
            _hpContaier.Attack(new AttackPowerInfo(0, damage));
        }
    }
}