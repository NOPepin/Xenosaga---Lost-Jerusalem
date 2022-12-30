using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GerePause : MonoBehaviour
{
	[SerializeField] private GameObject panelPause;
	[SerializeField] private GameObject menuRacine;
	[SerializeField] private GameObject menuEquipe;
	[SerializeField] private GameObject menuInventaire;
	[SerializeField] private GameObject menuTalents;
	[SerializeField] private GameObject menuTech;
	[SerializeField] private GameObject menuAnima;
	[SerializeField] private GameObject menuParametres;

	[SerializeField] private TextMeshProUGUI txtArgent;

	[SerializeField] private GameObject[] choixMenu;
	[SerializeField] private GameObject[] zonesPersonnagesEquipe;
	[SerializeField] private GameObject[] portraitsPersonnages;
	[SerializeField] private GameObject[] yeuxPortraitsPersonnages;

	[SerializeField] private TextMeshProUGUI[] txtNomsPersonnages, txtNiveauxPersonnages, txtPVPersonnages, txtPVMaxPersonnages, txtPEPersonnages, txtPEMaxPersonnages;
	[SerializeField] private Image[] jaugesXPPersonnages;

	private Personnage[] equipeActive;

	public bool estActive { get; private set; } = false;

	public static GerePause instance { get; private set; }

	// Start is called before the first frame update
	void Start()
	{
		if(instance != null)
		{
			Debug.LogWarning("il y a plus d'une instance de " + this.name);
		}

		instance = this;
		panelPause.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if(estActive)
		{
			for(int i = 0; i<DonneesDeJeu.nbPersonnagesEquipe; i++)
			{
				switch (equipeActive[i].getIdPersonnage())
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

	public void DemarageMenuPause()
	{
		equipeActive = DonneesDeJeu.equipeActive;
		panelPause.SetActive(true);
		menuRacine.SetActive(true);

		for(int i = 0; i<DonneesDeJeu.nbPersonnagesEquipe; i++)
		{
			zonesPersonnagesEquipe[i].SetActive(true);
			txtNomsPersonnages[i].text = equipeActive[i].getNom();
			txtNiveauxPersonnages[i].text = equipeActive[i].getNiveau().ToString();
			txtPVPersonnages[i].text = string.Format("{0,4:D4}", equipeActive[i].getPVActuels());
			txtPVMaxPersonnages[i].text = "/" + string.Format("{0,4:D4}", equipeActive[i].getStatsActuelles().pvMax);
			txtPEPersonnages[i].text = string.Format("{0,2:D2}", equipeActive[i].getPEActuels());
			txtPEMaxPersonnages[i].text = "/" + string.Format("{0,2:D2}", equipeActive[i].getStatsActuelles().peMax);

			jaugesXPPersonnages[i].fillAmount = equipeActive[i].getXPDuNiveau() / equipeActive[i].getXPPourNiveau();
		}

		txtArgent.text = string.Format("{0,9:D9}G", DonneesDeJeu.argent);

		for(int i = DonneesDeJeu.nbPersonnagesEquipe; i<equipeActive.Length; i++)
		{
			zonesPersonnagesEquipe[i].SetActive(false);
		}

		estActive = true;
	}

	public void ArretMenuPause()
	{
		panelPause.SetActive(false);
		estActive = false;
	}
}
