using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VRScrollViewLogger : MonoBehaviour
{
    public TextMeshProUGUI logText;
    public ScrollRect scrollRect;
    public int maxLogs = 50;

    Queue<string> logQueue = new Queue<string>();
    bool requiresUpdate;

    void OnEnable()
    {
        Application.logMessageReceivedThreaded += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceivedThreaded -= HandleLog;
    }

    void Update()
    {
        if (requiresUpdate)
        {
            lock (logQueue)
            {
                logText.text = string.Join("\n", logQueue);
            }
            
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
            requiresUpdate = false;
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        lock (logQueue)
        {
            if (logQueue.Count >= maxLogs)
            {
                logQueue.Dequeue();
            }

            string colorTag = type switch
            {
                LogType.Error or LogType.Exception or LogType.Assert => "<color=red>",
                LogType.Warning => "<color=yellow>",
                _ => "<color=white>"
            };

            logQueue.Enqueue($"{colorTag}{logString}</color>");
            requiresUpdate = true;
        }
    }
}