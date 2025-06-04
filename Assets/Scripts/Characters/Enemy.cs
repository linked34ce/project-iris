using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    [SerializeField] private int _dropExp;
    public int DropExp => _dropExp;

    [SerializeField] private RawImage _image;
    public RawImage Image => _image;
    [SerializeField] private Animator _imageAnimator;
    public Animator ImageAnimator => _imageAnimator;

    [SerializeField] private RectTransform _hpBarBackground;
    public RectTransform HpBarBackground => _hpBarBackground;
    [SerializeField] private RectTransform _hpBarFill;
    public RectTransform HpBarFill => _hpBarFill;
    [SerializeField] private TMP_Text _nameText;
    public TMP_Text NameText => _nameText;
    [SerializeField] private TMP_Text _levelText;
    public TMP_Text LevelText => _levelText;

    private const int MaxOpacity = 255;

    public bool IsAttacked
    {
        get => ImageAnimator.GetBool("isAttacked");
        set => ImageAnimator.SetBool("isAttacked", value);
    }

    private void ShowImage()
    {
        Color32 color = Image.color;
        color.a = MaxOpacity;
        Image.color = color;
    }

    public override void Attack(Character target, int damage)
    {
        if (target is Player player)
        {
            player.TakeDamage(damage);
        }
        else
        {
            Debug.LogError("Target is not Player.");
        }
    }

    public override void ShowAllStatus()
    {
        base.ShowAllStatus();
        ShowImage();
    }

    public override void ShowName() => NameText.SetText(Name);

    public override void ShowLevel() => LevelText.SetText($"Lv.{Level}");

    public override void ShowHp()
    {
        float width = HpBarBackground.sizeDelta.x;
        Vector2 anchoredPosition = HpBarFill.anchoredPosition;
        anchoredPosition.x = -width * (MaxHp - Hp) / MaxHp;
        HpBarFill.anchoredPosition = anchoredPosition;
    }
}
