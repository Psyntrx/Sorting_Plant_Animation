using UnityEngine;
using UnityEngine.Events;

public class WorkpieceSensor : MonoBehaviour
{
    public string targetTag = "Cube";

    [Header("Event")]
    public UnityEvent onTargetDetected;  // Drag PistonPusher.Trigger() here in Inspector

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object detected: " + other.gameObject.name);

        if (other.CompareTag(targetTag))
        {
            Debug.Log("Correct tag detected: " + other.tag);
            onTargetDetected?.Invoke();
        }
    }
}
