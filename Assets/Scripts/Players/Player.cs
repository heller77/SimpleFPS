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

        private WeaponController _weaponController;

        private PlayerMono _playerMono;

        [Inject]
        public Player(PlayerMover playerMover, MyInput myInput, PlayerMono playerMono)
        {
            _playerMover = playerMover;
            _input = myInput;
            _weaponController = new WeaponController(myInput);
            this._playerMono = playerMono;
        }

        public void Tick()
        {
            var moveValue = _input.MoveValue;
            this._playerMover.Move(moveValue * Time.deltaTime);
        }

        public void SetWeapon(IWeapon weapon)
        {
            _weaponController.SetWeapon(weapon);
        }

        public Transform GetHandTransform()
        {
            return this._playerMono.Hand;
        }
    }
}