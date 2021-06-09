using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampRectTransform : MonoBehaviour
{
    public float padding = 10f;
    public float elementSize = 128f;
    public float viewSize = 250f;
    public float leftPadding = 5f;

    private RectTransform rt;
    private int amountElements;
    private float contentSize;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
    }
    
    private void Update()
    {
        //Calmp the rect transform
        amountElements = rt.childCount;
        contentSize = ((amountElements * (elementSize + padding)) - viewSize) * rt.localScale.x;

        if(rt.localPosition.x > padding + leftPadding)
        {
            rt.localPosition = new Vector3(padding,rt.localPosition.y,rt.localPosition.z);
        }
        else if(rt.localPosition.x < -contentSize)
        {
            rt.localPosition = new Vector3(-contentSize, rt.localPosition.y, rt.localPosition.z);
        }
    }
}
