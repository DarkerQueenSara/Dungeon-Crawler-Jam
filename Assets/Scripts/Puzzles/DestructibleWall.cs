using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : MonoBehaviour
{
    public int hits;
    void Start()
    {
        hits = 1;
    }

    public void RecieveDamage()
    {
        hits--;
        if (hits == 0)
            Destroy(this.gameObject);
    }
    
}
