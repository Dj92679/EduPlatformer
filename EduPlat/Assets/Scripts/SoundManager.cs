using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip jump;
    public static AudioClip pickup;
    public static AudioClip solve;
    static AudioSource audioSource;

    private void Start()
    {
        jump = Resources.Load<AudioClip>("jump");
        pickup = Resources.Load<AudioClip>("pickup");
        solve = Resources.Load<AudioClip>("solve");
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Jump":
                audioSource.PlayOneShot(jump);
                break;
            case "Pickup":
                audioSource.PlayOneShot(pickup);
                break;
            case "Solve":
                audioSource.PlayOneShot(solve);
                break;
        }
    }

}
