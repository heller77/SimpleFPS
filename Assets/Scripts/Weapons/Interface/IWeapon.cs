using Managers;

namespace Weapons
{
    public interface IWeapon
    {
        void SetHitManager(HitManager hitManager);
        void Attack();
    }
}