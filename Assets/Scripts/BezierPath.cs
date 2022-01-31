using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierPath
{
	public int pointCount;

	private int segments;
	private LineRenderer lineRenderer;

	public BezierPath(LineRenderer renderer)
	{
		lineRenderer = renderer;
		pointCount = 100;
		lineRenderer.positionCount = pointCount;
	}

	private Vector3 BezierPathCalculation(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
	{
		float tt = t * t;
		float ttt = t * tt;
		float u = 1.0f - t;
		float uu = u * u;
		float uuu = u * uu;

		Vector3 B = new Vector3();
		B = uuu * p0;
		B += 3.0f * uu * t * p1;
		B += 3.0f * u * tt * p2;
		B += ttt * p3;

		return B;
	}

	public void CreateCurve(List<Vector3> controlPoints)
	{
		segments = controlPoints.Count / 3;

		// ³]©wLineRenderer
		for (int s = 0; s < controlPoints.Count - 3; s += 3)
		{
			Vector3 p0 = controlPoints[s];
			Vector3 p1 = controlPoints[s + 1];
			Vector3 p2 = controlPoints[s + 2];
			Vector3 p3 = controlPoints[s + 3];

			for (int p = 0; p < (pointCount / segments); p++)
			{
				float t = (1.0f / (pointCount / segments)) * p;
				Vector3 point = new Vector3();
				point = BezierPathCalculation(p0, p1, p2, p3, t);
				lineRenderer.SetPosition(p, point);
			}
		}
	}
}
