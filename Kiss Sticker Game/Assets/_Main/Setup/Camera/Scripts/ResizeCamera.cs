using UnityEngine;

public class ResizeCamera : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private BoxCollider2D b;
    [SerializeField] private float padding = 0f;

    void Start()
    {
        cam = GetComponent<Camera>();

        FitCamera();
    }

    void Update()
    {
        
    }

    private void FitCamera()
    {
        cam.orthographic = true;

        //float aspect = cam.aspect;
        float aspect = (float)Screen.width / Screen.height;

        float halfHeight = b.bounds.extents.y;
        float halfWidth = b.bounds.extents.x;

        // size necessário para caber tudo:
        float sizeByHeight = halfHeight;
        float sizeByWidth = halfWidth / aspect;

        cam.orthographicSize = Mathf.Max(sizeByHeight, sizeByWidth) + padding;
        
        var pos = b.bounds.center;
        pos.z = cam.transform.position.z;
        cam.transform.position = pos;
    }
}
