using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonneesDeJeu : MonoBehaviour
{
	public static Personnages leader { get; private set; } = Personnages.Messie;
	public static Personnage[] equipeActive { get; set; }
	public static List<Personnage> reserve { get; set; }

	private static Dictionary<Item, int> inventaire;
	public static int argent { get; set; }

	public static int nbPersonnagesEquipeActive { get; private set; } = 1;
	public static int nbPersonnagesTotal { get; private set; } = 1;

	private Personnage yeshua;
	private Personnage maria;
	private Personnage shion;
	private Personnage reinette;
	private Personnage mai;

	public static int[] paliersNiveaux { get; private set; } // correspond à la quantité d'exp qu'il faut pour passer au niveau suivant. il y a donc 98 cases pour passer au niveau 2 à 99



	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void Awake()
	{
		GameObject[] tabObjets = GameObject.FindGameObjectsWithTag("DonneesDeJeu");

		if(tabObjets.Length > 1)
		{
			Destroy(this.gameObject);
		}

		Object.DontDestroyOnLoad(this.gameObject);
		DefinitionValeursDeBase();
	}

	private void DefinitionValeursDeBase()
	{
		inventaire = new Dictionary<Item, int>();
		argent = 0;
		equipeActive = new Personnage[3];
		reserve = new List<Personnage>();

		paliersNiveaux = new int[] // copiés depuis xenogears pour l'instant
		{
				 16,      47,     105,     204,     363,     602,     945,    1418,
			   2049,    2870,    3914,    5218,    6819,    8759,   11080,   13827,   17049,   20794,
			  25115,   30065,   35701,   42081,   49265,   57316,   66298,   76278,   87324,   99507,
			 112899,  127575,  143611,  161086,  180080,  200675,  222956,  247009,  272921,  300783,
			 330686,  362724,  396992,  433588,  472611,  514162,  558343,  605260,  655018,  707726,
			 763494,  822433,  884658,  950283, 1019425, 1092204, 1168739, 1249153, 1333570, 1422116,
			1514918, 1612106, 1713810, 1820163, 1931299, 2047354, 2168465, 2294772, 2426416, 2563540,
			2706288, 2854805, 3009240, 3169741, 3336460, 3509549, 3689163, 3875457, 4068588, 4268716,
			4476001, 4690606, 4912694, 5142431, 5379984, 5625522, 5879215, 6141235, 6411755, 6690951,
			6978999, 7276077, 7582365, 7898044, 8223297, 8558309, 8903265, 9258353, 9623763, 9999685
		};

		StatistiquesPersonnage statsYeshua = new StatistiquesPersonnage(120, 8, 4, 4, 6, 6, 4, 4, 6);
		yeshua = new Personnage(statsYeshua, "Yeshua", "Anima", Personnages.Yeshua);

		StatistiquesPersonnage statsMaria = new StatistiquesPersonnage(120, 8, 4, 4, 6, 6, 4, 4, 6);
		maria = new Personnage(statsMaria, "Maria", "Animus", Personnages.Maria);

		StatistiquesPersonnage statsShion = new StatistiquesPersonnage(120, 8, 4, 4, 6, 6, 4, 4, 6);
		shion = new Personnage(statsShion, "Shion", "Chi", Personnages.Shion);

		StatistiquesPersonnage statsReinette = new StatistiquesPersonnage(120, 8, 4, 4, 6, 6, 4, 4, 6);
		reinette = new Personnage(statsReinette, "Reinette", "Ether", Personnages.Reinette);

		StatistiquesPersonnage statsMai = new StatistiquesPersonnage(120, 8, 4, 4, 6, 6, 4, 4, 6);
		mai = new Personnage(statsMai, "Mai", "Ether", Personnages.Mai);

		equipeActive[0] = yeshua;
	}

	public static void ItemObtenu(Item item, int quantite = 1)
	{
		if(inventaire.TryGetValue(item, out int quantiteActuelle))
		{
			inventaire[item] = quantiteActuelle + quantite;
		}
		else
		{
			inventaire.Add(item, quantite);
		}
	}

	public static void ItemPerdu(Item item, int quantite = 1)
	{
		inventaire[item] -= quantite;

		if (inventaire[item] == 0)
		{
			inventaire.Remove(item);
		}
	}

	public static int GetQuantite(Item i)
	{
		if(!inventaire.TryGetValue(i, out int quantite))
		{
			return 0;
		}

		return quantite;
	}

	public enum Personnages
	{
		Autres,
		Messie,
		Yeshua,
		Maria,
		Shion,
		Reinette,
		Mai
	}
}
