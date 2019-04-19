using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jamp : MonoBehaviour {

    [SerializeField] GameObject Fire; //プレイヤー

    [SerializeField] GameObject Target; //目的地

    [SerializeField] float JampAngle; //飛ばす角度

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Fire != null && Target != null)
        {
            Vector3 TargetPosition = Target.transform.position;

            float angle = JampAngle;

            Vector3 velocity;
        }
	}
}
