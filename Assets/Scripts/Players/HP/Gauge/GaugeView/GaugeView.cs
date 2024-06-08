using UnityEngine;
using UnityEngine.UI;

namespace Players.HP.Gauge.GaugeView
{
    /// <summary>
    ///     スタンゲージのview(ui管理する)
    /// </summary>
    public class GaugeView : MonoBehaviour, IGaugeView
    {
        /// <summary>
        ///     スタンゲージを表すスライダー
        /// </summary>
        [SerializeField] private Slider _gaugeSlider;

        /// <summary>
        ///     スタンゲージの値を設定
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(float value)
        {
            _gaugeSlider.value = value;
        }
    }
}