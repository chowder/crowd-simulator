using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject consolePanel = null;
    [SerializeField]
    private Text consoleTextObject = null;

    private const int maxMessages = Constants.consoleMaxMessages;
    private List<Text> messages = new List<Text>();

    private static ConsoleManager console;

    public static ConsoleManager Console
    {
        get
        {
            if (console == null)
            {
                GameObject o = new GameObject();
                console = o.AddComponent<ConsoleManager>();
            }
            return console;
        }
    }

    void Awake()
    {
        console = this;
    }
    private void addMessage(string message)
    {
        if (messages.Count >= maxMessages) {
            Destroy(messages[0].gameObject);
            messages.RemoveAt(0);
        }
        Text newConsoleText = Instantiate(consoleTextObject, consolePanel.transform);
        newConsoleText.text = message;
        messages.Add(newConsoleText);
    }

    public static void Log(string message)
    {
        int minutes = Mathf.FloorToInt(Time.time / 60F);
        int seconds = Mathf.FloorToInt(Time.time - minutes * 60);
        string time = string.Format("<color=silver><b>[{0:0}:{1:00}]</b></color> ", minutes, seconds);
        Console.addMessage(time + message);
    }
}