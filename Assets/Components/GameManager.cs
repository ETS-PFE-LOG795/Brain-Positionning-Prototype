using UnityEngine;

/**
 * Game Manager Class
 * Main, controls the app and the communications between the different scripts.
 * 
 */
public class GameManager : MonoBehaviour {
  [SerializeField] private Connector connector;
  [SerializeField] private ConnectionPanel connectionPanel;
  [SerializeField] private PositionningManager positionningManager;

  [Tooltip("Toggle Diagnostic System (Profiler)")]
  public bool debug = false;

  private void Awake() {
    Microsoft.MixedReality.Toolkit.CoreServices.DiagnosticsSystem.ShowDiagnostics = debug;
  }

  public void HolographicRemoteConnect() {
    connector.TryConnect(connectionPanel.GetIP());

    connectionPanel.SetConnected();
    positionningManager.Open();
    positionningManager.Init();
  }

  public void HolographicRemoteDisconnect() {
    connector.Disconnect();
    connectionPanel.SetDisconnected();
    positionningManager.Disconnected();
  }

  public void Quit() {
    Application.Quit();
  }
}
