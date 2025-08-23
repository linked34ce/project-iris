using TMPro;

using UnityEngine;

public class BattleResult : MonoBehaviour, IBattleResult
{
    [SerializeField] private TMP_Text _nextExpTitle;
    [SerializeField] private TMP_Text _nextExpValue;
    [SerializeField] private TMP_Text _levelUp;

    public bool IsShown { get; private set; } = false;
    public event System.Action Confirmed;
    private bool _isConfirmed = false;
    private int _showFrame;

    void Update()
    {
        if (!IsShown || Time.frameCount == _showFrame)
        {
            return;
        }

        if (!_isConfirmed)
        {
            if (!Input.GetKey(KeyCode.Return))
            {
                _isConfirmed = true;
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Confirmed?.Invoke();
        }
    }

    private void ShowNextExp(int nextExp)
    {
        _nextExpValue.SetText($"{nextExp}");
        _nextExpTitle.enabled = true;
        _nextExpValue.enabled = true;
    }

    public void Show(IPlayer player)
    {
        IsShown = true;
        _isConfirmed = false;
        _showFrame = Time.frameCount;

        if (player.Data.HasLeveledUp)
        {
            _levelUp.enabled = true;
        }

        player.ShowResult();
        ShowNextExp(player.Data.NextExp);
    }

    public void Hide()
    {
        _nextExpTitle.enabled = false;
        _nextExpValue.enabled = false;
        _levelUp.enabled = false;
        IsShown = false;
        _isConfirmed = false;
    }
}
