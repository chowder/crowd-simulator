using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool particlesEnabled = true;

    void Awake()
    {
        ConsoleManager.Log(
            "<color=orange><b>Welcome to Crowd Simulator.</b></color>\n" +
            "    <b>Commands:</b>\n" +
            "    <b>M</b> - Add new agents\n" +
            "    <b>P</b> - Hide and display device pings\n" +
            "    <b>Space</b> - Start and pause simulation");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleParticles();
        }
    }

    private void ToggleParticles()
    {
        if (particlesEnabled)
            Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Particles")); // Hide
        else
            Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("Particles"); // Show
        particlesEnabled = !particlesEnabled;
    }

    void OnApplicationQuit()
    {
        CSVWriter.Close();
        int minutes = Mathf.FloorToInt(Time.realtimeSinceStartup / 60F);
        int seconds = Mathf.FloorToInt(Time.realtimeSinceStartup - minutes * 60);
        string time = string.Format("{0:0}:{1:00}", minutes, seconds);
        Debug.Log("Total simulation time " + time);
    }
}
