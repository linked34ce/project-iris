using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button _button1;
    [SerializeField] private Button _button2;
    [SerializeField] private SceneLoader _sceneLoader;

    private const string TitleSceneName = "Scenes/Menu/BeforeEnteringDungeon";

    void OnEnable()
    {
        _button1.onClick.AddListener(() => Debug.Log("Button1 is selected"));
        _button2.onClick.AddListener(() => _sceneLoader.LoadScene(TitleSceneName));
    }
}
