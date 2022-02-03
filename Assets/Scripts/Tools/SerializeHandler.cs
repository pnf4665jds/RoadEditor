using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

public class SerializeHelper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(BezierPath));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
