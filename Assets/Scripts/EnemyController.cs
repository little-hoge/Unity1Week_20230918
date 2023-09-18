using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using MagicTween;
using Unity.Transforms;

namespace Unity1Week_20230918
{
    public enum MeatState
    {
        NONE,
        MOVE,
        SET,

    }
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] float rotationSpeed;
        [SerializeField] float energychargedSpeed;

        MeatState state = MeatState.NONE;
        public MeatState State => state;
        ReactiveProperty<bool> gamestate = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> Gamestate => gamestate;

        EnemyGenerator gr;
        int meatcount = 0;

        public void Init()
        {
            gamestate.Value = false;
            state = MeatState.NONE;
            meatcount = 0;

            foreach (var enemy in gr.enemyList)
            {
                enemy.transform.position = new Vector3(300, 0, 10);
                enemy.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                enemy.GetComponent<EnemyParameter>().param.Energy = 0;
            }
            gr.Init();
        }


        void Start()
        {
            gr = GetComponent<EnemyGenerator>();

            this.UpdateAsObservable()
                .Where(_ => state == MeatState.SET)
                .Subscribe(_ =>
                {

                    GrillMeat();
                }
                )
                .AddTo(this);

            this.UpdateAsObservable()
               .Where(_ => state == MeatState.NONE)
               .Subscribe(_ =>
               {

                   if (meatcount >= gr.enemyList.Count)
                   {
                       gamestate.Value = true;
                       return;
                   }
                   SetMeat();
               }
               )
               .AddTo(this);

        }

        void MaterialColorChange()
        {
            Color startColor = Color.white;
            Color middleColor = new Color(1.0f, 0.5f, 0.0f);
            Color endColor = Color.black;

            var i = gr.enemyList[meatcount - 1].GetComponent<EnemyParameter>().param.Energy;
            float t = Mathf.Clamp(i, 0, 255) / 255f;

            if (t < 0.5f)
            {
                gr.enemyList[meatcount - 1].GetComponent<Renderer>().material.color = Color.Lerp(startColor, middleColor, t * 2);
            }
            else
            {
                gr.enemyList[meatcount - 1].GetComponent<Renderer>().material.color = Color.Lerp(middleColor, endColor, (t - 0.5f) * 2);
            }
        }


        void SetMeat()
        {
            var tween = gr.enemyList[meatcount].transform
                .TweenPosition(new Vector3(0f, 0f, 10f), 2f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    state = MeatState.SET;
                });
            state = MeatState.MOVE;
            meatcount += 1;
        }

        void GrillMeat()
        {
            gr.enemyList[meatcount - 1].transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
            gr.enemyList[meatcount - 1].GetComponent<EnemyParameter>().param.Energy += energychargedSpeed;
            MaterialColorChange();
        }

        public void GrillMeatEnd()
        {
            gr.enemyList[meatcount - 1].transform.TweenPosition(new Vector3(2f, 10f, 10f), 1f).SetEase(Ease.OutQuad);
            state = MeatState.NONE;

        }
        public float GetEnergy()
        {
            return gr.enemyList[meatcount - 1].GetComponent<EnemyParameter>().param.Energy;

        }

    }
}
