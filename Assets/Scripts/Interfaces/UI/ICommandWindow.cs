using System.Collections.Generic;

using UnityEngine.Events;

public interface ICommandWindow
{
    void SubscribeEachEvent(Dictionary<Command, UnityAction> commandActions);
    void ClearAllEvents();
    void Show();
    void Hide();
}
