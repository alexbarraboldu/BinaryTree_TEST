using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hansel_IA : MonoBehaviour
{

    public enum States { Idle, KnockOut, PickingUp, Following, Heal, Coward, Fighthing };
    public States currState;
    public GameObject gGretel;


    //knockout vars
    private float gretelDistance;
    private float resurrectionDistance=1.3f;
    private float abandoneDistance = 30f;


    // Start is called before the first frame update
    void Start()
    {
        currState = States.KnockOut;
    }

    // Update is called once per frame
    void Update()
    {
        decisionTree();
    }



    private void decisionTree()
    {
        switch (currState)
        {
            //estic mort
            case States.KnockOut:

                //setejem la distancia. Si no tenim lobjecte Gretel posem distancia de 10f per no triguejar res
                gretelDistance = gGretel != null ? Vector3.Distance(transform.position, gGretel.transform.position) : 10f;

                //la Gretel mabandona, morim els dos
                if (gGretel != null && gretelDistance<= abandoneDistance)
                {
                    //crida a la funcio de mort dels dos jugadors

                }

                break;

            case States.Idle:


                break;


        }
    }

    //reviu en Hansel
    public void revive()
    {
        gretelDistance = gGretel != null ? Vector3.Distance(transform.position, gGretel.transform.position) : 10f;

        //Gretel aprop
        if (gGretel != null && gretelDistance <= resurrectionDistance)
        {
            //em reviu
            currState= States.Idle;

        }
    }
}
