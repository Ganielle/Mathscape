using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private Slider sfxSlider;

    public IEnumerator CheckVolumeOnStart()
    {
        sfxSlider.value = GameManager.Instance.SoundMnger.CurrentVolume;
        yield return null;
    }

    public void ChangeVolume()
    {
        GameManager.Instance.SoundMnger.CurrentVolume = sfxSlider.value;
    }
}
