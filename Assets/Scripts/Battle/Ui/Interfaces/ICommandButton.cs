using UnityEngine.Events;

public interface ICommandButton
{
    void SubscribeEvent(UnityAction call);
    void ClearEvent();
    void ShowCommand();
    void HideCommand();
}
