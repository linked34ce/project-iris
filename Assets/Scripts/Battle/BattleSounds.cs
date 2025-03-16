using UnityEngine;

public class BattleSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public AudioSource AudioSource => audioSource;
    [SerializeField] private AudioClip attack;
    public AudioClip Attack => attack;

    public void PlayAttack() => AudioSource.PlayOneShot(Attack);
}