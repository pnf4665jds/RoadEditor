using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class RoadRenderer : MonoBehaviour
{
    /// <summary>
    /// 獨立出一個component來畫出整個road
    /// </summary>

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

    // 更新路面mesh
    public void UpdateRoad(ReferenceLineWrapper wrapper, List<Lane> leftLanes, List<Lane> rightLanes)
    {
        if (!_renderer.enabled)
            return;

        _filter.sharedMesh = CreateRoadMesh(wrapper, leftLanes, rightLanes);
        int textureRepeat = Mathf.RoundToInt(tiling * wrapper.GetPointCount() * spacing * .05f);
        _renderer.sharedMaterial.mainTextureScale = new Vector2(1, textureRepeat);
    }

    public void SetVisibility(bool visible)
    {
        _renderer.enabled = visible;
    }

    private Mesh CreateRoadMesh(ReferenceLineWrapper wrapper, List<Lane> leftLanes, List<Lane> rightLanes)
    {
        int totalLaneCount = leftLanes.Count + rightLanes.Count;
        Vector3[] verts = new Vector3[wrapper.GetPointCount() * 2 * totalLaneCount];
        Vector2[] uvs = new Vector2[verts.Length];
        int numTris = 2 * (wrapper.GetPointCount() - 1) * totalLaneCount;
        int[] tris = new int[numTris * 3];
        int vertIndex = 0;
        int triIndex = 0;

        Vector3[] points = new Vector3[wrapper.GetPointCount()];
        for(int i = 0; i < points.Length; i++)
        {
            points[i] = wrapper.GetReferenceLinePos((1.0f / (points.Length - 1)) * i);
        }

        for(int j = 0; j < leftLanes.Count; j++){
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

                int preIndex = vertIndex - wrapper.GetPointCount() * 2;

                if (j == 0)
                {
                    verts[vertIndex] = points[i] + left;
                    verts[vertIndex + 1] = points[i];
                }
                else
                {
                    verts[vertIndex] = verts[preIndex] + left;
                    verts[vertIndex + 1] = verts[preIndex];
                }
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
                    triIndex += 6;
                }
                vertIndex += 2;
            }
        }

        for (int j = leftLanes.Count; j < totalLaneCount; j++)
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

                int preIndex = vertIndex - wrapper.GetPointCount() * 2;

                if (j == leftLanes.Count)
                {
                    verts[vertIndex] = points[i] + left;
                    verts[vertIndex + 1] = points[i];
                }
                else
                {
                    verts[vertIndex] = verts[preIndex] + left;
                    verts[vertIndex + 1] = verts[preIndex];
                }

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

                    triIndex += 6;
                }
                vertIndex += 2;
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.uv = uvs;

        return mesh;
    }
}
