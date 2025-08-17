using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class PlayerView : MonoBehaviour, IPlayerView
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Slider _hpBar;
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private Slider _spBar;
    [SerializeField] private TMP_Text _spText;

    [SerializeField] private PlayerPortraitLoader _playerPortraitLoader;

    public void ShowName(string name) => _nameText.SetText(name);

    public void ShowLevel(int level) => _levelText.SetText($"Lv.{level}");

    public void ShowHp(int hp, int maxHp)
    {
        _hpBar.value = (float)hp / maxHp;
        _hpText.SetText($"{hp}/{maxHp}");
    }

    public void ShowSp(int sp, int maxSp)
    {
        _spBar.value = (float)sp / maxSp;
        _spText.SetText($"{sp}/{maxSp}");
    }

    public async void ShowPortrait() => await _playerPortraitLoader.Create();

    public void ShowLevelUp()
    {
        BattleResult.Instance.ShowLevelUp();
    }
}
