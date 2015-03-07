using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class textbehaviour : MonoBehaviour {
	
	static string url = "http://ec2-54-148-235-108.us-west-2.compute.amazonaws.com:2000/message1.txt";
	private WWW www;
	public String message;
	public long timestamp;
	public bool go;
	public Text allo;
	Text alla;

	void Start () {
		go = true;
		alla = GetComponent<Text>();
	}
	
	
	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		
		// The text that will be retrieve will have the following form:
		// TEXT ------- \t POSITIVE_INDEX
		
		
		// check for errors
		if (www.error == null)
		{
			string text = www.text;
			string[] datas = text.Split('\t');
			message = datas[1];
			long actual_timestamp = Convert.ToInt64(datas[0]);
			if (actual_timestamp == timestamp){
				go = false;
			}
			timestamp = actual_timestamp;
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    

	}
	
	void Update () {
		
		if (Time.frameCount % 20 == 0) {
			www = new WWW (url);
			go = true;
			StartCoroutine(WaitForRequest(www));
			alla.text = "My cat died"; // message;
			Debug.Log(message);
		}
	}

}


