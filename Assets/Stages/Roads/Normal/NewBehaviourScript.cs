using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector2[] uvs = new Vector2[10];

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(50, 50);
        }
        mesh.uv = uvs;
    }
}

