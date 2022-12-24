using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePNJ : MonoBehaviour, IInteractible
{
	[SerializeField] TextAsset inkJSon;

	private Transform positionInitiale;

	private void Update()
	{
		if (!DialogueManager.GetInstance().cutsceneIsPlaying)
		{
			positionInitiale = this.transform;
		}
	}

	public bool interaction(GereInteractions i)
	{
		if (!DialogueManager.GetInstance().cutsceneIsPlaying)
		{
			DialogueManager.GetInstance().EnterDialogueMode(inkJSon, this);
			transform.LookAt(i.monPointInteraction);
			transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

			return true;
		}
		else
		{
			return false;
		}
		
	}

	public void finInteraction()
	{
		this.transform.rotation = positionInitiale.rotation;
	}
}
