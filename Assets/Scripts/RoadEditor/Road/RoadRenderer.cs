using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class RoadRenderer : MonoBehaviour
{
    [Range(.05f, 1.5f)]
    public float spacing = 0.3f;
    public float roadWidth = 1;
    public bool autoUpdate;
    public float tiling = 1;

    private MeshRenderer _renderer;
    private MeshFilter _filter;

    private void Awake()
    {
        _filter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();
    }

    public void UpdateRoad(Vector3[] points, bool rightLane, float widthOffset)
    {
        if (!_renderer.enabled)
            return;

        _filter.mesh = CreateRoadMesh(points, rightLane, widthOffset);
        int textureRepeat = Mathf.RoundToInt(tiling * points.Length * spacing * .05f);
        _renderer.sharedMaterial.mainTextureScale = new Vector2(1, textureRepeat);
    }

    public void SetVisibility(bool visible)
    {
        _renderer.enabled = visible;
    }

    private Mesh CreateRoadMesh(Vector3[] points, bool right, float widthOffset)
    {
        Vector3[] verts = new Vector3[points.Length * 2];
        Vector2[] uvs = new Vector2[verts.Length];
        int numTris = 2 * (points.Length - 1);
        int[] tris = new int[numTris * 3];
        int vertIndex = 0;
        int triIndex = 0;

        if (!right)
        {
            for (int i = 0; i < points.Length; i++)
            {
                Vector3 forward = Vector3.zero;
                if (i < points.Length - 1)
                {
                    forward += points[(i + 1) % points.Length] - points[i];
                }
                if (i > 0)
                {
                    forward += points[i] - points[(i - 1 + points.Length) % points.Length];
                }

                forward.Normalize();
                Vector3 left = Vector3.Cross(Vector3.down, forward);

                verts[vertIndex] = points[i] + left * (roadWidth + widthOffset);
                verts[vertIndex + 1] = points[i] + left * widthOffset;

                float completionPercent = i / (float)(points.Length - 1);
                float v = 1 - Mathf.Abs(2 * completionPercent - 1);
                uvs[vertIndex] = new Vector2(0, v);
                uvs[vertIndex + 1] = new Vector2(1, v);

                if (i < points.Length - 1)
                {
                    tris[triIndex] = vertIndex;
                    tris[triIndex + 1] = (vertIndex + 2) % verts.Length;
                    tris[triIndex + 2] = vertIndex + 1;

                    tris[triIndex + 3] = vertIndex + 1;
                    tris[triIndex + 4] = (vertIndex + 2) % verts.Length;
                    tris[triIndex + 5] = (vertIndex + 3) % verts.Length;
                }

                vertIndex += 2;
                triIndex += 6;
            }
        }
        else
        {
            for (int i = points.Length - 1; i >= 0; i--)
            {
                Vector3 forward = Vector3.zero;
                if (i > 0)
                {
                    forward += points[(i - 1) % points.Length] - points[i];
                }
                if (i < points.Length - 1)
                {
                    forward += points[i] - points[(i + 1 + points.Length) % points.Length];
                }

                forward.Normalize();
                Vector3 left = Vector3.Cross(Vector3.down, forward);

                verts[vertIndex] = points[i] + left * (roadWidth + widthOffset);
                verts[vertIndex + 1] = points[i] + left * widthOffset;

                float completionPercent = (points.Length - 1 - i) / (float)(points.Length - 1);
                float v = 1 - Mathf.Abs(2 * completionPercent - 1);
                uvs[vertIndex] = new Vector2(0, v);
                uvs[vertIndex + 1] = new Vector2(1, v);

                if (i > 0)
                {
                    tris[triIndex] = vertIndex;
                    tris[triIndex + 1] = (vertIndex + 2) % verts.Length;
                    tris[triIndex + 2] = vertIndex + 1;

                    tris[triIndex + 3] = vertIndex + 1;
                    tris[triIndex + 4] = (vertIndex + 2) % verts.Length;
                    tris[triIndex + 5] = (vertIndex + 3) % verts.Length;
                }

                vertIndex += 2;
                triIndex += 6;
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.uv = uvs;

        return mesh;
    }
}
