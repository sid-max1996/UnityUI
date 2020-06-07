using UnityEngine;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour,
    IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public int powerPoints = 0;
    Transform rootParent;
    CanvasGroup canvasGroup;
    public bool isAllowDrag = true;
    Vector3 startPosition;
    Transform startParent;

    void Awake()
    {
        rootParent = transform.parent;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isAllowDrag) return;
        transform.SetParent(rootParent);
        transform.SetAsLastSibling();
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        startParent = transform.parent;
        startPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isAllowDrag) return;
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if (!isAllowDrag) return;
        var dist = eventData.pointerEnter;
        if (dist.name != "Viewport")
        {
            transform.SetParent(startParent);
            transform.position = startPosition;
        }
    }
}
