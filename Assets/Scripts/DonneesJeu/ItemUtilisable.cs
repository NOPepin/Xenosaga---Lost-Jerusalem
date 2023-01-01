using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemUtilisable : Item
{
	protected bool utilisableEnCombat, utilisableHorsCombat;
	protected Cible cible;

	public virtual void utilisation()
	{
		Debug.Log("Objet " + this.nom + " utilisé :\n" + this.description);
	}

	public Cible GetCible()
	{
		return this.cible;
	}

	public bool EstUtilisableEnCombat()
	{
		return this.utilisableEnCombat;
	}

	public bool EstUtilisableHorsCombat()
	{
		return this.utilisableHorsCombat;
	}

	public enum Cible
	{
		Allié,
		Alliés,
		Ennemi,
		Ennemis,
		Tous,
		NImporte,
		Aucune
	}
}
