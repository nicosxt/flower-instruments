using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class H_DetectCollision : MonoBehaviour
{
    public string targetStringContains = "Hand"; // name of target collision must contain this string
    public UnityEvent collisionEnterEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject.name + "COlliding with " + collision.gameObject.name);
        if(collision.gameObject.name.Contains(targetStringContains)){
            collisionEnterEvent?.Invoke();
        }
    }
}
