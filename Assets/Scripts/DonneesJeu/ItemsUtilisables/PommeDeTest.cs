using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PommeDeTest : ItemUtilisable
{
	private TextAsset dialogue;
    public PommeDeTest()
	{
		this.id = 1;
		this.nom = "Pomme de test";
		this.description = "Une pomme � utiliser pour tester un effet";
		this.valeurVente = 5;
		this.utilisableEnCombat = true;
		this.utilisableHorsCombat = true;
		this.cible = Cible.Alli�;

		dialogue = (TextAsset)Resources.Load("Dialogue/PommeTest");
	}

	public override void utilisation()
	{
		DialogueManager.GetInstance().EnterDialogueMode(dialogue);
	}
}
