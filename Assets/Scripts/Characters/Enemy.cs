using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    public string ImageFileName { get; }
    public RawImage Image { get; private set; }
    public int DropExp { get; }
    override public int HpBarWidth { get; } = 1240;

    public Enemy(string name, string imageFileName, int dropExp, int level, int hp) : base(name, level, hp)
    {
        ImageFileName = imageFileName;
        DropExp = dropExp;
    }

    public void ShowImage()
    {
        if (!Image)
        {
            Image = GameObject.Find("/Battle UI/Enemy/Image").GetComponent<RawImage>();
            Image.texture = Resources.Load<Texture2D>($"Enemies/{ImageFileName}");
        }

        Color32 color = Image.color;

        if (color.a == 0)
        {
            color.a = 255;
            Image.color = color;
        }
    }

    public void HideImage()
    {
        Color32 color = Image.color;

        if (color.a != 0)
        {
            color.a = 0;
            Image.color = color;
        }
    }

    override public void Attack(Character target)
    {
        if (target is Player)
        {
            target.Hp -= 10;
            target.RenderHpBar();
            (target as Player).ShowHp();
        }
        else
        {
            throw new InvalidTargetException("Target is not Player");
        }
    }

    override public void ShowAllStatus()
    {
        ShowImage();
        ShowName();
        ShowLevel();
    }

    override public void ShowName()
    {
        GameObject.Find("/Battle UI/Enemy/Status/Name").GetComponent<TMP_Text>().SetText(Name);
    }

    override public void ShowLevel()
    {
        GameObject.Find("/Battle UI/Enemy/Status/Level").GetComponent<TMP_Text>().SetText($"Lv.{Level}");
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