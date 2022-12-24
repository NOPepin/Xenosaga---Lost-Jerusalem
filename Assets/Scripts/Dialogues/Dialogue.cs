using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour, IInteractible
{
	[SerializeField] TextAsset inkJSon;

	public bool interaction(GereInteractions i)
	{
		if (!DialogueManager.GetInstance().cutsceneIsPlaying)
		{
			DialogueManager.GetInstance().EnterDialogueMode(inkJSon, this);
			return true;
		}
		else
		{
			return false;
		}
		
	}

	public void finInteraction() { }
}
