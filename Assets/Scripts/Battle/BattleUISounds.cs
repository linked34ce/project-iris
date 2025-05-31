using UnityEngine;

public class BattleUISounds : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    public AudioSource AudioSource => _audioSource;
    [SerializeField] private AudioClip _buttonSelect;
    public AudioClip ButtonSelect => _buttonSelect;

    public void PlayButtonSelect() => AudioSource.PlayOneShot(ButtonSelect);
}