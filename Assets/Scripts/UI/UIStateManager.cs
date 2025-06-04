using UnityEngine;

public class UIStateManager : SingletonMonoBehaviour<UIStateManager>
{
    [SerializeField] private Canvas _dungeonUICanvas;
    public Canvas DungeonUICanvas => _dungeonUICanvas;
    [SerializeField] private Canvas _battleUICanvas;
    public Canvas BattleUICanvas => _battleUICanvas;

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
        BattleUICanvas.enabled = false;
        DungeonUICanvas.enabled = true;
        CameraController.Instance.enabled = true;
    }

    public void EnableBattleUI()
    {
        CameraController.Instance.enabled = false;
        DungeonUICanvas.enabled = false;
        BattleUICanvas.enabled = true;
        BattleManager.Instance.enabled = true;
    }
}
