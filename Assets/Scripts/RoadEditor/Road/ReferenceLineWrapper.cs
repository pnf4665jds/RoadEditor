using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceLineWrapper : MonoBehaviour
{
	// 這個class用來處理ReferenceLine，主要需要提供一個function取得參考線的位置，便於產生車道，這邊暫時寫一個bezier curve的function當作參考線
	public GameObject controlPoint1;
	public GameObject controlPoint2;
	public GameObject controlPoint3;
	public GameObject controlPoint4;
	public LineRenderer lineRenderer;

    private void Start()
    {
		lineRenderer.positionCount = 100;
	}

    /// <summary>
    /// 根據給定的t值取得referenceline位置
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public Vector3 GetReferenceLinePos(float t)
	{
		float tt = t * t;
		float ttt = t * tt;
		float u = 1.0f - t;
		float uu = u * u;
		float uuu = u * uu;

		Vector3 B = new Vector3();
		B = uuu * controlPoint1.transform.localPosition;
		B += 3.0f * uu * t * controlPoint2.transform.localPosition;
		B += 3.0f * u * tt * controlPoint3.transform.localPosition;
		B += ttt * controlPoint4.transform.localPosition;

		return B;
	}

    private void Update()
    {
		for (int p = 0; p < 100; p++)
		{
			float t = (1.0f / 100) * p;
			Vector3 point = new Vector3();
			point = GetReferenceLinePos(t);
			lineRenderer.SetPosition(p, point);
		}
	}
}
