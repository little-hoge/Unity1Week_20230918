using UniRx;
using UniRx.Triggers;
using UnityEngine;
using unityroom.Api;

namespace Unity1Week_20230918
{
    public class GameController : MonoBehaviour
    {
        float score;
        float nowscore;
        const float scorelimit = 70;
        PlayerController pc;
        MeatController mc;
        TimerController timerController = new TimerController();

        public void Init()
        {
            score = 0;
            Data.instance.Score = 0;
            timerController.ResetTimer(1f);
        }

        void Start()
        {
            SoundManager.Instance.PlayBgm(1);

            pc = GetComponent<PlayerController>();
            mc = GetComponent<MeatController>();

            mc.MeatState
              .Subscribe(_ =>
            {
                switch (mc.MeatState.Value)
                {
                    case Meat.NONE:
                        score += Function.CalcScore(mc.GetEnergy());
                        break;
                    case Meat.END:
                        nowscore = Function.CalcScore(mc.GetEnergy());
                        if (nowscore >= scorelimit) SoundManager.Instance.PlaySe(1);
                        score += nowscore;
                        DrawRanking();
                        break;
                }
            })
            .AddTo(this);

            this.UpdateAsObservable()
              .Where(_ => mc.MeatState.Value == Meat.SET)
              .Subscribe(_ =>
              {
                  timerController.UpdateTime();
                  if (timerController.IsSecondsElapsed(180)) SoundManager.Instance.PlaySe(2);
              })
              .AddTo(this);

            Init();
        }

        public void Restart()
        {
            Init();
            pc.Init();
            mc.Init();
        }

        void DrawRanking()
        {
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score);
            UnityroomApiClient.Instance.SendScore(1, score, ScoreboardWriteMode.HighScoreDesc);
            if (nowscore >= scorelimit)
            {
                SoundManager.Instance.PlaySe(1);
                UnityroomApiClient.Instance.SendScore(2, Data.instance.Score, ScoreboardWriteMode.HighScoreDesc);
            }
            else UnityroomApiClient.Instance.SendScore(2, 0, ScoreboardWriteMode.HighScoreDesc);
        }
    }
}
