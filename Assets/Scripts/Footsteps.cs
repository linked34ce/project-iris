using System;
using System.Collections;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip footStep1;
    [SerializeField] private AudioClip footStep2;
    [SerializeField] private AudioClip footStep3;
    [SerializeField] private AudioClip footStep4;
    [SerializeField] private AudioClip footStep5;
    [SerializeField] private AudioClip footStep6;
    [SerializeField] private AudioClip footStep7;
    [SerializeField] private AudioClip footStep8;
    [SerializeField] private AudioClip turn1;
    [SerializeField] private AudioClip turn2;
    [SerializeField] private AudioClip turn3;
    [SerializeField] private AudioClip stairs1;
    [SerializeField] private AudioClip stairs2;
    [SerializeField] private AudioClip stairs3;
    [SerializeField] private AudioClip stairs4;
    [SerializeField] private AudioClip stairs5;
    [SerializeField] private AudioClip stairs6;

    public void PlayFootStep()
    {
        switch (UnityEngine.Random.Range(1, 9))
        {
            case 1:
                audioSource.PlayOneShot(footStep1);
                break;
            case 2:
                audioSource.PlayOneShot(footStep2);
                break;
            case 3:
                audioSource.PlayOneShot(footStep3);
                break;
            case 4:
                audioSource.PlayOneShot(footStep4);
                break;
            case 5:
                audioSource.PlayOneShot(footStep5);
                break;
            case 6:
                audioSource.PlayOneShot(footStep6);
                break;
            case 7:
                audioSource.PlayOneShot(footStep7);
                break;
            case 8:
                audioSource.PlayOneShot(footStep8);
                break;
        }
    }

    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

    public void PlayStairs()
    {
        audioSource.PlayOneShot(stairs1, 1f);
        StartCoroutine(DelayMethod(0.4f, () =>
        {
            audioSource.PlayOneShot(stairs2, 0.7f);
        }));
        StartCoroutine(DelayMethod(0.8f, () =>
        {
            audioSource.PlayOneShot(stairs3, 0.45f);
        }));
        StartCoroutine(DelayMethod(1.2f, () =>
        {
            audioSource.PlayOneShot(stairs4, 0.25f);
        }));
        StartCoroutine(DelayMethod(1.6f, () =>
        {
            audioSource.PlayOneShot(stairs5, 0.15f);
        }));
        StartCoroutine(DelayMethod(2.0f, () =>
        {
            audioSource.PlayOneShot(stairs6, 0.1f);
        }));
    }

    public void PlayTurn()
    {
        switch (UnityEngine.Random.Range(1, 4))
        {
            case 1:
                audioSource.PlayOneShot(turn1);
                break;
            case 2:
                audioSource.PlayOneShot(turn2);
                break;
            case 3:
                audioSource.PlayOneShot(turn3);
                break;
        }
    }
}