using UnityEngine;

namespace Unity1Week_20230918
{
    public class BoController : MonoBehaviour
    {
        private Vector3 screenPoint;

        void OnMouseDrag()
        {
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);

            float screenX = Input.mousePosition.x;
            float screenY = Input.mousePosition.y;
            float screenZ = screenPoint.z;

            Vector3 currentScreenPoint = new Vector3(screenX, screenY, screenZ);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);
            transform.position = currentPosition;
        }

        void OnCollisionEnter(Collision collision)
        {
            var hit = collision.gameObject;
            var d = hit.GetComponent<IObjectHit>();

            if (d != null && hit.name == "wantan") d.Hit();

        }
    }
}
