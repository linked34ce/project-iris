using UnityEngine;
using UnityEngine.TestTools;

[ExcludeFromCoverage]
public class BattleUiSoundProvider : MonoBehaviour, IBattleUiSoundProvider
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _buttonSelect;

    public void PlayButtonSelect() => _audioSource.PlayOneShot(_buttonSelect);
}
