using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Sentiment : MonoBehaviour {
	
	static string url = "http://ec2-54-148-235-108.us-west-2.compute.amazonaws.com:2000/sentiment.txt";
	private WWW www;
	public static float positive_average;
	
	// Use this for initialization
	void Start () {
		www = new WWW(url);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.frameCount % 25 == 0) {
			StartCoroutine(WaitForRequest(www));
			Debug.Log (positive_average);
		}
	}
	
	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		
		// The text that will be retrieve will have the following form:
		// TEXT ------- \t POSITIVE_INDEX
		
		
		// check for errors
		if (www.error == null)
		{
			positive_average = Convert.ToSingle (www.text);

		}     
	}
}