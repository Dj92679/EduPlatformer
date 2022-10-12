using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MasterVolumeControl : MonoBehaviour
{
  public AudioMixer mixer;

  public void SetLevel(float sliderVal) {
    mixer.SetFloat("masterVol", Mathf.Log10(sliderVal) * 20);
  }

}
