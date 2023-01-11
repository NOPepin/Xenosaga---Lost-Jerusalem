using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GereMenuInventaire : MonoBehaviour, ISelectHandler
{
	[SerializeField] private GameObject menuRacine;
	[SerializeField] private GameObject menuInventaire;
	[SerializeField] private GameObject prefabItem;
	[SerializeField] private GameObject prefabCaseVide;
	[SerializeField] private GameObject contenuScrollViewInventaire;

	[SerializeField] private TextMeshProUGUI txtDescription;
	[SerializeField] private TextMeshProUGUI txtDescriptionNomObjet;
	[SerializeField] private TextMeshProUGUI txtDescriptionQuantite;
	[SerializeField] private TextMeshProUGUI txtDescriptionCible;
	[SerializeField] private TextMeshProUGUI txtDescriptionContexteUtilisation;

	private List<Item>			inventaire		= new List<Item>();
	private List<int>			quantite		= new List<int>();
	private List<GameObject>	casesUIItem		= new List<GameObject>();
	private List<GameObject>	casesUIvides	= new List<GameObject>();

	private Item itemSelectionne, itemSelectionnePrecedent = null;

	public static bool estActive = false;
	public static GereMenuInventaire instance { get; private set; }

	private void Start()
	{
		if (instance != null)
		{
			Debug.LogWarning("plusieurs instances de " + name);
		}

		instance = this;
	}

	private void LateUpdate()
	{
		if(estActive)
		{
			if (EventSystem.current.currentSelectedGameObject == null)
			{
				txtDescription.text = "";
				txtDescriptionNomObjet.text = "";
				txtDescriptionQuantite.text = "";
				txtDescriptionCible.text = "";
				txtDescriptionContexteUtilisation.text = "";
			}
			else if(itemSelectionnePrecedent != inventaire[casesUIItem.IndexOf(EventSystem.current.currentSelectedGameObject)])
			{
				AfficherDescription(EventSystem.current.currentSelectedGameObject);
			}
		}
		
	}

	public void DemarrageMenuInventaire()
	{
		EventSystem.current.SetSelectedGameObject(null);
		menuRacine.SetActive(false);
		menuInventaire.SetActive(true);

		inventaire = DonneesDeJeu.GetItemsInventaire();
		quantite = DonneesDeJeu.GetQuantitesInventaire();

		for(int i = 0; i<inventaire.Count; i++)
		{
			GameObject nouvelleCase = Instantiate(prefabItem, contenuScrollViewInventaire.transform);
			casesUIItem.Add(nouvelleCase);

			nouvelleCase.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventaire[i].getNom();
			nouvelleCase.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = string.Format("× {0,2:D2}", quantite[i]);
		}

		for (int i = casesUIItem.Count; i < 3; i++)
		{
			GameObject tmp;
			tmp = Instantiate(prefabCaseVide, contenuScrollViewInventaire.transform);
			casesUIvides.Add(tmp);
		}

		StartCoroutine(SelectFirstChoice());
	}

	private IEnumerator SelectFirstChoice()
	{
		// Event System requires we clear it first, then wait
		// for at least one frame before we set the current selected object.
		EventSystem.current.SetSelectedGameObject(null);
		yield return new WaitForEndOfFrame();
		if(casesUIItem.Count != 0)
		{
			EventSystem.current.SetSelectedGameObject(casesUIItem[0]);
		}
		
		estActive = true;
	}

	#region test
	public void AfficherDescription(GameObject maCase)
	{
		int index = casesUIItem.IndexOf(maCase);
		itemSelectionne = inventaire[index];

		if (itemSelectionne != itemSelectionnePrecedent)
		{
			txtDescriptionNomObjet.text = itemSelectionne.getNom();
			txtDescriptionQuantite.text = string.Format("× {0,2:D2}", quantite[index]);
			txtDescription.text = itemSelectionne.getDescription();

			ItemUtilisable tmpUtilisable = null;
			Equipement tmpEquipement = null;

			try
			{
				tmpUtilisable = (ItemUtilisable)itemSelectionne;
			}
			catch (InvalidCastException e)
			{
				//Debug.LogError(e);
			}

			try
			{
				tmpEquipement = (Equipement)itemSelectionne;
			}
			catch (InvalidCastException e)
			{
				//Debug.LogError(e);
			}

			if (tmpUtilisable != null)
			{
				string texteCible;
				switch (tmpUtilisable.GetCible())
				{
					case ItemUtilisable.Cible.Allié:
						texteCible = "1 Allié";
						break;
					case ItemUtilisable.Cible.Alliés:
						texteCible = "Tous les Alliés";
						break;
					case ItemUtilisable.Cible.Ennemi:
						texteCible = "1 Ennemi";
						break;
					case ItemUtilisable.Cible.Ennemis:
						texteCible = "Tous les Ennemis";
						break;
					case ItemUtilisable.Cible.Tous:
						texteCible = "Tout le monde";
						break;
					case ItemUtilisable.Cible.NImporte:
						texteCible = "N'importe qui";
						break;
					case ItemUtilisable.Cible.Aucune:
						texteCible = "";
						break;
					default:
						texteCible = "???";
						break;
				}

				txtDescriptionCible.text = texteCible;

				if (tmpUtilisable.EstUtilisableEnCombat() && tmpUtilisable.EstUtilisableHorsCombat())
				{
					txtDescriptionContexteUtilisation.text = "";
				}
				else
				{
					if (tmpUtilisable.EstUtilisableEnCombat())
					{
						txtDescriptionContexteUtilisation.text = "En combat seulement";
					}
					else if (tmpUtilisable.EstUtilisableHorsCombat())
					{
						txtDescriptionContexteUtilisation.text = "Hors combat seulement";
					}
					else
					{
						txtDescriptionContexteUtilisation.text = "Inutilisable";
					}
				}

			}
			else
			{
				txtDescriptionCible.text = "";

				if (tmpEquipement != null)
				{
					txtDescriptionContexteUtilisation.text = "Equipement";
				}
				else
				{
					txtDescriptionContexteUtilisation.text = "Inutilisable";
				}
			}
		}
	}
	#endregion test
	#region onSelect
	public void OnSelect(BaseEventData eventData)
	{
		int index = casesUIItem.IndexOf(eventData.selectedObject);

		itemSelectionne = inventaire[index];

		Debug.Log(itemSelectionne != itemSelectionnePrecedent);

		if(itemSelectionne != itemSelectionnePrecedent)
		{
			txtDescriptionNomObjet.text = itemSelectionne.getNom();
			txtDescriptionQuantite.text = string.Format("× {0,2:D2}", quantite[index]);
			txtDescription.text = itemSelectionne.getDescription();

			ItemUtilisable tmpUtilisable = null;
			Equipement     tmpEquipement = null;

			try
			{
				tmpUtilisable = (ItemUtilisable)itemSelectionne;
			}
			catch (InvalidCastException e)
			{
				//Debug.LogError(e);
			}

			try
			{
				tmpEquipement = (Equipement)itemSelectionne;
			}
			catch (InvalidCastException e)
			{
				//Debug.LogError(e);
			}

			if (tmpUtilisable != null)
			{
				string texteCible;
				switch (tmpUtilisable.GetCible())
				{
					case ItemUtilisable.Cible.Allié:
						texteCible = "1 Allié";
						break;
					case ItemUtilisable.Cible.Alliés:
						texteCible = "Tous les Alliés";
						break;
					case ItemUtilisable.Cible.Ennemi:
						texteCible = "1 Ennemi";
						break;
					case ItemUtilisable.Cible.Ennemis:
						texteCible = "Tous les Ennemis";
						break;
					case ItemUtilisable.Cible.Tous:
						texteCible = "Tout le monde";
						break;
					case ItemUtilisable.Cible.NImporte:
						texteCible = "N'importe qui";
						break;
					case ItemUtilisable.Cible.Aucune:
						texteCible = "";
						break;
					default:
						texteCible = "???";
						break;
				}

				txtDescriptionCible.text = texteCible;

				if(tmpUtilisable.EstUtilisableEnCombat() && tmpUtilisable.EstUtilisableHorsCombat())
				{
					txtDescriptionContexteUtilisation.text = "";
				}
				else
				{
					if(tmpUtilisable.EstUtilisableEnCombat())
					{
						txtDescriptionContexteUtilisation.text = "En combat seulement";
					}else if(tmpUtilisable.EstUtilisableHorsCombat())
					{
						txtDescriptionContexteUtilisation.text = "Hors combat seulement";
					}
					else
					{
						txtDescriptionContexteUtilisation.text = "Inutilisable";
					}
				}

			}
			else
			{
				txtDescriptionCible.text = "";
				
				if(tmpEquipement != null)
				{
					txtDescriptionContexteUtilisation.text = "Equipement";
				}
				else
				{
					txtDescriptionContexteUtilisation.text = "Inutilisable";
				}
			}
		}
	}
	#endregion onSelect

	public void FermerMenuInventaire()
	{
		foreach(GameObject caseUI in casesUIItem)
		{
			UnityEngine.Object.Destroy(caseUI);
		}
		casesUIItem.Clear();

		foreach(GameObject caseVide in casesUIvides)
		{
			UnityEngine.Object.Destroy(caseVide);
		}
		casesUIvides.Clear();

		estActive = false;
	}
}
