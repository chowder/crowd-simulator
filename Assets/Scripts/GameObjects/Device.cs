using System;
using System.Collections;
using UnityEngine;

public class Device
{
    private float pulseInterval = Constants.devicePulseInterval;
    private string macAddress;
    private Agent owner;
    private System.Random rand;

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
        rand = new System.Random();
    }

    public IEnumerator Pulse()
    {
        // Simulated initialising time 
        float init_delay = RandomPositiveGaussian(Constants.deviceInitialiseTime);
        ConsoleManager.Log(String.Format("Initialised with delay: {0:0.0}", init_delay));
        yield return new WaitForSeconds(init_delay);
        // Pulsing
        while (true)
        {
            yield return new WaitForSeconds(pulseInterval);
            owner.Pulse.Play();
            ConsoleManager.Log(macAddress);
            CSVWriter.Write(Time.time, macAddress, owner.transform.position);
            HttpManager.Send(Time.time, owner.transform.position);
        }
    }

    private float RandomGaussian(float mean, float stddev = 2)
    {
        double u1 = 1.0 - rand.NextDouble();
        double u2 = 1.0 - rand.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
            Math.Sin(2.0 * Math.PI * u2);
        return (float)(mean + stddev * randStdNormal);
    }

    private float RandomPositiveGaussian(float mean, float stddev = 2)
    {
        float r = 0;
        do
        {
            r = RandomGaussian(mean, stddev);
        } while (r <= 0);
        return r;
    }

    private float RandomTruncatedGaussian(float mean, float stddev = 1, float threshold = 0.2f)
    {
        float r = 0;
        do
        {
            r = RandomGaussian(mean, stddev);
        } while ((Math.Abs(r - mean) / mean) < threshold);
        return r;
    }
}
