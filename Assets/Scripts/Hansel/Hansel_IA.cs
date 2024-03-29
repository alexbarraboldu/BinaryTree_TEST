using Opsive.UltimateCharacterController.Demo.AI;
using Opsive.UltimateCharacterController.Items.Actions.Impact;
using Opsive.UltimateCharacterController.Objects;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Hansel_IA : MonoBehaviour
{

    //hansel general vars
    public bool alive;
    private float intervalBT = 0.5f; //segons que pasen entre un check del BT i el seguent
    private float btTimer = 0f;
    public int life = 10;
    public int maxLife = 10;
    public bool flee=false;


    //enemy general vars
    public float enemyRadius = 20f; //rang de deteccio de enemics
    public bool enemyNear = false;

    //apples
    public GameObject pApple;
    public float pickupDistance = 1.3f;
    public int apples;
    public float appleRadius = 20f; //rang de busqueda de pomes
    public int maxApples=5;
    private GameObject gThrowPoint;
    public float throwForceAir1 = 300f; //forca correctora
    public float throwForceAir2 = 500f; //forca que efectua al tirar lobjecte 
    public float throwForceFront1 = 100f; //forca correctora
    public float throwForceFront2 = 700f; //forca que efectua al tirar lobjecte 



    //navmesh
    private NavMeshAgent agent;
    public float followDistance=3.5f; //a partir daquesta distancia comença a seguir el jugador
    private float stopDistanceFollow = 2f; //ens parem a 2f de la Gretel
    public float healDistance = 20f; //a partir daquesta distancia pot tirar la poma a la Gretel (cap amunt)
    public float healDistanceFront = 7.5f; //a partir daquesta distancia li tirara la poma recta, perque esta aprop seu

    //gretel vars
    public GameObject gGretel;
    public int gretelLife;
    private int gretelNeedsHeal=5; // numero de vides en el qual haurem de curarla
    private float gretelDistance;


    //knockout vars
    private float resurrectionDistance=1.3f;
    private float abandoneDistance = 30f;


    void Start()
    {
        alive = true;
        gretelLife = 1; //testing (asigna)
        agent = GetComponent<NavMeshAgent>();
        gThrowPoint = transform.GetChild(0).gameObject;

        if (gGretel == null)
        {
            Debug.Log("falta asignar la Gretel");
        }
        if(pApple == null)
        {
            Debug.Log("falta asignar el prefab de la poma");
        }
    }

    void Update()
    {
        btTimer += Time.deltaTime;
        //executem cada X segons
        if (btTimer > intervalBT)
        {
            decisionTree();
            btTimer = 0f;
        }
        
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
                return;
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

                    //no hi han pomes aprop, segueixo la Gretel
                    Debug.Log("no hi ha pomes aprop i en necessito, follow gretel");
                    followGretel();
                    return;
                }

                //tinc pomes, curo a la Gretel
                else
                {
                    healGretel();
                    return;
                    

                }
            }

            //la Gretel no necesita cura
            else
            {
                //mirem si tenim enemics aprop
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, enemyRadius);
                enemyNear = false;
                
                foreach (var hitCollider in hitColliders)
                {
                    //tenim enemics aprop
                    if (hitCollider.tag == "Enemy")
                    {
                        enemyNear = true;
                        Debug.Log("enemy is near me!");
                    }
                }

                //tenim enemics aprop
                if (enemyNear)
                {
                    //si estic fugint, vaig cap a la Gretel
                    if (flee)
                    {

                    }
                    else
                    {
                        //si tinc la vida per menys de la mitat, sactiva la possibilitat de fugir
                        if (life < maxLife / 2)
                        {
                            //probabilitat mes alta segons menys vida tinguem
                            if (Random.Range(0, life) == life)
                            {
                                Debug.Log("fugir!");
                                flee = true;
                            }

                        }
                    }

                    

                }
                //no tenim enemics aprop
                else
                {

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
                updateApples(1);
                obj.GetComponent<Apple>().picked();
                //falta animacio
            }
        }
        else
        {
            //ens acostem al objecte
            Debug.Log("going to pickup object");
            agent.isStopped = false;
            agent.destination = obj.transform.position;
            agent.stoppingDistance = 0f;

        }

    }

    //segueix a la Gretel quan esta mes lluny de la followDistance
    private void followGretel()
    {
        if (gGretel!=null)
        {
            gretelDistance = Vector3.Distance(transform.position, gGretel.transform.position);

            if(gretelDistance > followDistance)
            {
                agent.isStopped = false;
                agent.destination = gGretel.transform.position;
                agent.stoppingDistance = stopDistanceFollow;

            }

            
        }
        
    }

    //a partir de X distancia de la Gretel, li pot llencar la poma per curarla. Sino si acosta
    private void healGretel()
    {
        if(gGretel!=null)
        {
            gretelDistance = Vector3.Distance(transform.position, gGretel.transform.position);

            //ens apropem
            if( gretelDistance > healDistance)
            {
                agent.isStopped = false;
                Debug.Log("ens aprop per curar");
                agent.destination= gGretel.transform.position;
                agent.stoppingDistance = healDistance;
            }
            //tirem la poma si tenim linia de visio amb la Gretel
            else
            {
                agent.isStopped = true;
                Debug.Log("hauriem de tirar la poma!");

                //mira cap a la Gretel
                transform.LookAt(gGretel.transform);

                //la direccio entre A i B =  (B - A)
                Vector3 directionGretel = gGretel.transform.position - gThrowPoint.transform.position;

                //tirem un raycast per mirar si no hi ha res entre mitg que impedeixi tirar la poma
                RaycastHit hit;

                if(Physics.Raycast(gThrowPoint.transform.position, directionGretel, out hit, healDistance))
                {
                    Debug.DrawRay(gThrowPoint.transform.position, directionGretel, Color.blue, 2f);
                    Debug.Log("Line Of Vision with Gretel");

                    if(hit.transform.tag == "Gretel")
                    {
                        Debug.Log("throw apple");
                        //instancio la poma i la llenco amb un add force
                        GameObject projectil = Instantiate(pApple, gThrowPoint.transform.position, gThrowPoint.transform.rotation);
                        projectil.GetComponent<Apple>().throwHealApple(gGretel.transform);
                        Rigidbody rbProjectil = projectil.GetComponent<Rigidbody>();

                        //segons lo lluny que estigui la Gretel, tirara la poma amunt o li tirara recte
                        gretelDistance = Vector3.Distance(transform.position, gGretel.transform.position);
                        Vector3 force;

                        //cura daprop
                        if (gretelDistance <= healDistanceFront)
                        {
                            force = directionGretel.normalized * throwForceFront2 + transform.up * throwForceFront1;
                        }
                        else
                        {
                        //cura de lluny
                            force = directionGretel.normalized * throwForceAir1 + transform.up * throwForceAir2;
                        }


                        rbProjectil.AddForce(force);
                        updateApples(-1);
                        Object.Destroy(projectil,1.5f); //destruim la poma
                    }

                    

                }

               
            }

        }
    }

    //suma i resta fins arribar als limits
    private void updateApples(int quantity)
    {
        apples += quantity;
        Mathf.Clamp(apples, 0, maxApples); 
    }


    private void updateLife(int quantity)
    {
        life += quantity;
        Mathf.Clamp(life, 0, maxLife);
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
