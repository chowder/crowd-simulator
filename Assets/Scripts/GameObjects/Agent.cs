using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Agent : MonoBehaviour
{
    // NavMesh variables
    private Bounds bounds;
    private NavMeshAgent agent;

    // Materials
    [SerializeField]
    private Material agentMaterial = null;
    [SerializeField]
    private Material outlineMaterial = null;

    // Pulse particles
    [SerializeField]
    private ParticleSystem _pulse = null;

    // Meta properties
    private Device device;

    public ParticleSystem Pulse { get => _pulse; }
    public Vector3 Destination { get; private set; }

    void Start()
    {
        Destination = GetRandomDestination();
        agent = GetComponent<NavMeshAgent>();
        bounds = GameObject.FindGameObjectWithTag("Ground").GetComponent<Renderer>().bounds;

        AssignDevice();
    }

    private void AssignDevice()
    {
        device = new Device(this);
        StartCoroutine(device.Pulse());
    }

    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Destination = GetRandomDestination();
            agent.SetDestination(Destination);
        }
    }

    private Vector3 GetRandomDestination()
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x, 0, z);
    }

    public void Select(bool selected)
    {
        GetComponent<MeshRenderer>().material = selected ? outlineMaterial : agentMaterial;
    }
}
