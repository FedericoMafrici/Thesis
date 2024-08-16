using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    public GameObject interactableCube;
    [SerializeField]
    private OVRSkeleton  leftHandSkeleton;
    [SerializeField]
    private OVRHand leftHand;
    private Vector3 targetPosition;
    [SerializeField] private HandInteractionLaser laserManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake()
    {
        Debug.developerConsoleEnabled = true;
        Debug.developerConsoleVisible = true;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SpawnInteractable()
    {
        Debug.LogError("ho spawnato il cubo");
        
        GameObject spawnedObject=Instantiate(interactableCube, leftHand.transform);
        spawnedObject.transform.SetParent(null);
        spawnedObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }
    public void EnableLaser()
    {
        laserManager.isActive = !laserManager.isActive;
    }
    
}
