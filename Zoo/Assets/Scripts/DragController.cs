using UnityEngine;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour,
    IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public bool isAlowDrag = true;
    public int powerPoints = 0;

    Vector3 startPosition;
    Transform startParent;
    CanvasGroup canvasGroup;
    Transform rootParent;

    private void Start()
    {
        rootParent = transform.parent;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isAlowDrag) 
            return;
        Debug.Log("OnBeginDrag");
        startParent = transform.parent;
        startPosition = transform.position;
        transform.SetParent(rootParent);
        transform.SetAsLastSibling();
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isAlowDrag)
            return;
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if (!isAlowDrag)
            return;
        var dist = eventData.pointerEnter;
        Debug.Log(dist.ToString());
        if (dist.name != "Viewport")
        {
            transform.SetParent(startParent);
            transform.position = startPosition;
        }
    }
}
