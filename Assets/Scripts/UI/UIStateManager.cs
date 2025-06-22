using UnityEngine;

public class UIStateManager : SingletonMonoBehaviour<UIStateManager>
{
    [SerializeField] private Canvas _dungeonUICanvas;
    [SerializeField] private Canvas _battleUICanvas;

    private UIState _uiState;

    public UIState UIState
    {
        get => _uiState;
        set
        {
            _uiState = value;
            switch (_uiState)
            {
                case UIState.None:
                    break;
                case UIState.Dungeon:
                    EnableDungeonUI();
                    break;
                case UIState.Battle:
                    EnableBattleUI();
                    break;
            }
            Debug.Log($"UIState: {_uiState}");
        }
    }

    protected override void Awake()
    {
        base.Awake();

        if (UIState == UIState.None)
        {
            UIState = UIState.Dungeon;
        }
    }

    public void EnableDungeonUI()
    {
        BattleManager.Instance.enabled = false;
        _battleUICanvas.enabled = false;
        _dungeonUICanvas.enabled = true;
        CameraController.Instance.enabled = true;
    }

    public void EnableBattleUI()
    {
        CameraController.Instance.enabled = false;
        _dungeonUICanvas.enabled = false;
        _battleUICanvas.enabled = true;
        BattleManager.Instance.enabled = true;
    }
}
