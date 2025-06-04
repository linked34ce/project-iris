using TMPro;

using UnityEngine;

public class BattleResult : SingletonMonoBehaviour<BattleResult>
{
    [SerializeField] private TMP_Text _nextExpTitle;
    public TMP_Text NextExpTitle => _nextExpTitle;
    [SerializeField] private TMP_Text _nextExpValue;
    public TMP_Text NextExpValue => _nextExpValue;
    [SerializeField] private TMP_Text _levelUp;
    public TMP_Text LevelUp => _levelUp;

    public void Show(int nextExp)
    {
        NextExpValue.SetText($"{nextExp}");
        NextExpTitle.enabled = true;
        NextExpValue.enabled = true;
    }

    public void ShowLevelUp()
    {
        LevelUp.enabled = true;
    }

    public void Hide()
    {
        NextExpTitle.enabled = false;
        NextExpValue.enabled = false;
        LevelUp.enabled = false;
    }
}
