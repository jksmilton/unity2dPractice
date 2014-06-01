using UnityEngine;
using System.Collections;

public class ZombieAnimator : MonoBehaviour {
	public Sprite[] sprites;	
	public float framesPerSecond;

	private SpriteRenderer renderOb;

	// Use this for initialization
	void Start () {
		renderOb = renderer as SpriteRenderer;
	}
	
	// Update is called once per frame
	void Update () {
		int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
		index = index % sprites.Length;
		renderOb.sprite = sprites [index];
	}




}
