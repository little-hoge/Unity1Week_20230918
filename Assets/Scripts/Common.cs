using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Unity1Week_20230918
{
    public static class DebugLogger
    {
        [Conditional("UNITY_EDITOR")]
        public static void Log(object o)
        {
            UnityEngine.Debug.Log(o);
        }
    }
    public static class Function
    {
        public static float CalcScore(float value)
        {
            float maxDistance = 125.0f;
            float maxScore = 100.0f;

            float distance = Mathf.Abs(value - 125.0f);
            float score = Mathf.Max(maxScore - (distance * maxScore / maxDistance), 0.0f);
            score = Mathf.Round(score * 100.0f) / 100.0f;

            return score;
        }
    }

    public class Data
    {
        public readonly static Data instance = new Data();

        public int Score = 0;

    }

}
