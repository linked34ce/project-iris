using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CommandButton : SingletonMonoBehaviour<CommandButton>
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _label;

    public void SetEvent(UnityAction call)
    {
        _button.onClick.AddListener(call);
    }

    public void ShowCommand()
    {
        _button.enabled = true;
        _image.enabled = true;
        _label.enabled = true;
    }

    public void HideCommand()
    {
        _button.enabled = false;
        _image.enabled = false;
        _label.enabled = false;
    }
}
