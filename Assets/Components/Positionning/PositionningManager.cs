using UnityEngine;

/**
 * Positionning Manager Class
 * Controls the steps to position the brain.
 * 
 */
public class PositionningManager : MonoBehaviour {
  [Header("Model")]
  [SerializeField] private GameObject brain;
  [SerializeField] private Transform[] anchorBrainPositions;
  [SerializeField] private GameObject[] anchors;

  // Toggle the ability of the user to move the brain
  [SerializeField] private Microsoft.MixedReality.Toolkit.UI.ObjectManipulator objectManipulator;

  [Header("UI")]
  [SerializeField] private GameObject[] buttons;
  [SerializeField] private ViewButtonController buttonMoveController;
  
  private int currentAnchorIndex = -1;

  private void Awake() {
    DisableAll();
    Close();
  }

  /*
   * Open the Options for the player to begin positionning
   * 
   */
  public void Open() {
    for (int i = 0; i < buttons.Length; ++i)
      buttons[i].SetActive(true);
  }

  /*
   * Hide the positionning Options
   * 
   */
  public void Close() {
    for (int i = 0; i < buttons.Length; ++i)
      buttons[i].SetActive(false);
  }

  /*
   * Toggle between the connection panel and this panel
   * 
   */
  public void Disconnected() {
    Close();
    DisableAll();
  }

  public void Init() {
    // Default action after open
    PlaceBrain();
  }

  /*
   * Step #1 - the user roughly position the brain manually
   * 
   */
  public void PlaceBrain() {
    DisableAll();
    brain.SetActive(true);
    objectManipulator.enabled = true;
  }

  /*
   * Step #1.1 - The user Apply the new brain transform
   * So the UI reflect its new positionning
   * 
   */
  public void ApplyBrainPlacement() {
    for(int i = 0; i < anchorBrainPositions.Length; ++i)
      anchors[i].transform.position = anchorBrainPositions[i].position;
    buttonMoveController.Translate(brain.transform.position);
  }

  /*
   * Step 2 - One by one, the user places the 3 anchors in their final position
   * Using the world space buttons and finer control
   * 
   */
  public void PlaceAnchor(int index) {
    DisableAll();
    currentAnchorIndex = index - 1;
    anchors[currentAnchorIndex].SetActive(true);
    buttonMoveController.Open();
  }

  public void TranslateAnchor(Vector3 translation) {
    // Translate the current anchor by a position delta defined in the ViewButtonController class
    anchors[currentAnchorIndex].transform.position += translation;
  }

  /*
   * Step 3 - Using the anchors, find the new, more accurate position of the brain
   * 
   */
  public void CalculateFinalPosition() {
    // Get the 2 vectors that will make the baseline for our new coordinate system (using the 3 anchors)
    Vector3 a = (anchors[4-1].transform.position - anchors[3-1].transform.position).normalized; // Vector "Left"
    Vector3 b = (anchors[2-1].transform.position - anchors[3-1].transform.position).normalized; // Vector "Forward"

    // Get the 3rd vector "Up" for our coordinate system
    Vector3 c = Vector3.Cross(a, b).normalized;
    
    // Debug
    Debug.Log("A: " + a + ", B: " + b);
    Debug.Log("UP: " + c);
    float dot = Mathf.Abs(Vector3.Dot(a, b));
    Debug.Log("Error in coordinate system: " + dot); // Perpendicular, expecting 0

    // Rotate the brain in the new coordinate system
    brain.transform.rotation = Quaternion.identity;
    brain.transform.position = anchors[3-1].transform.position;
    brain.transform.LookAt(anchors[2-1].transform.position, c);
    
    // Debug
    Vector3 compareVector = -brain.transform.right;
    float compareDot = Vector3.Dot(compareVector, a);
    Debug.Log("Error after rotation: " + (1f - compareDot)); // Same direction, expecting 1

    // Position the brain
    Vector3 dt = brain.transform.position - anchorBrainPositions[3-1].transform.position;
    brain.transform.position = anchors[3-1].transform.position + dt;

    // Display the brain
    DisableAll();
    brain.SetActive(true);
  }

  /*
   * Disable all components for this panel
   * 
   */
  private void DisableAll() {
    for (int i = 0; i < anchors.Length; ++i)
      anchors[i].SetActive(false);
    brain.SetActive(false);
    buttonMoveController.Close();
    objectManipulator.enabled = false;
  }
}
