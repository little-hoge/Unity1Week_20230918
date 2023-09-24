using System.Linq;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Unity1Week_20230918
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] GameObject[] buttonObj;
        [SerializeField] TextMeshProUGUI TimeText;
        [SerializeField] TextMeshProUGUI ScoreText;
        MeatController mc;

        public void Init()
        {
           
        }

        void Start()
        {
            mc = GetComponent<MeatController>();

            mc.MeatState
              .Subscribe(_ =>
            {
                switch (mc.MeatState.Value)
                {
                    case Meat.INIT:
                    case Meat.NONE:
                        GameObjectSelectActive(0);
                        break;
                    case Meat.SET:
                        GameObjectSelectActive(1);
                        break;
                    case Meat.MOVE:
                        GameObjectSelectActive(-1);
                        break;
                    case Meat.END:
                        GameObjectSelectActive(3);
                        break;
                }
            })
            .AddTo(this);

            this.UpdateAsObservable()
              .Where(_ => buttonObj[2].activeSelf)
              .Subscribe(_ =>
              {
                  ScoreText.text =  $"製造数：{Data.instance.Score}";
              })
            .AddTo(this);

            Init();
        }

        /// <summary>
        /// ボタン表示状態更新           <br />
        /// 0.設置                       <br />
        /// 1.焼けた                     <br />
        /// 2.ワンボタン作成             <br />
        /// 3.やり直し                   <br />
        /// </summary>
        /// <param name="index">表示ボタン:</param>
        public void GameObjectSelectActive(int index)
        {
            for (var i = 0; i < buttonObj.Count(); i++)
            {
                if (i == index) buttonObj[i].SetActive(true);
                else            buttonObj[i].SetActive(false);
            }

            if (2 == index)
            {
                TimeText.gameObject.SetActive(false);
                ScoreText.gameObject.SetActive(true);
            }
            else
            {
                TimeText.gameObject.SetActive(true);
                ScoreText.gameObject.SetActive(false);
            }

        }
    }
}
