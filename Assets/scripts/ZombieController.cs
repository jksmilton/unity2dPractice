﻿using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour {
	public float moveSpeed;
	public float turnSpeed;

	private Vector3 moveDirection = Vector3.right;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 crntPosition = transform.position;

		if (Input.GetButton ("Fire1")) {
			Vector3 moveToward = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			moveDirection = moveToward - crntPosition;
			moveDirection.z = 0;
			moveDirection.Normalize ();
		}

		Vector3 target = moveDirection * moveSpeed + crntPosition;
		transform.position = Vector3.Lerp (crntPosition, target, Time.deltaTime);

		float targetAngle = Mathf.Atan2 (moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

		transform.rotation =
			Quaternion.Slerp (transform.rotation,
			                 Quaternion.Euler (0, 0, targetAngle),
			                 turnSpeed * Time.deltaTime);

	}
}
