using UnityEngine;

/**
 * View Button Controller Class
 * Controller for the world space buttons used to move the anchors
 * 
 */
public class ViewButtonController : MonoBehaviour {
  [SerializeField] private PositionningManager positionner;

  private float[] speeds = new float[] { 0.0001f, 0.001f, 0.01f };
  private float currentSpeed;

  void Awake() {
    Close();
    currentSpeed = speeds[1];
  }

  /*
   * Translate this panel in world space to be relative to the brain position
   * So it's easier for the user to use this panel
   * 
   */
  public void Translate(Vector3 position) {
    transform.position = position;
  }

  /*
   * Control the display of this panel
   * 
   */
  public void Open() { gameObject.SetActive(true); }
  public void Close() { gameObject.SetActive(false); }

  /*
   * Select the speed at which the anchors are moved
   * As defined in the speeds array
   * 
   */
  public void Speed(int i) {
    currentSpeed = speeds[i];
  }

  /*
   * Apply translations to the anchors
   * 
   */
  public void RedPositive() { positionner.TranslateAnchor(Vector3.right * currentSpeed); }
  public void RedNegative() { positionner.TranslateAnchor(Vector3.left * currentSpeed); }
  public void GreenPositive() { positionner.TranslateAnchor(Vector3.up * currentSpeed); }
  public void GreenNegative() { positionner.TranslateAnchor(Vector3.down * currentSpeed); }
  public void BluePositive() { positionner.TranslateAnchor(Vector3.forward * currentSpeed); }
  public void BlueNegative() { positionner.TranslateAnchor(Vector3.back * currentSpeed); }
}
