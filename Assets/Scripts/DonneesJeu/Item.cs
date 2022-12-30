using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
	protected int id;
	protected string nom, description;
	protected int valeurVente;

	public string getNom()
	{
		return nom;
	}

	public string getDescription()
	{
		return description;
	}

	public int getId()
	{
		return id;
	}

	public int getValeurVente()
	{
		return valeurVente;
	}
}
