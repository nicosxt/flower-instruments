using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_Sequencer : MonoBehaviour
{
    public int barsPerLoop = 4;//total amount of bars for this sequencer
    public int beatsPerBar = 4;
    public float beatLength = 0.1f;
    public float barLength;
    List<EA_BarObject> bars = new List<EA_BarObject>();
    public EA_Instrument instrument;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInitialized(EA_Instrument _instrument){
        instrument = _instrument;
        barLength = beatLength * beatsPerBar;
        for(int i = 0; i < barsPerLoop; i++){
            GameObject bar = Instantiate(EA_SequenceManager.s.barPrefab, transform);
            bar.transform.parent = transform;
            bars.Add(bar.GetComponent<EA_BarObject>());
            bars[i].OnInitialized(this, beatsPerBar, beatLength);
            bar.transform.localPosition += new Vector3(0,0,barLength * i);

        }

        //after getting all the bar objects, offset their positions by half of total length of all the bars
        foreach(EA_BarObject bar in bars){
            bar.transform.localPosition -= new Vector3(0,0,barLength * barsPerLoop / 2);
        }

        //add self to total sequencers in EA_SequenceManager
        EA_SequenceManager.s.sequencers.Add(this);

    }
}

