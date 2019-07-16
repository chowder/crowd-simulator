using UnityEngine;
using UnityEngine.UI;

public class TimescaleManager : MonoBehaviour
{
    [SerializeField]
    private Text playButtonText = null;

    private const string playSymbol = "\u25B6";
    private const string pauseSymbol = "\u25A0";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        if (playButtonText.text == playSymbol) // if currently paused
        {
            playButtonText.text = pauseSymbol;
            playButtonText.fontSize = 16;
            Time.timeScale = 1;
        }
        else // if currently playing
        {
            playButtonText.text = playSymbol;
            playButtonText.fontSize = 12;
            Time.timeScale = 0;
        }
        ConsoleManager.Log("Simulation " + (Time.timeScale == 0 ? "paused" : "resumed"));
    }
}
