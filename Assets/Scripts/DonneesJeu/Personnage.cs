using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Personnage
{
	private StatistiquesPersonnage statsBase, statsActuelles;
	private int pvActuels, peActuels, niveau, pointsXP, XPNiveauSuivant, pointsTech, pointsSkill, pointsEther;
	private string nom, appellationTalent;
	private DonneesDeJeu.Personnages idPersonnage;
	private Equipement arme;
	private Equipement[] accessoires = new Equipement[3];

	public Personnage(StatistiquesPersonnage stats, string nom, string appellationTalent, DonneesDeJeu.Personnages idPersonnage = 0)
	{
		this.statsBase = stats;
		this.statsActuelles = new StatistiquesPersonnage(stats);
		this.pvActuels = this.statsActuelles.pvMax;
		this.peActuels = this.statsActuelles.peMax;
		this.niveau = 1;
		this.pointsXP = 0;
		this.pointsEther = 0;
		this.pointsSkill = 0;
		this.pointsTech = 0;
		this.XPNiveauSuivant = DonneesDeJeu.paliersNiveaux[0];
		this.nom = nom;
		this.appellationTalent = appellationTalent;
		this.idPersonnage = idPersonnage;
	}

	public StatistiquesPersonnage getStatsDeBase()
	{
		return this.statsBase;
	}

	public StatistiquesPersonnage getStatsActuelles()
	{
		return this.statsActuelles;
	}

	public int getPVActuels()
	{
		return this.pvActuels;
	}

	public int getPEActuels()
	{
		return this.peActuels;
	}

	public int getNiveau()
	{
		return this.niveau;
	}

	public int getXPTotal()
	{
		return this.pointsXP;
	}

	public int getXPDuNiveau()
	{
		if(this.niveau == 1)
		{
			return this.getXPTotal();
		}

		return this.pointsXP - DonneesDeJeu.paliersNiveaux[this.niveau-2];
	}

	public int getXPPourNiveau()
	{
		if (this.niveau == 1)
		{
			return DonneesDeJeu.paliersNiveaux[0];
		}

		return DonneesDeJeu.paliersNiveaux[this.niveau - 2] - DonneesDeJeu.paliersNiveaux[this.niveau - 1];
	}

	public int getPointsEther()
	{
		return this.pointsEther;
	}

	public int getPS()
	{
		return this.pointsSkill;
	}

	public int getPT()
	{
		return this.pointsTech;
	}

	public string getNom()
	{
		return this.nom;
	}

	public DonneesDeJeu.Personnages getIdPersonnage()
	{
		return this.idPersonnage;
	}
}
