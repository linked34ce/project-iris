using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommandWindow : SingletonMonoBehaviour<CommandWindow>
{
    [SerializeField] private Image _background;
    public Image Background => _background;

    [SerializeField] private SerializableDictionary<Command, CommandButton> _commands;
    public Dictionary<Command, CommandButton> Commands => _commands;

    [SerializeField] private EventSystem _battleEventSystem;
    public EventSystem BattleEventSystem => _battleEventSystem;

    public GameObject SelectedButton { get; private set; }
    public bool IsVisible { get; set; } = false;

    protected override void Awake()
    {
        base.Awake();
        BattleEventSystem.enabled = true;
        SelectedButton = EventSystem.current.currentSelectedGameObject;
    }

    public void PlayButtonSelect()
    {
        if (SelectedButton != EventSystem.current.currentSelectedGameObject)
        {
            SelectedButton = EventSystem.current.currentSelectedGameObject;
            BattleUISounds.Instance.PlayButtonSelect();
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
