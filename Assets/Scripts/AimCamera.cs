using UnityEngine;

public class AimCamera : MonoBehaviour
{
    public Vector2 OffsetAngle;

    public Vector3 Offset;
    public float Distance;

    private float yaw;
    private float pitch;
    private Vector3 anchorPoint;

    private void Awake()
    {
        yaw = OffsetAngle.x;
        pitch = OffsetAngle.y;
        anchorPoint = transform.position + Offset;
    }

    private void Update()
    {
        yaw += Input.GetAxis("Mouse X");
        pitch = Mathf.Clamp(pitch - Input.GetAxis("Mouse Y"), -45f, 45f);

        anchorPoint += Quaternion.Euler(0, yaw, 0)*new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(pitch, yaw, 0), 10f * Time.deltaTime);

        transform.position = anchorPoint + Distance * transform.TransformDirection(0, 0, -1f) + Offset;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Quaternion.Euler(pitch, yaw, 0)*new Vector3(0, 0, 1)));
    }
}
