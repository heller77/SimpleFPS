using R3;
using Unity.Mathematics;
using UnityEngine;
using VContainer.Unity;

namespace MyInputs
{
    public class MyInput
    {
        private GameInput _input;

        public Vector2 MoveValue
        {
            get => _input.Player.move.ReadValue<Vector2>();
        }

        private Subject<bool> _attackSubject = new Subject<bool>();

        public Observable<bool> Attack
        {
            get => _attackSubject;
        }

        // public ReactiveProperty<bool> Attack = new ReactiveProperty<bool>();

        public MyInput()
        {
            this._input = new GameInput();
            _input.Enable();

            _input.Player.Attack.performed += (input) => { _attackSubject.OnNext(true); };
        }
    }
}