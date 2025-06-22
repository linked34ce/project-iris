using System.Collections;

using UnityEngine;

public class DungeonSounds : SingletonMonoBehaviour<DungeonSounds>
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _footsteps;
    [SerializeField] private AudioClip[] _turns;
    [SerializeField] private AudioClip[] _stairs;

    private const float InitalVolumeScale = 1f;
    private const float DecayFactor = 0.7f;
    private const float WaitTime = 0.4f;

    public void PlayWalk() => _audioSource.PlayOneShot(
        _footsteps[Random.Range(0, _footsteps.Length)]
    );

    public void PlayTurn() => _audioSource.PlayOneShot(
        _turns[Random.Range(0, _turns.Length)]
    );

    private IEnumerator DelayMethod()
    {
        float volumeScale = InitalVolumeScale;
        for (int i = 0; i < _stairs.Length; i++)
        {
            if (i > 0)
            {
                volumeScale *= DecayFactor;
            }
            _audioSource.PlayOneShot(_stairs[i], volumeScale);
            yield return new WaitForSeconds(WaitTime);
        }
    }

    public void PlayStairs() => StartCoroutine(DelayMethod());
}
