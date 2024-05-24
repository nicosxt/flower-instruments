using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;

public class FO_ControllerManager : MonoBehaviour
{
    public GameObject instrumentSelectorUI;
    public OVRHand leftHand, rightHand;
    public OVRSkeleton leftSkeleton, rightSkeleton;
    //get boneID from wrist
    private OVRSkeleton.BoneId wristBoneId = OVRSkeleton.BoneId.Hand_WristRoot;
    private Transform leftWristTransform, rightWristTransform;
    private bool spawnedLeftMenu, spawnedRightMenu;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        leftWristTransform = GetBoneTransform(leftSkeleton, wristBoneId);
        rightWristTransform = GetBoneTransform(rightSkeleton, wristBoneId);
        if(leftWristTransform != null && !spawnedLeftMenu){
            SpawnInstrumentSelectorUI(leftSkeleton, leftWristTransform);
            spawnedLeftMenu = true;
        }
        
    }

    //spawn instrumentSelectorUI object to be attached to the leftHand
    public void SpawnInstrumentSelectorUI(OVRSkeleton _skeleton, Transform _boneTransform){
        int instrumentAmount = FO_InstrumentManager.s.instrumentPrefabs.Length;
        float angleStep = 360.0f / instrumentAmount;
        float radius = 0.1f; // Set the radius of the circle

        for (int i = 0; i < instrumentAmount; i++)
        {   
            Debug.Log("Spawned instrument selector " + i);
            float angle = i * angleStep * Mathf.Deg2Rad; // Convert angle to radians
            Vector3 spawnPos = new Vector3(Mathf.Cos(angle) * radius, -0.1f, Mathf.Sin(angle) * radius);
            GameObject newInstrumentSelector = Instantiate(FO_InstrumentManager.s.instrumentPrefabs[i].instrumentSelectorUI, _boneTransform.position, Quaternion.identity, _boneTransform);
            newInstrumentSelector.SetActive(true);
            newInstrumentSelector.transform.localPosition = spawnPos;
        }
    }

    public Transform GetBoneTransform(OVRSkeleton skeleton, OVRSkeleton.BoneId boneId)
    {
        if(skeleton == null) return null;
        foreach (var bone in skeleton.Bones)
        {
            if (bone.Id == boneId)
            {
                return bone.Transform;
            }
        }
        return null;
    }
}
