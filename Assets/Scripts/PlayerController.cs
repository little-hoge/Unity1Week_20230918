using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Unity1Week_20230918
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textMeshProUGUI;
        ReactiveProperty<float> score = new ReactiveProperty<float>(0);
        public ReactiveProperty<float> Score => score;

        EnemyController ec;

        public void Init()
        {
            score.Value = 0;
        }

        void Start()
        {
            ec = GetComponent<EnemyController>();

            this.UpdateAsObservable()
             .Where(_ => ec.State == MeatState.SET)
             .Where(_ => Input.GetMouseButtonUp(0))
             .Subscribe(_ =>
             {
                 ec.GrillMeatEnd();
                 AddScore();
                 Debug.Log("焼けましたー");
             }
             )
             .AddTo(this);

            this.score.Subscribe(newScore =>
            {
                textMeshProUGUI.text = $"Score: {newScore}";
            })
            .AddTo(this);

        }

        float CalcScore(float value)
        {
            float maxDistance = 125.0f;   
            float maxScore = 100.0f; 

            float distance = Mathf.Abs(value - 125.0f);
            float score = Mathf.Max(maxScore - (distance * maxScore / maxDistance), 0.0f);
            score = Mathf.Round(score * 100.0f) / 100.0f;

            return score;
        }

        void AddScore()
        {
            score.Value += CalcScore(ec.GetEnergy());
        }

    }
}
