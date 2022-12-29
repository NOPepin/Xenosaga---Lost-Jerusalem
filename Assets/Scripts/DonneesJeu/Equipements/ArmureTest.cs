using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArmureTest : Equipement
{
    public ArmureTest()
	{
		this.id = 0;
		this.nom = "Armure de Test";
		this.description = "Un objet à équiper pour tester l'effet secondaire propre à l'armure.";
		this.categorie = CategorieEquipement.Corps;
		this.valeurVente = 10;
		this.bonusStats = new StatistiquesPersonnage(0, 0, 0, 2, 0, 2, 0, -1, 0);
	}

	override public void effetSecondaire()
	{
		Debug.Log("Voilà un effet secondaire que seule " + nom + " peut conférer");
	}
}
