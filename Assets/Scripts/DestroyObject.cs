using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    // Start is called before the first frame update
    public void DestroyThisObject()
    {
        Destroy(gameObject);
    }

    public void DestroyParentObject()
    {
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}