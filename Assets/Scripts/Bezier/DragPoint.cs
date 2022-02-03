using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPoint : MonoBehaviour
{
    public bool CanDrag = true;
    // 是否固定某個軸的位置
    public bool IsXFixed = false;
    public bool IsYFixed = false;
    public bool IsZFixed = false;

    // 與掛有這個script的物件保持固定距離的另一個物件
    public List<GameObject> FixedObjects;

    private List<Vector3> fixedOffsets = new List<Vector3>();
    private Vector3 dist;
    private Vector3 startPos;
    private float posX;
    private float posZ;
    private float posY;

    private void Awake()
    {
        if (FixedObjects.Count > 0)
        {
            foreach (GameObject obj in FixedObjects)
            {
                fixedOffsets.Add(obj.transform.position - transform.position);
            }
        }
    }

    void OnMouseDown()
    {
        startPos = transform.position;
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
        posZ = Input.mousePosition.z - dist.z;
    }

    void OnMouseDrag()
    {
        if (CanDrag)
        {
            float disX = Input.mousePosition.x - posX;
            float disY = Input.mousePosition.y - posY;
            float disZ = Input.mousePosition.z - posZ;
            Vector3 lastPos = Camera.main.ScreenToWorldPoint(new Vector3(disX, disY, disZ));
            if (IsXFixed)
                lastPos.x = startPos.x;
            if (IsYFixed)
                lastPos.y = startPos.y;
            if (IsZFixed)
                lastPos.z = startPos.z;

            transform.position = new Vector3(lastPos.x, lastPos.y, lastPos.z);

            if(FixedObjects.Count > 0)
            {
                for(int i = 0; i < FixedObjects.Count; i++)
                {
                    FixedObjects[i].transform.position = transform.position + fixedOffsets[i];
                }
            }
        }

    }

}
