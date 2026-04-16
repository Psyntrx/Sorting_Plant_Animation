using UnityEngine;

public class WorkpieceMover : MonoBehaviour
{

    //We want the workpiece to move the forward in the global axis
    //we want to move using the rigid body for better physics interactions
    //we want to be able to adjust the speed of the movement
    //I want to use collision stay and exit on the Conveyor with a tag 'Conveyor'
    private Rigidbody workPiece;
    [SerializeField] float speed = 0.1f;
    private bool isOnBelt = false;
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Conveyor"))
        {
            isOnBelt = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Conveyor"))
        {
            isOnBelt = false;
        }
    }
    void Start()
    {
        workPiece = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (isOnBelt)
        {
            //movement script will come here
            MoveWorkpiece();
        }

    }
   
    private void MoveWorkpiece()
    {
        Vector3 movement = speed * Time.fixedDeltaTime * -Vector3.forward;
        workPiece.MovePosition(workPiece.position + movement);
    }
}
