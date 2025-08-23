using UnityEngine;

public class BattleUISoundProvider : MonoBehaviour, IBattleUISoundProvider
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _buttonSelect;

    public void PlayButtonSelect() => _audioSource.PlayOneShot(_buttonSelect);
}
