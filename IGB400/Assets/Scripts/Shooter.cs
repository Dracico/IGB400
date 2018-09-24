using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

    public GameObject player;
    private Rigidbody rigidbodyPlayer;
    public GameObject bullet;
    public GameObject aimImage;
    public bool isInSight = false;
    public bool shot = false;
    public GameObject instance;
    public float directionCoef;
    
	// Use this for initialization
	void Start () {
        rigidbodyPlayer = player.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        isInSight = InSight();
	}

    //Check if the player is in sight
    bool InSight()
    {
       
        if (instance != null)
        {
            Destroy(instance);
        }
        RaycastHit hit;
        var rayDirection = player.transform.position - transform.position;
        if (Physics.Raycast(transform.position, rayDirection,out hit))
        {
            if (hit.transform.tag == "Player")
            {
                if (shot == false)
                {
                    instance = Instantiate(aimImage, hit.transform.position+(Direction()*directionCoef), aimImage.transform.rotation);
                    
                }
                
                return true;
            }
            else
            {
                return false;
            }
        } else
        {
            return false;
        }
    }

    Vector3 Direction()
    {
        Vector3 playerDirection = rigidbodyPlayer.velocity;
        playerDirection.Normalize();
        return playerDirection;
    }
}
