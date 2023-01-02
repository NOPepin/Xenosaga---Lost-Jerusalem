using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GereMenuInventaire : MonoBehaviour, ISelectHandler
{
	[SerializeField] private GameObject menuRacine;
	[SerializeField] private GameObject menuInventaire;
	[SerializeField] private GameObject prefabItem;
	[SerializeField] private GameObject contenuScrollViewInventaire;

	[SerializeField] private TextMeshProUGUI txtDescription;
	[SerializeField] private TextMeshProUGUI txtDescriptionNomObjet;
	[SerializeField] private TextMeshProUGUI txtDescriptionQuantite;
	[SerializeField] private TextMeshProUGUI txtDescriptionCible;
	[SerializeField] private TextMeshProUGUI txtDescriptionContexteUtilisation;

	private List<Item>			inventaire = new List<Item>();
	private List<int>			quantite = new List<int>();
	private List<GameObject>	casesUIItem = new List<GameObject>();

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

	private void Update()
	{
		if(EventSystem.current.currentSelectedGameObject == null)
		{
			txtDescription.text = "";
			txtDescriptionNomObjet.text = "";
			txtDescriptionQuantite.text = "";
			txtDescriptionCible.text = "";
			txtDescriptionContexteUtilisation.text = "";
		}
	}

	public void DemarrageMenuInventaire()
	{
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
			Instantiate(new GameObject(), contenuScrollViewInventaire.transform);
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

	public void OnSelect(BaseEventData eventData)
	{
		int index = casesUIItem.IndexOf(eventData.selectedObject);

		itemSelectionne = inventaire[index];

		if(itemSelectionne != itemSelectionnePrecedent)
		{
			txtDescriptionNomObjet.text = itemSelectionne.getNom();
			txtDescriptionQuantite.text = string.Format("× {0,2:D2}", quantite[index]);
			txtDescription.text = itemSelectionne.getDescription();

			if(itemSelectionne.GetType() == typeof(ItemUtilisable))
			{
				ItemUtilisable tmp = (ItemUtilisable)itemSelectionne;
				string texteCible;
				switch (tmp.GetCible())
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

				if(tmp.EstUtilisableEnCombat() && tmp.EstUtilisableHorsCombat())
				{
					txtDescriptionContexteUtilisation.text = "";
				}
				else
				{
					if(tmp.EstUtilisableEnCombat())
					{
						txtDescriptionContexteUtilisation.text = "En combat seulement";
					}

					if(tmp.EstUtilisableHorsCombat())
					{
						txtDescriptionContexteUtilisation.text = "Hors combat seulement";
					}

					txtDescriptionContexteUtilisation.text = "Inutilisable";
				}

			}
			else
			{
				txtDescriptionCible.text = "";
				
				if(itemSelectionne.GetType() == typeof(Equipement))
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
}
