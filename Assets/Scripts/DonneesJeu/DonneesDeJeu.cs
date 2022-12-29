using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonneesDeJeu : MonoBehaviour
{
    public Personnages leader { get; private set; } = Personnages.Messie;
    public Personnage[] equipeActive { get; set; }
    public List<Personnage> reserve { get; set; }

    [SerializeField] private Dictionary<Item, int> inventaire;
    public int argent { get; set; }


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
	}

    public void ItemObtenu(Item item, int quantite = 1)
	{
        if(this.inventaire.TryGetValue(item, out int quantiteActuelle))
		{
            this.inventaire[item] = quantiteActuelle + quantite;
		}
        else
		{
            this.inventaire.Add(item, quantite);
		}
	}

    public void ItemPerdu(Item item, int quantite = 1)
	{
        this.inventaire[item] -= quantite;

        if (this.inventaire[item] == 0)
		{
            this.inventaire.Remove(item);
		}
	}

    public enum Personnages
	{
        Messie,
        Yeshua,
        Maria,
        Shion,
        Reinette,
        Mai
	}
}
