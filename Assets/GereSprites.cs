using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GereSprites : MonoBehaviour
{

	[SerializeField] scriptCamera maCamera;

	[SerializeField]
	List<Sprite> listeIdleSpriteN, listeIdleSpriteNE, listeIdleSpriteE, listeIdleSpriteSE,
								  listeIdleSpriteS, listeIdleSpriteSO, listeIdleSpriteO, listeIdleSpriteNO;
	[SerializeField]
	List<Sprite> listeWalkSpriteN, listeWalkSpriteNE, listeWalkSpriteE, listeWalkSpriteSE,
								  listeWalkSpriteS, listeWalkSpriteSO, listeWalkSpriteO, listeWalkSpriteNO;
	[SerializeField]
	List<Sprite> listeRunSpriteN, listeRunSpriteNE, listeRunSpriteE, listeRunSpriteSE,
								  listeRunSpriteS, listeRunSpriteSO, listeRunSpriteO, listeRunSpriteNO;
	[SerializeField]
	List<Sprite> listeJumpSpriteN, listeJumpSpriteNE, listeJumpSpriteE, listeJumpSpriteSE,
								  listeJumpSpriteS, listeJumpSpriteSO, listeJumpSpriteO, listeJumpSpriteNO;
	[SerializeField]
	List<Sprite> listeLandSpriteN, listeLandSpriteNE, listeLandSpriteE, listeLandSpriteSE,
								  listeLandSpriteS, listeLandSpriteSO, listeLandSpriteO, listeLandSpriteNO;
	[SerializeField]
	List<Sprite> listeClimbIdleSpriteN, listeClimbIdleSpriteNE, listeClimbIdleSpriteE, listeClimbIdleSpriteSE,
								  listeClimbIdleSpriteS, listeClimbIdleSpriteSO, listeClimbIdleSpriteO, listeClimbIdleSpriteNO;
	[SerializeField]
	List<Sprite> listeClimbUpSpriteN, listeClimbUpSpriteNE, listeClimbUpSpriteE, listeClimbUpSpriteSE,
								  listeClimbUpSpriteS, listeClimbUpSpriteSO, listeClimbUpSpriteO, listeClimbUpSpriteNO;
	[SerializeField]
	List<Sprite> listeClimbDownSpriteN, listeClimbDownSpriteNE, listeClimbDownSpriteE, listeClimbDownSpriteSE,
								  listeClimbDownSpriteS, listeClimbDownSpriteSO, listeClimbDownSpriteO, listeClimbDownSpriteNO;
	[SerializeField]
	List<Sprite> listeClimbRightSpriteN, listeClimbRightSpriteNE, listeClimbRightSpriteE, listeClimbRightSpriteSE,
								  listeClimbRightSpriteS, listeClimbRightSpriteSO, listeClimbRightSpriteO, listeClimbRightSpriteNO;
	[SerializeField]
	List<Sprite> listeClimbLeftSpriteN, listeClimbLeftSpriteNE, listeClimbLeftSpriteE, listeClimbLeftSpriteSE,
								  listeClimbLeftSpriteS, listeClimbLeftSpriteSO, listeClimbLeftSpriteO, listeClimbLeftSpriteNO;


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public List<Sprite> getIdleSpriteDirection()
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

	public List<Sprite> getWalkSpriteDirection()
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

	public List<Sprite> getRunSpriteDirection()
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

	public List<Sprite> getJumpSpriteDirection()
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

	public List<Sprite> getLandSpriteDirection()
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

	public List<Sprite> getClimbIdleSpriteDirection()
	{
		List<Sprite> listeRet = null;
		float compareRotations = ((this.transform.rotation.eulerAngles.y - this.maCamera.transform.rotation.eulerAngles.y) % 360 + 360) % 360;

		if (compareRotations > 337 || compareRotations <= 22) // N : centre = 0 | 360
		{
			listeRet = listeClimbIdleSpriteN;
		}

		if (compareRotations > 22 && compareRotations <= 67) // NE : centre = 45
		{
			listeRet = listeClimbIdleSpriteNE;
		}

		if (compareRotations > 67 && compareRotations <= 112) // E : centre = 90
		{
			listeRet = listeClimbIdleSpriteE;
		}

		if (compareRotations > 112 && compareRotations <= 157) // SE : centre = 135
		{
			listeRet = listeClimbIdleSpriteSE;
		}

		if (compareRotations > 157 && compareRotations <= 202) // S : centre = 180
		{
			listeRet = listeClimbIdleSpriteS;
		}

		if (compareRotations > 202 && compareRotations <= 247) // SO : centre = 225
		{
			listeRet = listeClimbIdleSpriteSO;
		}

		if (compareRotations > 247 && compareRotations <= 292) // O : centre = 270
		{
			listeRet = listeClimbIdleSpriteO;
		}

		if (compareRotations > 292 && compareRotations <= 337) // NO : centre = 315
		{
			listeRet = listeClimbIdleSpriteNO;
		}

		return listeRet;
	}

	public List<Sprite> getClimbUpSpriteDirection()
	{
		List<Sprite> listeRet = null;
		float compareRotations = ((this.transform.rotation.eulerAngles.y - this.maCamera.transform.rotation.eulerAngles.y) % 360 + 360) % 360;

		if (compareRotations > 337 || compareRotations <= 22) // N : centre = 0 | 360
		{
			listeRet = listeClimbUpSpriteN;
		}

		if (compareRotations > 22 && compareRotations <= 67) // NE : centre = 45
		{
			listeRet = listeClimbUpSpriteNE;
		}

		if (compareRotations > 67 && compareRotations <= 112) // E : centre = 90
		{
			listeRet = listeClimbUpSpriteE;
		}

		if (compareRotations > 112 && compareRotations <= 157) // SE : centre = 135
		{
			listeRet = listeClimbUpSpriteSE;
		}

		if (compareRotations > 157 && compareRotations <= 202) // S : centre = 180
		{
			listeRet = listeClimbUpSpriteS;
		}

		if (compareRotations > 202 && compareRotations <= 247) // SO : centre = 225
		{
			listeRet = listeClimbUpSpriteSO;
		}

		if (compareRotations > 247 && compareRotations <= 292) // O : centre = 270
		{
			listeRet = listeClimbUpSpriteO;
		}

		if (compareRotations > 292 && compareRotations <= 337) // NO : centre = 315
		{
			listeRet = listeClimbUpSpriteNO;
		}

		return listeRet;
	}

	public List<Sprite> getClimbDownSpriteDirection()
	{
		List<Sprite> listeRet = null;
		float compareRotations = ((this.transform.rotation.eulerAngles.y - this.maCamera.transform.rotation.eulerAngles.y) % 360 + 360) % 360;

		if (compareRotations > 337 || compareRotations <= 22) // N : centre = 0 | 360
		{
			listeRet = listeClimbDownSpriteN;
		}

		if (compareRotations > 22 && compareRotations <= 67) // NE : centre = 45
		{
			listeRet = listeClimbDownSpriteNE;
		}

		if (compareRotations > 67 && compareRotations <= 112) // E : centre = 90
		{
			listeRet = listeClimbDownSpriteE;
		}

		if (compareRotations > 112 && compareRotations <= 157) // SE : centre = 135
		{
			listeRet = listeClimbDownSpriteSE;
		}

		if (compareRotations > 157 && compareRotations <= 202) // S : centre = 180
		{
			listeRet = listeClimbDownSpriteS;
		}

		if (compareRotations > 202 && compareRotations <= 247) // SO : centre = 225
		{
			listeRet = listeClimbDownSpriteSO;
		}

		if (compareRotations > 247 && compareRotations <= 292) // O : centre = 270
		{
			listeRet = listeClimbDownSpriteO;
		}

		if (compareRotations > 292 && compareRotations <= 337) // NO : centre = 315
		{
			listeRet = listeClimbDownSpriteNO;
		}

		return listeRet;
	}

	public List<Sprite> getClimbRightSpriteDirection()
	{
		List<Sprite> listeRet = null;
		float compareRotations = ((this.transform.rotation.eulerAngles.y - this.maCamera.transform.rotation.eulerAngles.y) % 360 + 360) % 360;

		if (compareRotations > 337 || compareRotations <= 22) // N : centre = 0 | 360
		{
			listeRet = listeClimbRightSpriteN;
		}

		if (compareRotations > 22 && compareRotations <= 67) // NE : centre = 45
		{
			listeRet = listeClimbRightSpriteNE;
		}

		if (compareRotations > 67 && compareRotations <= 112) // E : centre = 90
		{
			listeRet = listeClimbRightSpriteE;
		}

		if (compareRotations > 112 && compareRotations <= 157) // SE : centre = 135
		{
			listeRet = listeClimbRightSpriteSE;
		}

		if (compareRotations > 157 && compareRotations <= 202) // S : centre = 180
		{
			listeRet = listeClimbRightSpriteS;
		}

		if (compareRotations > 202 && compareRotations <= 247) // SO : centre = 225
		{
			listeRet = listeClimbRightSpriteSO;
		}

		if (compareRotations > 247 && compareRotations <= 292) // O : centre = 270
		{
			listeRet = listeClimbRightSpriteO;
		}

		if (compareRotations > 292 && compareRotations <= 337) // NO : centre = 315
		{
			listeRet = listeClimbRightSpriteNO;
		}

		return listeRet;
	}

	public List<Sprite> getClimbLeftSpriteDirection()
	{
		List<Sprite> listeRet = null;
		float compareRotations = ((this.transform.rotation.eulerAngles.y - this.maCamera.transform.rotation.eulerAngles.y) % 360 + 360) % 360;

		if (compareRotations > 337 || compareRotations <= 22) // N : centre = 0 | 360
		{
			listeRet = listeClimbLeftSpriteN;
		}

		if (compareRotations > 22 && compareRotations <= 67) // NE : centre = 45
		{
			listeRet = listeClimbLeftSpriteNE;
		}

		if (compareRotations > 67 && compareRotations <= 112) // E : centre = 90
		{
			listeRet = listeClimbLeftSpriteE;
		}

		if (compareRotations > 112 && compareRotations <= 157) // SE : centre = 135
		{
			listeRet = listeClimbLeftSpriteSE;
		}

		if (compareRotations > 157 && compareRotations <= 202) // S : centre = 180
		{
			listeRet = listeClimbLeftSpriteS;
		}

		if (compareRotations > 202 && compareRotations <= 247) // SO : centre = 225
		{
			listeRet = listeClimbLeftSpriteSO;
		}

		if (compareRotations > 247 && compareRotations <= 292) // O : centre = 270
		{
			listeRet = listeClimbLeftSpriteO;
		}

		if (compareRotations > 292 && compareRotations <= 337) // NO : centre = 315
		{
			listeRet = listeClimbLeftSpriteNO;
		}

		return listeRet;
	}
}
