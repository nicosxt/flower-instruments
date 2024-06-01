using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EA_NoteObject_Drum : EA_NoteObject
{
    
    // Start is called before the first frame update
    void Start()
    {
        noteAudio = GetComponent<AudioSource>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void PlayNote(){
        noteAudio.Play();
    }

}
