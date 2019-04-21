using TMPro;
using UnityEngine;
using Vuforia;

public static class VuforiaTools {

    public static bool IsBeingTracked(string imageTargetName) {
        var imageTarget = GameObject.Find(imageTargetName);
        var trackable = imageTarget.GetComponent<TrackableBehaviour>();
        var status = trackable.CurrentStatus;

        return status == TrackableBehaviour.Status.TRACKED;
    }

    public static void AddTextToDebugger(string text) {
        var debuggerTextGO = GameObject.FindGameObjectWithTag("DebuggerText");

        if (debuggerTextGO) {
            var textPro = debuggerTextGO.GetComponent<TextMeshPro>();

            textPro.text = text;
        }
    }
}