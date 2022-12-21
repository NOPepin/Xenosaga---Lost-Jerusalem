using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptTranformJoueur : MonoBehaviour
{
	[SerializeField] GameObject joueur;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform = Instantiate(joueur.transform);
		this.transform.position = joueur.transform.position;
		this.transform.rotation = joueur.transform.rotation;
        this.transform.Translate(Vector3.up * 1.25f);
    }
}
