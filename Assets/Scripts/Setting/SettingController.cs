using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Unity1Week_20230619.Setting
{
    public class SettingController : MonoBehaviour
    {
        public class VolumeController : MonoBehaviour
        {
            public enum VolumeType { MASTER, BGM, SE }

            [SerializeField]
            VolumeType volumeType = 0;

            Slider slider;
            SoundManager soundManager;

            void Start()
            {
                slider = GetComponent<Slider>();
                soundManager = FindObjectOfType<SoundManager>();

                switch (volumeType)
                {
                    case VolumeType.BGM:
                        slider.value = soundManager.BgmVolume;
                        break;
                    case VolumeType.SE:
                        slider.value = soundManager.SeVolume;
                        break;
                }
            }

            public void OnValueChanged()
            {

                switch (volumeType)
                {
                    case VolumeType.BGM:
                        if (soundManager.BgmVolume != slider.value)
                            SoundManager.Instance.PlayBgm(0);
                        soundManager.BgmVolume = slider.value;
                        break;
                    case VolumeType.SE:
                        if (soundManager.SeVolume != slider.value)
                            SoundManager.Instance.PlaySe(0);
                        soundManager.SeVolume = slider.value;
                        break;
                }
            }
        }
    }
}