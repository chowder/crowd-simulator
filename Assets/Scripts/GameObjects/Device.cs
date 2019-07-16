using System.Collections;
using UnityEngine;

public class Device
{
    private float pulseRate = Constants.devicePulseRate;
    private string macAddress;
    private Agent owner;

    public Agent Owner
    {
        get => owner;
        set
        {
            owner = owner ?? value;
        }
    }

    public Device(Agent owner)
    {
        Owner = owner;
        macAddress = MACGenerator.Generate();
    }

    public IEnumerator Pulse()
    {
        while (true)
        {
            yield return new WaitForSeconds(pulseRate);
            owner.Pulse.Play();
            ConsoleManager.Log(macAddress);
            CSVWriter.Write(Time.time, macAddress, owner.transform.position);
        }
    }
}
