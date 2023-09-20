using UniRx;
using UniRx.Triggers;
using UnityEngine;
using unityroom.Api;

namespace Unity1Week_20230918
{
    public class GameController : MonoBehaviour
    {
        float score;
        PlayerController pc;
        MeatController ec;
        TimerController timerController = new TimerController();

        public void Init()
        {
            score = 0;
            timerController.ResetTimer(1f);
        }

        void Start()
        {
            SoundManager.Instance.PlayBgm(1);
            
            pc = GetComponent<PlayerController>();
            ec = GetComponent<MeatController>();

            ec.MeatState.Subscribe(_ =>
            {
                switch (ec.MeatState.Value)
                {
                    case Meat.NONE:
                        score += Function.CalcScore(ec.GetEnergy());
                        break;                    
                    case Meat.END:
                        var nowscore = Function.CalcScore(ec.GetEnergy());
                        if (nowscore > 90) SoundManager.Instance.PlaySe(1);
                        score += nowscore;
                        DrawRanking();
                        break;
                }
            })
            .AddTo(this);

            this.UpdateAsObservable()
              .Where(_  => ec.MeatState.Value == Meat.SET)
              .Subscribe(_ =>
              {
                  DebugLogger.Log(timerController.GetFormattedTime());
                  timerController.UpdateTime();
                  if(timerController.IsSecondsElapsed(120)) SoundManager.Instance.PlaySe(2);
              })
              .AddTo(this);

            Init();
        }

        public void Restart()
        {
            Init();
            pc.Init();
            ec.Init();
        }

        void DrawRanking()
        {
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score);
            UnityroomApiClient.Instance.SendScore(1, score, ScoreboardWriteMode.HighScoreDesc);
        }
    }
}
