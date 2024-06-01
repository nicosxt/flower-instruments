using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EA_Instrument : MonoBehaviour
{
    //attached to each instrument object

    // public string instrumentName;
    // public AudioClip[] instrumentSounds;
    // public AudioSource[] instrumentAudioSources;
    // public bool isActive;
    // public bool isBeingPlayed;

    //PLAY RULES
    //play rules TBD
    public EA_Sequencer sequencer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        OnInitialized();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public void OnInitialized(){
        sequencer.OnInitialized(this);
    }
}
