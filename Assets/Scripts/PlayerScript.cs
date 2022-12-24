using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	[SerializeField] CharacterController cc;
	[SerializeField] Transform origineRayCast;
	[SerializeField] scriptCamera maCamera;
	[SerializeField] GereSprites gestionSprites;
	[SerializeField] SpriteRenderer sprite;
	[SerializeField] private float vitesseMarche = 0.3f, vitesseRun = 0.6f, vitesseGrimpe, animationFrameRate;
	[SerializeField] private int nbLandingFrames;

	private Vector3 playerVelocity, direction;
	private bool isRunning = false, isGrounded, wasGrounded = false, isLanding = false, isClimbing = false;
	private float jumpHeight = 1.0f;
	private float gravityValue = -9.81f;

	private RaycastHit pointGrimpe;

	private float animationStartTime = 0f;
	private string currentAnimationName = "Idle", climbingDirection = "Idle";

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		this.isGrounded = Physics.CheckSphere(this.transform.position, 1.1f, LayerMask.GetMask("Sol"));

		if (!DialogueManager.GetInstance().cutsceneIsPlaying)
		{
			// on v�rifie si le joueur veux commencer � grimper
			if (!this.isClimbing && Input.GetButtonDown("Submit") && Physics.Raycast(this.origineRayCast.position, this.origineRayCast.forward, out this.pointGrimpe, 2f, LayerMask.GetMask("Echelle")))
			{
				Debug.DrawRay(this.origineRayCast.position, this.origineRayCast.forward, Color.red, 2f);
				this.isClimbing = true;
			}

			// on s'occupe du mouvement
			if(!this.isClimbing)
			{
				this.Mouvement();

				if (!this.isGrounded)
				{
					this.wasGrounded = false;
					this.isLanding = false;
				}

				if (this.isGrounded && playerVelocity.y < 0)
				{
					playerVelocity.y = 0f;
				}

				if (this.isGrounded && !this.wasGrounded)
				{
					this.wasGrounded = true;
					this.isLanding = true;
				}

				if (Input.GetButtonDown("Jump") && this.isGrounded) { this.Saut(); }

				transform.rotation.eulerAngles.Set(0, transform.rotation.eulerAngles.y, 0);

				playerVelocity.y += gravityValue * Time.deltaTime;
				cc.Move(playerVelocity * Time.deltaTime);
			}
			else
			{
				this.Grimpe();
			}
		}
		else
		{
			this.direction = Vector3.zero;
		}

		gereAnimation();
	}

	void Mouvement()
	{
		Vector3 cameraForeward, cameraRight;

		float horizontal	= Input.GetAxis("Horizontal");
		float vertical		= Input.GetAxis("Vertical");
		float vitesse;

		cameraForeward		= maCamera.transform.forward;
		cameraRight			= maCamera.transform.right;

		Vector3 deplacement = (cameraForeward * vertical) + (cameraRight * horizontal);
		deplacement.y		= 0;

		direction = deplacement.normalized;

		if(direction != Vector3.zero)
		{
			Quaternion directionRotation = Quaternion.LookRotation(direction, Vector3.up);

			this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, directionRotation, 1.5f);
		}

		isRunning = !Input.GetKey(KeyCode.LeftShift);

		if (isRunning)
		{
			vitesse = vitesseRun;
		}
		else
		{
			vitesse = vitesseMarche;
		}

		if (deplacement != Vector3.zero)
		{
			cc.Move(deplacement * vitesse * Time.deltaTime);
		}
	}

	void Saut()
	{
		playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
		this.isGrounded = false;
	}

	void Grimpe()
	{
		if(Input.GetButtonDown("Jump"))
		{
			this.isClimbing = false;
			this.Saut();
			return;
		}

		if (!Physics.Raycast(this.origineRayCast.position, this.origineRayCast.forward, out this.pointGrimpe, 2f, LayerMask.GetMask("Echelle")) || Input.GetButtonDown("Cancel"))
		{
			this.isClimbing = false;
			return;
		}

		this.transform.forward = Vector3.Reflect(this.pointGrimpe.normal, this.pointGrimpe.normal);

		/*Vector3 placementInitialGrimpe = this.pointGrimpe.point;
		placementInitialGrimpe -= this.transform.forward.normalized;
		this.transform.position = placementInitialGrimpe;*/

		this.transform.position = this.pointGrimpe.point;


		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		if((horizontal > vertical && vertical >= 0) || (horizontal < vertical && vertical <= 0))
		{
			vertical = 0;
		}
		else
		{
			horizontal = 0;
		}

		Vector3 deplacement = (this.transform.up * vertical) + (this.transform.right * horizontal);

		this.cc.Move(deplacement * vitesseGrimpe * Time.deltaTime);

		if(vertical > 0)
		{
			this.climbingDirection = "Haut";
		}

		if (vertical < 0)
		{
			this.climbingDirection = "Bas";
		}

		if (horizontal > 0)
		{
			this.climbingDirection = "Droite";
		}

		if (horizontal < 0)
		{
			this.climbingDirection = "Gauche";
		}

		if (vertical == 0 && horizontal == 0)
		{
			this.climbingDirection = "Idle";
		}
	}

	/********************
	 * Animation Sprite *
	 ********************/

	void gereAnimation()
	{
		List<Sprite> spriteDirection;
		string animationName;
		float animationTime;

		if (!this.isClimbing)
		{
			if (this.isGrounded)
			{
				if (this.isLanding || currentAnimationName == "Land")
				{
					spriteDirection = this.gestionSprites.getLandSpriteDirection();
					animationName = "Land";
				}
				else
				{
					if (direction != Vector3.zero)
					{
						if (isRunning)
						{
							spriteDirection = this.gestionSprites.getRunSpriteDirection();
							animationName = "Run";
						}
						else
						{
							spriteDirection = this.gestionSprites.getWalkSpriteDirection();
							animationName = "Walk";
						}
					}
					else
					{
						spriteDirection = this.gestionSprites.getIdleSpriteDirection();
						animationName = "Idle";
					}
				}
			}
			else
			{
				spriteDirection = this.gestionSprites.getJumpSpriteDirection();
				animationName = "Jump";
			}
		}
		else
		{
			animationName = "Grimpe" + this.climbingDirection;

			switch(this.climbingDirection)
			{
				case "Haut":
					spriteDirection = this.gestionSprites.getClimbUpSpriteDirection();
					break;
				case "Bas":
					spriteDirection = this.gestionSprites.getClimbDownSpriteDirection();
					break;
				case "Droite":
					spriteDirection = this.gestionSprites.getClimbRightSpriteDirection();
					break;
				case "Gauche":
					spriteDirection = this.gestionSprites.getClimbLeftSpriteDirection();
					break;
				case "Idle":
					spriteDirection = this.gestionSprites.getClimbIdleSpriteDirection();
					break;
				default:
					spriteDirection = this.gestionSprites.getClimbIdleSpriteDirection();
				break;
			}
		}
		

		if (animationName != currentAnimationName)
		{
			currentAnimationName = animationName;
			animationStartTime = Time.time;
			animationTime = 0;
		}
		else
		{
			animationTime = Time.time - animationStartTime;
		}

		int nbFrame = (int)(animationTime * animationFrameRate); // le nombre total de frames qui ont �t� pass�es au cours de cette animation
		int frame = nbFrame % spriteDirection.Count;

		sprite.sprite = spriteDirection[frame];

		if(currentAnimationName == "Land" && nbFrame >= nbLandingFrames)
		{
			currentAnimationName = "";
			this.isLanding = false;
		}
	}
}