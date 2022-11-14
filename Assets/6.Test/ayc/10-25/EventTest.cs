using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventTest : MonoBehaviour
{
    public RectTransform rectTransform;
    public Rect rect;

    private void Update()
    {
        rect = rectTransform.rect;
        if(Input.GetKey(KeyCode.Space))
        {
            if (rect.Contains(Event.current.mousePosition))
            {
                if(Event.current.type == EventType.MouseDown)
                    Event.current.Use();
            }
        }
    }
}