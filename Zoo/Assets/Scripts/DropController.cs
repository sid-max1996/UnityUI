using UnityEngine;
using UnityEngine.EventSystems;

public class DropController : MonoBehaviour, IDropHandler
{
    private void AddChild(GameObject obj, DragController targetScript)
    {
        var ancdPos = GetComponent<RectTransform>().anchoredPosition;
        obj.GetComponent<RectTransform>().anchoredPosition = ancdPos;
        obj.transform.SetParent(transform);
        targetScript.isAllowDrag = false;
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject target = eventData.pointerDrag;
        var targetScript = target.GetComponent<DragController>();
        if (!targetScript.isAllowDrag) return;
        if (transform.childCount == 0)
            AddChild(target, targetScript);
        else
        {
            var child = transform.GetChild(0).gameObject;
            var childController = child.GetComponent<DragController>();
            if (childController.powerPoints >= targetScript.powerPoints)
            {
                Destroy(target);
            } else
            {
                Destroy(child);
                AddChild(target, targetScript);
            }
        }
    }
}
