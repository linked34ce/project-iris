using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CommandButton : SingletonMonoBehaviour<CommandButton>
{
    [SerializeField] private Button _button;
    public Button Button => _button;
    [SerializeField] private Image _image;
    public Image Image => _image;
    [SerializeField] private TMP_Text _label;
    public TMP_Text Label => _label;

    public void SetEvent(UnityAction call)
    {
        Button.onClick.AddListener(call);
    }

    public void ShowCommand()
    {
        Button.enabled = true;
        Image.enabled = true;
        Label.enabled = true;
    }

    public void HideCommand()
    {
        Button.enabled = false;
        Image.enabled = false;
        Label.enabled = false;
    }
}
