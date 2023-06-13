using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : Interactable
{
    public override void Interact()
    {
        float start = 0f;
        start += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y + start, transform.position.z);
    }
}
