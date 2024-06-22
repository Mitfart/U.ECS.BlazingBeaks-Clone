using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Loading {
  public class SceneLoader : ISceneLoader {
    public async void Load(string name, Action onLoaded = null) {
      await LoadScene(name, onLoaded);
    }


    private static async Task LoadScene(string name, Action onLoaded = null) {
      AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(name);

      while (!loadSceneOperation.isDone)
        await Task.Delay(100);

#if UNITY_EDITOR
      if (!Application.isPlaying)
        return;
#endif

      Scene loadedScene = SceneManager.GetSceneByName(name);
      SceneManager.SetActiveScene(loadedScene);

      onLoaded?.Invoke();
    }
  }

  public interface ISceneLoader {
    void Load(string name, Action onLoaded = null);
  }
}