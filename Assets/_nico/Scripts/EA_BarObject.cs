using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_BarObject : MonoBehaviour
{   
    //TEMP DEBUGGING OBJ
    public EA_Sequencer sequencer;
    public GameObject barStart, barEnd;
    public List<GameObject> beats = new List<GameObject>();
    public Vector3 nextBarPosition;
    // public float barLength;
    // public float beatLength;
    // public int beatsPerBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInitialized(EA_Sequencer _sequencer, int _beatsPerBar, float _beatLength){
        sequencer = _sequencer;
        //generate beatsPerBar amount of beats by spawning the beatVisualPrefab from SequenceManager and at the distance of (0,0,beatLength * index)
        for(int i = 0; i < _beatsPerBar; i++){
            GameObject beat = Instantiate(EA_SequenceManager.s.beatVisualPrefab, transform);
            beat.transform.localPosition = new Vector3(0,0,_beatLength * i);
            beats.Add(beat);
        }
        // nextBarPosition = transform.position + new Vector3(0,0,barLength);
        // barEnd.transform.position = nextBarPosition;
        // beatsPerBar = _beatsPerBar;
        // beatLength = _beatLength;
        // barLength = _barLength;
    }
}
