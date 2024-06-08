using UnityEngine;

namespace Players.HP
{
    /// <summary>
    ///     攻撃についての情報
    ///     スタンゲージへの攻撃力、アーマーorHPへの攻撃力の二つを設定
    /// </summary>
    public struct AttackPowerInfo
    {
        internal float AttackPowerToStunGauge;
        internal float AttackPowerToArmorOrHp;

        public AttackPowerInfo(float attackPowerToStunGauge, float attackPowerToArmorOrHp)
        {
            AttackPowerToStunGauge = attackPowerToStunGauge;
            AttackPowerToArmorOrHp = attackPowerToArmorOrHp;
        }
    }

    /// <summary>
    ///     HP系の管理
    ///     HP シールド　スタンゲージの管理
    /// </summary>
    public class HpContaier : MonoBehaviour
    {
        //スタンゲージ、アーマー、HPクラスの初期化で使うパラメータ
        [Header("スタンゲージ")] [SerializeField] private StunGaugeSetting _stunGaugeSetting;

        [Header("アーマー")] [SerializeField] private ArmorSetting _armorSetting;

        [Header("HP")] [SerializeField] private HpSetting hpSetting;

        private StunGauge _stunGauge;
        private Armor _armor;
        private Hp _hp;

        private void Awake()
        {
            Initialize();
        }

        /// <summary>
        ///     スタンゲージのModelとPresenterを生成し繋ぎこむ(viewはエディタから指定)
        /// </summary>
        private void Initialize()
        {
            _stunGauge = new StunGauge(_stunGaugeSetting);
            _armor = new Armor(_armorSetting);
            _hp = new Hp(hpSetting);
        }

        /// <summary>
        ///     HPとアーマ　スタンゲージへ攻撃
        /// </summary>
        /// <param name="attackPowerInfo">HPとアーマへの攻撃力とスタンゲージへの攻撃力を指定</param>
        public void Attack(AttackPowerInfo attackPowerInfo)
        {
            _stunGauge.Attack(attackPowerInfo.AttackPowerToStunGauge);

            var excessPower = _armor.Attack(attackPowerInfo.AttackPowerToArmorOrHp);
            _hp.Attack(excessPower);
        }

        /// <summary>
        ///     今スタンしているかどうか(スキルが使えなくなるかの判断に使う)
        /// </summary>
        /// <returns></returns>
        public bool GetIsStunning()
        {
            return _stunGauge.GetIsStun();
        }
    }
}