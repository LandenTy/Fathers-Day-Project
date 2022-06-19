using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class rideTrack : MonoBehaviour {

	[SerializeField] private List<Transform> points = new List<Transform>();
	[SerializeField] private Transform Train;
	public float interpolateAmount;
	public bool StartTrain;

	public createTrack createTrack;

	void Start()
	{
		Train.gameObject.SetActive (false);
	}

	void Update()
	{
		// Update Lists
		points = createTrack.placedTracks;

		interpolateAmount = (interpolateAmount + Time.deltaTime) % 1f;

		if ((Input.GetAxis("TRIANGLE_BUTTON") == 1) || (Input.GetKeyDown(KeyCode.Space)))
		{
			print ("ENTER PERSPECTIVE OF TRAIN");
			EnterPerspectiveMode ();
		}

		if ((StartTrain) && (points.Count == 4))
		{
			Train.gameObject.SetActive (true);
			Train.position = CubicLerp (points[0].position, points[1].position, points[2].position, points[3].position, interpolateAmount);
		}
	}

	private Vector3 QuadraticLerp(Vector3 a, Vector3 b, Vector3 c, float t)
	{
		Vector3 AB = Vector3.Lerp (a, b, t);
		Vector3 BC = Vector3.Lerp (b, c, t);

		return Vector3.Lerp (AB, BC, interpolateAmount);
	}

	private Vector3 CubicLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
	{
		Vector3 AB_BC = QuadraticLerp (a, b, c, t);
		Vector3 BC_CD = QuadraticLerp (b, c, d, t);

		return Vector3.Lerp (AB_BC, BC_CD, interpolateAmount);
	}

	public void EnterPerspectiveMode()
	{
		StartTrain = !StartTrain;

		if (StartTrain)
		{
			Train.gameObject.SetActive (true);
		}
		else
		{
			Train.gameObject.SetActive (false);
		}
	}
}
