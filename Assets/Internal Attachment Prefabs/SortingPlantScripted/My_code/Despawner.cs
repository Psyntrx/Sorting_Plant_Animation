using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Despawner : MonoBehaviour
{
    public float timeBeforeDestroy = 2f;    // How long object must touch before destroying

    // Track each object and its coroutine
    private Dictionary<GameObject, Coroutine> trackedObjects = new Dictionary<GameObject, Coroutine>();

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() == null) return;

        // Start countdown for this object
        if (!trackedObjects.ContainsKey(other.gameObject))
        {
            Coroutine coroutine = StartCoroutine(DestroyAfterDelay(other.gameObject));
            trackedObjects.Add(other.gameObject, coroutine);
            Debug.Log($"[Despawner] Started countdown for: {other.gameObject.name}");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>() == null) return;

        // Object left before time was up — cancel its countdown
        if (trackedObjects.TryGetValue(other.gameObject, out Coroutine coroutine))
        {
            StopCoroutine(coroutine);
            trackedObjects.Remove(other.gameObject);
            Debug.Log($"[Despawner] Countdown cancelled for: {other.gameObject.name}");
        }
    }

    IEnumerator DestroyAfterDelay(GameObject obj)
    {
        yield return new WaitForSeconds(timeBeforeDestroy);

        if (obj != null)
        {
            Debug.Log($"[Despawner] Destroying: {obj.name}");
            trackedObjects.Remove(obj);
            Destroy(obj);
        }
    }
}