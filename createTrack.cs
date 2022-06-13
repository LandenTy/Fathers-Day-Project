using UnityEngine;
using System.Collections;

public class createTrack : MonoBehaviour {

	// PUBLIC VARIABLES
	public Camera editorCamera;

	public GameObject[] trackPieces;
	public GameObject splinePoint;

	public Vector3[] positions;

	public float xOffset;

	// PRIVATE VARIABLES
	private bool isSpawned = false;
	private Quaternion quaternion;

	void Start()
	{
		Instantiate (trackPieces[0], positions[0], quaternion);
	}

	void Update()
	{
		editorCamera.transform.position = positions[1];

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
		float updatedCamX = positions[1].x += xOffset;
		float updatedX = positions[0].x += xOffset;

		positions[0].x = updatedX;
		positions[1].x = updatedCamX;

		Instantiate (trackPeice, positions[0], quaternion);

		isSpawned = false;
	}

	public void deleteTrack()
	{
		
	}
}
