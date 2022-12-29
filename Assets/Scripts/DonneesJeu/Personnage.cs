using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Personnage
{
	private StatistiquesPersonnage statsBase, statsActuelles;
	private int pvActuels, peActuels, niveau, pointsXP, pointsTech, pointsSkill, pointsEther;
	private string nom, appellationTalent;
	private DonneesDeJeu.Personnages idPersonnage;
	private Equipement arme;
	private Equipement[] accessoires = new Equipement[3];
}
