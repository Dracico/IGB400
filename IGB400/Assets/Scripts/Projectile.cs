using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject startingPos;
    public Shooter sniper;
    private int scorePower = 1;
    // Use this for initialization
    void Start()
    {
        startingPos = GameObject.FindGameObjectWithTag("Start");
        sniper = GameObject.FindGameObjectWithTag("Sniper").GetComponent<Shooter>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other) // Will check if the player is hit, and return him to his start position if that's the case/
    {
        if (other.tag == "Player")
        {
            other.transform.position = startingPos.transform.position;
            sniper.numberOfHits = sniper.numberOfHits + scorePower;
            scorePower = 0;
            Destroy(this.gameObject);

        } else
        {
            sniper.numberOfMiss = sniper.numberOfMiss + scorePower;
            scorePower = 0; 
            Destroy(this.gameObject);
        }
    }
}
