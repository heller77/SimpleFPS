using System;
using Players.HP.Gauge;
using Players.HP.Gauge.GaugeView;
using UnityEngine;

namespace Players.HP
{
    [Serializable]
    internal class ArmorSetting
    {
        public float armorMaxValue;
        public GaugeView gaugeView;
    }

    internal class Armor
    {
        private readonly Gauge.Gauge _armorGauge;
        private GaugeView armorGaugeView;
        private GaugePresenter _gaugePresenter;

        public Armor(ArmorSetting armorSetting)
        {
            armorGaugeView = armorSetting.gaugeView;
            _armorGauge = new Gauge.Gauge(armorSetting.armorMaxValue, armorSetting.armorMaxValue);
            _gaugePresenter = new GaugePresenter(_armorGauge, armorGaugeView);
        }

        /// <summary>
        ///     アーマへの攻撃
        /// </summary>
        /// <param name="attackPower"></param>
        /// <returns>受けた攻撃力の余りを返す</returns>
        public float Attack(float attackPower)
        {
            var nowgaugeValue = _armorGauge.GetGaugeValue();
            float excessAttackPower = attackPower - nowgaugeValue;
            _armorGauge.DecreaseGage(attackPower);
            return Mathf.Max(excessAttackPower, 0);
        }
    }
}