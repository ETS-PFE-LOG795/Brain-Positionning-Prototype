using UnityEngine;

/**
 * Connector Class
 * Enable the connection via Holographic Remoting between the app and the Hololens
 * 
 */
public class Connector : MonoBehaviour {
  private Microsoft.MixedReality.OpenXR.Remoting.RemotingConfiguration remotingConfiguration = new Microsoft.MixedReality.OpenXR.Remoting.RemotingConfiguration { RemotePort = 8265, MaxBitrateKbps = 20000 };
  private bool connected = false;

  /*
   * Attempt to connect via ip to the Holographic Remoting Player app on the HoloLens
   * 
   */
  public void TryConnect(string ip) {
    if (!connected) {
      connected = true;
      remotingConfiguration.RemoteHostName = ip;
      StartCoroutine(Microsoft.MixedReality.OpenXR.Remoting.AppRemoting.Connect(remotingConfiguration));
    } else {
      Debug.Log("Cannot connect: Already connected");
    }
  }

  /*
   * Stop the connection between the app and the HoloLens
   * 
   */
  public void Disconnect() {
    if (connected) {
      Microsoft.MixedReality.OpenXR.Remoting.AppRemoting.Disconnect();
      connected = false;
    } else {
      Debug.Log("Cannot disconnect: Not connected");
    }
  }
}
