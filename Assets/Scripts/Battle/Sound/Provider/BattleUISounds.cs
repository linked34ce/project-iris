using UnityEngine;

public class BattleUiSoundProvider : MonoBehaviour, IBattleUiSoundProvider
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _buttonSelect;

    public void PlayButtonSelect() => _audioSource.PlayOneShot(_buttonSelect);
}
