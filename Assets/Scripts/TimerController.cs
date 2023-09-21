using UnityEngine;

namespace Unity1Week_20230918
{
    public class TimerController
    {
        float elapsedTime = 0f;
        string announceText;

        public float ElapsedTime => elapsedTime;
        public string AnnounceText => announceText;

        public void UpdateTime()
        {
            elapsedTime += Time.deltaTime;
            elapsedTime = Mathf.Clamp(elapsedTime, 0f, Mathf.Infinity);
        }

        public void ResetTimer(float time)
        {
            elapsedTime = time;
        }

        public string GetFormattedTime()
        {
            int hours        = Mathf.FloorToInt(elapsedTime / 3600f);
            int minutes      = Mathf.FloorToInt((elapsedTime % 3600f) / 60f);
            int seconds      = Mathf.FloorToInt(elapsedTime % 60f);
            int milliseconds = Mathf.FloorToInt(elapsedTime * 1000f) % 1000;

            if (hours >= 1)
            {
                minutes = 59;
                seconds = 59;
                milliseconds = 999;
            }

            return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }

        public bool IsSecondsElapsed(int seconds)
        {
            return Mathf.FloorToInt(elapsedTime) % seconds == 0;
        }
    }
}