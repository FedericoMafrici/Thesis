using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class MrukBoundingBoxGenerator : MonoBehaviour
{
    protected MRUK SceneManager { get; private set; }

    // Start is called before the first frame update
    
    private void Awake()
    {
        SceneManager = GetComponent<MRUK>();
    }
    public void OnSceneModelLoadedSuccessfully()
    {
        StartCoroutine(AddCollidersAndFixClassifications());
    }
    
    private IEnumerator AddCollidersAndFixClassifications()
    {
        // [Note] jackyangzzh: to avoid racing condition, wait for end of frame
        //                     for all prefabs to be populated properly before continuing
        yield return new WaitForEndOfFrame();

        MeshRenderer[] allObjects = FindObjectsOfType<MeshRenderer>();

        foreach (var obj in allObjects)
        {
            if (obj.GetComponent<Collider>() == null)
            {
                obj.AddComponent<BoxCollider>();
            }
        }

        // fix to desks - for some reason they are upside down with Meta's default code
        OVRSemanticClassification[] allClassifications = FindObjectsOfType<OVRSemanticClassification>()
            .Where(c =>
            {
                return c.Contains(MRUKAnchor.SceneLabels.BED.ToString()) ;
            })
            .ToArray();

        foreach(var classification in allClassifications)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * -1);
            
        }
    }
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
