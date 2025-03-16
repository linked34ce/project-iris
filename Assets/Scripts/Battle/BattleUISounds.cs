using System;
using System.Collections;
using UnityEngine;

public class BattleUISounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public AudioSource AudioSource => audioSource;
    [SerializeField] private AudioClip buttonSelect;
    public AudioClip ButtonSelect => buttonSelect;

    public void PlayButtonSelect() => AudioSource.PlayOneShot(ButtonSelect);
}