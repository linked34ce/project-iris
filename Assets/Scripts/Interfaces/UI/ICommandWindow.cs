using System.Collections.Generic;

using UnityEngine.Events;

public interface ICommandWindow
{
    bool IsVisible { get; }
    void PlayButtonSelect();
    void SubscribeEachEvent(Dictionary<Command, UnityAction> commandActions);
    void ClearAllEvents();
    void Show();
    void Hide();
}
