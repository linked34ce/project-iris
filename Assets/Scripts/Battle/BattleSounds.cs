using UnityEngine;

public class BattleSounds : SingletonMonoBehaviour<BattleSounds>
{
    [SerializeField] private AudioSource _audioSource;
    public AudioSource AudioSource => _audioSource;
    [SerializeField] private AudioClip _attack;
    public AudioClip Attack => _attack;

    public void PlayAttack() => AudioSource.PlayOneShot(Attack);
}
