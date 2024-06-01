using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_NoteObject : MonoBehaviour
{
    public AudioSource noteAudio;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void PlayNote(){
        noteAudio.Play();
    }
}
