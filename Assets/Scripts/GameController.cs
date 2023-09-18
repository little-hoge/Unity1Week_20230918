using UniRx;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using unityroom.Api;

namespace Unity1Week_20230918
{
    public class GameController : MonoBehaviour
    {
        PlayerController pc;
        EnemyController ec;

        void Start()
        {
            pc = GetComponent<PlayerController>();
            ec = GetComponent<EnemyController>();

            ec.Gamestate.Subscribe(_ =>
            {
                DebugLogger.Log($"Gamestate更新：{ec.Gamestate.Value}");

                if (ec.Gamestate.Value)
                {
                    DrawRanking();
                }
            })
           .AddTo(this);
        }

        public void Restart()
        {
            pc.Init();
            ec.Init();
        }

        void DrawRanking()
        {
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(pc.Score.Value);
            UnityroomApiClient.Instance.SendScore(1, pc.Score.Value, ScoreboardWriteMode.HighScoreDesc);
        }
    }
}
