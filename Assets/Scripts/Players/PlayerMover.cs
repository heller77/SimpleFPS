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

        public void Move(Vector2 movevalue)
        {
            _characterController.Move(new Vector3(movevalue.x, 0, movevalue.y));
        }
    }
}