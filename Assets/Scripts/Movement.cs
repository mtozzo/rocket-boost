using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private InputAction turnAction;
    
    [SerializeField]
    private InputAction boosterAction;
    
    private Rigidbody rb;
    
    [SerializeField]
    public float thrust = 1000f;

    private void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        if (boosterAction.IsPressed())
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            rb.AddRelativeForce(Vector3.up * (thrust * Time.fixedDeltaTime));
        }
    }

    private void OnEnable()
    {
        turnAction.Enable();
        boosterAction.Enable();
    }
}
