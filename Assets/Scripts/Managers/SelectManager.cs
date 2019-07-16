using UnityEngine;

public class SelectManager : MonoBehaviour
{
    private Agent selectedAgent = null;
    [SerializeField]
    private GameObject destinationMarker = null;
    [SerializeField]
    private LayerMask selectableLayer = 0;
    [SerializeField]
    private new Camera camera = null;

    void Update()
    {
        CheckForSelect();
        if (selectedAgent != null)
        {
            DrawDestination();
        }
    }

    void CheckForSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectableLayer))
            {
                selectedAgent?.Select(false);
                selectedAgent = hit.transform.gameObject.GetComponent<Agent>();
                selectedAgent.Select(true);
                destinationMarker.SetActive(true);
            }
            else
            {
                selectedAgent?.Select(false);
                selectedAgent = null;
                destinationMarker.SetActive(false);
            }
        }
    }

    void DrawDestination()
    {
        destinationMarker.transform.position = new Vector3(selectedAgent.Destination.x, 0.5f, selectedAgent.Destination.z);
    }
}
