#region

using Players.HP.Gauge.GaugeView;
using R3;

#endregion

namespace Players.HP.Gauge
{
    /// <summary>
    ///     ゲージのPresenter
    /// </summary>
    public class GaugePresenter
    {
        private readonly Gauge _gauge;
        private readonly IGaugeView _gaugeView;

        public GaugePresenter(Gauge gauge, GaugeView.GaugeView gaugeView)
        {
            //フィールド初期化
            _gauge = gauge;
            _gaugeView = gaugeView;
            //購読
            _gauge._gaugeValue.Subscribe(SetGaugeViewValue);
        }

        /// <summary>
        ///     viewに値をセット
        /// </summary>
        /// <param name="gaugeValue"></param>
        private void SetGaugeViewValue(float gaugeValue)
        {
            float value = gaugeValue / _gauge._maxValue;
            _gaugeView.SetValue(value);
        }
    }
}