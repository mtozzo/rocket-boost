using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private InputAction turnAction;
    
    [SerializeField]
    private InputAction boosterAction;
    
    // private Rigidbody rb;
    // private Vector2 turnInput;
    // public float turnSpeed = 5f;
    
    // [SerializeField]
    // public float m_Thrust = 10f;

    private void Start()
    {
        // turnAction = InputSystem.actions.FindAction("Move");
        // boosterAction = InputSystem.actions.FindAction("Jump");
        
        // rb = GetComponent<Rigidbody>();
        // rb.interpolation = RigidbodyInterpolation.Interpolate;
        // rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        // rb.freezeRotation = true;
    }

    private void Update()
    {
        // turnInput = turnAction.ReadValue<Vector2>();
        if (boosterAction.IsPressed())
        {
            Debug.Log("booster action is pressed");
        }
    }

    private void FixedUpdate()
    {
        // if (turnInput.x != 0f)
        // {
        //     Quaternion turn = Quaternion.Euler(0f, 0f, -1 * turnInput.x * turnSpeed * Time.fixedDeltaTime);
        //     rb.MoveRotation(rb.rotation * turn);
        // }
        //
        // if (boosterAction.IsPressed())
        // {
        //     rb.AddForce(transform.up * m_Thrust);
        // }
    }

    private void OnEnable()
    {
        turnAction.Enable();
        boosterAction.Enable();
    }
}
