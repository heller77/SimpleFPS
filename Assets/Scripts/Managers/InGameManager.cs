﻿using Datas;
using Players;
using R3;
using UIs;
using Unity.VisualScripting;
using UnityEngine;
using VContainer.Unity;
using Weapons;
using Weapons.Bullets;

namespace Managers
{
    public class InGameManager : IStartable, ITickable
    {
        private InGameUIManager _inGameUIManager;
        private Player _player;
        private WeaponsData _weaponsData;
        private BulletController _bulletController;

        public InGameManager(InGameUIManager inGameUIManager, Player player, WeaponsData weaponsData,
            BulletController bulletController)
        {
            this._inGameUIManager = inGameUIManager;
            this._player = player;
            this._weaponsData = weaponsData;
            this._bulletController = bulletController;
        }

        public void Start()
        {
            Debug.Log("ingamemanager init");
            _inGameUIManager.StartButtonClicked.Take(1).Subscribe(_ =>
            {
                _player.SetWeapon(WeaponFactory.GenerateWeapon(_weaponsData.HandGun, _player.GetHandTransform()));
            });
        }

        public void Tick()
        {
            _bulletController.UpdateBullet();
        }
    }
}