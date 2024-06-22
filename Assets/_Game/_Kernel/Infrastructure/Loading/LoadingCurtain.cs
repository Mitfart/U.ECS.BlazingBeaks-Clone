using UnityEngine;

namespace Infrastructure.Loading {
  public class LoadingCurtain : MonoBehaviour, ILoadingCurtain {
    public void Show() {
      gameObject.SetActive(true);
    }

    public void Hide() {
      gameObject.SetActive(false);
    }

    private void Awake() {
      DontDestroyOnLoad(this);
    }
  }
}