using UnityEditor.SceneTemplate;
using UnityEngine.SceneManagement;

namespace _Scenes._Templates.Editor.Main {
  public class Scene_Main__TemplatePipeline : ISceneTemplatePipeline {
    public virtual bool IsValidTemplateForInstantiation(SceneTemplateAsset sceneTemplateAsset) {
      return true;
    }

    public virtual void BeforeTemplateInstantiation(
      SceneTemplateAsset sceneTemplateAsset,
      bool               isAdditive,
      string             sceneName
    ) { }

    public virtual void AfterTemplateInstantiation(
      SceneTemplateAsset sceneTemplateAsset,
      Scene              scene,
      bool               isAdditive,
      string             sceneName
    ) { }
  }
}