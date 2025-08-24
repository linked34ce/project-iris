using UnityEngine;

public class UiStateManager : MonoBehaviour
{
    [SerializeField] private Canvas _dungeonUiCanvas;
    [SerializeField] private Canvas _battleUiCanvas;
    [SerializeField] private BattleUiManager _battleUiManager;
    [SerializeField] private CameraController _cameraController;

    private UiState _uiState;

    public UiState UiState
    {
        get => _uiState;
        set
        {
            _uiState = value;
            switch (_uiState)
            {
                case UiState.None:
                    break;
                case UiState.Dungeon:
                    EnableDungeonUi();
                    break;
                case UiState.Battle:
                    EnableBattleUi();
                    break;
            }
            Debug.Log($"UiState: {_uiState}");
        }
    }

    void Awake()
    {
        if (UiState == UiState.None)
        {
            UiState = UiState.Dungeon;
        }
    }

    private void EnableDungeonUi()
    {
        _battleUiManager.enabled = false;
        _battleUiCanvas.enabled = false;
        _dungeonUiCanvas.enabled = true;
        _cameraController.enabled = true;
    }

    private void EnableBattleUi()
    {
        _cameraController.enabled = false;
        _dungeonUiCanvas.enabled = false;
        _battleUiCanvas.enabled = true;
        _battleUiManager.enabled = true;
    }
}
