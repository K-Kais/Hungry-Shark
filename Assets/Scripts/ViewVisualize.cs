using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewVisualize : MonoBehaviour
{
    [SerializeField] FloatVariable _viewRadius;
    [SerializeField] int _meshResolution;
    private MeshFilter _meshFilter;
    private Mesh _mesh;
    private void Start()
    {
        _mesh = new Mesh();
        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = _mesh;
    }
    private void Update()
    {
        DrawViewSpace();
    }
    private void DrawViewSpace()
    {
        int triangleCount = _meshResolution;
        int vertexCount = _meshResolution + 1;
        int[] triangles = new int[triangleCount * 3];
        Vector3[] vertices = new Vector3[vertexCount];

        for (int i = 0; i < triangleCount; i++)
        {
            triangles[3 * i] = 0;
            triangles[3 * i + 1] = i + 1;
            triangles[3 * i + 2] = i + 2;
        }
        triangles[3 * triangleCount - 1] = 1;

        vertices[0] = Vector3.zero;
        for (int i = 1; i < vertexCount; i++)
        {
            float stepSize = 360f / _meshResolution;
            float xPos = Mathf.Cos(stepSize * (i - 1) * Mathf.Deg2Rad);
            float xyPos = Mathf.Sin(stepSize * (i - 1) * Mathf.Deg2Rad);
            Vector3 direction = new Vector3(xPos, xyPos, 0);
            vertices[i] = direction * _viewRadius.Value;
        }
        _mesh.Clear();
        _mesh.vertices = vertices;
        _mesh.triangles = triangles;
        _mesh.RecalculateNormals();
    }
}
