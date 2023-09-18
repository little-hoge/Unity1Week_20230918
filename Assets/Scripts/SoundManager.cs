using UnityEngine;

namespace Unity1Week_20230619
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField, Range(0, 1), Tooltip("BGMの音量")]
        float bgmvolume = 0.3f;
        [SerializeField, Range(0, 1), Tooltip("SEの音量")]
        float sevolume = 0.3f;

        [SerializeField] AudioSource bgmaudiosource;
        [SerializeField] AudioSource seaudiosource;
        [SerializeField] AudioClip[] bgm;
        [SerializeField] AudioClip[] se;

        public static SoundManager Instance { private set; get; }

        void Awake()
        {
            // シングルトン
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            // オーディオ関連取得
            bgmaudiosource = gameObject.AddComponent<AudioSource>();
            seaudiosource  = gameObject.AddComponent<AudioSource>();
            bgm = Resources.LoadAll<AudioClip>("audio/bgm");
            se  = Resources.LoadAll<AudioClip>("audio/se");

            bgmaudiosource.volume = bgmvolume;
            seaudiosource.volume = sevolume;
        }

        public float BgmVolume
        {
            set
            {
                bgmvolume = Mathf.Clamp01(value);
                bgmaudiosource.volume = bgmvolume;
            }
            get
            {
                return bgmvolume;
            }
        }
        public float SeVolume
        {
            set
            {
                sevolume = Mathf.Clamp01(value);
                seaudiosource.volume = sevolume;
            }
            get
            {
                return sevolume;
            }
        }

        /// <summary>
        /// BGM再生                        <br />
        /// 0.test用                       <br />
        /// </summary>
        public void PlayBgm(int index)
        {
            index = Mathf.Clamp(index, 0, bgm.Length);
            bgmaudiosource.clip = bgm[index];
            bgmaudiosource.loop = true;
            bgmaudiosource.volume = bgmvolume;
            bgmaudiosource.Play();
        }
        /// <summary>
        /// SE再生                      <br />
        /// 0.test用                    <br />
        /// 1.hit                       <br />
        /// 2.miss                      <br />
        /// 3.bad                       <br />
        /// 4.shuffle                   <br />
        /// 5.launch                    <br />
        /// </summary>
        public void PlaySe(int index)
        {
            index = Mathf.Clamp(index, 0, se.Length);
            seaudiosource.volume = sevolume;
            seaudiosource.PlayOneShot(se[index]);
        }

        /// <summary>
        /// Audio再生停止
        /// </summary>
        public void StopAudio()
        {
            bgmaudiosource.Stop();
            seaudiosource.Stop();
        }
    }
}