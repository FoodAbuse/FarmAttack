using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grassWalkSFX : MonoBehaviour
{
    // Assign these in the Unity Editor
    public AudioClip[] audioClips; // Array to hold the audio clips, if it's annoying we should just remove it and have 1
    public AudioSource audioSource;
    AudioClip chosenClip;

    public float minPitch = 0.95f;
    public float maxPitch = 1.05f;
    void Start()
    {
    }

    // Call this method to play a random audio clip
    public void PlayRandomAudio()
    {
        if (audioClips.Length > 0)
        {

            int randomIndex = Random.Range(0, audioClips.Length);
            float randomPitch = Random.Range(minPitch, maxPitch);

            audioSource.pitch = randomPitch;

            chosenClip = audioClips[randomIndex];
            audioSource.PlayOneShot(chosenClip);
        }
    }
}
