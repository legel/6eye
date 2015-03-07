using UnityEngine;
using System.Collections;

public class CameraTransform : MonoBehaviour {

	public Transform cam; 
	public Vector3 cameraRelativeRight;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		cam = Camera.main.transform;
		cameraRelativeRight=cam.TransformDirection(Vector3.right);
		rigidbody.AddForce(cameraRelativeRight * 10);
	
	}
}
