using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class Select : SingletonMonoBehaviour<Select>
{
    [SerializeField] private Button _button1;
    public Button Button1 => _button1;

    [SerializeField] private TMP_Text _button1Text;
    public TMP_Text Button1Text => _button1Text;

    [SerializeField] private Button _button2;
    public Button Button2 => _button2;

    public string DungeonName => Dungeons.DisplayNames[Status.DungeonName];

    private const float FadeDuration = 1f;
    private const string DungeonScene = "Scenes/Dungeons/TohoGakuenOldBuilding/1stFloor";

    protected override void Awake()
    {
        base.Awake();
        Button1Text.SetText($"{DungeonName}を探索する");

        Button1.onClick.AddListener(() =>
            Initiate.Fade(DungeonScene, Color.black, FadeDuration)
        );

        Button2.onClick.AddListener(() =>
            Debug.Log("Button2 is selected")
        );
    }
}
