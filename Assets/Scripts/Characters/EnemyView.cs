using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class EnemyView : MonoBehaviour, IEnemyView
{
    [SerializeField] private RectTransform _hpBarBackground;
    [SerializeField] private RectTransform _hpBarFill;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _levelText;

    [SerializeField] private RawImage _image;

    private const int MaxOpacity = 255;

    public void ShowImage()
    {
        Color32 color = _image.color;
        color.a = MaxOpacity;
        _image.color = color;
    }

    public void ShowName(string name) => _nameText.SetText(name);

    public void ShowLevel(int level) => _levelText.SetText($"Lv.{level}");

    public void ShowHp(int hp, int maxHp)
    {
        float width = _hpBarBackground.sizeDelta.x;
        Vector2 anchoredPosition = _hpBarFill.anchoredPosition;
        anchoredPosition.x = -width * (maxHp - hp) / maxHp;
        _hpBarFill.anchoredPosition = anchoredPosition;
    }
}
