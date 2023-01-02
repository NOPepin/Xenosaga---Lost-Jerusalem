using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AmeliorationXP : ItemUtilisable
{
    private int xpGagne;

    public AmeliorationXP(int xpGagne, int cout, string niveau = "XS")
	{
		this.xpGagne = xpGagne;
		this.id = 3;
		this.nom = "Amélioration XP " + niveau;
		this.description = "Consommé, cet objet confère " + xpGagne + " XP au personnage.";
		this.valeurVente = cout;
		this.utilisableEnCombat = false;
		this.utilisableHorsCombat = true;
		this.cible = Cible.Allié;
	}
}
