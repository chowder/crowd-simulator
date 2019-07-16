using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    private Transform map;
    [SerializeField]
    private GameObject agentPrefab = null;

    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Ground").transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SpawnAgent();
        }
    }

    private Vector3 RandomPoint()
    {
        Bounds bounds = map.GetComponent<Renderer>().bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x, 1.083333f, z);
    }

    private Quaternion RandomAngle()
    {
        return new Quaternion(0, Random.Range(-360, 360), 0, 0);
    }

    private void SpawnAgent()
    {
        Instantiate(agentPrefab, RandomPoint(), RandomAngle());
        ConsoleManager.Log("Spawning an agent");
    }
}
