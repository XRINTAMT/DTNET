using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
	public int iterations = 5;
	public float gravity = 10;
	public float damping = 0.7f;
	public PathAutoEndPoints pathInfo;
	public int numPoints = 20;
	public float meshThickness = 1;
	public MeshFilter meshFilter;
	public int cylinderResolution = 5;

	float pathLength;
	float pointSpacing;
	Vector3[] points;
	Vector3[] pointsOld;
	Vector3 LastOrigin;
	Vector3 LastTarget;

	Mesh mesh;
	bool pinStart = true;
	bool pinEnd = true;
	[SerializeField] float floorOffset;
	[SerializeField] static float refreshDistance = 0.01f;
	[SerializeField] static float refreshTimeout = 2.5f;
	float refreshProgress;

	void Start()
	{
		refreshProgress = 5f;
		LastTarget = Vector3.zero;
		LastOrigin = Vector3.zero;
		points = new Vector3[numPoints];
		pointsOld = new Vector3[numPoints];

		for (int i = 0; i < numPoints; i++)
		{
			float t = i / (numPoints - 1f);
			points[i] = pathInfo.pathCreator.path.GetPointAtTime(t, PathCreation.EndOfPathInstruction.Stop);
			pointsOld[i] = points[i];
		}

		for (int i = 0; i < numPoints - 1; i++)
		{
			pathLength += Vector3.Distance(points[i], points[i + 1]);
		}
		pointSpacing = pathLength / points.Length;
	}

	void LateUpdate()
	{
		if(refreshProgress > 0)
        {
			CylinderGenerator.CreateMesh(ref mesh, points, cylinderResolution, meshThickness);
			meshFilter.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity); // mesh is in worldspace
			meshFilter.mesh = mesh;
			LastOrigin = pathInfo.origin.position;
			LastTarget = pathInfo.target.position;
			refreshProgress -= Time.deltaTime;
		}
        else
        {
			if (((pathInfo.origin.position - LastOrigin).magnitude > refreshDistance) || ((pathInfo.target.position - LastTarget).magnitude > refreshDistance))
			{
				CylinderGenerator.CreateMesh(ref mesh, points, cylinderResolution, meshThickness);
				meshFilter.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity); // mesh is in worldspace
				meshFilter.mesh = mesh;
				LastOrigin = pathInfo.origin.position;
				LastTarget = pathInfo.target.position;
				refreshProgress = refreshTimeout;
			}
		}
		
	}


	void FixedUpdate()
	{
		if (refreshProgress <= 0)
		{
			return;
		}
		points[0] = pathInfo.origin.position;
        points[points.Length - 1] = pathInfo.target.position;
        for (int i = 0; i < points.Length; i++)
        {
            bool pinned = (i == 0 && pinStart) || (i == points.Length - 1 && pinEnd);
            if (!pinned)
            {
                Vector3 curr = points[i];
                points[i] = points[i] + (points[i] - pointsOld[i]) * damping + Vector3.down * gravity * Time.deltaTime * Time.deltaTime;
                pointsOld[i] = curr;
            }
        }
        for (int i = 0; i < iterations; i++)
        {
            ConstrainCollisions();
            ConstrainConnections();
        }
    }

    void ConstrainConnections()
	{
		for (int i = 0; i < points.Length - 1; i++)
		{
			Vector3 centre = (points[i] + points[i + 1]) / 2;
			Vector3 offset = points[i] - points[i + 1];
			float length = offset.magnitude;
			Vector3 dir = offset / length;

			//if (length > pointSpacing || length < pointSpacing * 0.5f)
			{
				//float desiredLength = Mathf.Min(length, pointSpacing);
				//desiredLength = Mathf.Lerp(desiredLength, pointSpacing, 0.25f * Time.deltaTime);
				if (i != 0 || !pinStart)
				{
					points[i] = centre + dir * pointSpacing / 2;
				}
				if (i + 1 != points.Length - 1 || !pinEnd)
				{
					points[i + 1] = centre - dir * pointSpacing / 2;
				}
			}
		}
	}

	void ConstrainCollisions()
	{

		for (int i = 0; i < points.Length; i++)
		{
			bool pinned = i == 0 || i == points.Length - 1;
			if (!pinned)
			{
				if (points[i].y < (meshThickness / 2) + floorOffset)
				{
					points[i].y = (meshThickness / 2) + floorOffset;
				}
			}
		}
	}

	void OnDrawGizmos()
	{
		if (points != null)
		{
			for (int i = 0; i < points.Length; i++)
			{
				Gizmos.DrawSphere(points[i], 0.05f);
			}
		}
	}
}
