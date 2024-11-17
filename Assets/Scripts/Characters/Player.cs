using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public string Role { get; }  // this property should be deleted when class for each role is made
    private int _exp;
    public int Exp
    {
        get
        {
            return _exp;
        }
        set
        {
            _exp = value;
            if (_exp >= expList[Level - 1])
            {
                LevelUp();
            }
        }
    }
    public int Sp { get; set; }
    public int MaxSp { get; set; }
    override public int HpBarWidth { get; } = 300;
    // this property should be refined
    private readonly int[] expList = {
         10, 30, 60, 100, 150, 210, 280, 360, 450, 550,
    };
    // this property should be deleted when class for each character is made
    private readonly Dictionary<int, int> hpList = new(){
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
    // this property should be deleted when class for each character is made
    private readonly Dictionary<int, int> spList = new(){
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

    public Player(string name, string role, int exp, int level, int hp, int sp) : base(name, level, hp)
    {
        Role = role;
        Exp = exp;
        Sp = sp;
    }

    private void LevelUp()
    {
        Level++;

        MaxHp = hpList[Level];
        MaxSp = spList[Level];
        // Atk = atkList[Level];
        // Def = defList[Level];


        if (hpList[Level - 1] < MaxHp)
        {
            // show the max hp has been increased
        }

        if (spList[Level - 1] < MaxSp)
        {
            // show the max sp has been increased
        }

        Hp = MaxHp;
        Sp = MaxSp;

        RenderHpBar();
        RenderSpBar();
        ShowLevel();
        ShowHp();
        ShowMaxHp();
        ShowSp();
        ShowMaxSp();
    }

    public void ShowRole()
    {
        Image roleIcon = GameObject.Find("/Battle UI/Panel/Status/Attacker 1/Basic/Role").GetComponent<Image>();
        roleIcon.sprite = Resources.Load<Sprite>($"Icons/{Role}");
        roleIcon.color = GetColorCodeForRole(Role);
    }

    public void ShowHp()
    {
        GameObject.Find("/Battle UI/Panel/Status/Attacker 1/HP/Value/Current").GetComponent<TMP_Text>().SetText($"{Hp}");
    }

    public void ShowMaxHp()
    {
        GameObject.Find("/Battle UI/Panel/Status/Attacker 1/HP/Value/Max").GetComponent<TMP_Text>().SetText($"{MaxHp}");
    }

    public void ShowSp()
    {
        GameObject.Find("/Battle UI/Panel/Status/Attacker 1/SP/Value/Current").GetComponent<TMP_Text>().SetText($"{Sp}");
    }

    public void ShowMaxSp()
    {
        GameObject.Find("/Battle UI/Panel/Status/Attacker 1/SP/Value/Max").GetComponent<TMP_Text>().SetText($"{MaxSp}");
    }

    public void RenderSpBar()
    {
        Vector2 sizeDelta = GameObject.Find("/Battle UI/Panel/Status/Attacker 1/SP/Bar/Current").GetComponent<RectTransform>().sizeDelta;
        sizeDelta.x = HpBarWidth * (float)Hp / MaxHp;
        GameObject.Find("/Battle UI/Panel/Status/Attacker 1/SP/Bar/Current").GetComponent<RectTransform>().sizeDelta = sizeDelta;
    }

    public void ResetSpBar()
    {
        Vector2 sizeDelta = GameObject.Find("/Battle UI/Panel/Status/Attacker 1/SP/Bar/Current").GetComponent<RectTransform>().sizeDelta;
        sizeDelta.x = HpBarWidth;
        GameObject.Find("/Battle UI/Panel/Status/Attacker 1/SP/Bar/Current").GetComponent<RectTransform>().sizeDelta = sizeDelta;
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
        GameObject.Find("/Battle UI/Panel/Status/Attacker 1/Basic/Name").GetComponent<TMP_Text>().SetText(Name);
    }

    override public void ShowLevel()
    {
        GameObject.Find("/Battle UI/Panel/Status/Attacker 1/Basic/Level").GetComponent<TMP_Text>().SetText($"Lv.{Level}");
    }

    override public void RenderHpBar()
    {
        Vector2 sizeDelta = GameObject.Find("/Battle UI/Panel/Status/Attacker 1/HP/Bar/Current").GetComponent<RectTransform>().sizeDelta;
        sizeDelta.x = HpBarWidth * (float)Hp / MaxHp;
        GameObject.Find("/Battle UI/Panel/Status/Attacker 1/HP/Bar/Current").GetComponent<RectTransform>().sizeDelta = sizeDelta;
    }

    override public void ResetHpBar()
    {
        Vector2 sizeDelta = GameObject.Find("/Battle UI/Panel/Status/Attacker 1/HP/Bar/Current").GetComponent<RectTransform>().sizeDelta;
        sizeDelta.x = HpBarWidth;
        GameObject.Find("/Battle UI/Panel/Status/Attacker 1/HP/Bar/Current").GetComponent<RectTransform>().sizeDelta = sizeDelta;
    }
}