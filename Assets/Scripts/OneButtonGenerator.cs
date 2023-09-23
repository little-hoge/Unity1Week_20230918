using MagicTween;
using UnityEngine;

namespace Unity1Week_20230918
{
    public class OneButtonGenerator : MonoBehaviour, IObjectHit
    {
        [SerializeField] GameObject onebuttonPrefab;
        [SerializeField] Transform parent;
        Sequence sequence;

        void OneButtonCreate()
        {
            var pos = transform.position;
            pos.y -= 0.2f;
            var ins = Instantiate(onebuttonPrefab, pos, transform.rotation, parent);
            Destroy(ins, 3.0f);
        }

        public void SetWantan()
        {
            if (!sequence.IsActive())
            {
                sequence = Sequence.Create()
                .Append(transform.TweenPosition(new Vector3(-5f, 2f, Random.Range(-10.8f, -9.2f)), 3f))
                .AppendInterval(2.0f)
                .Append(transform.TweenPosition(new Vector3(-5f, 2f, -13f), 3f))
                .OnComplete(() =>
                {
                    transform.position = new Vector3(-5, 2, -6);
                });
            }
        }

        public void Hit()
        {
            OneButtonCreate();
        }

    }
}
