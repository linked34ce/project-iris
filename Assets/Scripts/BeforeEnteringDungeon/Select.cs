using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Select : MonoBehaviour
{

    [SerializeField] private GameObject select1;
    [SerializeField] private GameObject select2;
    public string DungeonName { get; } = Dungeons.DisplayNames[Status.DungeonName];
    public bool IsSelect1 { get; private set; } = true;

    void Awake()
    {
        GameObject.Find("/UI/Select Box/Select 1/Text").GetComponent<TMP_Text>().SetText($"{DungeonName}を探索する");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (IsSelect1)
                Initiate.Fade("Scenes/Dungeons/To-o Gakuen Old Building/1st Floor", Color.black, 1f);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (IsSelect1)
            {
                ChangeOpcaity(select2, 1);
                ChangeOpcaity(select1, 0);
            }
            else
            {
                ChangeOpcaity(select1, 1);
                ChangeOpcaity(select2, 0);
            }
            IsSelect1 = !IsSelect1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (IsSelect1)
            {
                ChangeOpcaity(select2, 1);
                ChangeOpcaity(select1, 0);
            }
            else
            {
                ChangeOpcaity(select1, 1);
                ChangeOpcaity(select2, 0);
            }
            IsSelect1 = !IsSelect1;
        }
    }

    public void ChangeOpcaity(GameObject select, float opacity)
    {
        Image selectBackground = select.transform.Find("Background").GetComponent<Image>();
        Color oldColor = selectBackground.color;
        Color newColor = new(oldColor.r, oldColor.g, oldColor.b, opacity);
        selectBackground.color = newColor;
    }
}
