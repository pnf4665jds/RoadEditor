using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkNodeEvent
{
    public List<RoadNode> selectNodes = new List<RoadNode>();

    public IEnumerator LinkTwoNodes()
    {
        foreach (RoadNode node in SceneManager.Instance.roadNodes)
        {
            // 新增事件，當點擊node時，加進selectNodes
            node.ActionOnMouseDown += AddSelectNodes;
        }

        yield return SelectNodes();
        CancelLinkTwoNodes();
    }

    public void CancelLinkTwoNodes()
    {
        foreach (RoadNode node in SceneManager.Instance.roadNodes)
        {
            // 取消事件
            node.ActionOnMouseDown -= AddSelectNodes;
        }
        selectNodes.Clear();
    }

    private void AddSelectNodes(RoadNode node)
    {
        selectNodes.Add(node);
    }

    private IEnumerator SelectNodes()
    {
        while (selectNodes.Count < 2)
        {
            yield return null;
        }

        RoadNode node1 = selectNodes[0];
        RoadNode node2 = selectNodes[1];

        if (node1 == node2)
        {
            Debug.Log("Can't link the same node");
            yield break;
        }

        SceneRoad node1_inRoad = node1.inRoad;
        SceneRoad node1_outRoad = node1.outRoad;
        SceneRoad node2_inRoad = node2.inRoad;
        SceneRoad node2_outRoad = node2.outRoad;

        if ((node1_inRoad && node1_outRoad) || (node2_inRoad && node2_outRoad))
        {
            Debug.Log("Can't link the node that had two road already");
            yield break;
        }
        else
        {
            // Road1尾連Road2頭
            if (node1_inRoad && node2_outRoad)
            {
                LinkNode(node1_inRoad, node2_outRoad);
            }
            // Road2尾連Road1頭
            else if (node2_inRoad && node1_outRoad)
            {
                LinkNode(node2_inRoad, node1_outRoad);
            }
            else
            {
                Debug.Log("Link fail");
            }
        }
    }

    private void LinkNode(SceneRoad r1, SceneRoad r2)
    {
        if (r1 == r2)
        {
            Debug.Log("Can't link two node share same road");
            return;
        }

        RoadNode temp = r2.startNode;
        r2.startNode = r1.endNode;
        r1.endNode.outRoad = r2;
        Object.Destroy(temp.gameObject);
    }
}
