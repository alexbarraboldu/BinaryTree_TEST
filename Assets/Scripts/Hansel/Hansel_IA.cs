using Opsive.UltimateCharacterController.Demo.AI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Hansel_IA : MonoBehaviour
{

    //hansel vars
    public bool alive;
    public int apples;
    public float appleRadius=20f;
    public float pickupDistance=1.3f;

    //navmesh
    private NavMeshAgent agent;
    public float followDistance=3.5f; //a partir daquesta distancia comença a seguir el jugador
    private float stopDistanceFollow = 2f; //ens parem a 2f de la Gretel


    //gretel vars
    public GameObject gGretel;
    public int gretelLife;
    private int gretelNeedsHeal=5; // numero de vides en el qual haurem de curarla
    

    //knockout vars
    private float gretelDistance;
    private float resurrectionDistance=1.3f;
    private float abandoneDistance = 30f;


    void Start()
    {
        alive = true;
        gretelLife = 10; //testing (asigna)
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        decisionTree();
    }



    private void decisionTree()
    {

        //si estic mort
        if (!alive)
        {

            gretelDistance = gGretel != null ? Vector3.Distance(transform.position, gGretel.transform.position) : 10f;
             

            //la Gretel mabandona, morim els dos
            if (gGretel != null && gretelDistance <= abandoneDistance)
            {
                //crida a la funcio de mort dels dos jugadors

            }

        }
        else
        {
        //estic viu

            //gretelLife = 10; //testing (asigna)
            
            //Gretel necessita cura
            if (gretelLife < gretelNeedsHeal)
            {
                Debug.Log("gretel needs heal");

                //no tinc pomes
                if (apples == 0)
                {
                    //busquem pomes
                    Collider[] hitColliders = Physics.OverlapSphere(transform.position, appleRadius);
                    foreach (var hitCollider in hitColliders)
                    {

                        //tenim pomes que podem recollir
                        if (hitCollider.tag == "Apple" && hitCollider.GetComponent<Apple>().isCollectable())
                        {

                            pickup(hitCollider.gameObject);
                            return;
                        }
                    }

                    //no hi han pomes aprop
                    Debug.Log("no hi ha pomes aprop i en necessito, follow gretel");
                    followGretel();
                }
            }

        }

       

         
    }

    public void pickup(GameObject obj)
    {

        float objectDistance = Vector3.Distance(transform.position, obj.transform.position);

        //el pillem
        if(objectDistance <= pickupDistance)
        {
            if (obj.GetComponent<Apple>())
            {
                apples++;
                obj.GetComponent<Apple>().picked();
            }
        }
        else
        {
            //ens acostem al objecte
            Debug.Log("going to pickup object");
            agent.destination = obj.transform.position;


        }

    }

    //segueix a la Gretel quan esta mes lluny de la followDistance
    public void followGretel()
    {
        if (gGretel!=null)
        {
            gretelDistance = Vector3.Distance(transform.position, gGretel.transform.position);

            if(gretelDistance > followDistance)
            {
                agent.destination = gGretel.transform.position;
                agent.stoppingDistance = stopDistanceFollow;

            }

            
        }
        
    }


    //reviu en Hansel
    public void revive()
    {
        if (gGretel != null) {

            gretelDistance = Vector3.Distance(transform.position, gGretel.transform.position);

            //Gretel aprop
            if (gretelDistance <= resurrectionDistance)
            {
                //em reviu
                alive = true;
            }
        }
    }
}
