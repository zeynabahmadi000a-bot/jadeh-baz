using UnityEngine;

public class QuickStartScene : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void CreateDemo()
    {
        if (Object.FindObjectOfType<QuickStartScene>()) return;
        var root = new GameObject("JadehBaz Demo Setup");
        root.AddComponent<QuickStartScene>();
    }

    void Start()
    {
        var world = new GameObject("World");
        world.AddComponent<WorldBuilder>();

        var car = GameObject.CreatePrimitive(PrimitiveType.Cube);
        car.name = "Player Car - Prototype";
        car.transform.position = new Vector3(0, 1.1f, 12);
        car.transform.localScale = new Vector3(1.9f, 1f, 4f);
        var rb = car.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        car.AddComponent<ArcadeCarController>();

        var cameraObject = new GameObject("Follow Camera");
        var cam = cameraObject.AddComponent<Camera>();
        cam.fieldOfView = 68f;
        var follow = cameraObject.AddComponent<FollowCamera>();
        follow.target = car.transform;
        cameraObject.transform.position = car.transform.position + new Vector3(0, 4, -8);
        cameraObject.transform.LookAt(car.transform.position);
        cam.tag = "MainCamera";

        var lightObject = new GameObject("Sun");
        var light = lightObject.AddComponent<Light>();
        light.type = LightType.Directional;
        light.intensity = 1.15f;
        lightObject.transform.rotation = Quaternion.Euler(50, -30, 0);

        var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Temporary Ground";
        ground.transform.position = new Vector3(0, -0.15f, 210);
        ground.transform.localScale = new Vector3(45, 1, 45);
    }
}
