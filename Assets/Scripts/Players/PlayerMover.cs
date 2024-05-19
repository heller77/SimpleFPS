using UnityEngine;
using VContainer;

namespace Players
{
    public class PlayerMover
    {
        private CharacterController _characterController;

        [Inject]
        public PlayerMover(CharacterController characterController)
        {
            _characterController = characterController;
        }
    }
}