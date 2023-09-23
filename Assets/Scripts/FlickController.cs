using MagicTween;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Unity1Week_20230918
{
    public class FlickController : MonoBehaviour
    {
        Vector3 startTouchPos;
        Vector3 endTouchPos;

        float flickValue_x;
        [SerializeField] PlayerController pc;
        [SerializeField] MeatController mc;

        void Init() 
        {
            transform.TweenEulerAngles(new Vector3(0f, 0f, 0f), 0f).SetEase(Ease.InOutCubic);
        }

        void Start()
        {
            this.UpdateAsObservable()
                .Where(_ => mc.MeatState.Value == Meat.SET)
                .Subscribe(_ =>
            {
                if (Input.GetMouseButtonDown(0) == true)
                {
                    startTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
                }
                if (Input.GetMouseButtonUp(0) == true)
                {
                    endTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
                    FlickDirection();
                    GetDirection();
                }
            })
            .AddTo(this);

            Init();
        }

        void FlickDirection()
        {
            flickValue_x = endTouchPos.x - startTouchPos.x;
            //DebugLogger.Log("x スワイプ量" + flickValue_x);
        }

        void GetDirection()
        {
            if (flickValue_x > 200)
            {
                transform.TweenEulerAngles(new Vector3(0f, 270, 0f), 0.5f).SetEase(Ease.InOutCubic);
                pc.GameObjectSelectActive(2);
            }

            if (flickValue_x < -200)
            {
                transform.TweenEulerAngles(new Vector3(0f, 0f, 0f), 0.5f).SetEase(Ease.InOutCubic);
                pc.GameObjectSelectActive(1);

            }
        }
    }
}
