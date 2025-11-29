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
    
    private AudioSource audioSource;

    [SerializeField]
    public float thrust = 1000f;
    
    [SerializeField]
    public float rotationSpeed = 10f;

    private void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        rb = GetComponent<Rigidbody>();
        
        audioSource = GetComponent<AudioSource>();
        
        Debug.Log("gravity is " + Physics.gravity);
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
            ProcessBoost();

            if (!audioSource.isPlaying)
            {
                audioSource.Play();   
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();   
            }
        }

        if (!turnAction.IsPressed()) return;
        var turnValue = turnAction.ReadValue<float>();
            
        switch (turnValue)
        {
            case > 0:
                //Turning Right
                transform.Rotate(0, 0, -rotationSpeed * Time.fixedDeltaTime);
                break;
            case < 0:
                //Turning Left
                transform.Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);
                break;
        }
    }

    private void ProcessBoost()
    {
        rb.AddRelativeForce(Vector3.up * (thrust * Time.fixedDeltaTime));
    }

    private void OnEnable()
    {
        turnAction.Enable();
        boosterAction.Enable();
        resetAction.Enable();
    }
}
