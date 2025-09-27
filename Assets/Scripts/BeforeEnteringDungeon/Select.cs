using TMPro;

using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

[ExcludeFromCoverage]
public class Select : MonoBehaviour
{
    [SerializeField] private Button _button1;
    public Button Button1 => _button1;

    [SerializeField] private TMP_Text _button1Text;
    public TMP_Text Button1Text => _button1Text;

    [SerializeField] private Button _button2;
    public Button Button2 => _button2;
    [SerializeField] private SceneLoader _sceneLoader;

    private readonly Dungeons _dungeons = new();
    public string DungeonName => _dungeons.DisplayNames[Status.DungeonName];

    private readonly Logger _logger = new();

    private const string DungeonScene = "Scenes/Dungeons/TohoGakuenOldBuilding/1stFloor";

    void Awake()
    {
        Button1Text.SetText($"{DungeonName}を探索する");
        Button1.onClick.AddListener(() => _sceneLoader.LoadScene(DungeonScene));
        Button2.onClick.AddListener(() => _logger.Debug("Button2 is selected"));
    }
}
