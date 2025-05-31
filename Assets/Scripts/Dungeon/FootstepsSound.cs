using System;
using System.Collections;

using UnityEngine;

public class DungeonSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    public AudioSource AudioSource => _audioSource;

    [SerializeField] private AudioClip[] _footsteps;
    public AudioClip[] Footsteps => _footsteps;

    [SerializeField] private AudioClip[] _turns;
    public AudioClip[] Turns => _turns;

    [SerializeField] private AudioClip[] _stairs;
    public AudioClip[] Stairs => _stairs;

    public void PlayWalk() => AudioSource.PlayOneShot(Footsteps[UnityEngine.Random.Range(0, Footsteps.Length)]);

    public void PlayTurn() => AudioSource.PlayOneShot(Turns[UnityEngine.Random.Range(0, Turns.Length)]);

    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        WaitForSeconds cachedWait = new(waitTime);
        while (true)
        {
            yield return cachedWait;
            action();
        }
    }

    public void PlayStairs()
    {
        AudioSource.PlayOneShot(Stairs[0], 1f);
        StartCoroutine(DelayMethod(0.4f, () => AudioSource.PlayOneShot(Stairs[1], 0.7f)));
        StartCoroutine(DelayMethod(0.8f, () => AudioSource.PlayOneShot(Stairs[2], 0.45f)));
        StartCoroutine(DelayMethod(1.2f, () => AudioSource.PlayOneShot(Stairs[3], 0.25f)));
        StartCoroutine(DelayMethod(1.6f, () => AudioSource.PlayOneShot(Stairs[4], 0.15f)));
        StartCoroutine(DelayMethod(2.0f, () => AudioSource.PlayOneShot(Stairs[5], 0.1f)));
    }
}
