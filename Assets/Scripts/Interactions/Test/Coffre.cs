using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Coffre : MonoBehaviour, IInteractible
{
	private List<Item> objetsObtenus = new List<Item>();
	private List<int> quantite = new List<int>();
	private int index;
	private bool aEteOuvert = false;

	public void finInteraction()
	{
		if (++index < objetsObtenus.Count && !aEteOuvert)
		{
			MessageCoffre();
			ObtentionObjet();
		}
		else
		{
			GereInteractions.GetInstance().interactionEnCours = false;
			aEteOuvert = true;
		}
	}

	public bool interaction(GereInteractions i)
	{
		if(!aEteOuvert)
		{
			this.objetsObtenus.Add(new PommeDeTest());
			this.quantite.Add(2);
			this.objetsObtenus.Add(new ArmureTest());
			this.quantite.Add(1);

			index = 0;

			MessageCoffre();
			ObtentionObjet();

			return true;
		}
		else
		{
			finInteraction();
			return false;
		}
	}

	private void ObtentionObjet()
	{
		DonneesDeJeu.ItemObtenu(this.objetsObtenus.ElementAt(index), this.quantite.ElementAt(index));
	}

	private void MessageCoffre()
	{
		string nomObj;
		int quantite;

		nomObj = this.objetsObtenus.ElementAt(index).getNom();
		quantite = this.quantite.ElementAt(index);

		DialogueManager.GetInstance().MessageObjetsObtenus(nomObj, quantite, this);
	}
}
