using TMPro;

using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private int _dropExp;
    public int DropExp => _dropExp;
    [SerializeField] private string _imageAddress;
    public string ImageAddress => _imageAddress;

    [SerializeField] private RectTransform _hpBarBackground;
    public RectTransform HpBarBackground => _hpBarBackground;
    [SerializeField] private RectTransform _hpBarFill;
    public RectTransform HpBarFill => _hpBarFill;
    [SerializeField] private TMP_Text _nameText;
    public TMP_Text NameText => _nameText;
    [SerializeField] private TMP_Text _levelText;
    public TMP_Text LevelText => _levelText;

    public void ResetStatus() => Hp = MaxHp;

    private async void ShowImage() => await EnemyImagePrefabManager
                                            .Instance
                                            .LoadPrefab(ImageAddress);

    public void HideImage() => EnemyImagePrefabManager.Instance.DestroyPrefab();

    public override void Attack(Character target, int damage)
    {
        if (target is Player)
        {
            target.TakeDamage(damage);
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
