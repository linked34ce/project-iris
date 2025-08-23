using System.Collections;

using UnityEngine;

public class DungeonSoundProvider : MonoBehaviour, IDungeonSoundProvider
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _footsteps;
    [SerializeField] private AudioClip[] _turns;
    [SerializeField] private AudioClip[] _stairs;
    [SerializeField] CoroutineController _coroutineController;
    private readonly WaitForSeconds _waitForSeconds = new(0.4f);

    private const float InitialVolumeScale = 1f;
    private const float DecayFactor = 0.7f;

    public void PlayWalk() => _audioSource.PlayOneShot(
        _footsteps[Random.Range(0, _footsteps.Length)]
    );

    public void PlayTurn() => _audioSource.PlayOneShot(
        _turns[Random.Range(0, _turns.Length)]
    );

    private IEnumerator DelayMethod()
    {
        float volumeScale = InitialVolumeScale;
        for (int i = 0; i < _stairs.Length; i++)
        {
            if (i > 0)
            {
                volumeScale *= DecayFactor;
            }
            _audioSource.PlayOneShot(_stairs[i], volumeScale);
            yield return _waitForSeconds;
        }
        _coroutineController.Stop();
    }

    public void PlayStairs() => _coroutineController.Begin(DelayMethod());
}
