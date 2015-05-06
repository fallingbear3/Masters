using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof (Camera))]
public class LockCameraToBoxCollider2D : MonoBehaviour
{
    public BoxCollider2D rect;

    private void LateUpdate()
    {
        if (rect != null)
        {
            // Extract infomation about camera rectangle from orthographic size.
            float aspectRatio = (float) Screen.width/Screen.height;
            float height = GetComponent<Camera>().orthographicSize;
            float width = GetComponent<Camera>().orthographicSize*aspectRatio;
            var cameraExtents = new Vector3(width, height, 0);

            Rect cameraRect = toRect(transform.position, cameraExtents);
            Rect colliderRect = toRect(rect.transform.position, rect.bounds.extents);

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