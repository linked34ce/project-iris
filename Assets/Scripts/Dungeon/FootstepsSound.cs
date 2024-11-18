using System;
using System.Collections;
using UnityEngine;

public class FootstepsSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public AudioSource AudioSource => audioSource;

    [SerializeField] private AudioClip[] footsteps;
    public AudioClip[] Footsteps => footsteps;

    [SerializeField] private AudioClip[] turns;
    public AudioClip[] Turns => turns;

    [SerializeField] private AudioClip[] stairs;
    public AudioClip[] Stairs => stairs;

    public void PlayWalk() => audioSource.PlayOneShot(Footsteps[UnityEngine.Random.Range(0, Footsteps.Length)]);

    public void PlayTurn() => audioSource.PlayOneShot(Turns[UnityEngine.Random.Range(0, Turns.Length)]);

    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

    public void PlayStairs()
    {
        audioSource.PlayOneShot(Stairs[0], 1f);
        StartCoroutine(DelayMethod(0.4f, () => audioSource.PlayOneShot(Stairs[1], 0.7f)));
        StartCoroutine(DelayMethod(0.8f, () => audioSource.PlayOneShot(Stairs[2], 0.45f)));
        StartCoroutine(DelayMethod(1.2f, () => audioSource.PlayOneShot(Stairs[3], 0.25f)));
        StartCoroutine(DelayMethod(1.6f, () => audioSource.PlayOneShot(Stairs[4], 0.15f)));
        StartCoroutine(DelayMethod(2.0f, () => audioSource.PlayOneShot(Stairs[5], 0.1f)));
    }
}