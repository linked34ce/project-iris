using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommandWindow : SingletonMonoBehaviour<CommandWindow>, ICommandWindow
{
    [SerializeField] private Image _background;

    [SerializeField] private SerializableDictionary<Command, CommandButton> _commands;
    public SerializableDictionary<Command, CommandButton> Commands => _commands;

    [SerializeField] private EventSystem _battleEventSystem;

    private GameObject _selectedButton;
    public bool IsVisible { get; set; } = false;

    protected override void Awake()
    {
        base.Awake();
        _battleEventSystem.enabled = true;
        _selectedButton = EventSystem.current.currentSelectedGameObject;
    }

    public void PlayButtonSelect()
    {
        if (_selectedButton != EventSystem.current.currentSelectedGameObject)
        {
            _selectedButton = EventSystem.current.currentSelectedGameObject;
            BattleUISounds.Instance.PlayButtonSelect();
        }
    }

    public void SubscribeEachEvent(Dictionary<Command, UnityAction> commandActions)
    {
        foreach (var command in Commands)
        {
            if (commandActions.TryGetValue(command.Key, out var action))
            {
                command.Value.SubscribeEvent(action);
            }
            else
            {
                command.Value.ClearEvent();
            }
        }
    }

    public void ClearAllEvents()
    {
        foreach (var command in Commands.Values)
        {
            command.ClearEvent();
        }
    }

    public void Show()
    {
        IsVisible = true;
        _background.enabled = true;
        foreach (var command in Commands.Values)
        {
            command.ShowCommand();
        }
    }

    public void Hide()
    {
        IsVisible = false;
        _background.enabled = false;
        foreach (var command in Commands.Values)
        {
            command.HideCommand();
        }
    }
}
