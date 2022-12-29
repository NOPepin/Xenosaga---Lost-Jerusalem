using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemUtilisable : Item
{
	protected bool utilisableEnCombat, utilisableHorsCombat;

	public virtual void utilisation()
	{
		Debug.Log("Objet " + this.nom + " utilisé :\n" + this.description);
	}
}
