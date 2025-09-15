using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

[ExcludeFromCoverage]
public class GameOver : MonoBehaviour
{
    [SerializeField] private Button _button1;
    [SerializeField] private Button _button2;
    [SerializeField] private SceneLoader _sceneLoader;

    private readonly Logger _logger = new();

    private const string TitleSceneName = "Scenes/Menu/BeforeEnteringDungeon";

    void OnEnable()
    {
        _button1.onClick.AddListener(() => _logger.Debug("Button1 is selected"));
        _button2.onClick.AddListener(() => _sceneLoader.LoadScene(TitleSceneName));
    }
}
