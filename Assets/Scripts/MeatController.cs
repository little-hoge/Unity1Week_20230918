using UnityEngine;
using UniRx;
using UniRx.Triggers;
using MagicTween;

namespace Unity1Week_20230918
{
    public enum Meat
    {
        INIT,
        NONE,
        MOVE,
        SET,
        END,
    }
    public class MeatController : MonoBehaviour
    {
        [SerializeField] float rotationSpeed;
        float energychargedSpeed;

        ReactiveProperty<Meat> meatstate = new ReactiveProperty<Meat>(Meat.NONE);
        public ReactiveProperty<Meat> MeatState => meatstate;

        MeatGenerator gr;
        int meatcount;

        public void Init()
        {
            energychargedSpeed = Random.Range(0.0008f, 0.001f);
            meatstate.Value = Meat.INIT;
            meatcount = -1;
#if UNITY_EDITOR
            energychargedSpeed = 0.5f;
#endif
            foreach (var enemy in gr.meatList)
            {
                enemy.transform.position = new Vector3(300, 0, 10);
                enemy.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                enemy.GetComponent<MeatParameter>().param.Energy = 0;
            }
            gr.Init();
        }

        void Start()
        {
            gr = GetComponent<MeatGenerator>();

            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    switch (meatstate.Value) {
                        case Meat.SET:
                            RotateGrillMeat();
                            MaterialColorUpDate();
                            break;
                        case Meat.END:
                            RotateGrillMeat();
                            break;
                    }
                })
                .AddTo(this);

            Init();
        }

        void MaterialColorUpDate()
        {
            Color startColor = Color.white;
            Color middleColor = new Color(1.0f, 0.5f, 0.0f);
            Color endColor = Color.black;

            var i = gr.meatList[meatcount].GetComponent<MeatParameter>().param.Energy;
            float t = Mathf.Clamp(i, 0, 255) / 255f;

            if (t < 0.5f) gr.meatList[meatcount].GetComponent<Renderer>().material.color = Color.Lerp(startColor, middleColor, t * 2);
            else          gr.meatList[meatcount].GetComponent<Renderer>().material.color = Color.Lerp(middleColor, endColor, (t - 0.5f) * 2);
        }

        void RotateGrillMeat() 
        {
            gr.meatList[meatcount].transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
            gr.meatList[meatcount].GetComponent<MeatParameter>().param.Energy += energychargedSpeed;
        }

        public void SetMeat()
        {
            meatcount += 1;
            var tween = gr.meatList[meatcount].transform
                .TweenPosition(new Vector3(0f, 0f, 10f), 2f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    meatstate.Value = Meat.SET;
                });
            meatstate.Value = Meat.MOVE;
           
        }

        public void GrillEnd()
        {
            gr.meatList[meatcount].transform.TweenPosition(new Vector3(-3f, 10f, 10f), 1f).SetEase(Ease.OutQuad);

            if ((meatcount+1) < gr.meatList.Count) meatstate.Value = Meat.NONE;
            else                                   meatstate.Value = Meat.END;

        }

        public float GetEnergy()
        {
            return gr.meatList[meatcount].GetComponent<MeatParameter>().param.Energy;
        }
       
    }
}
