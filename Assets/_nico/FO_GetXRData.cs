using Meta.XR.MRUtilityKit;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;
using System.Collections.Generic;
public class FO_GetXRData : MonoBehaviour
{
    public static FO_GetXRData s { get; private set; }
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

    public Transform mrRoomPrefab;
    public List<Transform> mrAnchorTransforms = new List<Transform>();
    [SerializeField]
    public EffectMesh effectMesh;
    public List<Collider> colliders = new List<Collider>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetMRUKRoom(){
        var room = MRUK.Instance.GetCurrentRoom();
        //get data type of GetCurrentRoom()
        Debug.Log("room.GetType() = " + room.GetType());
        if (room != null)
        {
            foreach (MRUKAnchor anchorPrefab in room.Anchors)
            {
                Debug.Log("anchor.name = " + anchorPrefab.name);
                Debug.Log("type = " + anchorPrefab.GetType());
                if(!mrRoomPrefab && anchorPrefab.transform.parent != null){
                    mrRoomPrefab = anchorPrefab.transform.parent;
                }
                if(anchorPrefab.transform != null){
                    mrAnchorTransforms.Add(anchorPrefab.transform);
                }
                if(anchorPrefab.GetComponentInChildren<Collider>() != null){
                    colliders.Add(anchorPrefab.GetComponentInChildren<Collider>());
                }
            }
        }

        //OPTIONAL CALLS
        //FO_SetRaycastRoom_MR.s.SetUpRaycasting();

        
    }

    // public void GetCollidersFromEffectMesh(){
    //     colliders = effectMesh.GetComponentsInChildren<Collider>().ToList();
    // }
}
