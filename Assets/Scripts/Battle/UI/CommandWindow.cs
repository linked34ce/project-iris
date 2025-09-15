using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using UnityEngine.UI;

[ExcludeFromCoverage]
public class CommandWindow : MonoBehaviour, ICommandWindow
{
    [SerializeField] private Image _background;
    [SerializeField] private SerializableDictionary<Command, CommandButton> _commands;
    [SerializeField] private EventSystem _battleEventSystem;

    void OnEnable()
    {
        _battleEventSystem.enabled = true;
    }

    public void SubscribeEachEvent(Dictionary<Command, UnityAction> commandActions)
    {
        foreach (var command in _commands)
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
        foreach (var command in _commands.Values)
        {
            command.ClearEvent();
        }
    }

    public void Show()
    {
        _background.enabled = true;
        foreach (var command in _commands.Values)
        {
            command.ShowCommand();
        }
    }

    public void Hide()
    {
        _background.enabled = false;
        foreach (var command in _commands.Values)
        {
            command.HideCommand();
        }
    }
}
