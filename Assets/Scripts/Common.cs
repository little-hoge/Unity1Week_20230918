using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity1Week_20230918
{
    public enum GameSceneState
    {
        GAME_TITLE,
        GAME_MAIN,
        GAME_RANKING,
        GAME_SETTING,
    }

    public static class DebugLogger
    {
        [Conditional("UNITY_EDITOR")]
        public static void Log(object o)
        {
            UnityEngine.Debug.Log(o);
        }
    }

    public static class Define
    {
        // 定数
        public const string GAME_TITLE   = ("TitleScene");
        public const string GAME_MAIN    = ("MainScene");
        public const string GAME_RANKING = ("Ranking");
        public const string GAME_SETTING = ("SettingScene");

        //
        public const int DEFAULT_GAMEID = (-1);


    }

    public static class Function
    {
        /// <summary>
        /// bool型乱数の取得
        /// </summary>
        /// <returns>bool型の乱数</returns>
        public static bool RandomBool()
        {
            return UnityEngine.Random.Range(0, 2) == 0;
        }

        /// <summary>
        /// 指定時間後に処理を呼び出すコルーチン
        /// </summary>
        /// <param name="seconds">時間</param>
        /// <param name="action">処理</param>
        /// <returns></returns>
        public static IEnumerator DelayCoroutine(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);
            action?.Invoke();
        }

        /// <summary>
        /// 指定フレーム後に実行を呼び出すコルーチン
        /// </summary>
        /// <param name="delayFrameCount">フレーム数</param>
        /// <param name="action">処理</param>
        /// <returns></returns>
        public static IEnumerator DelayCoroutine(int delayFrameCount, Action action)
        {
            for (var i = 0; i < delayFrameCount; i++)
            {
                yield return null;
            }
            action();
        }

        /// <summary>
        /// 次のシーンに変更
        /// </summary>
        /// <param name="sceneName">次のシーン名</param>
        /// <param name="gameState">次のゲーム状態</param>
        /// <returns></returns>
        public static void SceneChange(string sceneName, GameSceneState gameState)
        {
            SceneManager.LoadScene(sceneName);
            Data.instance.gameState = gameState;

        }

        /// <summary>
        /// 現在その名前のSceneが存在するか
        /// </summary>
        /// <param name="sceneName"> Scene名 </param>
        /// <returns> 有無を返す </returns>
        public static bool ContainsScene(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name == sceneName)
                {
                    return true;
                }
            }
            return false;
        }
        
       /// <summary>
       /// 配列をシャッフル
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="array">シャッフルする配列</param>
        public static void ShuffleArray<T>(T[] array)
        {
            System.Random rng = new System.Random();
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

    }

    /// <summary>
    /// string 型の拡張メソッドを管理するクラス
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 指定した文字列をすべて空文字列に置換
        /// </summary>
        public static string ReplaceEmpty(this string self, string oldValue)
        {
            return self.Replace(oldValue, string.Empty);
        }
    }


    public class RankingData
    {
        public float Score { get; set; }
        public int GameID { get; set; }

        public RankingData(float score, int gameID)
        {
            Score  = score;
            GameID = gameID;
        }
    }

    // 共有データ
    public class Data
    {
        public readonly static Data instance = new Data();

        /// <summary> ゲーム状態 </summary>
        public GameSceneState gameState = GameSceneState.GAME_TITLE;

        // 
        public List<RankingData> rankingDate = new List<RankingData>();


    }

}
