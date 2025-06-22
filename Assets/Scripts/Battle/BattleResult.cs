using TMPro;

using UnityEngine;

public class BattleResult : SingletonMonoBehaviour<BattleResult>
{
    [SerializeField] private TMP_Text _nextExpTitle;
    [SerializeField] private TMP_Text _nextExpValue;
    [SerializeField] private TMP_Text _levelUp;

    public void Show(int nextExp)
    {
        _nextExpValue.SetText($"{nextExp}");
        _nextExpTitle.enabled = true;
        _nextExpValue.enabled = true;
    }

    public void ShowLevelUp()
    {
        _levelUp.enabled = true;
    }

    public void Hide()
    {
        _nextExpTitle.enabled = false;
        _nextExpValue.enabled = false;
        _levelUp.enabled = false;
    }
}
