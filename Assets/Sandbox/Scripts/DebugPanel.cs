using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour
{
    public Text log;
    public InputField stringToSend;

    private readonly int MAX_CHARS = 1280;
    private readonly string NEW_LINE = "\r\n";

    public void AddLog(string logString)
    {
        var newText = logString + NEW_LINE + log.text;
        newText = newText.Substring(0, Mathf.Min(newText.Length, MAX_CHARS));
        log.text = newText;
    }
}