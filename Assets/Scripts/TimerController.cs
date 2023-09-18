using System.Collections;
using UnityEngine;

namespace Unity1Week_20230918
{
    public class TimerController
    {
        float elapsedTime = 0f;
        string announceText;
        bool isRunning = false;
        public float startTime;

        public float ElapsedTime => elapsedTime;
        public string AnnounceText => announceText;

        public IEnumerator Countdown()
        {
            int count = 3; // カウントダウンの開始数値
            while (count > 0)
            {
                announceText = count.ToString();
                yield return new WaitForSeconds(1f);
                count--;
            }

            announceText = "GO!";
            yield return new WaitForSeconds(0.5f);

            announceText = "";

            DebugLogger.Log("Countdown Complete");
            StartTimer();
        }

        public void TimeUpdate()
        {
            if (isRunning)
            {
                elapsedTime -= Time.deltaTime;
                elapsedTime = Mathf.Clamp(elapsedTime, 0f, Mathf.Infinity);
            }
        }

        public void StartTimer()
        {
            isRunning = true;
        }

        public void StopTimer()
        {
            isRunning = false;
        }

        public void ResetTimer(float time)
        {
            startTime = time;
            elapsedTime = startTime;
        }

        public string GetTime()
        {
            int hours = Mathf.FloorToInt(elapsedTime / 3600f);
            int minutes = Mathf.FloorToInt((elapsedTime % 3600f) / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            int milliseconds = Mathf.FloorToInt(elapsedTime * 1000f) % 1000;

            if (hours >= 1)
            {
                minutes = 59;
                seconds = 59;
                milliseconds = 999;
            }

            return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);

        }
    }
}