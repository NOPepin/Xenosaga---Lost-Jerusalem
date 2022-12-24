using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CurseurDialogue : MonoBehaviour
{

	[SerializeField] GameObject bouttonParrent;

	// Update is called once per frame
	void Update()
	{
		if (EventSystem.current.currentSelectedGameObject == bouttonParrent)
		{
			this.GetComponent<Image>().enabled = true;
		}
		else
		{
			this.GetComponent<Image>().enabled = false;
		}
	}
}
