using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INode
{
    void OnNodeInit();

    void OnPreviewMode();

    void OnEditMode();
}
