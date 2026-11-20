using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 4.2f, -7.5f);
    public float smooth = 6f;

    void LateUpdate()
    {
        if (!target) return;
        Vector3 desired = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, desired, smooth * Time.deltaTime);
        Quaternion look = Quaternion.LookRotation(target.position + Vector3.up * 1.2f - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, look, smooth * Time.deltaTime);
    }
}
