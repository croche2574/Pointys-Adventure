using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))] 
public class LayoutAnchor : MonoBehaviour
{
    RectTransform thisRT;
    RectTransform parentRT;
    void Awake()
    {
        thisRT = transform as RectTransform;
        parentRT = transform.parent as RectTransform;
        if(parentRT == null)
        {
            Debug.Log("Rect transform parent required");
        }
    }
    Vector2 GetPosition (RectTransform rt, TextAnchor anchor)
    {
        Vector2 retValue = Vector2.zero;
        switch (anchor)
        {
            case TextAnchor.LowerCenter: 
            case TextAnchor.MiddleCenter: 
            case TextAnchor.UpperCenter:
                retValue.x += rt.rect.width * 0.5f;
                break;
            case TextAnchor.LowerRight: 
            case TextAnchor.MiddleRight: 
            case TextAnchor.UpperRight:
                retValue.x += rt.rect.width;
                break;
        }

        switch (anchor)
        {
            case TextAnchor.MiddleLeft: 
            case TextAnchor.MiddleCenter: 
            case TextAnchor.MiddleRight:
                retValue.y += rt.rect.height * 0.5f;
                break;
            case TextAnchor.UpperLeft: 
            case TextAnchor.UpperCenter: 
            case TextAnchor.UpperRight:
                retValue.y += rt.rect.height;
                break;
        }
        return retValue;
    }
    public Vector2 AnchorPosition (TextAnchor myAnchor, TextAnchor parentAnchor, Vector2 offset)
    {
        Vector2 myOffset = GetPosition(thisRT, myAnchor);
        Vector2 parentOffset = GetPosition(parentRT, parentAnchor);
        Vector2 anchorCenter = new Vector2( Mathf.Lerp(thisRT.anchorMin.x, thisRT.anchorMax.x, thisRT.pivot.x), Mathf.Lerp(thisRT.anchorMin.y, thisRT.anchorMax.y, thisRT.pivot.y) );
        Vector2 myAnchorOffset = new Vector2(parentRT.rect.width * anchorCenter.x, parentRT.rect.height * anchorCenter.y);
        Vector2 myPivotOffset = new Vector2(thisRT.rect.width * thisRT.pivot.x, thisRT.rect.height * thisRT.pivot.y);
        Vector2 pos = parentOffset - myAnchorOffset - myOffset + myPivotOffset + offset;
        pos.x = Mathf.RoundToInt(pos.x);
        pos.y = Mathf.RoundToInt(pos.y);
        return pos;
    }
    public void SnapToAnchorPosition(TextAnchor myAnchor, TextAnchor parentAnchor, Vector2 offset)
    {
        thisRT.anchoredPosition = AnchorPosition(myAnchor, parentAnchor, offset);
    }
    public Tweener MoveToAnchorPosition(TextAnchor thisAnchor, TextAnchor parentAnchor, Vector2 offset)
    {
        return thisRT.AnchorTo(AnchorPosition(thisAnchor, parentAnchor, offset));
    }
}
