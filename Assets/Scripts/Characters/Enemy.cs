using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    [SerializeField] private int _dropExp;
    public int DropExp => _dropExp;
    [SerializeField] private string _imageFileName;
    public string ImageFileName => _imageFileName;

    [SerializeField] private RectTransform _hpBarBackground;
    public RectTransform HpBarBackground => _hpBarBackground;
    [SerializeField] private RectTransform _hpBarFill;
    public RectTransform HpBarFill => _hpBarFill;
    [SerializeField] private RawImage _enemyImage;
    public RawImage EnemyImage => _enemyImage;
    [SerializeField] private RectTransform _enemyImageRectTransform;
    public RectTransform EnemyImageRectTransform => _enemyImageRectTransform;

    [SerializeField] private TMP_Text _nameText;
    public TMP_Text NameText => _nameText;
    [SerializeField] private TMP_Text _levelText;
    public TMP_Text LevelText => _levelText;

    public Vector2 DefaultPosition { get; private set; }

    private const int MAX_OPACITY = 255;
    private const int MIN_OPACITY = 0;

    protected override void Awake()
    {
        base.Awake();
        LoadImage();
        ResetStatus();
    }

    public void ResetStatus()
    {
        Hp = MaxHp;
        ShowAllStatus();
    }

    private void LoadImage()
    {
        EnemyImage.texture = Resources.Load<Texture2D>($"Enemies/{ImageFileName}");
        DefaultPosition = EnemyImageRectTransform.anchoredPosition;
    }

    private void ShowImage()
    {
        Color32 color = EnemyImage.color;
        color.a = MAX_OPACITY;
        EnemyImage.color = color;
    }

    public void HideImage()
    {
        Color32 color = EnemyImage.color;
        color.a = MIN_OPACITY;
        EnemyImage.color = color;
        ResetEnemyImagePosition();
    }

    private void ResetEnemyImagePosition() => EnemyImageRectTransform.anchoredPosition = DefaultPosition;

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