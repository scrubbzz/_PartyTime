using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioManageable 
{
    public delegate void OnMovement(AudioSource audioSource);
    public static event OnMovement onMovement;
    public abstract void PlaySoundEffect(AudioSource audioSource);
}
