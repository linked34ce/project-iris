using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    public string ImageFileName { get; }
    public RawImage Image { get; private set; }
    public int DropExp { get; }
    override public int HpBarWidth { get; } = 1240;

    public Enemy(string name, string imageFileName, int dropExp, int level,
                int hp, int atk, int mag, int def, int res, int agi, int luk)
                : base(name, level, hp, atk, mag, def, res, agi, luk)
    {
        ImageFileName = imageFileName;
        DropExp = dropExp;
    }

    public void ShowImage()
    {
        if (!Image)
        {
            Image = GameObject.Find("/BattleUI/EnemyImage").GetComponent<RawImage>();
            Image.texture = Resources.Load<Texture2D>($"Enemies/{ImageFileName}");
        }

        Color32 color = Image.color;
        color.a = 255;
        Image.color = color;
    }

    public void HideImage()
    {
        Color32 color = Image.color;
        color.a = 0;
        Image.color = color;
    }

    override public void Attack(Character target)
    {
        if (target is Player)
        {
            target.Hp -= 5;
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
        ResetHpBar();
        ShowImage();
        ShowName();
        ShowLevel();
    }

    override public void ShowName() => GameObject.Find("/BattleUI/Enemy/Status/Name").GetComponent<TMP_Text>().SetText(Name);

    override public void ShowLevel() => GameObject.Find("/BattleUI/Enemy/Status/Level").GetComponent<TMP_Text>().SetText($"Lv.{Level}");

    override public void RenderHpBar()
    {
        float width = GameObject.Find("/BattleUI/Enemy/Status/HP/Background").GetComponent<RectTransform>().sizeDelta.x;
        GameObject fill = GameObject.Find("/BattleUI/Enemy/Status/HP/Mask/Fill");
        Vector2 anchoredPosition = fill.GetComponent<RectTransform>().anchoredPosition;
        anchoredPosition.x = -width * (MaxHp - Hp) / MaxHp;
        fill.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
    }

    override public void ResetHpBar()
    {
        GameObject fill = GameObject.Find("/BattleUI/Enemy/Status/HP/Mask/Fill");
        Vector2 anchoredPosition = fill.GetComponent<RectTransform>().anchoredPosition;
        anchoredPosition.x = 0;
        fill.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
    }
}