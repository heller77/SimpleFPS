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
    /// <summary>
    ///     オートヒールを行うタスク（止めたら止まったのを確認してから、親（リスト）から自分を消す）
    /// </summary>
    class AutoHealTask
    {
        private CancellationTokenSource cts;
        private readonly List<AutoHealTask> parent;

        public AutoHealTask(List<AutoHealTask> parent)
        {
            cts = new CancellationTokenSource();
            this.parent = parent;
        }

        public void Stop()
        {
            cts.Cancel();
        }

        public async UniTask StartAutoHeal(float timeToStartautoHeal, Gauge.Gauge _hpGauge, float timeToEndHeal,
            int divideNum)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(timeToStartautoHeal), cancellationToken: cts.Token);


                cts = new CancellationTokenSource();
                //オートヒール
                await GaugeUtility.AutoHealMax(_hpGauge, timeToEndHeal, divideNum, cts.Token);
            }
            catch (OperationCanceledException e)
            {
                parent.Remove(this);
                throw;
            }
        }
    }

    [Serializable]
    internal class HpSetting
    {
        public float timeToStartautoHeal;
        public float timeToEndHeal;
        public float hpMaxValue;
        public int healingDivideNum;
        public GaugeView hpGaugeView;
    }

    internal class Hp
    {
        private readonly Gauge.Gauge _hpGauge;
        private GaugePresenter _gaugePresenter;

        /// <summary>
        ///     オートヒールが始まるまでの時間
        /// </summary>
        private readonly float timeToStartautoHeal;

        /// <summary>
        ///     オートヒールが終わるまでの時間
        /// </summary>
        private readonly float timeToEndHeal;

        /// <summary>
        ///     全回復する時に何回に分けて回復するか
        /// </summary>
        private readonly int healingDivideNum = 3;

        private readonly Subject<Unit> dead = new Subject<Unit>();

        private readonly List<AutoHealTask> _autoHealTasks = new List<AutoHealTask>();

        public Hp(HpSetting hpSetting)
        {
            _hpGauge = new Gauge.Gauge(hpSetting.hpMaxValue, hpSetting.hpMaxValue);
            _gaugePresenter = new GaugePresenter(_hpGauge, hpSetting.hpGaugeView);


            timeToStartautoHeal = hpSetting.timeToStartautoHeal;
            timeToEndHeal = hpSetting.timeToEndHeal;
            healingDivideNum = hpSetting.healingDivideNum;

            //死んだら
            dead.Subscribe(_ =>
            {
                for (int i = 0; i < _autoHealTasks.Count; i++)
                {
                    _autoHealTasks[i].Stop();
                }
            });
        }

        /// <summary>
        ///     HPへの攻撃
        /// </summary>
        /// <param name="attackPower"></param>
        /// <returns>受けた攻撃力の余りを返す</returns>
        public async UniTask<float> Attack(float attackPower)
        {
            if (attackPower <= 0)
            {
                return 0;
            }

            var nowgaugeValue = _hpGauge.GetGaugeValue();
            float excessAttackPower = attackPower - nowgaugeValue;
            _hpGauge.DecreaseGage(attackPower);


            if (_hpGauge.GetGaugeValue() <= 0)
            {
                //hpが0になったなら
                dead.OnNext(Unit.Default);
                return Mathf.Max(excessAttackPower, 0);
            }

            //今は知ってるオートヒールを全て止める
            for (int i = 0; i < _autoHealTasks.Count; i++)
            {
                _autoHealTasks[i].Stop();
            }

            var autohealTask = new AutoHealTask(_autoHealTasks);
            _autoHealTasks.Add(autohealTask);
            autohealTask.StartAutoHeal(timeToEndHeal, _hpGauge, timeToEndHeal, healingDivideNum);

            return Mathf.Max(excessAttackPower, 0);
        }
    }
}