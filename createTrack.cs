using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class createTrack : MonoBehaviour {

	// PUBLIC VARIABLES
	public rideTrack rideTrack;

	public Camera editorCamera;

	public GameObject[] trackPieces;
	public GameObject splinePoint;

	public Vector3[] positions;

	public float xOffset;
	public float xDeleteOffset;
	public float camXReset;
	public int itemHeldIndex;

	// PRIVATE VARIABLES
	public List<Transform> placedTracks = new List<Transform>();
	public List<GameObject> tracks = new List<GameObject>();

	private bool isSpawned = false;
	private bool leftPadDown = false;
	private bool rightPadDown = false;
	private bool leftTriggerDown = false;
	private bool rightTriggerDown = false;
	private bool triangleDown = false;
	private bool isDeleted = true;

	private GameObject lastPlacedTrack;
	private GameObject lastPlacedPoint;

	[SerializeField] private float rotateDeg;

	void Update()
	{
		editorCamera.transform.position = positions[1];

		// Add Track
		if ((!isSpawned) && (Input.GetAxisRaw("X_Button") == 1))
		{
			isSpawned = true;
			Invoke ("WaitSeconds", 0.5f);
		}

		// Delete Tack
		if ((isDeleted) && (Input.GetAxisRaw("SQUARE_BUTTON") == 1))
		{
			isDeleted = false;
		}

		if ((!isDeleted) && (Input.GetAxisRaw("SQUARE_BUTTON") < 1))
		{
			deleteTrack();
		}

		if ((!triangleDown) && (Input.GetAxisRaw("TRIANGLE_BUTTON") == 1))
		{
			triangleDown = true;
		}

		if ((triangleDown) && (Input.GetAxisRaw("TRIANGLE_BUTTON") < 1))
		{
			rideTrack.EnterPerspectiveMode ();
			triangleDown = false;
		}

		// Rotate Track
		checkRotateTrack();

		// Switch Current Track
		checkTrackSwitch();
	}

	public void checkRotateTrack()
	{
		if ((!leftPadDown) && (Input.GetAxis("LEFT_PAD") == 1))
		{
			leftPadDown = true;
		}

		if ((!rightPadDown) && (Input.GetAxis("RIGHT_PAD") == 1))
		{
			rightPadDown = true;
		}

		if ((leftPadDown) && (Input.GetAxis("LEFT_PAD") < 1))
		{
			rotateTrack (trackPieces [itemHeldIndex], -90);
		}

		if ((rightPadDown) && (Input.GetAxis("RIGHT_PAD") < 1))
		{
			rotateTrack (trackPieces [itemHeldIndex], 90);
		}
	}

	public void checkTrackSwitch()
	{
		if ((!rightTriggerDown) && (Input.GetAxisRaw("RIGHT_TRIGGER") == 1))
		{
			rightTriggerDown = true;
			print ("RIGHT TRIGGER DOWN!");
		}

		if ((!leftTriggerDown) && (Input.GetAxisRaw("LEFT_TRIGGER") == 1))
		{
			leftTriggerDown = true;
			print ("LEFT TRIGGER DOWN!");
		}

		if ((leftTriggerDown) && (Input.GetAxis("LEFT_TRIGGER") < 1))
		{
			try 
			{
				itemHeldIndex -= 1;
				print(trackPieces[itemHeldIndex]);
			}
			catch
			{
				itemHeldIndex = 0;
			}

			leftTriggerDown = false;
		}

		if ((rightTriggerDown) && (Input.GetAxis("RIGHT_TRIGGER") < 1))
		{
			try 
			{
				itemHeldIndex += 1;
				print(trackPieces[itemHeldIndex]);
			}
			catch
			{
				itemHeldIndex = 0;
			}

			rightTriggerDown = false;
		}
	}

	// Get Around Function for Invoke Method. (I'm Lazy)
	public void WaitSeconds()
	{
		addTrack (trackPieces [itemHeldIndex]);
	}

	// Rotates passed in track, based on rotation deg variable
	// TODO: ADD OFFSET TRACK TO PEICES
	public void rotateTrack(GameObject trackPeice, float rotationDegrees)
	{
		rotateDeg += rotationDegrees;
		print (rotationDegrees);

		leftPadDown = false;
		rightPadDown = false;

		print (leftPadDown);
		print (rightPadDown);
	}

	// Adds track with passed in parameter
	public void addTrack(GameObject trackPeice)
	{
		// If track is a turn, allow rotations. Else: Don't allow rotations.
		if (trackPeice == trackPieces[1])
		{
			lastPlacedTrack = (GameObject)Instantiate (trackPeice, positions[0], transform.rotation * Quaternion.Euler (0f, rotateDeg, 0f)) as GameObject;
			lastPlacedPoint = (GameObject)Instantiate (splinePoint, positions[2], transform.rotation * Quaternion.Euler (0f, rotateDeg, 0f)) as GameObject;
		}
		else
		{
			lastPlacedTrack = (GameObject)Instantiate (trackPeice, positions[0], transform.rotation * Quaternion.identity) as GameObject;
			lastPlacedPoint = (GameObject)Instantiate (splinePoint, positions[2], transform.rotation * Quaternion.identity) as GameObject;
		}

		float updatedCamX = positions[1].x += xOffset;
		float updatedSplineX = positions[2].x += xOffset;
		float updatedX = positions[2].x += xOffset;

		if (placedTracks.Count < 4)
		{
			placedTracks.Add (lastPlacedPoint.transform);
		}
		else
		{
			placedTracks.Remove (placedTracks[3]);
			placedTracks.Add (lastPlacedPoint.transform);
		}

		positions[0].x = updatedX;
		positions[1].x = updatedCamX;
		positions[2].x = updatedSplineX;

		tracks.Add (lastPlacedTrack);

		isSpawned = false;
	}

	public void deleteTrack()
	{
		int lastTrack = (tracks.Count - 1);
		print (lastTrack);

		Destroy (tracks[lastTrack]);
		Destroy (lastPlacedPoint, 0.5f);

		float updatedCamX = positions[1].x -= camXReset;
		float updatedX = positions[2].x - xDeleteOffset;

		positions [1].x = updatedCamX;
		positions[0].x = updatedX;

		tracks.Remove (tracks[lastTrack]);

		isDeleted = true;
	}
}
