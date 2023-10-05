using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public GameObject droppedBombExplosion;
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

    public void BombExplosion()
    {
        Instantiate(droppedBombExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
}