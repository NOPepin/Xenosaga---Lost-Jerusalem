using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	[SerializeField] CharacterController cc;
	[SerializeField] scriptCamera maCamera;
	[SerializeField] SpriteRenderer sprite;
	[SerializeField] private float vitesseMarche = 0.3f, vitesseRun = 0.6f, animationFrameRate;
	[SerializeField] private int nbLandingFrames;

	[SerializeField] List<Sprite> listeIdleSpriteN, listeIdleSpriteNE, listeIdleSpriteE, listeIdleSpriteSE, 
								  listeIdleSpriteS, listeIdleSpriteSO, listeIdleSpriteO, listeIdleSpriteNO;
	[SerializeField] List<Sprite> listeWalkSpriteN, listeWalkSpriteNE, listeWalkSpriteE, listeWalkSpriteSE,
								  listeWalkSpriteS, listeWalkSpriteSO, listeWalkSpriteO, listeWalkSpriteNO;
	[SerializeField] List<Sprite> listeRunSpriteN, listeRunSpriteNE, listeRunSpriteE, listeRunSpriteSE,
								  listeRunSpriteS, listeRunSpriteSO, listeRunSpriteO, listeRunSpriteNO;
	[SerializeField] List<Sprite> listeJumpSpriteN, listeJumpSpriteNE, listeJumpSpriteE, listeJumpSpriteSE,
								  listeJumpSpriteS, listeJumpSpriteSO, listeJumpSpriteO, listeJumpSpriteNO;
	[SerializeField] List<Sprite> listeLandSpriteN, listeLandSpriteNE, listeLandSpriteE, listeLandSpriteSE,
								  listeLandSpriteS, listeLandSpriteSO, listeLandSpriteO, listeLandSpriteNO;
	[SerializeField] List<Sprite> listeClimbSpriteN, listeClimbSpriteNE, listeClimbSpriteE, listeClimbSpriteSE,
								  listeClimbSpriteS, listeClimbSpriteSO, listeClimbSpriteO, listeClimbSpriteNO;

	private Vector3 playerVelocity, direction;
	private bool isRunning = false, isGrounded, wasGrounded = false, isLanding = false, isClimbing = false;
	private float jumpHeight = 1.0f;
	private float gravityValue = -9.81f;
	
	private float animationStartTime = 0f;
	private string currentAnimationName = "Idle";

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

			this.transform.rotation = directionRotation;
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
			cc.Move(deplacement * vitesse);
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

		if (!Physics.CheckSphere(this.transform.position, 1.1f, LayerMask.GetMask("Echelle")))
		{
			this.isClimbing = false;
			return;
		}


	}

	/********************
	 * Animation Sprite *
	 ********************/

	private List<Sprite> getIdleSpriteDirection()
	{
		List<Sprite> listeRet = null;                                                                          // j'ajoute 360 pour éviter d'avoir un nombre négatif
		int compareRotations = (int)(this.transform.rotation.eulerAngles.y - this.maCamera.transform.rotation.eulerAngles.y + 360) % 360;

		if (compareRotations > 337 || compareRotations <= 22) // N : centre = 0 | 360
		{
			listeRet = listeIdleSpriteN;
		}

		if (compareRotations > 22 && compareRotations <= 67) // NE : centre = 45
		{
			listeRet = listeIdleSpriteNE;
		}

		if (compareRotations > 67 && compareRotations <= 112) // E : centre = 90
		{
			listeRet = listeIdleSpriteE;
		}

		if (compareRotations > 112 && compareRotations <= 157) // SE : centre = 135
		{
			listeRet = listeIdleSpriteSE;
		}

		if (compareRotations > 157 && compareRotations <= 202) // S : centre = 180
		{
			listeRet = listeIdleSpriteS;
		}

		if (compareRotations > 202 && compareRotations <= 247) // SO : centre = 225
		{
			listeRet = listeIdleSpriteSO;
		}

		if (compareRotations > 247 && compareRotations <= 292) // O : centre = 270
		{
			listeRet = listeIdleSpriteO;
		}

		if (compareRotations > 292 && compareRotations <= 337) // NO : centre = 315
		{
			listeRet = listeIdleSpriteNO;
		}

		return listeRet;
	}

	private List<Sprite> getWalkSpriteDirection()
	{
		List<Sprite> listeRet = null;
		int compareRotations = (int)(this.transform.rotation.eulerAngles.y - this.maCamera.transform.rotation.eulerAngles.y + 360) % 360;

		if (compareRotations > 337 || compareRotations <= 22) // N : centre = 0 | 360
		{
			listeRet = listeWalkSpriteN;
		}

		if (compareRotations > 22 && compareRotations <= 67) // NE : centre = 45
		{
			listeRet = listeWalkSpriteNE;
		}

		if (compareRotations > 67 && compareRotations <= 112) // E : centre = 90
		{
			listeRet = listeWalkSpriteE;
		}

		if (compareRotations > 112 && compareRotations <= 157) // SE : centre = 135
		{
			listeRet = listeWalkSpriteSE;
		}

		if (compareRotations > 157 && compareRotations <= 202) // S : centre = 180
		{
			listeRet = listeWalkSpriteS;
		}

		if (compareRotations > 202 && compareRotations <= 247) // SO : centre = 225
		{
			listeRet = listeWalkSpriteSO;
		}

		if (compareRotations > 247 && compareRotations <= 292) // O : centre = 270
		{
			listeRet = listeWalkSpriteO;
		}

		if (compareRotations > 292 && compareRotations <= 337) // NO : centre = 315
		{
			listeRet = listeWalkSpriteNO;
		}

		return listeRet;
	}

	private List<Sprite> getRunSpriteDirection()
	{
		List<Sprite> listeRet = null;
		int compareRotations = (int)(this.transform.rotation.eulerAngles.y - this.maCamera.transform.rotation.eulerAngles.y + 360) % 360;

		if (compareRotations > 337 || compareRotations <= 22) // N : centre = 0 | 360
		{
			listeRet = listeRunSpriteN;
		}

		if (compareRotations > 22 && compareRotations <= 67) // NE : centre = 45
		{
			listeRet = listeRunSpriteNE;
		}

		if (compareRotations > 67 && compareRotations <= 112) // E : centre = 90
		{
			listeRet = listeRunSpriteE;
		}

		if (compareRotations > 112 && compareRotations <= 157) // SE : centre = 135
		{
			listeRet = listeRunSpriteSE;
		}

		if (compareRotations > 157 && compareRotations <= 202) // S : centre = 180
		{
			listeRet = listeRunSpriteS;
		}

		if (compareRotations > 202 && compareRotations <= 247) // SO : centre = 225
		{
			listeRet = listeRunSpriteSO;
		}

		if (compareRotations > 247 && compareRotations <= 292) // O : centre = 270
		{
			listeRet = listeRunSpriteO;
		}

		if (compareRotations > 292 && compareRotations <= 337) // NO : centre = 315
		{
			listeRet = listeRunSpriteNO;
		}

		return listeRet;
	}

	private List<Sprite> getJumpSpriteDirection()
	{
		List<Sprite> listeRet = null;
		int compareRotations = (int)(this.transform.rotation.eulerAngles.y - this.maCamera.transform.rotation.eulerAngles.y + 360) % 360;

		if (compareRotations > 337 || compareRotations <= 22) // N : centre = 0 | 360
		{
			listeRet = listeJumpSpriteN;
		}

		if (compareRotations > 22 && compareRotations <= 67) // NE : centre = 45
		{
			listeRet = listeJumpSpriteNE;
		}

		if (compareRotations > 67 && compareRotations <= 112) // E : centre = 90
		{
			listeRet = listeJumpSpriteE;
		}

		if (compareRotations > 112 && compareRotations <= 157) // SE : centre = 135
		{
			listeRet = listeJumpSpriteSE;
		}

		if (compareRotations > 157 && compareRotations <= 202) // S : centre = 180
		{
			listeRet = listeJumpSpriteS;
		}

		if (compareRotations > 202 && compareRotations <= 247) // SO : centre = 225
		{
			listeRet = listeJumpSpriteSO;
		}

		if (compareRotations > 247 && compareRotations <= 292) // O : centre = 270
		{
			listeRet = listeJumpSpriteO;
		}

		if (compareRotations > 292 && compareRotations <= 337) // NO : centre = 315
		{
			listeRet = listeJumpSpriteNO;
		}

		return listeRet;
	}

	private List<Sprite> getLandSpriteDirection()
	{
		List<Sprite> listeRet = null;
		float compareRotations = ((this.transform.rotation.eulerAngles.y - this.maCamera.transform.rotation.eulerAngles.y) % 360 + 360) % 360;

		if (compareRotations > 337 || compareRotations <= 22) // N : centre = 0 | 360
		{
			listeRet = listeLandSpriteN;
		}

		if (compareRotations > 22 && compareRotations <= 67) // NE : centre = 45
		{
			listeRet = listeLandSpriteNE;
		}

		if (compareRotations > 67 && compareRotations <= 112) // E : centre = 90
		{
			listeRet = listeLandSpriteE;
		}

		if (compareRotations > 112 && compareRotations <= 157) // SE : centre = 135
		{
			listeRet = listeLandSpriteSE;
		}

		if (compareRotations > 157 && compareRotations <= 202) // S : centre = 180
		{
			listeRet = listeLandSpriteS;
		}

		if (compareRotations > 202 && compareRotations <= 247) // SO : centre = 225
		{
			listeRet = listeLandSpriteSO;
		}

		if (compareRotations > 247 && compareRotations <= 292) // O : centre = 270
		{
			listeRet = listeLandSpriteO;
		}

		if (compareRotations > 292 && compareRotations <= 337) // NO : centre = 315
		{
			listeRet = listeLandSpriteNO;
		}

		return listeRet;
	}

	void gereAnimation()
	{
		List<Sprite> spriteDirection;
		string animationName;
		float animationTime;

		if (this.isGrounded)
		{
			if(this.isLanding || currentAnimationName == "Land")
			{
				spriteDirection = getLandSpriteDirection();
				animationName = "Land";
			}
			else
			{
				if (direction != Vector3.zero)
				{
					if (isRunning)
					{
						spriteDirection = getRunSpriteDirection();
						animationName = "Run";
					}
					else
					{
						spriteDirection = getWalkSpriteDirection();
						animationName = "Walk";
					}
				}
				else
				{
					spriteDirection = getIdleSpriteDirection();
					animationName = "Idle";
				}
			}
		}
		else
		{
			spriteDirection = getJumpSpriteDirection();
			animationName = "Jump";
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

		int nbFrame = (int)(animationTime * animationFrameRate); // le nombre total de frames qui ont été passées au cours de cette animation
		int frame = nbFrame % spriteDirection.Count;

		sprite.sprite = spriteDirection[frame];

		if(currentAnimationName == "Land" && nbFrame >= nbLandingFrames)
		{
			currentAnimationName = "";
			this.isLanding = false;
		}
	}
}
