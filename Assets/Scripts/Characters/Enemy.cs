using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    public string ImageFileName { get; }
    public RawImage Image { get; private set; }
    public int DropExp { get; }

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

    public override void Attack(Character target)
    {
        if (target is Player)
        {
            target.Hp -= 5;
        }
        else
        {
            throw new InvalidTargetException("Target is not Player");
        }
    }

    public override void ShowAllStatus()
    {
        base.ShowAllStatus();
        ShowImage();
    }

    public override void ShowName() => GameObject.Find("/BattleUI/Enemy/Status/Name").GetComponent<TMP_Text>().SetText(Name);

    public override void ShowLevel() => GameObject.Find("/BattleUI/Enemy/Status/Level").GetComponent<TMP_Text>().SetText($"Lv.{Level}");

    public override void ShowHp()
    {
        float width = GameObject.Find("/BattleUI/Enemy/Status/HP/Background").GetComponent<RectTransform>().sizeDelta.x;
        GameObject fill = GameObject.Find("/BattleUI/Enemy/Status/HP/Mask/Fill");
        Vector2 anchoredPosition = fill.GetComponent<RectTransform>().anchoredPosition;
        anchoredPosition.x = -width * (MaxHp - Hp) / MaxHp;
        fill.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
    }
}