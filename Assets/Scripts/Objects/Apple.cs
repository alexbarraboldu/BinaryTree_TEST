using Opsive.UltimateCharacterController.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private bool collectable=false;
    private bool from_hansel = false;
    private Transform throwDestination;

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

    public void throwHealApple(Transform destination)
    {
        from_hansel = true;
        throwDestination = destination;
    }
     

    private void OnDestroy()
    {
        //curem a la Gretel
        if (from_hansel)
        {
            //CRIDA A LA FUNCIO DE CURAR LA GRETEL
            Debug.Log("IMPLEMENTA CURACIO GRETEL");
        }
    }


    private void Update()
    {
        if(throwDestination != null && from_hansel)
        {
            //destrueix abans que xoqui contra la Gretel i la cures
            if(Vector3.Distance(this.transform.position, throwDestination.position) < 1f)
            {
                Destroy(gameObject);
            }
        }
    }

}
