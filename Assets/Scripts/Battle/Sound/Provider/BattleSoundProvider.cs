using UnityEngine;
using UnityEngine.TestTools;

[ExcludeFromCoverage]
public class BattleSoundProvider : MonoBehaviour, IBattleSoundProvider
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _attack;

    public void PlayAttack() => _audioSource.PlayOneShot(_attack);
}
