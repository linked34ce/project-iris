using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    // this property should be deleted when class for each role is made
    public string Role { get; }

    private int _exp;
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

    private int _sp;
    public int Sp
    {
        get => _sp;
        set => _sp = Mathf.Clamp(value, 0, MaxSp);
    }

    public int MaxSp { get; private set; }
    override public int HpBarWidth { get; } = 300;

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

    public Player(string name, string role, int exp, int level) : base(name, level)
    {
        Role = role;
        Exp = exp;
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
        GameObject.Find("/Battle UI/Panel/Attacker 1/Result/Level Up").SetActive(true);

        // Debug.Log($"Lv: {Level}, HP: {Hp}, SP: {Sp}, ATK: {Atk}, MAG: {Mag}, DEF: {Def}, RES: {Res}, AGI: {Agi}, LUK: {Luk}");
    }

    public void ShowRole()
    {
        Image roleIcon = GameObject.Find("/Battle UI/Panel/Attacker 1/Basic/Role").GetComponent<Image>();
        roleIcon.sprite = Resources.Load<Sprite>($"Icons/{Role}");
        roleIcon.color = GetColorCodeForRole(Role);
    }

    public void ShowHp()
    {
        GameObject.Find("/Battle UI/Panel/Attacker 1/Status/HP/Value/Current").GetComponent<TMP_Text>().SetText($"{Hp}");
    }

    public void ShowMaxHp()
    {
        GameObject.Find("/Battle UI/Panel/Attacker 1/Status/HP/Value/Max").GetComponent<TMP_Text>().SetText($"{MaxHp}");
    }

    public void ShowSp()
    {
        GameObject.Find("/Battle UI/Panel/Attacker 1/Status/SP/Value/Current").GetComponent<TMP_Text>().SetText($"{Sp}");
    }

    public void ShowMaxSp()
    {
        GameObject.Find("/Battle UI/Panel/Attacker 1/Status/SP/Value/Max").GetComponent<TMP_Text>().SetText($"{MaxSp}");
    }

    public void RenderSpBar()
    {
        Vector2 sizeDelta = GameObject.Find("/Battle UI/Panel/Attacker 1/Status/SP/Bar/Current").GetComponent<RectTransform>().sizeDelta;
        sizeDelta.x = HpBarWidth * (float)Sp / MaxSp;
        GameObject.Find("/Battle UI/Panel/Attacker 1/Status/SP/Bar/Current").GetComponent<RectTransform>().sizeDelta = sizeDelta;
    }

    public void ResetSpBar()
    {
        Vector2 sizeDelta = GameObject.Find("/Battle UI/Panel/Attacker 1/Status/SP/Bar/Current").GetComponent<RectTransform>().sizeDelta;
        sizeDelta.x = HpBarWidth;
        GameObject.Find("/Battle UI/Panel/Attacker 1/Status/SP/Bar/Current").GetComponent<RectTransform>().sizeDelta = sizeDelta;
    }

    public void ShowStatus()
    {
        GameObject.Find("/Battle UI/Panel/Attacker 1/Status").SetActive(true);
        GameObject.Find("/Battle UI/Panel/Attacker 1/Result").SetActive(false);
        GameObject.Find("/Battle UI/Panel/Attacker 1/Result/Level Up").SetActive(false);
    }

    public void ShowResult()
    {
        GameObject.Find("/Battle UI/Panel/Attacker 1/Status").SetActive(false);
        GameObject.Find("/Battle UI/Panel/Attacker 1/Result").SetActive(true);

        GameObject.Find("/Battle UI/Panel/Attacker 1/Result/EXP/Value").GetComponent<TMP_Text>().SetText($"{Exp}");

        int expToNextLevel = ExpList[Level - 1] - Exp;
        GameObject.Find("/Battle UI/Panel/Attacker 1/Result/Next Level/Value").GetComponent<TMP_Text>().SetText($"{expToNextLevel}");
    }

    // this method should be deleted when class for each role is made 
    public Color32 GetColorCodeForRole(string role)
    {
        return role switch
        {
            "attacker" => new(255, 73, 73, 255),
            "tank" => new(45, 241, 255, 255),
            "healer" => new(0, 255, 75, 255),
            _ => new(0, 0, 0, 255),
        };
    }

    override public void Attack(Character target)
    {
        if (target is Enemy)
        {
            target.Hp -= 4;
            target.RenderHpBar();
        }
        else
        {
            throw new InvalidTargetException("Target is not Enemy");
        }
    }

    override public void ShowAllStatus()
    {
        ShowStatus();
        RenderHpBar();
        RenderSpBar();
        ShowRole();
        ShowName();
        ShowLevel();
        ShowHp();
        ShowMaxHp();
        ShowSp();
        ShowMaxSp();
    }

    override public void ShowName()
    {
        GameObject.Find("/Battle UI/Panel/Attacker 1/Basic/Name").GetComponent<TMP_Text>().SetText(Name);
    }

    override public void ShowLevel()
    {
        GameObject.Find("/Battle UI/Panel/Attacker 1/Basic/Level").GetComponent<TMP_Text>().SetText($"Lv.{Level}");
    }

    override public void RenderHpBar()
    {
        Vector2 sizeDelta = GameObject.Find("/Battle UI/Panel/Attacker 1/Status/HP/Bar/Current").GetComponent<RectTransform>().sizeDelta;
        sizeDelta.x = HpBarWidth * (float)Hp / MaxHp;
        GameObject.Find("/Battle UI/Panel/Attacker 1/Status/HP/Bar/Current").GetComponent<RectTransform>().sizeDelta = sizeDelta;
    }

    override public void ResetHpBar()
    {
        Vector2 sizeDelta = GameObject.Find("/Battle UI/Panel/Attacker 1/Status/HP/Bar/Current").GetComponent<RectTransform>().sizeDelta;
        sizeDelta.x = HpBarWidth;
        GameObject.Find("/Battle UI/Panel/Attacker 1/Status/HP/Bar/Current").GetComponent<RectTransform>().sizeDelta = sizeDelta;
    }
}