using UnityEngine;

public class BattleUISounds : SingletonMonoBehaviour<BattleUISounds>
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _buttonSelect;

    public void PlayButtonSelect() => _audioSource.PlayOneShot(_buttonSelect);
}
