using UnityEngine;

public class UIStateManager : MonoBehaviour
{
    [SerializeField] private Canvas _dungeonUICanvas;
    [SerializeField] private Canvas _battleUICanvas;
    [SerializeField] private BattleUIManager _battleUIManager;

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

    void Awake()
    {
        if (UIState == UIState.None)
        {
            UIState = UIState.Dungeon;
        }
    }

    public void EnableDungeonUI()
    {
        _battleUIManager.enabled = false;
        _battleUICanvas.enabled = false;
        _dungeonUICanvas.enabled = true;
        CameraController.Instance.enabled = true;
    }

    public void EnableBattleUI()
    {
        CameraController.Instance.enabled = false;
        _dungeonUICanvas.enabled = false;
        _battleUICanvas.enabled = true;
        _battleUIManager.enabled = true;
    }
}
