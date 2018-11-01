using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    // Sniper script reference
    public Shooter sniper;

    //Canvas text references
    public Text hitText;
    public Text missText;

    void Start () {
        sniper = GameObject.FindGameObjectWithTag("Sniper").GetComponent<Shooter>();
    }
	
	// Update is called once per frame
	void Update () {
        hitText.text = "Number of Hits : " + sniper.numberOfHits;
        missText.text = "Number of Misses : " + sniper.numberOfMiss;

    }
}
