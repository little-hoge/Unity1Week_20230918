using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
