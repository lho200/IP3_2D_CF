using UnityEngine;
using System.Collections;

public enum attackTypes{
	singleFire,
	multipleShots,
	spreadShot,
	superAttack
};

public class bossAttack : MonoBehaviour {

	public Rigidbody2D attackPrefab;
	attackTypes bossAttackTypes;


	Vector3 playerPos;

	public float fireDelay = 10.0f;
	public float fireSpeed = 1.0f;

	int shotCount;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//anticipate player movement
		//Should A.I know about map boundary? if so, fires below/above player
		//when their near top/bottom of boundary

		StartCoroutine (delayTimer (fireDelay));
	}

	IEnumerator delayTimer(float delayTimer){
		yield return new WaitForSeconds(delayTimer);
		spawnObject ();
	}

	void spawnObject(){
		Vector3 bossPos = transform.position;
		
		Vector3 fireSpawn;
		fireSpawn.x = bossPos.x;
		fireSpawn.y = bossPos.y - 1;
		fireSpawn.z = 0.0f;

		Rigidbody2D attackInstance = Instantiate (attackPrefab, fireSpawn, Quaternion.identity) as Rigidbody2D;
	}

	void attackTypes()
	{
		//other attack ideas:
		//projectiles stop dead in their tracks, and starting floating down (spaces inbetween each projectile)
		//heatseeker projectile (follows the player, not too precisely so its dodgable)
		//portal barriage (portal opens up releasing trees/rocks/monsters?)
		//

		switch (bossAttackTypes) {

		case global::attackTypes.singleFire:
			//allocate shot counts?
			shotCount = 1;

			break;
		case global::attackTypes.multipleShots:
			shotCount = 5;

			break;
		case global::attackTypes.spreadShot:
			//shotCount must be dividle by 3! 
			//shotCount will fire projectiles straight, diagonally up and down 
			shotCount = 15;

			break;
		case global::attackTypes.superAttack:
			shotCount = 20;

			break;
		default:

			break;

		}
	}

	//if player hasn't been hit once, boss fires multiple shots, more frequent superAttacks
	//if player's health is below 25% the boss's superAttacks are less frequent


}
