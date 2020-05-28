using UnityEngine;
using UnityEngine.EventSystems;

public class DropController : MonoBehaviour, IDropHandler
{
    private void AddChildOnDropEnd(GameObject obj, DragController controller)
    {
        Vector2 anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        obj.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        obj.transform.SetParent(transform);
        controller.isAlowDrag = false;
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        GameObject target = eventData.pointerDrag;
        var targetController = target.GetComponent<DragController>();
        if (!targetController.isAlowDrag)
            return;
        if (transform.childCount == 0)
        {
            AddChildOnDropEnd(target, targetController);
        } else
        {
            var child = transform.GetChild(0).gameObject;
            if (target == child)
                return;
            var childController = child.GetComponent<DragController>();
            if (childController.powerPoints >= targetController.powerPoints)
            {
                Destroy(target);
            } else
            {
                Destroy(child);
                AddChildOnDropEnd(target, targetController);
            }
        }
    }
}
