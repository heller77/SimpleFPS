using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Players
{
    public class Player : ITickable
    {
        private PlayerMover _playerMover;

        [Inject]
        public Player(PlayerMover playerMover)
        {
            _playerMover = playerMover;
        }

        public void Tick()
        {
            Debug.Log("tick");
        }
    }
}