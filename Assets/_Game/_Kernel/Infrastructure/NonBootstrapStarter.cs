using UnityEngine;

namespace Infrastructure {
  [DefaultExecutionOrder(-1)]
  public class NonBootstrapStarter : MonoBehaviour {
#if UNITY_EDITOR
    private void Awake() {
      if (FindObjectOfType<Bootstrap>() != null)
        return;

      Bootstrap prefab = Resources.Load<Bootstrap>(ResourcesPath.BOOTSTRAP);
      Instantiate(prefab);
    }
#endif
  }
}