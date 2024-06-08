using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Players.HP.Gauge
{
    public class GaugeUtility
    {
        public static async UniTask AutoHealMin(Gauge gauge, float healingtime, int healingDivide,
            CancellationToken cancellationToken)

        {
            double onehealTIme = healingtime / healingDivide;
            float oneframehealValue = gauge.GetGaugeValue() / healingDivide;
            for (int i = 0; i < healingDivide; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                gauge.DecreaseGage(oneframehealValue);
                await UniTask.Delay(TimeSpan.FromSeconds(onehealTIme));
            }

            cancellationToken.ThrowIfCancellationRequested();
            //一応完全に0に回復
            gauge.DecreaseGage(gauge.GetGaugeMaxValue());
        }

        public static async UniTask AutoHealMax(Gauge gauge, float healingtime, int healingDivide,
            CancellationToken cancellationToken)

        {
            double onehealTIme = healingtime / healingDivide;
            float oneframehealValue = (gauge.GetGaugeMaxValue() - gauge.GetGaugeValue()) / healingDivide;
            for (int i = 0; i < healingDivide; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                gauge.IncreaseGage(oneframehealValue);
                await UniTask.Delay(TimeSpan.FromSeconds(onehealTIme), cancellationToken: cancellationToken);
            }

            cancellationToken.ThrowIfCancellationRequested();
            //一応完全に0に回復
            gauge.IncreaseGage(gauge.GetGaugeMaxValue());
        }
    }
}