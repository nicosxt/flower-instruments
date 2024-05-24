using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit; 
using Oculus.Interaction;
using Oculus.Interaction.Input;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventVector3 : UnityEvent<Vector3> {}
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
    public OVRHand hand;
    [SerializeField]
    private OVRSkeleton handSkeleton;

    //RAYCASTING
    public Transform LinePointer;
    private OVRSkeleton.BoneId indexTipBoneId = OVRSkeleton.BoneId.Hand_IndexTip;
    private OVRSkeleton.BoneId thumbRootBoneId = OVRSkeleton.BoneId.Hand_Thumb0;
    private OVRSkeleton.BoneId thumbTipBoneId = OVRSkeleton.BoneId.Hand_ThumbTip;
    public GameObject raycastingPlane;

    //HAND GESTURE DETECTION
    public UnityEventVector3 OnPinchStart = new UnityEventVector3();
    private bool pinchStart = false;
    private Vector3 raycastTargetPos;
    private float lerpAmount = 0.1f;

    public GameObject handHit;

    void Start()
    {
    }

    void Update()
    {
        PerformRaycast(handSkeleton, LinePointer);
        handHit.transform.position = Vector3.Lerp(handHit.transform.position, raycastTargetPos, lerpAmount);
    }

    void HandleHandGesture_Pinch(Vector3 _rayPosition){
        if(hand.GetFingerIsPinching(OVRHand.HandFinger.Index)){
            if(!pinchStart){
                OnPinchStart.Invoke(_rayPosition);
                pinchStart = true;
            }
        }
        else{
            pinchStart = false;
        }
    }

    void PerformRaycast(OVRSkeleton skeleton, Transform linePointer)
    {
        Transform indexTip = GetBoneTransform(skeleton, indexTipBoneId);
        Transform thumbTip = GetBoneTransform(skeleton, thumbTipBoneId);
        Transform thumbRoot = GetBoneTransform(skeleton, thumbRootBoneId);
        if (indexTip != null && thumbTip != null && thumbRoot != null)
        {
            Vector3 medianTipPos = (indexTip.position + thumbTip.position) / 2;
            Vector3 pointingDirection = (medianTipPos - thumbRoot.position).normalized;

            AlignLinePointer(linePointer, thumbRoot.position, pointingDirection);

            Ray ray = new Ray(thumbRoot.position, pointingDirection);
            RaycastHit[] hits = Physics.RaycastAll(ray, 5000f);

            RaycastHit? closestHit = null;
            float closestDistance = float.MaxValue;

            raycastingPlane = null;
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.name.Contains("EffectMesh"))
                {
                    float distance = (hit.point - thumbRoot.position).sqrMagnitude;
                    if (distance < closestDistance)
                    {
                        closestHit = hit;
                        closestDistance = distance;
                        raycastingPlane = hit.collider.gameObject;
                    }
                }
            }

            if (closestHit.HasValue)
            {
                raycastTargetPos = closestHit.Value.point;
                HandleHandGesture_Pinch(closestHit.Value.point);
            }
        }
    }

    void AlignLinePointer(Transform linePointer, Vector3 pos, Vector3 dir){
        linePointer.position = pos;
        linePointer.rotation = Quaternion.LookRotation(dir);
    }

    Transform GetBoneTransform(OVRSkeleton skeleton, OVRSkeleton.BoneId boneId)
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