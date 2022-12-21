using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitManager : MonoBehaviour
{
	[SerializeField] List<Sprite> listeVisagesNeutresYeshua, listeYeuxNeutresYeshua;
	[SerializeField] float frameRateYeux, frameRateVisage;

	public static PortraitManager instance { get; private set; }

	private float animationYeuxStartTime, animationVisageStartTime;
	private Image visage, yeux;

	private float intervaleYeux;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("Found more than one Portrait Manager in the scene");
		}
		instance = this;
	}

	private void Start()
	{
		intervaleYeux = Random.Range(0.5f, 3.5f);
	}

	public void gereAnimations(string nomPortrait, GameObject imageVisage, GameObject imageYeux)
	{
		gereAnimationYeux(nomPortrait, imageYeux.GetComponent<Image>());
		
		if (DialogueManager.GetInstance().characterIsTalking)
		{
			gereAnimationVisage(nomPortrait, imageVisage.GetComponent<Image>());
		}
		else
		{
			boucheFermee(nomPortrait, imageVisage.GetComponent<Image>());
		}
	}

	public List<Sprite> getListeVisages(string nomPortrait)
	{
		List<Sprite> listeRet = null;

		if (nomPortrait == "YeshuaNeutre") { listeRet = listeVisagesNeutresYeshua; }
				

		return listeRet;
	}

	public List<Sprite> getListeYeux(string nomPortrait)
	{
		List<Sprite> listeRet = null;

		if (nomPortrait == "YeshuaNeutre") { listeRet = listeYeuxNeutresYeshua; }


		return listeRet;
	}

	void gereAnimationYeux(string nomPortrait, Image yeux)
	{
		List<Sprite> listeSprites = getListeYeux(nomPortrait);
		float animationTime;

		animationTime = Time.time - animationYeuxStartTime;

		if (animationTime >= intervaleYeux)
		{
			intervaleYeux = Random.Range(0.5f, 3.5f);
			animationYeuxStartTime = Time.time;
			animationTime = 0;
		}

		

		int frame = (int)(animationTime * frameRateYeux); 

		if (frame < 5)
		{
			yeux.sprite = listeSprites[frame % listeSprites.Count];
		}
		else
		{
			yeux.sprite = listeSprites[0];
		}
	}

	void gereAnimationVisage(string nomPortrait, Image visage)
	{
		List<Sprite> listeSprites = getListeVisages(nomPortrait);
		float animationTime;

		animationTime = Time.time - animationYeuxStartTime;

		int nbFrame = (int)(animationTime * frameRateYeux);
		int frame = nbFrame % listeSprites.Count;

		visage.sprite = listeSprites[frame];
	}

	private void boucheFermee(string nomPortrait, Image visage)
	{
		List<Sprite> listeSprites = getListeVisages(nomPortrait);

		visage.sprite = listeSprites[0];
	}
}
