using UnityEngine;

public class UIStateManager : MonoBehaviour
{
    [SerializeField] private Canvas _dungeonUI;
    public Canvas DungeonUI => _dungeonUI;
    [SerializeField] private Canvas _battleUI;
    public Canvas BattleUI => _battleUI;

    [SerializeField] private BattleManager _battleManager;
    public BattleManager BattleManager => _battleManager;

    [SerializeField] private CameraController _cameraController;
    public CameraController CameraController => _cameraController;

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

    private static UIStateManager s_instance;

    public static UIStateManager Instance
    {
        get
        {
            if (null == s_instance)
            {
                s_instance = (UIStateManager)FindAnyObjectByType(typeof(UIStateManager));
                if (null == s_instance)
                {
                    Debug.Log("UIStateManager Instance Error");
                }
            }
            return s_instance;
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
        BattleManager.enabled = false;
        BattleUI.enabled = false;
        DungeonUI.enabled = true;
        CameraController.enabled = true;
    }

    public void EnableBattleUI()
    {
        CameraController.enabled = false;
        DungeonUI.enabled = false;
        BattleUI.enabled = true;
        BattleManager.enabled = true;
    }
}
