using System.Collections;

using UnityEngine;

public class DungeonSounds : SingletonMonoBehaviour<DungeonSounds>
{
    [SerializeField] private AudioSource _audioSource;
    public AudioSource AudioSource => _audioSource;

    [SerializeField] private AudioClip[] _footsteps;
    public AudioClip[] Footsteps => _footsteps;

    [SerializeField] private AudioClip[] _turns;
    public AudioClip[] Turns => _turns;

    [SerializeField] private AudioClip[] _stairs;
    public AudioClip[] Stairs => _stairs;

    private const float InitalVolumeScale = 1f;
    private const float DecayFactor = 0.7f;
    private const float WaitTime = 0.4f;

    public void PlayWalk() => AudioSource.PlayOneShot(
        Footsteps[Random.Range(0, Footsteps.Length)]
    );

    public void PlayTurn() => AudioSource.PlayOneShot(
        Turns[Random.Range(0, Turns.Length)]
    );

    private IEnumerator DelayMethod()
    {
        float volumeScale = InitalVolumeScale;
        for (int i = 0; i < Stairs.Length; i++)
        {
            if (i > 0)
            {
                volumeScale *= DecayFactor;
            }
            AudioSource.PlayOneShot(Stairs[i], volumeScale);
            yield return new WaitForSeconds(WaitTime);
        }
    }

    public void PlayStairs() => StartCoroutine(DelayMethod());
}
