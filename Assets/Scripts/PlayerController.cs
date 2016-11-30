﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float					life = 1500;
	public float					speed = 5f;
	public Vector2					maxSpeed = Vector2.one * 8;
	public Vector2					spawnOffset =  Vector2.one;
	public PolygonSpawnPattern		projectileSpawnPattern;
	public const string				playerBulletTag = "PlayerBullets";
	public const string				playerTag = "Player";

	// Use this for initialization
	void Start () {
		tag = playerTag;
		projectileSpawnPattern.attachedGameObject = gameObject;
		projectileSpawnPattern.maxObjects = -1;
	}
	
	// Update is called once per frame
	void Update () {
		projectileSpawnPattern.InstanciateFramePolygons(playerBulletTag);
	}

	void FixedUpdate() {
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		input.x = Mathf.Clamp(input.x, -maxSpeed.x, maxSpeed.x);
		input.y = Mathf.Clamp(input.y, -maxSpeed.y, maxSpeed.y);

		transform.position += (Vector3)input.normalized * Time.fixedDeltaTime * speed;
		// rbody.AddForce((Vector3)input.normalized * Time.fixedDeltaTime * speed);

		Vector3 mouseDiff = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
		float z = Mathf.Atan2(mouseDiff.x, mouseDiff.y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, -z);
	}

	void OnTriggerStay2D(Collider2D c)
	{
		if (c.tag != playerBulletTag)
			Debug.Log("player hitted !");
		if (c.tag != "Map")
			life -= 10;
	}
}
