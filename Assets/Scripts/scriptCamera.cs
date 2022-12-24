using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptCamera : MonoBehaviour
{
	[SerializeField] GameObject joueur;
	[SerializeField] CharacterController cc;
	[SerializeField] GameObject transformJoueur;
	[SerializeField] private int distance = 7;
	[SerializeField] private float vitesseHorizontale;
	[SerializeField] private float vitesseVerticale;
	[SerializeField] private float _smoothTime = 0.2f;
	[SerializeField] private float rotationXInitiale;
	[SerializeField] private float clampMin = -30.0f;
	[SerializeField] private float clampMax =  30.0f;

	private Vector3 rotationActuelle;
	private Vector3 _smoothVelocity = Vector3.zero;

	private float rotationX, rotationY;

    // Start is called before the first frame update
    void Start()
    {
		this.rotationX += rotationXInitiale;

		Vector3 rotationInitiale = new Vector3(rotationX, rotationY);

		transform.localEulerAngles = rotationInitiale;

		// Substract forward vector of the GameObject to point its forward vector to the target
		transform.position = transformJoueur.transform.position - transform.forward * distance;
	}

    // Update is called once per frame
    void LateUpdate()
    {
		float x = 0, y = 0;

		if (Input.GetAxis("Joystick2Vertical") == 0 && Input.GetAxis("Joystick2Horizontal") == 0)
		{
			if (Input.GetKey(KeyCode.UpArrow))
			{
				x++;
			}
			if (Input.GetKey(KeyCode.DownArrow))
			{
				x--;
			}
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				y++;
			}
			if (Input.GetKey(KeyCode.RightArrow))
			{
				y--;
			}

			this.rotationX += x * 0.2f * vitesseVerticale;
			this.rotationY += y * 0.2f * vitesseHorizontale;
		}
		else
		{
			this.rotationX += Input.GetAxis("Joystick2Vertical") * 0.2f + vitesseVerticale;
			this.rotationY += Input.GetAxis("Joystick2Horizontal") * 0.2f * vitesseHorizontale;
		}

		rotationX = Mathf.Clamp(rotationX, clampMin, clampMax);

		Vector3 rotationSuivante = new Vector3(rotationX, rotationY);

		// Apply damping between rotation changes
		rotationActuelle = Vector3.SmoothDamp(rotationActuelle, rotationSuivante, ref _smoothVelocity, _smoothTime);
		transform.localEulerAngles = rotationActuelle;

		// Substract forward vector of the GameObject to point its forward vector to the target
		transform.position = transformJoueur.transform.position - transform.forward * distance;
	}
}
