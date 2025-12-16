using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] public float thrust = 1000f;
    [SerializeField] public float rotationSpeed = 10f;
    [SerializeField] public AudioClip boosterAudioClip;
    [SerializeField] public ParticleSystem mainBoosterParticles;
    [SerializeField] public ParticleSystem leftBoosterParticles;
    [SerializeField] public ParticleSystem rightBoosterParticles;

    [SerializeField] private InputAction turnAction;
    [SerializeField] private InputAction boosterAction;
    [SerializeField] private InputAction resetAction;
    [SerializeField] private InputAction levelSkipAction;
    
    private Rigidbody rb;
    private AudioSource audioSource;
    
    private void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        rb = GetComponent<Rigidbody>();
        
        audioSource = GetComponent<AudioSource>();
        
        Debug.Log("gravity is " + Physics.gravity);
    }
    
    private void FixedUpdate()
    {
        if (resetAction.WasPressedThisFrame())
        {
            //Reset the position and rotation of the Rigidbody
            Debug.Log("Processing reset");
            ProcessReset();
            return;
        }

        if (levelSkipAction.WasPressedThisFrame())
        {
            Debug.Log("Processing level skip");
            ProcessLevelSkip();
            return;
        }
        
        if (boosterAction.IsPressed())
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            ProcessBoost();
        }
        else
        {
            ProcessBoostStop();
        }

        if (!turnAction.IsPressed())
        {
            ProcessNotTurning();
            return;
        }
            
        ProcessTurning();
    }

    private void ProcessTurning()
    {
        var turnValue = turnAction.ReadValue<float>();

        switch (turnValue)
        {
            case > 0:
                //Turning Right
                transform.Rotate(0, 0, -rotationSpeed * Time.fixedDeltaTime);
                leftBoosterParticles.Play();
                break;
            case < 0:
                //Turning Left
                transform.Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);
                rightBoosterParticles.Play();
                break;
            default:
                if (leftBoosterParticles.isPlaying)
                {
                    leftBoosterParticles.Stop();
                }
            
                if (rightBoosterParticles.isPlaying)
                {
                    rightBoosterParticles.Stop();
                }
                break;
        }
    }

    private void ProcessNotTurning()
    {
        if (leftBoosterParticles.isPlaying)
        {
            leftBoosterParticles.Stop();
        }

        if (rightBoosterParticles.isPlaying)
        {
            rightBoosterParticles.Stop();
        }
    }

    private void ProcessReset()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
    
    private void ProcessLevelSkip()
    {
        Invoke(nameof(LoadNextLevel), 0f);
    }

    private void ProcessBoostStop()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        if (mainBoosterParticles.isPlaying)
        {
            mainBoosterParticles.Stop();
        }
    }

    private void ProcessBoost()
    {
        rb.AddRelativeForce(Vector3.up * (thrust * Time.fixedDeltaTime));
        mainBoosterParticles.Play();
        
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(boosterAudioClip);
        }
    }

    private void OnEnable()
    {
        turnAction.Enable();
        boosterAction.Enable();
        resetAction.Enable();
        levelSkipAction.Enable();
    }
    
    private void OnDisable()
    {
        turnAction.Disable();
        boosterAction.Disable();
        resetAction.Disable();
        levelSkipAction.Disable();        
    }    
    
    private void LoadNextLevel()
    {
        var currentScene = SceneManager.GetActiveScene();
        var nextSceneIndex = currentScene.buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }    
}
