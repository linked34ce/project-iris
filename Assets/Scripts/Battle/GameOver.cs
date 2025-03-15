using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button button1;
    public Button Button1 => button1;

    [SerializeField] private Button button2;
    public Button Button2 => button2;

    public string DungeonName { get; } = Dungeons.DisplayNames[Status.DungeonName];

    void Awake()
    {
        Button2.onClick.AddListener(() =>
            Initiate.Fade("Scenes/Menu/BeforeEnteringDungeon", Color.black, 1f)
        );
    }
}
