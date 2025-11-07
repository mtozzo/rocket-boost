using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private InputAction turnAction;
    
    [SerializeField]
    private InputAction boosterAction;

    [SerializeField]
    private InputAction resetAction;
    
    private Rigidbody rb;
    
    [SerializeField]
    public float thrust = 1000f;
    
    [SerializeField]
    public float rotationSpeed = 10f;

    private void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        if (resetAction.IsPressed())
        {
            //Reset the position and rotation of the Rigidbody
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            return;
        }
        
        if (boosterAction.IsPressed())
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            rb.AddRelativeForce(Vector3.up * (thrust * Time.fixedDeltaTime));
        }
        
        if (turnAction.IsPressed())
        {
            float turnValue = turnAction.ReadValue<float>();
            //Apply a torque to this Rigidbody around this GameObjects forward axis
            rb.AddRelativeTorque(Vector3.forward * (turnValue * rotationSpeed * Time.fixedDeltaTime));
        }
    }

    private void OnEnable()
    {
        turnAction.Enable();
        boosterAction.Enable();
        resetAction.Enable();
    }
}
