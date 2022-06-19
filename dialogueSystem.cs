using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class dialogueSystem : MonoBehaviour {

	public GameObject[] dialogueText;
	private int dialogueIndex = 0;
	private bool isPressed = false;

	void Start()
	{
		dialogueText[dialogueIndex].SetActive(true);

		dialogueText [1].SetActive (false);
		dialogueText [2].SetActive (false);
		dialogueText [3].SetActive (false);
	}

	void Update()
	{
		if ((!isPressed) && (Input.GetAxisRaw("START_BUTTON") == 1))
		{
			isPressed = true;
			Invoke ("NextDialogue", 0.5f);
		}
	}

	public void NextDialogue()
	{
		dialogueText [dialogueIndex].SetActive (false);

		dialogueIndex++;

		if (dialogueIndex < dialogueText.Length)
		{
			dialogueText [dialogueIndex].SetActive (true);
		}
		else
		{
			gameObject.SetActive (false);
		}

		isPressed = false;
	}
}
