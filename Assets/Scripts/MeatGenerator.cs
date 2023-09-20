using UnityEngine;
using System.Collections.Generic;

namespace Unity1Week_20230918
{
    public class MeatGenerator : MonoBehaviour
    {
        [SerializeField] int MAXMEAT;
        [SerializeField] GameObject meatPrefab;
        [SerializeField] Transform parent;
        public List<GameObject> meatList = new();

        public void Init()
        {

        }

        void Start()
        {
            for (var i = 0; i < MAXMEAT; i++)
            {
                GameObject instance = Instantiate(meatPrefab, new Vector3(300, 0, 10), Quaternion.identity, parent);
                instance.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                instance.AddComponent<MeatParameter>();
                meatList.Add(instance);
            }
        }
    }
}
