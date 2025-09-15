using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;

[ExcludeFromCoverage]
public class SoundEffectOnHover : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData e) =>
        EventSystem.current.SetSelectedGameObject(gameObject);
}
