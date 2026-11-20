using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ArcadeCarController : MonoBehaviour
{
    [Header("Driving")]
    public float acceleration = 18f;
    public float reverseAcceleration = 9f;
    public float maxSpeed = 24f;
    public float steering = 85f;
    public float brakeStrength = 5f;
    public float grip = 5f;

    Rigidbody body;
    float throttle;
    float turn;
    bool braking;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        body.mass = 1150f;
        body.drag = 0.12f;
        body.angularDrag = 2.5f;
        body.centerOfMass = new Vector3(0, -0.45f, 0.1f);
    }

    void Update()
    {
        throttle = Input.GetAxis("Vertical");
        turn = Input.GetAxis("Horizontal");
        braking = Input.GetKey(KeyCode.Space);
    }

    void FixedUpdate()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(body.velocity);
        float speed = body.velocity.magnitude;

        if (throttle > 0.05f && speed < maxSpeed)
            body.AddForce(transform.forward * throttle * acceleration, ForceMode.Acceleration);
        else if (throttle < -0.05f)
            body.AddForce(transform.forward * throttle * reverseAcceleration, ForceMode.Acceleration);

        if (braking)
            body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, brakeStrength * Time.fixedDeltaTime);

        float steeringFactor = Mathf.Clamp01(speed / 3f);
        float direction = localVelocity.z >= -0.2f ? 1f : -1f;
        transform.Rotate(0f, turn * steering * steeringFactor * direction * Time.fixedDeltaTime, 0f);

        Vector3 sideways = transform.right * localVelocity.x;
        body.AddForce(-sideways * grip, ForceMode.Acceleration);
    }
}
