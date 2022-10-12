using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicVolControl : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetLevel(float sliderVal) {
        mixer.SetFloat("musicVol", Mathf.Log10(sliderVal) * 20);
  }
}
