using UnityEngine;

// tell unity what other components are required (adding script will automatically add them to game object)
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PipeMainWater : MonoBehaviour
{
    public Pipe pipe;
    public int sizeX, sizeY;
    public float width, length;

    private Vector3[] vertices;

    private Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        Generate();
    }

    private void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "PipeMain_Water";

        // generate vertices
        vertices = new Vector3[(sizeX + 1) * (sizeY + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        float unitW = width / (float)sizeX;
        float unitZ = length / (float)sizeY;
        for (int i = 0, y = 0; y <= sizeY; y++)
        {
            
            for (int x = 0; x <= sizeX; x++, i++)
            {
                vertices[i] = new Vector3(
                    x * unitW - width * 0.5f,
                    Mathf.Cos(2.0f * (x / (float)sizeX) - 1.0f) * -6.0f + 2.0f, 
                    y * unitZ - 10.0f);

                uv[i] = new Vector2(x / (float)sizeX, y / (float)sizeY);
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;

        // generate triangles
        int[] triangles = new int[sizeX * sizeY * 6];
        for (int ti = 0, vi = 0, y = 0; y < sizeY; y++, vi++)
        {
            for (int x = 0; x < sizeX; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 1] = sizeX + vi + 1;
                triangles[ti + 2] = vi + 1;
                triangles[ti + 3] = vi + 1;
                triangles[ti + 4] = sizeX + vi + 1;
                triangles[ti + 5] = sizeX + vi + 2;
            }
        }
        mesh.triangles = triangles;
    }
}
