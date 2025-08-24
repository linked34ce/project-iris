using UnityEngine;
using UnityEngine.EventSystems;

public class SoundEffectOnHover : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData e) =>
        EventSystem.current.SetSelectedGameObject(gameObject);
}
