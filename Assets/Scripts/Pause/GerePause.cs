using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
	[SerializeField] private GameObject menuEncyclopedie;
	[SerializeField] private GameObject menuSauvegarder;
	[SerializeField] private GameObject menuSelectionPersonnage;

	[SerializeField] private TextMeshProUGUI txtArgent;

	[SerializeField] private GameObject[] choixMenu;
	[SerializeField] private GameObject[] zonesPersonnagesEquipe;
	[SerializeField] private GameObject[] portraitsPersonnages;
	[SerializeField] private GameObject[] yeuxPortraitsPersonnages;

	[SerializeField] private TextMeshProUGUI[] txtNomsPersonnages, txtNiveauxPersonnages, txtPVPersonnages, txtPVMaxPersonnages, txtPEPersonnages, txtPEMaxPersonnages;
	[SerializeField] private Image[] jaugesXPPersonnages;

	private Personnage[] equipeActive;

	public bool estActive { get; private set; } = false;
	public bool sousMenuActif { get; private set; } = false;

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
			for(int i = 0; i<DonneesDeJeu.nbPersonnagesEquipeActive; i++)
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
		sousMenuActif = false;
		GereMenuEquipe.estActive = false;

		if(GereMenuInventaire.estActive) { GereMenuInventaire.instance.FermerMenuInventaire(); }

		equipeActive = DonneesDeJeu.equipeActive;
		panelPause.SetActive(true);
		menuRacine.SetActive(true);
		menuEquipe.SetActive(false);
		menuInventaire.SetActive(false);
		menuTalents.SetActive(false);
		menuTech.SetActive(false);
		menuAnima.SetActive(false);
		menuParametres.SetActive(false);
		menuEncyclopedie.SetActive(false);
		menuSauvegarder.SetActive(false);
		menuSelectionPersonnage.SetActive(false);

		for (int i = 0; i<DonneesDeJeu.nbPersonnagesEquipeActive; i++)
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

		for(int i = DonneesDeJeu.nbPersonnagesEquipeActive; i<equipeActive.Length; i++)
		{
			zonesPersonnagesEquipe[i].SetActive(false);
		}

		estActive = true;

		StartCoroutine(SelectFirstChoice());
	}

	private IEnumerator SelectFirstChoice()
	{
		// Event System requires we clear it first, then wait
		// for at least one frame before we set the current selected object.
		EventSystem.current.SetSelectedGameObject(null);
		yield return new WaitForEndOfFrame();
		EventSystem.current.SetSelectedGameObject(choixMenu[0].gameObject);
	}

	public void ChoisirMenu(string nomMenu)
	{
		switch(nomMenu)
		{
			case "equipe":
				sousMenuActif = true;
				GereMenuEquipe.instance.DemarageMenuEquipe();
				break;
			case "inventaire":
				sousMenuActif = true;
				GereMenuInventaire.instance.DemarrageMenuInventaire();
				break;
			case "talents":
				Debug.Log("On passe au menu Talents ! [pas encore implémenté]");
				break;
			case "techniques":
				Debug.Log("On passe au menu Techniques ! [pas encore implémenté]");
				break;
			case "anima":
				Debug.Log("On passe au menu Anima ! [pas encore implémenté]");
				break;
			case "parametres":
				Debug.Log("On passe au menu Paramètres ! [pas encore implémenté]");
				break;
			case "encyclopedie":
				Debug.Log("On passe au menu Encyclopédie ! [pas encore implémenté]");
				break;
			case "sauvegarder":
				Debug.Log("On passe au menu Sauvegarder ! [pas encore implémenté]");
				break;
			default:
				Debug.Log("Le menu séléctionné n'existe pas...?");
				break;
		}
	}

	public IEnumerator ArretMenuPause()
	{
		yield return new WaitForSeconds(0.1f);
		panelPause.SetActive(false);
		estActive = false;
		sousMenuActif = false;

	}
}
