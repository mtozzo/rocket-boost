using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private const string FRIENDLY_TAG = "Friendly";
    private const string FINISH_TAG = "Finish";
    private const string FUEL_TAG = "Fuel";
    
    private void OnCollisionEnter(Collision  other)
    {
        // Debug.Log("hit something");
        // Debug.Log(gameObject.name + " collided with " + other.gameObject.name + " this object has tag " +
        //           other.gameObject.tag + " and " + (other.gameObject.CompareTag("Player") ? "is" : "is not") +
        //           " the Player");

        switch (other.gameObject.tag)
        {
            case FRIENDLY_TAG:
                Debug.Log("Collided with Friendly object.");
                break;
            case FINISH_TAG:
                Debug.Log("Collided with Finish object.");
                break;
            case FUEL_TAG:
                Debug.Log("Collided with Fuel object.");
                break;
            default:
                Debug.Log("You crashed!");
                break;
        }
    }
}
