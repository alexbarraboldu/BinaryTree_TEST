using Opsive.UltimateCharacterController.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private bool collectable=false;
    private bool from_hansel = false;


    //ballistica
    public float speed = 8.5f; // Speed of projectile.
    public float radius = 1f; // Collision radius.
    float radiusSq; // Radius squared; optimisation.
    public Transform target; // Who we are homing at.

    Vector3 currentPosition; // Store the current position we are at.
    float distanceTravelled; // Record the distance travelled.

    public float arcFactor = 0.5f; // Higher number means bigger arc.
    Vector3 origin; // To store where the projectile first spawned.


    // Quan li tirin un projectil caura
    private void OnCollisionEnter(Collision collision)
    {
        collectable = true;
        if (collision.gameObject.tag == "Gretel")
        {
            //crida la funcio de curacio de Gretel
            //collision.gameObject.GetComponent<Test>().heal();
            Destroy(gameObject);
        }
    }
    

    public bool isCollectable() {  
        return collectable; 
    }

    public void picked()
    {
        Debug.Log("apple picked up!");
        Destroy(gameObject);
    }

    public void setFromHansel(bool c)
    {
        from_hansel=c;
    }

    public bool isFromHansel()
    {
        return from_hansel;
    }



    //ballistica
    void Update()
    {
        // Nomes farem el calcul de ballistica quan el llencem
        if (target!=null)
        {
            Debug.Log("trhooooow");

            // Move ourselves towards the target at every frame.
            Vector3 direction = target.position - currentPosition;
            currentPosition += direction.normalized * speed * Time.deltaTime;
            distanceTravelled += speed * Time.deltaTime; // Record the distance we are travelling.

            // Set our position to <currentPosition>, and add a height offset to it.
            float totalDistance = Vector3.Distance(origin, target.position);
            float heightOffset = arcFactor * totalDistance * Mathf.Sin(distanceTravelled * Mathf.PI / totalDistance);
            transform.position = currentPosition + new Vector3(0, 0, heightOffset);
 

        }

        
    }

    // So that other scripts can use Projectile.Spawn to spawn a projectile.
    public void throwApple(Transform targetEntered)
    {
        radiusSq = radius * radius;
        origin = transform.position;
        target = targetEntered;
    }
}
