using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button _button1;
    public Button Button1 => _button1;

    [SerializeField] private Button _button2;
    public Button Button2 => _button2;

    public string DungeonName { get; } = Dungeons.DisplayNames[Status.DungeonName];

    private const float FadeDuration = 1f;

    void Awake()
    {
        Button1.onClick.AddListener(() =>
            Debug.Log("Button1 is selected")
        );

        Button2.onClick.AddListener(() =>
            Initiate.Fade("Scenes/Menu/BeforeEnteringDungeon", Color.black, FadeDuration)
        );
    }
}
