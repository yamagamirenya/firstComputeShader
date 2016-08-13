using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

    float t = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        t += Time.deltaTime * 10;

        transform.rotation = Quaternion.Euler(t, t, t);

	}
}
