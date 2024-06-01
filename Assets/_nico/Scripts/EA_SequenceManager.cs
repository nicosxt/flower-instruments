using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_SequenceManager : MonoBehaviour
{
    public static EA_SequenceManager s { get; private set; }
    private void Awake()
    {
        if (s == null)
        {
            s = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (s != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    public GameObject beatVisualPrefab;
    public GameObject barPrefab;
    public List<EA_Sequencer> sequencers = new List<EA_Sequencer>();
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
