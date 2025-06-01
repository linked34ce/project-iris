using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommandWindow : MonoBehaviour
{
    [SerializeField] private Image _background;
    public Image Background => _background;

    [SerializeField] private SerializableDictionary<Command, CommandButton> _commands;
    public Dictionary<Command, CommandButton> Commands => _commands;

    [SerializeField] private EventSystem _battleEventSystem;
    public EventSystem BattleEventSystem => _battleEventSystem;
    [SerializeField] private BattleUISounds _battleUISounds;
    public BattleUISounds BattleUISounds => _battleUISounds;

    public GameObject SelectedButton { get; private set; }
    public bool IsVisible { get; set; } = false;

    private static CommandWindow s_instance;
    public static CommandWindow Instance
    {
        get
        {
            if (null == s_instance)
            {
                s_instance = (CommandWindow)FindAnyObjectByType(typeof(CommandWindow));
                if (null == s_instance)
                {
                    Debug.Log("CommandWindow Instance Error");
                }
            }
            return s_instance;
        }
    }

    void Awake()
    {
        BattleEventSystem.enabled = true;
        SelectedButton = EventSystem.current.currentSelectedGameObject;
    }

    public void PlayButtonSelect()
    {
        if (SelectedButton != EventSystem.current.currentSelectedGameObject)
        {
            SelectedButton = EventSystem.current.currentSelectedGameObject;
            BattleUISounds.PlayButtonSelect();
        }
    }

    public void Show()
    {
        IsVisible = true;
        Background.enabled = true;
        foreach (var command in Commands.Values)
        {
            command.ShowCommand();
        }
    }

    public void Hide()
    {
        IsVisible = false;
        Background.enabled = false;
        foreach (var command in Commands.Values)
        {
            command.HideCommand();
        }
    }
}
