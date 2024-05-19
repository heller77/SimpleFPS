using MyInputs;
using Players;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DIContainers
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [SerializeField] private CharacterController playerCharacterController;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PlayerMover>(Lifetime.Singleton);
            builder.RegisterComponent(playerCharacterController);
            builder.Register<Player>(Lifetime.Singleton);

            builder.Register<MyInput>(Lifetime.Singleton);
            builder.RegisterEntryPoint<MyInput>();
            builder.RegisterEntryPoint<Player>();
        }
    }
}