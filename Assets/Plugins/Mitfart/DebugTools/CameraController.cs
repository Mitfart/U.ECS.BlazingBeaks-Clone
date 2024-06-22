#if UNITY_EDITOR
#if ENABLE_INPUT_SYSTEM
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
#endif

namespace DebugTools {
  public class CameraController : MonoBehaviour {
    [SerializeField] private     float  speed;
    [SerializeField] private new Camera camera;

    private void Update() {
      Transform t     = camera.transform;
      float     delta = Time.deltaTime;


      Vector3 input = GetKeyboardInput();
      t.LookAt(t.position + MouseUtils.WorldPos3D(camera));
      input = t.rotation * input;
      if (input != default)
        t.Translate(input * (speed * delta));
    }

    private void OnValidate() {
      if (camera.IsUnityNull())
        TryGetComponent(out camera);
    }

    private static Vector3 GetKeyboardInput() {
      bool inputLeft;
      bool inputRight;
      bool inputForward;
      bool inputBackward;
      bool inputUp;
      bool inputDown;
#if ENABLE_INPUT_SYSTEM
      inputLeft     = Keyboard.current.aKey.isPressed;
      inputRight    = Keyboard.current.dKey.isPressed;
      inputForward  = Keyboard.current.wKey.isPressed;
      inputBackward = Keyboard.current.sKey.isPressed;
      inputUp       = Keyboard.current.spaceKey.isPressed;
      inputDown     = Keyboard.current.leftShiftKey.isPressed;
#elif ENABLE_LEGACY_INPUT_MANAGER
			inputLeft = Input.GetKey(KeyCode.A);
			inputRight = Input.GetKey(KeyCode.D);
			inputForward = Input.GetKey(KeyCode.W);
			inputBackward = Input.GetKey(KeyCode.S);
			inputUp = Input.GetKey(KeyCode.Space);
			inputDown = Input.GetKey(KeyCode.LeftShift);
#endif


      float x = inputRight && inputLeft ? 0f :
        inputRight                      ? 1f :
        inputLeft                       ? -1f : 0f;
      float y = inputUp && inputDown ? 0f :
        inputUp                      ? 1f :
        inputDown                    ? -1f : 0f;
      float z = inputForward && inputBackward ? 0f :
        inputForward                          ? 1f :
        inputBackward                         ? -1f : 0f;

      return new Vector3(x, y, z);
    }
  }
}
#endif