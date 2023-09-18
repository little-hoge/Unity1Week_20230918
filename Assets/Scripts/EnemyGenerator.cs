using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections.Generic;

namespace Unity1Week_20230918
{
    public class EnemyGenerator : MonoBehaviour
    {
        [SerializeField] int MAXENEMY;
        [SerializeField] GameObject enemyPrefab;
        [SerializeField] Transform parent;
        public List<GameObject> enemyList = new();

        Transform playerTransform;

        public void Init()
        {

        }

        void Start()
        {
            for (var i = 0; i < MAXENEMY; i++)
            {
                GameObject instance = Instantiate(enemyPrefab, new Vector3(300, 0, 10), Quaternion.identity, parent);
                instance.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                instance.AddComponent<EnemyParameter>();
                enemyList.Add(instance);
            }
        }
    }
}
