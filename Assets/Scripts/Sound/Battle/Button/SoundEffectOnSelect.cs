using UnityEngine;
using UnityEngine.EventSystems;

public class SoundEffectOnSelect : MonoBehaviour, ISelectHandler
{
    [SerializeField] private BattleUISoundProvider _soundProvider;
    [SerializeField] private bool _isFirstCommand;

    private bool _isFirstCall = true;

    public void OnSelect(BaseEventData eventData)
    {
        if (_isFirstCommand && _isFirstCall)
        {
            _isFirstCall = false;
            return;
        }

        _soundProvider.PlayButtonSelect();
    }
}
