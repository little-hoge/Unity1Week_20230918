using UnityEngine;
using UnityEngine.UI;

namespace Unity1Week_20230619.Setting
{
    public class VolumeController : MonoBehaviour
    {
        public enum VolumeType { BGM, SE }

        [SerializeField]
        VolumeType volumeType = 0;

        Slider slider;

        void Start()
        {
            slider = GetComponent<Slider>();

            switch (volumeType)
            {
                case VolumeType.BGM:
                    slider.value = SoundManager.Instance.BgmVolume;
                    break;
                case VolumeType.SE:
                    slider.value = SoundManager.Instance.SeVolume;
                    break;
            }
        }

        public void OnValueChanged()
        {
            switch (volumeType)
            {
                case VolumeType.BGM:
                    SoundManager.Instance.BgmVolume = slider.value;
                    break;
                case VolumeType.SE:
                    if (SoundManager.Instance.SeVolume != slider.value) SoundManager.Instance.PlaySe(0);
                    SoundManager.Instance.SeVolume = slider.value;
                    break;
            }
        }
    }
}