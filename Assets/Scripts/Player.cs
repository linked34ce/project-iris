using TMPro;
using UnityEngine;

public class Player : Character
{
    public int Sp { get; set; }
    public int MaxSp { get; set; }
    override public int HpBarWidth { get; } = 300;

    public Player(string name, int level, int hp, int sp) : base(name, level, hp)
    {
        Sp = sp;
    }

    public void ShowAllStatus()
    {
        ShowLevel();
        ShowHp();
        ShowMaxHp();
    }

    public void ShowHp()
    {
        GameObject.Find("/Battle UI/Panel/Status/Attacker 1/HP/Value/Current").GetComponent<TMP_Text>().SetText($"{Hp}");
    }

    public void ShowMaxHp()
    {
        GameObject.Find("/Battle UI/Panel/Status/Attacker 1/HP/Value/Max").GetComponent<TMP_Text>().SetText($"{MaxHp}");
    }

    override public void Attack(Character target)
    {
        target.Hp -= 5;
        target.RenderHpBar();
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