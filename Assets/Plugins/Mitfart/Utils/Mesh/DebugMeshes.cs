#if UNITY_EDITOR
using UnityEngine;

namespace Utils.Mesh {
  public static class DebugMeshes {
    public static UnityEngine.Mesh Arrow(Vector3 size = default, Vector3 percentsOffset = default) {
      UnityEngine.Mesh mesh = new();

      Vector3[] vertices = {
        new Vector3(0,    2.4f),
        new Vector3(-.4f, 1.6f),
        new Vector3(+.4f, 1.6f),
        new Vector3(-.1f, 0f),
        new Vector3(-.1f, 1.6f),
        new Vector3(+.1f, 1.6f),
        new Vector3(+.1f, 0f)
      };
      int[] triangles = {
        1,
        0,
        2,
        3,
        4,
        5,
        5,
        6,
        3
      };

      for (var i = 0; i < vertices.Length; i++) {
        vertices[i]   /= 2.4f;
        vertices[i]   += percentsOffset;
        vertices[i].x *= Mathf.Abs(size.x);
        vertices[i].y *= Mathf.Abs(size.y);
        vertices[i].z *= Mathf.Abs(size.z);
      }

      mesh.vertices  = vertices;
      mesh.triangles = triangles;
      mesh.RecalculateNormals();
      return mesh;
    }
  }
}

#endif