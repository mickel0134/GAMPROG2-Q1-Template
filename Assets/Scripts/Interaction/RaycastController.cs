using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastController : MonoBehaviour
{
    [SerializeField]
    private float raycastDistance = 5.0f;

    [SerializeField]
    //The layer that will determine what the raycast will hit
    LayerMask layerMask;
    //The UI text component that will display the name of the interactable hit
    [SerializeField]private TextMeshProUGUI interactionInfo;

    
    // Update is called once per frame
    private void Update()
    {
        

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, raycastDistance, layerMask))
        {
            Debug.Log($"{hit.collider.gameObject.name} is within reach");
            if (hit.collider.gameObject.GetComponent<Interactable>() != null)
            {
                if (Input.GetMouseButton(0))
                {
                    hit.collider.gameObject.GetComponent<Interactable>().Interact();
                }

                interactionInfo.text = hit.collider.gameObject.GetComponent<Interactable>().id;
            }
            else interactionInfo.text = "";
        }
        else interactionInfo.text = "";
        Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.red);
        
    }

    public bool IsGrounded()
    {
        
        Debug.Log(Physics.Raycast(transform.position, -Vector3.up, raycastDistance, layerMask));
        return Physics.Raycast(transform.position, -Vector3.up, raycastDistance, layerMask);
    }
}