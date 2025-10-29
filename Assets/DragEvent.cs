using UnityEngine;
using UnityEngine.EventSystems;

public class DragEvent : MonoBehaviour
{
    Vector3 dragPos;


    public void OnPointerDown(BaseEventData data)
    {
        var p = data as PointerEventData;
        Vector3 mousePos = p.position;
        this.dragPos = Camera.main.WorldToScreenPoint(transform.position) - mousePos;

        // ドラッグ＆ドロップ可能なGameObjectをすべて列挙
        var objects = FindObjectsByType<DragEvent>(FindObjectsSortMode.None);
        // 一番手前となるOrder値
        int top = objects.Length - 1;
        int current = GetComponent<SpriteRenderer>().sortingOrder;
        // クリックしたGameObjectが一番手前になるよう調整
        foreach (var obj in objects)
        {
            var renderer = obj.GetComponent<SpriteRenderer>();
            if (renderer.sortingOrder > current)
            {
                renderer.sortingOrder = renderer.sortingOrder - 1;
            }
            else if (renderer.sortingOrder == current)
            {
                renderer.sortingOrder = top; 
            }
        }
    }

    public void OnDrag(BaseEventData data)
    {
        var p = data as PointerEventData;
        Vector3 mousePos = p.position;
        transform.position = Camera.main.ScreenToWorldPoint(mousePos + this.dragPos);
    }
}   