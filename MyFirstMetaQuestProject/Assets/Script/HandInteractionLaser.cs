using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteractionLaser : MonoBehaviour
{
    public OVRHand rightHand;
    public GameObject CurrentTarget
    {
        get;
        private set;
    }

    [SerializeField] private bool showRaycast = false;
    [SerializeField] private Color[] highlightColor= new Color[5] { Color.red, Color.green, Color.yellow, Color.blue, Color.cyan }; // Example includes 5 colors
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LineRenderer lineRender;
    [SerializeField] public bool isActive=false;

    private Color _originalColor;
    private Renderer _currentRenderer;
    private int _currColorIndex = 0;
    void CheckHandPointer(OVRHand hand)
    {
        if(!isActive)
        {
            lineRender.enabled = false;
            
            
            
            return; 
            
        }
        Vector3 rayOrigin = hand.transform.position;
        Vector3 rayTarget = hand.transform.position - hand.transform.right * 10.0f;
        Vector3 rayDirection = (rayTarget - rayOrigin).normalized;
            
        if (    hand.IsTracked && Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, Mathf.Infinity, targetLayer))
        {
            if (CurrentTarget != hit.transform.gameObject)
            {
                CurrentTarget = hit.transform.gameObject;
                _currentRenderer = CurrentTarget.GetComponent<Renderer>();
                _originalColor = _currentRenderer.material.color;
                if (_currColorIndex == 5)
                    _currColorIndex = 0;
                _currentRenderer.material.color = highlightColor[_currColorIndex];
                _currColorIndex= _currColorIndex + 1;
            }

            UpdateRayVisualization(rayOrigin, hit.point, true);
        }
        else
        {
            if (CurrentTarget != null)
            {
              //  _currentRenderer.material.color = _originalColor;
                CurrentTarget = null;
            }

            UpdateRayVisualization(rayOrigin, rayOrigin + rayDirection * 1000, false);
        }
    }

    private void UpdateRayVisualization(Vector3 startPosition, Vector3 endPosition, bool hitSomething)
    {
        if (showRaycast && lineRender != null)
        {
            lineRender.enabled = true;
            lineRender.SetPosition(0, startPosition);
            lineRender.SetPosition(1, endPosition);
            lineRender.material.color = hitSomething ? highlightColor[_currColorIndex-1] : Color.red;
        }
        else if (lineRender != null)
        {
            lineRender.enabled = false;
        }
    }

    void Update()
    {
        CheckHandPointer(rightHand);
    }
}
