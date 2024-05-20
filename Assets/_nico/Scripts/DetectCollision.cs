using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    public string targetStringContains = "Hand"; // The tag of the HAND object

    private void Start()
    {
        // Find all colliders in the children
        Collider[] childColliders = GetComponentsInChildren<Collider>();

        // Add the event handlers to each child collider
        foreach (Collider collider in childColliders)
        {
            // Add collision detection handlers
            ColliderEventHandler handler = collider.gameObject.AddComponent<ColliderEventHandler>();
            handler.Initialize(targetStringContains, OnHandCollision);
        }
    }

    // This method will be called when any child collider detects a collision with the HAND
    private void OnHandCollision(GameObject colliderObject)
    {
        if(GetComponent<Instrument>()){
            GetComponent<Instrument>().TriggerSound();
        }
    }
}

// Helper class to handle collider events and call the parent method
public class ColliderEventHandler : MonoBehaviour
{
    private string targetStringContains;
    private System.Action<GameObject> onHandCollision;

    public void Initialize(string targetStringContains, System.Action<GameObject> onHandCollision)
    {
        this.targetStringContains = targetStringContains;
        this.onHandCollision = onHandCollision;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("COlliding with " + collision.gameObject.name);

        if (collision.gameObject.name.Contains(targetStringContains))
        {
            onHandCollision?.Invoke(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains(targetStringContains))
        {
            //Debug.Log("Triggering with " + other.gameObject.name);
            onHandCollision?.Invoke(gameObject);
        }
    }
}
