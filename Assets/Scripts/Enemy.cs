using UnityEngine;

public class Enemy : Character
{
    override public int HpBarWidth { get; } = 1240;

    public Enemy(string name, int hp) : base(name, hp) { }

    override public void Attack(Character target)
    {
        target.Hp -= 3;
    }

    override public void RenderHpBar()
    {
        Vector2 sizeDelta = GameObject.Find("/Battle UI/Enemy/Status/HP/Bar/Current").GetComponent<RectTransform>().sizeDelta;
        sizeDelta.x = HpBarWidth * (float)Hp / MaxHp;
        GameObject.Find("/Battle UI/Enemy/Status/HP/Bar/Current").GetComponent<RectTransform>().sizeDelta = sizeDelta;
    }

    override public void ResetHpBar()
    {
        Vector2 sizeDelta = GameObject.Find("/Battle UI/Enemy/Status/HP/Bar/Current").GetComponent<RectTransform>().sizeDelta;
        sizeDelta.x = HpBarWidth;
        GameObject.Find("/Battle UI/Enemy/Status/HP/Bar/Current").GetComponent<RectTransform>().sizeDelta = sizeDelta;
    }
}