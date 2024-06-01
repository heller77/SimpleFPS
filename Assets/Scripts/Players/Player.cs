using MyInputs;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Weapons;

namespace Players
{
    public class Player : ITickable
    {
        private PlayerMover _playerMover;
        private MyInput _input;

        private IWeapon _weapon;

        [Inject]
        public Player(PlayerMover playerMover, MyInput myInput)
        {
            _playerMover = playerMover;
            _input = myInput;
        }

        public void Tick()
        {
            var moveValue = _input.MoveValue;
            this._playerMover.Move(moveValue * Time.deltaTime);
        }

        public void SetWeapon(IWeapon weapon)
        {
            this._weapon = weapon;
        }
    }
}