using Datas;
using Players;
using R3;
using UIs;
using Unity.VisualScripting;
using UnityEngine;
using VContainer.Unity;
using Weapons;

namespace Managers
{
    public class InGameManager : IStartable
    {
        private InGameUIManager _inGameUIManager;
        private Player _player;
        private WeaponsData _weaponsData;

        public InGameManager(InGameUIManager inGameUIManager, Player player, WeaponsData weaponsData)
        {
            this._inGameUIManager = inGameUIManager;
            this._player = player;
            this._weaponsData = weaponsData;
        }

        public void Start()
        {
            Debug.Log("ingamemanager init");
            _inGameUIManager.StartButtonClicked.Take(1).Subscribe(_ =>
            {
                _player.SetWeapon(WeaponFactory.GenerateWeapon(_weaponsData.HandGun, _player.GetHandTransform()));
            });
        }
    }
}