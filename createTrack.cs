using UnityEngine;
using System.Collections;

public class createTrack : MonoBehaviour {

	// PUBLIC VARIABLES
	public GameObject[] trackPieces;
	public Vector3 spawnPos;
	public float xOffset;

	// PRIVATE VARIABLES
	private bool isSpawned = false;
	private Quaternion quaternion;

	void Start()
	{
		Instantiate (trackPieces[0], spawnPos, quaternion);
	}

	void Update()
	{
		if ((!isSpawned) && (Input.GetAxisRaw("X_Button") == 1))
		{
			// ADDS A STRAIGHT TRACK
			isSpawned = true;
			Invoke ("WaitSeconds", 0.5f);
		}
	}

	// Get Around Function for Invoke Method. (I'm Lazy)
	public void WaitSeconds()
	{
		addTrack (trackPieces [0]);
	}

	// Adds track with passed in parameter
	public void addTrack(GameObject trackPeice)
	{
		Instantiate (trackPeice, spawnPos, quaternion);

		float updatedX = spawnPos.x + xOffset;
		spawnPos.x = updatedX;

		isSpawned = false;
	}
}
