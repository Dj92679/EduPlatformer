using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXVolController : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetLevel(float sliderVal) {
        mixer.SetFloat("sfxVol", Mathf.Log10(sliderVal) * 20);
  }
}
