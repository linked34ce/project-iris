using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    // this property should be deleted when class for each role is made
    [SerializeField] private string _role;
    public string Role => _role;

    [SerializeField] private int _exp;

    public int Exp
    {
        get => _exp;
        set
        {
            _exp = value;
            if (_exp >= ExpList[Level - 1])
            {
                LevelUp();
            }
        }
    }

    [SerializeField] private int _sp;
    public int Sp
    {
        get => _sp;
        set
        {
            _sp = Mathf.Clamp(value, 0, MaxSp);
            ShowSp();
        }
    }

    public int MaxSp { get; private set; }

    [SerializeField] private TMP_Text _nameText;
    public TMP_Text NameText => _nameText;
    [SerializeField] private TMP_Text _levelText;
    public TMP_Text LevelText => _levelText;
    [SerializeField] private Slider _hpBar;
    public Slider HpBar => _hpBar;
    [SerializeField] private TMP_Text _hpText;
    public TMP_Text HpText => _hpText;
    [SerializeField] private Slider _spBar;
    public Slider SpBar => _spBar;
    [SerializeField] private TMP_Text _spText;
    public TMP_Text SpText => _spText;

    [SerializeField] private BattleResult _battleResult;
    public BattleResult BattleResult => _battleResult;

    [SerializeField] private EnemyImagePrefabManager _enemyImagePrefabManager;
    public EnemyImagePrefabManager EnemyImagePrefabManager => _enemyImagePrefabManager;
    public Animator EnemyImageAnimator { get; private set; }

    [SerializeField] private string _portratAddress;
    public string PortraitAddress => _portratAddress;

    [SerializeField] private AttackerPortraitPrefabManager _attackerPortraitPrefabManager;
    public AttackerPortraitPrefabManager AttackerPortraitPrefabManager
                                         => _attackerPortraitPrefabManager;

    [SerializeField] private BattleSounds _battleSounds;
    public BattleSounds BattleSounds => _battleSounds;

    // this property should be refined
    public int[] ExpList { get; } = {
        10, 30, 60, 100, 150, 210, 280, 360, 450, 550,
    };

    // properties below should be deleted when class for each character is made
    public Dictionary<int, int> HpList { get; } = new(){
        {1, 21},
        {2, 22},
        {3, 24},
        {4, 26},
        {5, 28},
        {6, 31},
        {7, 34},
        {8, 37},
        {9, 40},
        {10, 44},
    };

    public Dictionary<int, int> SpList { get; } = new(){
        {1, 12},
        {2, 13},
        {3, 14},
        {4, 15},
        {5, 16},
        {6, 17},
        {7, 18},
        {8, 19},
        {9, 21},
        {10, 23},
    };

    public Dictionary<int, int> AtkList { get; } = new(){
        {1, 2},
        {2, 2},
        {3, 3},
        {4, 3},
        {5, 3},
        {6, 3},
        {7, 4},
        {8, 4},
        {9, 4},
        {10, 5},
    };

    public Dictionary<int, int> MagList { get; } = new(){
        {1, 5},
        {2, 6},
        {3, 6},
        {4, 7},
        {5, 7},
        {6, 8},
        {7, 8},
        {8, 9},
        {9, 9},
        {10, 10},
    };

    public Dictionary<int, int> DefList { get; } = new(){
        {1, 2},
        {2, 2},
        {3, 3},
        {4, 3},
        {5, 3},
        {6, 3},
        {7, 4},
        {8, 4},
        {9, 4},
        {10, 4},
    };

    public Dictionary<int, int> ResList { get; } = new(){
        {1, 6},
        {2, 7},
        {3, 7},
        {4, 8},
        {5, 8},
        {6, 8},
        {7, 9},
        {8, 9},
        {9, 10},
        {10, 11},
    };

    public Dictionary<int, int> AgiList { get; } = new(){
        {1, 2},
        {2, 2},
        {3, 2},
        {4, 3},
        {5, 3},
        {6, 3},
        {7, 4},
        {8, 4},
        {9, 4},
        {10, 5},
    };

    public Dictionary<int, int> LukList { get; } = new(){
        {1, 4},
        {2, 4},
        {3, 5},
        {4, 5},
        {5, 6},
        {6, 6},
        {7, 7},
        {8, 8},
        {9, 8},
        {10, 9},
    };

    protected override void Awake()
    {
        base.Awake();
        MaxHp = HpList[Level];
        MaxSp = SpList[Level];
        Hp = MaxHp;
        Sp = MaxSp;
        Atk = AtkList[Level];
        Mag = MagList[Level];
        Def = DefList[Level];
        Res = ResList[Level];
        Agi = AgiList[Level];
        Luk = LukList[Level];
        ShowPortrait();
    }

    private void LevelUp()
    {
        while (Exp >= ExpList[Level - 1])
        {
            Level++;
        }

        MaxHp = HpList[Level];
        MaxSp = SpList[Level];
        Atk = AtkList[Level];
        Mag = MagList[Level];
        Def = DefList[Level];
        Res = ResList[Level];
        Agi = AgiList[Level];
        Luk = LukList[Level];

        Hp = MaxHp;
        Sp = MaxSp;

        ShowLevel();
        BattleResult.ShowLevelUp();
        // Debug.Log(
        //     $"Lv: {Level}, HP: {Hp}, SP: {Sp}, ATK: {Atk}, MAG: {Mag}, "
        //     + $"DEF: {Def}, RES: {Res}, AGI: {Agi}, LUK: {Luk}"
        // );
    }

    private async void ShowPortrait() => await AttackerPortraitPrefabManager
                                                .LoadPortraitPrefab(PortraitAddress);

    public void ShowSp()
    {
        SpBar.value = (float)Sp / MaxSp;
        SpText.SetText($"{Sp}/{MaxSp}");
    }

    public void ShowResult()
    {
        int nextExp = ExpList[Level - 1] - Exp;
        BattleResult.Show(nextExp);
    }

    public override void Attack(Character target)
    {
        if (target is Enemy)
        {
            if (EnemyImageAnimator == null)
            {
                EnemyImageAnimator = EnemyImagePrefabManager.EnemyImageAnimator;
            }

            EnemyImageAnimator.SetBool("isAttacked", true);
            BattleSounds.PlayAttack();
            target.Hp -= 4;
        }
        else
        {
            throw new InvalidTargetException("Target is not Enemy");
        }
    }

    public override void ShowAllStatus()
    {
        BattleResult.Hide();
        base.ShowAllStatus();
        ShowSp();
    }

    public override void ShowName() => NameText.SetText(Name);

    public override void ShowLevel() => LevelText.SetText($"Lv.{Level}");

    public override void ShowHp()
    {
        HpBar.value = (float)Hp / MaxHp;
        HpText.SetText($"{Hp}/{MaxHp}");
    }
}
