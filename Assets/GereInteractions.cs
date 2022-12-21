using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GereInteractions : MonoBehaviour
{
	[SerializeField] Transform PointInteraction;
	[SerializeField] float rayonInteraction = 0.5f;

	private Collider[] listeInteractibles = new Collider[3];
	public Transform monPointInteraction { get; private set; }

	private void Start()
	{
		monPointInteraction = PointInteraction;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Submit"))
        {
			if (Physics.OverlapSphereNonAlloc(PointInteraction.position, rayonInteraction, listeInteractibles, LayerMask.GetMask("Interactible")) > 0)
            {
				var interactible = listeInteractibles[0].GetComponent<IInteractible>();

				if (interactible != null) { interactible.interaction(this); }
            }
        }
	}
}
