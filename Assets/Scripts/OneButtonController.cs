using UnityEngine;

namespace Unity1Week_20230918
{
    public class OneButtonController : MonoBehaviour, IObjectHit
    {
        public void Hit()
        {
            transform.gameObject.SetActive(false);
            Data.instance.Score++;
        }
    }
}
