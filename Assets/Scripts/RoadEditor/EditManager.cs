using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditManager : MonoSingleton<EditManager>
{
    public LinkNodeEvent linkNodeEvent;

    private void Awake()
    {
        linkNodeEvent = new LinkNodeEvent();
    }

    public void StartLinkNode()
    {
        StartCoroutine(linkNodeEvent.LinkTwoNodes());
    }
}
