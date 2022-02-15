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
	/// �ھڵ��w��t�Ȩ��oreferenceline�W�Y���I�������y��
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

	/// <summary>
	/// �ھڵ��w��t�Ȩ��oreferenceline�W�Y���I���@�ɮy��
	/// </summary>
	/// <param name="t"></param>
	/// <returns></returns>
	public Vector3 GetReferenceLineWorldPos(float t)
	{
		float tt = t * t;
		float ttt = t * tt;
		float u = 1.0f - t;
		float uu = u * u;
		float uuu = u * uu;

		Vector3 B = new Vector3();
		B = uuu * controlPoints[0].transform.position;
		B += 3.0f * uu * t * controlPoints[1].transform.position;
		B += 3.0f * u * tt * controlPoints[2].transform.position;
		B += ttt * controlPoints[3].transform.position;

		return B;
	}

	/// <summary>
	/// �ھڵ��w��t�Ȩ��oreferenceline�W�Y���I���������u��V
	/// </summary>
	/// <param name="t"></param>
	/// <returns></returns>
	public Vector3 GetReferenceLineTangent(float t)
	{
		float tt = t * t;
		float ttt = t * tt;
		float u = 1.0f - t;
		float uu = u * u;
		float uuu = u * uu;

		Vector3 B = new Vector3();
		B = -3.0f * uu * controlPoints[0].transform.localPosition;
		B += 3.0f * (-2.0f * u * t + uu) * controlPoints[1].transform.localPosition;
		B += 3.0f * (-tt + 2 * u * t) * controlPoints[2].transform.localPosition;
		B += 3.0f * tt * controlPoints[3].transform.localPosition;

		return B.normalized;
	}

	/// <summary>
	/// �ھڵ��w��t�Ȩ��oreferenceline�W�Y���I�����u��V(�@�ɮy��)
	/// </summary>
	/// <param name="t"></param>
	/// <returns></returns>
	public Vector3 GetReferenceLineWorldTangent(float t)
	{
		float tt = t * t;
		float ttt = t * tt;
		float u = 1.0f - t;
		float uu = u * u;
		float uuu = u * uu;

		Vector3 B = new Vector3();
		B = -3.0f * uu * controlPoints[0].transform.position;
		B += 3.0f * (-2.0f * u * t + uu) * controlPoints[1].transform.position;
		B += 3.0f * (-tt + 2 * u * t) * controlPoints[2].transform.position;
		B += 3.0f * tt * controlPoints[3].transform.position;

		return B.normalized;
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

	/// <summary>
	/// ���o�Ĥ@�ӱ����I����m
	/// </summary>
	/// <returns></returns>
	public Vector3 GetStartControlPointPos()
    {
		return controlPoints[0].transform.position;
    }

	/// <summary>
	/// ���o�̫�@�ӱ����I����m
	/// </summary>
	/// <returns></returns>
	public Vector3 GetEndControlPointPos()
	{
		return controlPoints[controlPoints.Count - 1].transform.position;
	}
}
