using Datas;
using Managers;
using MyInputs;
using Players;
using Players.Parameters;
using UIs;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Weapons.Bullets;

namespace DIContainers
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [SerializeField] private CharacterController playerCharacterController;
        [SerializeField] private PlayerMoveParameter _playerMoveParameter;
        [SerializeField] private PlayerMono _playermono;

        [SerializeField] private InGameUIManager _inGameUIManager;
        [SerializeField] private WeaponsData _weaponsData;

        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private BulletMono _bulletPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_inGameUIManager);
            builder.RegisterInstance(_weaponsData);
            builder.RegisterInstance(_playermono);
            builder.RegisterInstance(_bulletPool);
            builder.RegisterInstance(_bulletPrefab);

            builder.Register<HitManager>(Lifetime.Singleton);
            builder.Register<BulletController>(Lifetime.Singleton);
            builder.Register<InGameManager>(Lifetime.Singleton);
            builder.Register<PlayerMover>(Lifetime.Singleton);
            builder.RegisterComponent(playerCharacterController);
            builder.Register<Player>(Lifetime.Singleton);
            builder.RegisterInstance(_playerMoveParameter);

            builder.Register<MyInput>(Lifetime.Singleton);
            // builder.RegisterEntryPoint<MyInput>();
            builder.RegisterEntryPoint<Player>();
            builder.RegisterEntryPoint<InGameManager>();
        }
    }
}