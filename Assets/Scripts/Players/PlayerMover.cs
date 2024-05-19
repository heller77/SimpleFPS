using Players.Parameters;
using UnityEngine;
using VContainer;

namespace Players
{
    public class PlayerMover
    {
        private CharacterController _characterController;
        private PlayerMoveParameter _playerMoveParameter;

        [Inject]
        public PlayerMover(CharacterController characterController, PlayerMoveParameter playerMoveParameter)
        {
            _characterController = characterController;
            _playerMoveParameter = playerMoveParameter;
        }

        public void Move(Vector2 movevalue)
        {
            _characterController.Move(new Vector3(movevalue.x, 0, movevalue.y) * _playerMoveParameter.moveSpeed);
        }
    }
}