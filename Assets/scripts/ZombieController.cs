using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour {
	public float moveSpeed;
	public float turnSpeed;

	[SerializeField]
	private PolygonCollider2D[] colliders;

	private int currentColliderIndex = 0;
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
		EnforceBounds ();

	}

	public void SetColliderForSprite( int spriteNum ) {
		colliders [currentColliderIndex].enabled = false;
		currentColliderIndex = spriteNum;
		colliders [currentColliderIndex].enabled = true;
	}

	
	void OnTriggerEnter2D( Collider2D other )
	{
		Debug.Log ("Hit " + other.gameObject);
	}

	private void EnforceBounds(){
		Vector3 newPosition = transform.position;
		Camera mainCamera = Camera.main;
		Vector3 cameraPosition = mainCamera.transform.position;

		float xDist = mainCamera.aspect * mainCamera.orthographicSize;
		float xMax = cameraPosition.x + xDist;
		float xMin = cameraPosition.x - xDist;
		float yMax = cameraPosition.y + mainCamera.orthographicSize;
		float yMin = cameraPosition.y - mainCamera.orthographicSize;

		if (newPosition.x < xMin || newPosition.x > xMax) {
			newPosition.x = Mathf.Clamp (newPosition.x, xMin, xMax);
			moveDirection.x = -moveDirection.x;
		}
		if (newPosition.y > yMax || newPosition.y < yMin) {
			newPosition.y = Mathf.Clamp (newPosition.y, yMin, yMax);
			moveDirection.y = -moveDirection.y;
		}
		transform.position = newPosition;
	}

}
