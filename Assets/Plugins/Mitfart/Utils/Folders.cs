#if UNITY_EDITOR
using System.IO;
using UnityEngine;

namespace Utils {
  public static class Folders {
    public static void Create(string root, params string[] dirs) {
      string fullPath = Path.Combine(Application.dataPath, root);

      Directory.CreateDirectory(fullPath);
      foreach (string dir in dirs)
        Directory.CreateDirectory(Path.Combine(fullPath, dir));
    }
  }
}
#endif