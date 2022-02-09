using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
	public class Width
	{
		public float sOffset;
		public float a = 1;
		public float b;
		public float c;
		public float d;
	}

	// 這個Lane屬於哪個Road
	public Road parentRoad;

	public RoadRenderer roadRenderer;

	public Width laneWidth;

    private void Awake()
    {
		laneWidth = new Width();
    }

    public void DrawLane(ReferenceLineWrapper wrapper, bool rightLane, float widthOffset)
    {
		Vector3[] points = new Vector3[100];
		wrapper.lineRenderer.GetPositions(points);
		roadRenderer.roadWidth = laneWidth.a;
		roadRenderer.UpdateRoad(points, rightLane, widthOffset);
    }

	public void SetVisibility(bool visible)
	{
		roadRenderer.SetVisibility(visible);
	}
}
