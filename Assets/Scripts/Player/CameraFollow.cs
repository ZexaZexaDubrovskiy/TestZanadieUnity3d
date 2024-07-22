using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector2 mapBoundsX = new Vector2(-20.0f, 20.0f);
    public Vector2 mapBoundsZ = new Vector2(-15.0f, 15.0f);

    private Vector3 offset;

    void Start() => offset = transform.position - player.position;
    
    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        float cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float cameraHalfHeight = Camera.main.orthographicSize;

        float minX = mapBoundsX.x + cameraHalfWidth;
        float maxX = mapBoundsX.y - cameraHalfWidth;
        float minZ = mapBoundsZ.x + cameraHalfHeight;
        float maxZ = mapBoundsZ.y - cameraHalfHeight;

        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
        desiredPosition.z = Mathf.Clamp(desiredPosition.z, minZ, maxZ);

        transform.position = new Vector3(desiredPosition.x, transform.position.y, desiredPosition.z);
    }
}
