using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    public Material roadMaterial;
    public Material groundMaterial;

    void Start()
    {
        BuildGround();
        BuildRoad(new Vector3(0, 0.02f, 0), new Vector3(0, 0, 420), 12f);
        BuildRoad(new Vector3(0, 0.03f, 110), new Vector3(170, 0, 110), 10f);
        BuildRoad(new Vector3(0, 0.03f, 285), new Vector3(-150, 0, 285), 10f);
        BuildCity(new Vector3(55, 0, 90));
        BuildCity(new Vector3(-55, 0, 310));
        BuildForest();
    }

    void BuildGround()
    {
        GameObject g = GameObject.CreatePrimitive(PrimitiveType.Plane);
        g.name = "Open World Ground";
        g.transform.position = new Vector3(0, -0.08f, 210);
        g.transform.localScale = new Vector3(45, 1, 45);
        if (groundMaterial) g.GetComponent<Renderer>().material = groundMaterial;
    }

    void BuildRoad(Vector3 start, Vector3 end, float width)
    {
        Vector3 delta = end - start;
        GameObject road = GameObject.CreatePrimitive(PrimitiveType.Cube);
        road.name = "Connected Road";
        road.transform.position = (start + end) * 0.5f;
        road.transform.rotation = Quaternion.LookRotation(delta.normalized);
        road.transform.localScale = new Vector3(width, 0.12f, delta.magnitude + width);
        if (roadMaterial) road.GetComponent<Renderer>().material = roadMaterial;
    }

    void BuildCity(Vector3 center)
    {
        for (int x = -2; x <= 2; x++)
        for (int z = -2; z <= 2; z++)
        {
            if (Mathf.Abs(x) == 2 && Mathf.Abs(z) == 2) continue;
            GameObject building = GameObject.CreatePrimitive(PrimitiveType.Cube);
            building.name = "City Building";
            float h = Random.Range(5f, 17f);
            building.transform.position = center + new Vector3(x * 17f, h * 0.5f, z * 17f);
            building.transform.localScale = new Vector3(Random.Range(8f, 12f), h, Random.Range(8f, 12f));
        }
    }

    void BuildForest()
    {
        Random.InitState(42);
        for (int i = 0; i < 180; i++)
        {
            Vector3 p = new Vector3(Random.Range(-190f, 190f), 0, Random.Range(0f, 420f));
            if (Mathf.Abs(p.x) < 12f || (Mathf.Abs(p.z - 110f) < 8f) || (Mathf.Abs(p.z - 285f) < 8f)) continue;
            GameObject trunk = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            trunk.name = "Tree Trunk";
            trunk.transform.position = p + Vector3.up * 1.5f;
            trunk.transform.localScale = new Vector3(0.45f, 1.5f, 0.45f);
            GameObject crown = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            crown.name = "Tree Crown";
            crown.transform.position = p + Vector3.up * 4f;
            crown.transform.localScale = Vector3.one * Random.Range(3f, 4.5f);
        }
    }
}
