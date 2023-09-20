using System.Linq;
using UniRx;
using UnityEngine;

namespace Unity1Week_20230918
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] GameObject[] buttonObj;
        MeatController ec;

        public void Init()
        {
           
        }

        void Start()
        {
            ec = GetComponent<MeatController>();

            ec.MeatState.Subscribe(_ =>
            {
                switch (ec.MeatState.Value)
                {
                    case Meat.INIT:
                    case Meat.NONE:
                        GameObjectSelectActive(buttonObj, 0);
                        break;
                    case Meat.SET:
                        GameObjectSelectActive(buttonObj, 1);
                        break;
                    case Meat.MOVE:
                        GameObjectSelectActive(buttonObj, -1);
                        break;
                    case Meat.END:
                        GameObjectSelectActive(buttonObj, 2);
                        break;
                }
            })
            .AddTo(this);

            Init();
        }

        void GameObjectSelectActive(GameObject[] obj, int index)
        {
            for (var i = 0; i < obj.Count(); i++)
            {
                if (i == index) obj[i].SetActive(true);
                else            obj[i].SetActive(false);
            }

        }
    }
}
