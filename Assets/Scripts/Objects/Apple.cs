using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public bool collectable=false;


    // Quan li tirin un projectil caura
    private void OnCollisionEnter(Collision collision)
    {
        collectable = true;   
    }
    

    public bool isCollectable() {  
        return collectable; 
    }

    public void picked()
    {
        Debug.Log("apple picked up!");
        Destroy(gameObject);
    }
}
