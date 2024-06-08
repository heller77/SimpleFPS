using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Players.HP.Gauge;
using Players.HP.Gauge.GaugeView;
using R3;
using UnityEngine;

namespace Players.HP
{
    [Serializable]
    class StunGaugeSetting
    {
        /// <summary>
        ///     スタンゲージのView
        /// </summary>
        [Header("スタンゲージ")] [Tooltip("スタンゲージのView")] [SerializeField]
        internal GaugeView _stunGaugeView;

        /// <summary>
        ///     スタンした時の、スタンを維持する時間(秒)
        /// </summary>
        [Tooltip("スタンした時の、スタンを維持する時間(秒)")] [SerializeField]
        internal float stunTime = 1.0f;

        /// <summary>
        ///     スタンが終わるときの全回復する時間
        /// </summary>
        [Tooltip("スタンが終わるときの全回復する時間")] [SerializeField]
        internal float healingtimeWhenStun = 1.0f;

        /// <summary>
        ///     スタンが終わった時の全回復する時に何回に分けて回復するか
        /// </summary>
        [Tooltip("スタンが終わった時の全回復する時に何回に分けて回復するか")] [SerializeField]
        internal int healingDivideNumWhenStun = 3;

        /// <summary>
        ///     一定時間攻撃されなかったら自動回復するので、自動回復するまでの時間
        /// </summary>
        [Tooltip(" 一定時間攻撃されなかったら自動回復するので、自動回復するまでの時間")] [SerializeField]
        internal float autoHealTimeFromAttack = 1.0f;
    }
    internal class StunGauge
    {
        /// <summary>
        ///     スタンゲージのView
        /// </summary>
        private GaugeView gaugeView;

        /// <summary>
        ///     スタンした時の、スタンを維持する時間(秒)
        /// </summary>
        private readonly float stunTime = 1.0f;

        /// <summary>
        ///     スタンが終わるときの全回復する時間
        /// </summary>
        private readonly float healingtimeWhenStun = 1.0f;

        /// <summary>
        ///     スタンが終わった時の全回復する時に何回に分けて回復するか
        /// </summary>
        private readonly int healingDivideNumWhenStun = 3;

        /// <summary>
        ///     一定時間攻撃されなかったら自動回復するので、自動回復するまでの時間
        /// </summary>
        private readonly float autoHealTimeFromAttack = 1.0f;

        /// <summary>
        ///     自動回復するUniTaskのCancellationTokenSourceを管理する。
        /// </summary>
        private readonly Queue<CancellationTokenSource> _autoStunGageHealCancellationTokenSourceQueue =
            new Queue<CancellationTokenSource>();

        /// <summary>
        ///     スタンゲージのPresenter
        /// </summary>
        private GaugePresenter _gaugePresenter;

        /// <summary>
        ///     スタンゲージのModelクラス
        /// </summary>
        private readonly Gauge.Gauge _stungauge;

        /// <summary>
        ///     スタンしているかのフラグ
        /// </summary>
        private bool isStuning;

        public StunGauge(StunGaugeSetting stunGaugeSetting)
        {
            gaugeView = stunGaugeSetting._stunGaugeView;
            stunTime = stunGaugeSetting.stunTime;
            healingtimeWhenStun = stunGaugeSetting.healingtimeWhenStun;
            healingDivideNumWhenStun = stunGaugeSetting.healingDivideNumWhenStun;
            autoHealTimeFromAttack = stunGaugeSetting.autoHealTimeFromAttack;

            //Model生成
            _stungauge = new Gauge.Gauge(0, 3);
            //Prenseter生成
            _gaugePresenter = new GaugePresenter(_stungauge, gaugeView);

            _stungauge.GaugeMax.Subscribe(_ => Stun());
        }

        public void Attack(float attackValue)
        {
            if (!isStuning)
            {
                _stungauge.IncreaseGage(attackValue);
                //ここでスタンする場合もあるので、
                if (!isStuning)
                {
                    foreach (var cts in _autoStunGageHealCancellationTokenSourceQueue)
                    {
                        cts.Cancel();
                    }

                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                    _autoStunGageHealCancellationTokenSourceQueue.Enqueue(cancellationTokenSource);
                    AutoHealIfNotAttack(cancellationTokenSource.Token);
                }
            }
        }

        /// <summary>
        ///     一度攻撃されてからautoHealTimeFromAttack秒攻撃されなかったら回復する
        ///     回復途中に攻撃された場合回復をキャンセルする
        /// </summary>
        private async UniTask AutoHealIfNotAttack(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(autoHealTimeFromAttack));
            var healtime = (_stungauge.GetGaugeValue() / _stungauge.GetGaugeMaxValue()) * healingtimeWhenStun;
            await GaugeUtility.AutoHealMin(_stungauge, healtime, healingDivideNumWhenStun,
                token);
            foreach (var cancellationTokenSource in _autoStunGageHealCancellationTokenSourceQueue)
            {
                cancellationTokenSource.Dispose();
            }

            _autoStunGageHealCancellationTokenSourceQueue.Clear();
        }

        /// <summary>
        ///     スタンして、一定時間たったら全回復して、スタン状態を解除する
        /// </summary>
        private async UniTask Stun()
        {
            foreach (var cts in _autoStunGageHealCancellationTokenSourceQueue)
            {
                cts.Cancel();
            }

            isStuning = true;
            await UniTask.WaitForSeconds(stunTime);
            var canceltokensource = new CancellationTokenSource();
            //スタンを終わらせて回復する
            await GaugeUtility.AutoHealMin(_stungauge, healingtimeWhenStun, healingDivideNumWhenStun,
                canceltokensource.Token);
            isStuning = false;

            foreach (var cancellationTokenSource in _autoStunGageHealCancellationTokenSourceQueue)
            {
                cancellationTokenSource.Dispose();
            }

            _autoStunGageHealCancellationTokenSourceQueue.Clear();
        }

        public bool GetIsStun()
        {
            return isStuning;
        }
    }
}