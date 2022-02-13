using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ReferenceLineWrapper : MonoBehaviour
{
	// �o��class�ΨӳB�zReferenceLine�A�D�n�ݭn���Ѥ@��function���o�Ѧҽu����m�A�K�󲣥ͨ��D�A�o��Ȯɼg�@��bezier curve��function��@�Ѧҽu
	public List<GameObject> controlPoints;
	public LineRenderer lineRenderer;

    private void Start()
    {
		lineRenderer.positionCount = 100;
	}

	public int GetPointCount()
    {
		return lineRenderer.positionCount;
    }

    /// <summary>
    /// �ھڵ��w��t�Ȩ��oreferenceline��m
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
		B = uuu * controlPoints[0].transform.localPosition;
		B += 3.0f * uu * t * controlPoints[1].transform.localPosition;
		B += 3.0f * u * tt * controlPoints[2].transform.localPosition;
		B += ttt * controlPoints[3].transform.localPosition;

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

	public Vector3 GetStartControlPointPos()
    {
		return controlPoints[0].transform.position;
    }

	public Vector3 GetEndControlPointPos()
	{
		return controlPoints[controlPoints.Count - 1].transform.position;
	}
}
