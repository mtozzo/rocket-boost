using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]
    public float delayBeforeReload = 2f;

    [SerializeField] 
    public AudioClip crashAudioClip;    

    [SerializeField] 
    public AudioClip levelFinishAudioClip;    
    
    private AudioSource audioSource;

    private const string FriendlyTag = "Friendly";
    private const string FinishTag = "Finish";
    private const string FuelTag = "Fuel";
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }    
    
    private void OnCollisionEnter(Collision  other)
    {
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
        
        audioSource.PlayOneShot(crashAudioClip);
        
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadCurrentLevel), delayBeforeReload);
    }

    private void StartLevelFinishSequence()
    {
        Debug.Log("Collided with Finish object.");
        
        audioSource.PlayOneShot(levelFinishAudioClip);
        
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), delayBeforeReload);
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
}
