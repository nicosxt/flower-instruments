using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedObject : MonoBehaviour
{
    public Renderer meshRenderer;
    public int materialIndex; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeSpawnedObject(Color _color, int _materialIndex)
    {
        meshRenderer.materials[materialIndex].color = _color;
    }
}
