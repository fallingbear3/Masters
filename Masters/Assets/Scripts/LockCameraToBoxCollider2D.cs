using Assets.Shared.Scripts;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof (Camera))]
    public class LockCameraToBoxCollider2D : MonoBehaviour
    {
        public BoxCollider2D Rect;

        private void LateUpdate()
        {
            if (Rect != null)
            {
                // Extract infomation about camera rectangle from orthographic size.
                var aspectRatio = (float) Screen.width/Screen.height;
                var height = GetComponent<Camera>().orthographicSize;
                var width = GetComponent<Camera>().orthographicSize*aspectRatio;
                var cameraExtents = new Vector3(width, height, 0);

                var cameraRect = toRect(transform.position, cameraExtents);
                var colliderRect = toRect(Rect.transform.position, Rect.bounds.extents);

                if (cameraRect.xMin < colliderRect.xMin) transform.SetX(colliderRect.xMin + cameraExtents.x);
                if (cameraRect.xMax > colliderRect.xMax) transform.SetX(colliderRect.xMax - cameraExtents.x);
                if (cameraRect.yMin < colliderRect.yMin) transform.SetY(colliderRect.yMin + cameraExtents.y);
                if (cameraRect.yMax > colliderRect.yMax) transform.SetY(colliderRect.yMax - cameraExtents.y);
            }
        }

        private Rect toRect(Vector3 center, Vector3 extents)
        {
            return new Rect(center.x - extents.x, center.y - extents.y, extents.x*2, extents.y*2);
        }
    }
}