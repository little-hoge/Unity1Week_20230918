using System;
using UnityEngine;

namespace Unity1Week_20230918
{
    public class EnemyParameter : MonoBehaviour
    {
        [Serializable]
        public class Parameter
        {
            public float Energy;
        }

        public Parameter param = new Parameter();

        public void Init()
        {
            param.Energy = 0;
        }
    }
}
