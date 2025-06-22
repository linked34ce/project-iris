using UnityEngine;

public class BattleSounds : SingletonMonoBehaviour<BattleSounds>
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _attack;

    public void PlayAttack() => _audioSource.PlayOneShot(_attack);
}
