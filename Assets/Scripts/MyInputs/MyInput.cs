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

        public MyInput()
        {
            this._input = new GameInput();
            _input.Enable();
        }
    }
}