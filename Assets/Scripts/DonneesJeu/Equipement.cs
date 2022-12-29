using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipement : Item
{
	protected CategorieEquipement categorie;
	protected StatistiquesPersonnage bonusStats;

	protected enum CategorieEquipement
	{
		Arme,
		Tete,
		Corps,
		Pieds,
		Anneau
	}

	public virtual void effetSecondaire()
	{
		Debug.Log("Incroyable effet secondaire de la part de " + this.nom);
	}

	public StatistiquesPersonnage getBonus()
	{
		return this.bonusStats;
	}
}
