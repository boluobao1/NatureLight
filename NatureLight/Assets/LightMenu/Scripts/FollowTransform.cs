using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour {

    Transform FollowTarget;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = FollowTarget.transform.position;
        transform.localScale = FollowTarget.transform.localScale;

	}
}
