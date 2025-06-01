using TMPro;

using UnityEngine;

public class BattleResult : MonoBehaviour
{
    [SerializeField] private TMP_Text _nextExpTitle;
    public TMP_Text NextExpTitle => _nextExpTitle;
    [SerializeField] private TMP_Text _nextExpValue;
    public TMP_Text NextExpValue => _nextExpValue;
    [SerializeField] private TMP_Text _levelUp;
    public TMP_Text LevelUp => _levelUp;

    private static BattleResult s_instance;
    public static BattleResult Instance
    {
        get
        {
            if (null == s_instance)
            {
                s_instance = (BattleResult)FindAnyObjectByType(typeof(BattleResult));
                if (null == s_instance)
                {
                    Debug.Log("BattleResult Instance Error");
                }
            }
            return s_instance;
        }
    }

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
