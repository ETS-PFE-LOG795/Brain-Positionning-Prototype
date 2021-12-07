using UnityEngine;

/**
 * Console Debugger Class
 * Debug tool, display the unity console in the app
 * 
 * https://answers.unity.com/questions/125049/is-there-any-way-to-view-the-console-in-a-build.html
 * 
 */
public class ConsoleDebugger : MonoBehaviour {
  [Tooltip("Displays an in-app console (for debugging or connection issues)")]
  public bool active = false;

  #if !UNITY_EDITOR
  static string myLog = "";
  private string output;
  private string stack;

  void OnEnable() {
    if (active)
      Application.logMessageReceived += Log;
  }

  private void OnDisable() {
    if (active)
      Application.logMessageReceived -= Log;
  }

  public void Log(string logString, string stackTrace, LogType type) {
    output = logString;
    stack = stackTrace;
    myLog = output + "\n" + myLog;
    if (myLog.Length > 5000)
      myLog = myLog.Substring(0, 4000);
  }

  private void OnGUI() {
    if (active)
      myLog = GUI.TextArea(new Rect(10, Screen.height - Screen.height / 4.0f - 10, Screen.width - 20, Screen.height / 4.0f), myLog);
  }
  #endif
}
