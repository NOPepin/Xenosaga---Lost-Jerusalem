using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatistiquesPersonnage
{
	public int pvMax { get; private set; }
	public int peMax { get; private set; }
	public int attaque { get; private set; }
	public int defense { get; private set; }
	public int ether { get; private set; }
	public int defenseEther { get; private set; }
	public int precision { get; private set; }
	public int esquive { get; private set; }
	public int vitesse { get; private set; }

	public StatistiquesPersonnage(int pvMax = 0, int peMax = 0, int attaque = 0, int defense = 0, int ether = 0, int defenseEther = 0, int precision = 0, int esquive = 0, int vitesse = 0)
	{
		this.pvMax			= pvMax;
		this.peMax			= peMax;
		this.attaque        = attaque;
		this.defense        = defense;
		this.ether          = ether;
		this.defenseEther   = defenseEther;
		this.precision      = precision;
		this.esquive        = esquive;
		this.vitesse        = vitesse;
	}

	public static StatistiquesPersonnage operator +(StatistiquesPersonnage s1, StatistiquesPersonnage s2)
	{
		StatistiquesPersonnage sRet = new StatistiquesPersonnage();

		sRet.pvMax			= s1.pvMax			+ s2.pvMax;
		sRet.peMax			= s1.peMax			+ s2.peMax;
		sRet.attaque		= s1.attaque		+ s2.attaque;
		sRet.defense		= s1.defense		+ s2.defense;
		sRet.ether			= s1.ether			+ s2.ether;
		sRet.defenseEther	= s1.defenseEther	+ s2.defenseEther;
		sRet.precision		= s1.precision		+ s2.precision;
		sRet.esquive		= s1.esquive		+ s2.esquive;
		sRet.vitesse		= s1.vitesse		+ s2.vitesse;

		return sRet;
	}

	public static StatistiquesPersonnage operator -(StatistiquesPersonnage s1, StatistiquesPersonnage s2)
	{
		StatistiquesPersonnage sRet = new StatistiquesPersonnage();

		sRet.pvMax			= s1.pvMax			- s2.pvMax;
		sRet.peMax			= s1.peMax			- s2.peMax;
		sRet.attaque		= s1.attaque		- s2.attaque;
		sRet.defense		= s1.defense		- s2.defense;
		sRet.ether			= s1.ether			- s2.ether;
		sRet.defenseEther	= s1.defenseEther	- s2.defenseEther;
		sRet.precision		= s1.precision		- s2.precision;
		sRet.esquive		= s1.esquive		- s2.esquive;
		sRet.vitesse		= s1.vitesse		- s2.vitesse;

		return sRet;
	}
}
