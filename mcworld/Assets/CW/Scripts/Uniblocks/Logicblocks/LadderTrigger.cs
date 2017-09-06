using System.Collections;
using System.Collections.Generic;
using Core.GameLogic.ActiveObjects;
using UnityEngine;

public class LadderTrigger : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

        other.GetComponent<PlayerController>().bInClimbRegion = true;
        other.GetComponent<PlayerController>().curLadderTrans = transform;
        Debug.Log("进入可爬梯子区域......");
    }

    void OnTriggerExit(Collider other)
    {
		if (other.gameObject.tag != "Player")
			return;

        other.GetComponent<PlayerController>().bInClimbRegion = false;
        other.GetComponent<PlayerController>().curLadderTrans = null;
        Debug.Log("离开可爬梯子区域......");
    }
}
