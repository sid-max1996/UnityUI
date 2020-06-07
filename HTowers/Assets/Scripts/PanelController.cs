using UnityEngine;
using UnityEngine.EventSystems;

public class PanelController : MonoBehaviour,
    IDragHandler, IEndDragHandler, IBeginDragHandler
{
    MainController mainController;
    CanvasGroup canvasGroup;
    Vector3 startPosition;
    Transform startParent;
    bool isLastChild;
    
    void Awake()
    {
        var canvas = GameObject.Find("Canvas");
        mainController = canvas.GetComponent<MainController>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FindPosition ()
    {
        var rt = GetComponent<RectTransform>();
        int i = GetComponent<RectTransform>().GetSiblingIndex();
        Vector3 v = gameObject.transform.localPosition;
        v.x = 0;
        var pRt = rt.parent.GetComponent<RectTransform>();
        float yCenter = (pRt.rect.height / 2);
        int h = State.panelHeight;
        v.y = -yCenter + h / 2 + (i * h);
        gameObject.transform.localPosition = v;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (State.isGameEnd) return;
        startParent = transform.parent;
        startPosition = transform.position;
        var ind = transform.parent.childCount - 1;
        isLastChild = 
            gameObject.transform == transform.parent.GetChild(ind);
        if (!isLastChild) return;
        canvasGroup.blocksRaycasts = false;
        var rootParent = 
            mainController.gameObject.GetComponent<Transform>();
        transform.SetParent(rootParent);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (State.isGameEnd || !isLastChild) return;
        transform.position = eventData.position;
    }

    void CancelStep ()
    {
        transform.SetParent(startParent);
        transform.position = startPosition;
    }

    void FinishStep(GameObject dist)
    {
        transform.SetParent(dist.transform);
        FindPosition();
        mainController.scoreLabel.text = "Steps: " + --State.stepCount;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (State.isGameEnd || !isLastChild) return;
        canvasGroup.blocksRaycasts = true;
        var dist = eventData.pointerEnter;
        if (dist == null || !dist.name.StartsWith("Col"))
            CancelStep();
        else
        {
            var rt = GetComponent<RectTransform>();
            var dRt = dist.GetComponent<RectTransform>();
            int i = dRt.childCount - 1;
            if (i < 0) FinishStep(dist);
            else
            {
                var child = dRt.GetChild(i);
                var cRt = child.GetComponent<RectTransform>();
                if (cRt.rect.width > rt.rect.width) 
                    FinishStep(dist);
                else CancelStep();
            }
        }
        var mRt = mainController.col2.GetComponent<RectTransform>();
        State.isGameEnd = mRt.childCount == State.level;
        if (State.isGameEnd || State.stepCount == 0)
        {
            bool condition = State.stepCount >= 0 && State.isGameEnd;
            mainController.modalText.text =
                condition ? "You Win!!!" : "You Lose!!!";
            mainController.modal.SetActive(true);
        }
    }
}
