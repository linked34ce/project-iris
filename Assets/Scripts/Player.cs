using UnityEngine;

public class Player : Character
{
    public int Sp { get; set; }
    public int MaxSp { get; set; }
    override public int HpBarWidth { get; } = 300;

    public Player(string name, int hp, int sp) : base(name, hp)
    {
        Sp = sp;
    }

    override public void Attack(Character target)
    {
        target.Hp -= 5;
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