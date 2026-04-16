using UnityEngine;
using System.Collections;

public class PistonPusher : MonoBehaviour
{
    public Transform piston;

    public float pushDistance = 0.5f;
    public float pushSpeed = 2f;
    public float delayBeforePush = 0.3f;
    public float retractDelay = 0.2f;
    public Vector3 pushDirection = Vector3.right;

    private Vector3 pistonStartPos;
    private bool pistonBusy = false;
    private Rigidbody pistonRb;

    void Start()
    {
        pistonStartPos = piston.position;           // world space now
        pistonRb = piston.GetComponent<Rigidbody>();

        // Safety check
        if (pistonRb == null)
        {
            Debug.LogError("PistonPusher: No Rigidbody found on piston!");
            return;
        }

        pistonRb.isKinematic = true;                // we control it, not physics
        pistonRb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    public void Trigger()
    {
        if (pistonBusy) return;
        StartCoroutine(PushPiston());
    }

    IEnumerator PushPiston()
    {
        pistonBusy = true;

        yield return new WaitForSeconds(delayBeforePush);

        Vector3 pushTarget = pistonStartPos + pushDirection.normalized * pushDistance;

        // Push forward
        while (Vector3.Distance(piston.position, pushTarget) > 0.001f)
        {
            Vector3 next = Vector3.MoveTowards(
                piston.position,
                pushTarget,
                pushSpeed * Time.fixedDeltaTime
            );
            pistonRb.MovePosition(next);

            yield return new WaitForFixedUpdate();  // sync with physics engine
        }

        yield return new WaitForSeconds(retractDelay);

        // Retract
        while (Vector3.Distance(piston.position, pistonStartPos) > 0.001f)
        {
            Vector3 next = Vector3.MoveTowards(
                piston.position,
                pistonStartPos,
                pushSpeed * Time.fixedDeltaTime
            );
            pistonRb.MovePosition(next);

            yield return new WaitForFixedUpdate();
        }

        pistonBusy = false;
    }
}