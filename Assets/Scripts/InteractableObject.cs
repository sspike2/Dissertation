using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public GameObject InteractIcon;

    public virtual void EnableInteractIcon()
    {
        InteractIcon.SetActive(true);
    }
    public virtual void DisableInteractIcon()
    {

        InteractIcon.SetActive(false);
    }

    public virtual void Interact()
    {

    }
}
