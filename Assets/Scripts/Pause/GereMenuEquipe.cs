using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GereMenuEquipe : MonoBehaviour
{
	[SerializeField] private GameObject[] casesPersonnages;
	[SerializeField] private GameObject[] portraitsPersonnages;
	[SerializeField] private GameObject[] yeuxPortraitsPersonnages;

	[SerializeField] private TextMeshProUGUI[] txtNomsPersonnages, txtNiveauxPersonnages, txtPVPersonnages, txtPVMaxPersonnages, txtPEPersonnages, txtPEMaxPersonnages;
	[SerializeField] private Image[] jaugesXPPersonnages;

	[SerializeField] private RectTransform panelContenuScroll;

	[SerializeField] private GameObject menuRacine;
	[SerializeField] private GameObject menuEquipe;

	private Personnage[] equipe;
	private int nbPersonnagesEquipe = 0;

	private RectTransform oldRect;

	public static GereMenuEquipe instance { get; private set; }

	public static bool estActive = false;

	private void Start()
	{
		if(instance!=null)
		{
			Debug.LogWarning("plusieurs instances de " + name);
		}

		instance = this;
	}

	private void Update()
	{
		if(estActive)
		{
			SnapTo(EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>());

			for (int i = 0; i < nbPersonnagesEquipe; i++)
			{
				switch (equipe[i].getIdPersonnage())
				{
					case DonneesDeJeu.Personnages.Yeshua:
						PortraitManager.instance.gereAnimations("YeshuaNeutre", portraitsPersonnages[i], yeuxPortraitsPersonnages[i]);
						break;
					default:

						break;
				}
			}
		}
	}

	public void DemarageMenuEquipe()
	{
		menuRacine.SetActive(false);
		menuEquipe.SetActive(true);
		nbPersonnagesEquipe = 0;

		equipe = new Personnage[DonneesDeJeu.equipeActive.Length + DonneesDeJeu.reserve.Count];

		for(int i = 0; i<DonneesDeJeu.nbPersonnagesEquipeActive; i++)
		{
			equipe[i] = DonneesDeJeu.equipeActive[i];
			nbPersonnagesEquipe++;
			casesPersonnages[i].SetActive(true);
			txtNomsPersonnages[i].text = equipe[i].getNom();
			txtNiveauxPersonnages[i].text = equipe[i].getNiveau().ToString();
			txtPVPersonnages[i].text = string.Format("{0,4:D4}", equipe[i].getPVActuels());
			txtPVMaxPersonnages[i].text = "/" + string.Format("{0,4:D4}", equipe[i].getStatsActuelles().pvMax);
			txtPEPersonnages[i].text = string.Format("{0,2:D2}", equipe[i].getPEActuels());
			txtPEMaxPersonnages[i].text = "/" + string.Format("{0,2:D2}", equipe[i].getStatsActuelles().peMax);

			jaugesXPPersonnages[i].fillAmount = equipe[i].getXPDuNiveau() / equipe[i].getXPPourNiveau();
		}

		for(int i = DonneesDeJeu.nbPersonnagesEquipeActive; i < DonneesDeJeu.nbPersonnagesTotal; i++)
		{
			equipe[i] = DonneesDeJeu.reserve[i];
			nbPersonnagesEquipe++;
			casesPersonnages[i].SetActive(true);
			txtNomsPersonnages[i].text = equipe[i].getNom();
			txtNiveauxPersonnages[i].text = equipe[i].getNiveau().ToString();
			txtPVPersonnages[i].text = string.Format("{0,4:D4}", equipe[i].getPVActuels());
			txtPVMaxPersonnages[i].text = "/" + string.Format("{0,4:D4}", equipe[i].getStatsActuelles().pvMax);
			txtPEPersonnages[i].text = string.Format("{0,2:D2}", equipe[i].getPEActuels());
			txtPEMaxPersonnages[i].text = "/" + string.Format("{0,2:D2}", equipe[i].getStatsActuelles().peMax);

			jaugesXPPersonnages[i].fillAmount = equipe[i].getXPDuNiveau() / equipe[i].getXPPourNiveau();
		}

		for(int i = nbPersonnagesEquipe; i<casesPersonnages.Length; i++)
		{
			casesPersonnages[i].SetActive(false);
		}

		StartCoroutine(SelectFirstChoice());
	}

	private IEnumerator SelectFirstChoice()
	{
		// Event System requires we clear it first, then wait
		// for at least one frame before we set the current selected object.
		EventSystem.current.SetSelectedGameObject(null);
		yield return new WaitForEndOfFrame();
		EventSystem.current.SetSelectedGameObject(casesPersonnages[0]);
		estActive = true;
	}

	public void SnapTo(RectTransform target)
	{
		int i = System.Array.IndexOf(casesPersonnages, target.gameObject);
		Vector2 v = target.position; //We are getting the Position of the Selected Inventory Item's RectTransform.
		bool estVisible = RectTransformUtility.RectangleContainsScreenPoint(target, v); //We are checking if the Selected Inventory Item is visible from the camera.

		float incrimentSize = target.rect.height; //We are getting the height of our Inventory Items

		if (!estVisible)
		{
			if (oldRect != null) //If we haven't assigned Old before we do nothing.
			{
				if (oldRect.localPosition.y < target.localPosition.y) //If the last rect we were selecting is lower than our newly selected rect.
				{
					panelContenuScroll.anchoredPosition += new Vector2(0, -incrimentSize); //We move the content panel down.
				}
				else if (oldRect.localPosition.y > target.localPosition.y) //if the last rect we were selecting is higher than our newly selected rect.
				{
					panelContenuScroll.anchoredPosition += new Vector2(0, incrimentSize); //We move the content panel up.
				}
			}
		}

		oldRect = target; //We assign our newly selected rect as the OldRect.
	}
}
