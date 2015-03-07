using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RetroPrinterScriptBasic : MonoBehaviour {

	#region Private fields
	
	private AudioSource Audio;
	
	private string mainText = "";
	private string textCursor;
	private string closingTags = string.Empty;
	
	private bool running = true;

	private TextMesh textComponent;
	
	#endregion
	
	#region Public fields

	public GameObject ObjectToUpdate = null;
	
	public string CursorCharacter = " █";
	public string AlternateCursorCharacter = " _";
	
	public float LetterInterval = 0.1f;
	public float EndLineWait = 0.0f;

	public List<string> FullText;
	
	public bool alternateCursor = true;
	
	public bool runOnStart = true;
	
	
	#endregion

	void Start () {
		
		Init();

		Run();
	}

	void Update () {
		
		UpdateProperty();
	}
	
	void Init()
	{
		textCursor = CursorCharacter;

		textComponent = (ObjectToUpdate.GetComponent(typeof(TextMesh)) as TextMesh);

		StartCoroutine( ChangeCursor() );
	}
	
	IEnumerator ChangeCursor()
	{
		while(alternateCursor)
		{
			if(textCursor == CursorCharacter)
				textCursor = AlternateCursorCharacter;
			else
				textCursor = CursorCharacter;
			
			yield return new WaitForSeconds(0.5f);
		}
	}
	
	IEnumerator UpdateText()
	{
		foreach(string s in FullText)
		{
			if(!running)
				yield break;
			
			yield return StartCoroutine( UpdateLine(s) );
			
		}
	}
	
	IEnumerator UpdateLine(string line)
	{
		if(!running)
			yield break;

		if(string.IsNullOrEmpty(line))
		{
			mainText += "\n";
			yield return new WaitForSeconds(EndLineWait);
		}
		
		else
		{
			for(int i = 0; i < line.Length; i++)
			{
				if(!running)
					yield break;

				else
				{
					mainText += line[i];

					yield return new WaitForSeconds(LetterInterval);
				}
			}
			
			yield return new WaitForSeconds(EndLineWait);
			
			if(FullText.IndexOf(line) != (FullText.Count() - 1)) // make sure that we don't add an empty line at the end of our text
				mainText += "\n";
		}
		
	}
	
	public void Run()
	{
		running = true;
		StartCoroutine( UpdateText() );
	}
	
	public void Stop()
	{
		running = false;
	}
	
	public void EnableAlternateCursor()
	{
		alternateCursor = true;
	}
	
	public void DisableAlternateCursor()
	{
		alternateCursor = false;
		textCursor = CursorCharacter;
	}
	
	void Clear()
	{
		mainText = string.Empty;
	}
	
	
	private void UpdateProperty()
	{
		textComponent.text = string.Format("{0}{1}{2}", mainText, closingTags, textCursor);
	}
}
