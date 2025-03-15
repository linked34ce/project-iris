using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Select : MonoBehaviour
{
    [SerializeField] private Button button1;
    public Button Button1 => button1;

    [SerializeField] private Button button2;
    public Button Button2 => button2;

    public string DungeonName { get; } = Dungeons.DisplayNames[Status.DungeonName];

    void Awake()
    {
        GameObject.Find("/UI/SelectBox/Button1/Text").GetComponent<TMP_Text>().SetText($"{DungeonName}を探索する");
        Button1.onClick.AddListener(() =>
            Initiate.Fade("Scenes/Dungeons/ToOhGakuenOldBuilding/1stFloor", Color.black, 1f)
        );
    }
}
