using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit; 
using Oculus.Interaction;
using Oculus.Interaction.Input;
public class FO_Raycast : MonoBehaviour
{
    public static FO_Raycast s { get; private set; }
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

    [SerializeField]
    public OVRHand leftHand;
    [SerializeField]
    public OVRHand rightHand;
    [SerializeField]
    private OVRSkeleton leftHandSkeleton;
    [SerializeField]
    private OVRSkeleton rightHandSkeleton;
    public Transform LeftLinePointer;
    public Transform RightLinePointer;
    private OVRSkeleton.BoneId indexTipBoneId = OVRSkeleton.BoneId.Hand_IndexTip;
    private OVRSkeleton.BoneId thumbRootBoneId = OVRSkeleton.BoneId.Hand_Thumb0;
    private OVRSkeleton.BoneId thumbTipBoneId = OVRSkeleton.BoneId.Hand_ThumbTip;


    public GameObject leftHandHit, rightHandHit;
    //private RoomMeshCollector roomMeshCollector;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PerformRaycast(leftHandSkeleton, leftHandHit, LeftLinePointer);
        PerformRaycast(rightHandSkeleton, rightHandHit, RightLinePointer);
    }

    void PerformRaycast(OVRSkeleton handSkeleton, GameObject hitObj, Transform linePointer)
    {
        Transform indexTip = GetBoneTransform(handSkeleton, indexTipBoneId);
        Transform thumbTip = GetBoneTransform(handSkeleton, thumbTipBoneId);
        Transform thumbRoot = GetBoneTransform(handSkeleton, thumbRootBoneId);
        if (indexTip != null && thumbTip != null && thumbRoot != null)
        {
            Vector3 medianTipPos = (indexTip.position + thumbTip.position) / 2;
            Vector3 pointingDirection = (medianTipPos - thumbRoot.position).normalized;

            //Debug.Log("Bone is working!");
            AlignLinePointer(linePointer, thumbRoot.position, pointingDirection);

            Ray ray = new Ray(thumbRoot.position, pointingDirection);
            RaycastHit[] hits = Physics.RaycastAll(ray, 5000f);

            foreach(RaycastHit hit in hits){
                if (hit.collider.name.Contains("EffectMesh"))
                {
                    hitObj.transform.position = hit.point;
                }
            }
        }
    }


    void AlignLinePointer(Transform linePointer, Vector3 pos, Vector3 dir){
        
        linePointer.position = pos;
        linePointer.rotation = Quaternion.LookRotation(dir);
    }


    Transform GetBoneTransform(OVRSkeleton skeleton, OVRSkeleton.BoneId boneId)
    {
        foreach (var bone in skeleton.Bones)
        {
            if (bone.Id == boneId)
            {
                return bone.Transform;
            }
        }
        return null;
    }

    // public void SetUpRaycasting(){
    //     //for all the transforms in FO_GetXRData.s.mrAnchorTransforms, add the RayInteractable script to the object
    //     foreach(Transform anchor in FO_GetXRData.s.mrAnchorTransforms){
    //         anchor.gameObject.AddComponent<RayInteractable>();
    //         //assign collider from child to Surface in RayInteractable
    //         Collider collider = anchor.GetComponentInChildren<Collider>();
    //         if(collider != null){
    //             Debug.Log("Getting Colliders");
    //             colliders.Add(collider);

    //             ColliderSurface cs = collider.gameObject.AddComponent<ColliderSurface>();
    //             cs._collider = collider;
    //             // if(anchor.GetComponent<RayInteractable>() != null){
    //             //     anchor.GetComponent<RayInteractable>().Surface = collider;
    //             // }
    //         }

            
    //     }
    // }
}
