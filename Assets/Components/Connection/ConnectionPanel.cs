using UnityEngine;
using UnityEngine.UI;

/**
 * Connection Panel Class
 * View class to display the connection (Holographic Remoting) panel.
 * 
 */
public class ConnectionPanel : MonoBehaviour {
  [Header("Connect Panel")]
  [SerializeField] GameObject connectPanel;
  [SerializeField] private Text IPTextField;
  [SerializeField] private Text logTextField;

  [Header("Disconnect Panel")]
  [SerializeField] GameObject disconnectPanel;

  void Awake() {
    OpenConnectPanel();
    WriteLog("");
  }

  /*
   * Returns the ip value entered by the user
   *
   */
  public string GetIP() {
    return IPTextField.text;
  }

  /*
   * Write a log message on the interface
   * 
   */
  public void WriteLog(string log) {
    logTextField.text = log;
  }

  /*
   * Set the connection state
   * Toggles between the connection panel and the disconnect button
   * 
   */
  public void SetConnected() { OpenDisconnectPanel(); }
  public void SetDisconnected() {
    OpenConnectPanel();
    WriteLog("Successfully disconnected");
  }

  private void OpenConnectPanel() {
    disconnectPanel.SetActive(false);
    connectPanel.SetActive(true);
  }

  private void OpenDisconnectPanel() {
    connectPanel.SetActive(false);
    disconnectPanel.SetActive(true);
  }
}
