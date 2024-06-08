using R3;

namespace Players.HP.Gauge
{
    /// <summary>
    ///     ゲージ。
    /// </summary>
    public class Gauge
    {
        /// <summary>
        ///     ゲージの値
        /// </summary>
        public readonly ReactiveProperty<float> _gaugeValue = new ReactiveProperty<float>();

        /// <summary>
        ///     MAX値
        /// </summary>
        public readonly float _maxValue;

        /// <summary>
        ///     ゲージがMAX時に発火する
        /// </summary>
        public Observable<Unit> GaugeMax
        {
            get { return _gaugeMaxSubject; }
        }

        private readonly Subject<Unit> _gaugeMaxSubject = new Subject<Unit>();

        /// <summary>
        ///     コンストラクター
        /// </summary>
        /// <param name="firstGaugeValue">ゲージの初期値</param>
        /// <param name="maxValue">ゲージのMAX値</param>
        public Gauge(float firstGaugeValue, float maxValue)
        {
            _gaugeValue.Value = firstGaugeValue;
            _maxValue = maxValue;
        }

        /// <summary>
        ///     increaseValueだけゲージを増加させる
        /// </summary>
        /// <param name="increaseValue">ゲージを増やす量</param>
        public void IncreaseGage(float increaseValue)
        {
            _gaugeValue.Value += increaseValue;
            if (_gaugeValue.Value > _maxValue)
            {
                //マックス値を超えてたら
                _gaugeValue.Value = _maxValue;
            }

            if (_gaugeValue.Value >= _maxValue)
            {
                _gaugeMaxSubject.OnNext(Unit.Default);
            }
        }

        /// <summary>
        ///     ゲージを減らす（ゲージが0以下になったら、ゲージは0となる）
        /// </summary>
        /// <param name="decreaseGage">減らす量</param>
        public void DecreaseGage(float decreaseGage)
        {
            if (_gaugeValue.Value <= 0)
            {
                //回復しなくてももうゲージが0の場合、回復しない
                return;
            }

            _gaugeValue.Value -= decreaseGage;
            if (_gaugeValue.Value < 0)
            {
                _gaugeValue.Value = 0;
            }
        }

        /// <summary>
        ///     ゲージの値を返す
        /// </summary>
        /// <returns>ゲージの値</returns>
        public float GetGaugeValue()
        {
            return _gaugeValue.Value;
        }

        /// <summary>
        ///     ゲージのマックス値を返す
        /// </summary>
        /// <returns></returns>
        public float GetGaugeMaxValue()
        {
            return _maxValue;
        }
    }
}