using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private const string FriendlyTag = "Friendly";
    private const string FinishTag = "Finish";
    private const string FuelTag = "Fuel";
    
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
                Debug.Log("Collided with Finish object.");
                LoadNextLevel();
                break;
            case FuelTag:
                Debug.Log("Collided with Fuel object.");
                break;
            default:
                Debug.Log("You crashed!");
                ReloadCurrentLevel();
                break;
        }
    }
    
    private static void ReloadCurrentLevel()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
    
    private static void LoadNextLevel()
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
