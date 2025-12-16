using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] public float delayBeforeReload = 2f;
    [SerializeField] public AudioClip crashAudioClip;    
    [SerializeField] public AudioClip levelFinishAudioClip;
    [SerializeField] public ParticleSystem crashParticles;
    [SerializeField] public ParticleSystem levelFinishParticles;
    [SerializeField] public ParticleSystem mainBoosterParticles;
    [SerializeField] public ParticleSystem leftBoosterParticles;
    [SerializeField] public ParticleSystem rightBoosterParticles;
    [SerializeField] private InputAction disableCollisionAction;
    
    private AudioSource audioSource;
    
    private Rigidbody rb;
    
    private bool isControllable = true;
    private bool isCollidable = true;

    private const string FriendlyTag = "Friendly";
    private const string FinishTag = "Finish";
    private const string FuelTag = "Fuel";
    
    private void Start()
    {
        isControllable = true;
        isCollidable = true;
        
        //Fetch the Rigidbody from the GameObject with this script attached
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        if (disableCollisionAction.WasPressedThisFrame())
        {
            isCollidable  = !isCollidable ;
            Debug.Log("Toggled collision handling to " + isCollidable);
        } 
    }
    
    private void OnCollisionEnter(Collision  other)
    {
        if (false == isControllable)
        {
            Debug.Log("No collision handling, player is not controllable.");
            return;
        }
        
        if (false == isCollidable)
        {
            Debug.Log("No collision handling, collisions are disabled.");
            return;
        }

        // Debug.Log("hit something");
        // Debug.Log(gameObject.name + " collided with " + other.gameObject.name + " this object has tag " +
        //           other.gameObject.tag + " and " + (other.gameObject.CompareTag("Player") ? "is" : "is not") +
        //           " the Player");

        switch (other.gameObject.tag)
        {
            case FriendlyTag:
                Debug.Log("Collided with Friendly object.");
                break;
            case FinishTag:
                StartLevelFinishSequence();
                break;
            case FuelTag:
                Debug.Log("Collided with Fuel object.");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        Debug.Log("You crashed!");
        
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudioClip);
        mainBoosterParticles.Stop();
        leftBoosterParticles.Stop();
        rightBoosterParticles.Stop();
        crashParticles.Play();
        FreezePlayerMovement();
        Invoke(nameof(ReloadCurrentLevel), delayBeforeReload);
    }

    private void StartLevelFinishSequence()
    {
        Debug.Log("Collided with Finish object.");
        
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(levelFinishAudioClip);
        levelFinishParticles.Play();
        mainBoosterParticles.Stop();
        leftBoosterParticles.Stop();
        rightBoosterParticles.Stop();
        FreezePlayerMovement();
        Invoke(nameof(LoadNextLevel), delayBeforeReload);
    }

    private void FreezePlayerMovement()
    {
        rb.isKinematic = true;
        
        var movementComponent = GetComponent<Movement>();
        movementComponent.enabled = false;
    }
    
    private void ReloadCurrentLevel()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
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
    
    private void OnEnable()
    {
        disableCollisionAction.Enable();
    }
    
    private void OnDisable()
    {
        disableCollisionAction.Disable();
    }
}
