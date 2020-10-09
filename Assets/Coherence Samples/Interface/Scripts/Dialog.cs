namespace Coherence.Samples
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class Dialog : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public CanvasGroup canvasGroup;
        public float dragSmooth;
        protected RectTransform rectTransform;
        protected bool dragging;

        private Vector2 target;

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            dragging = true;
            target = rectTransform.anchoredPosition;
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            target += eventData.delta;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            dragging = false;
        }

        protected virtual void Awake()
        {
            rectTransform = transform as RectTransform;
            target = rectTransform.anchoredPosition;
        }

        protected virtual void OnDestroy()
        {
        }

        protected virtual void Update()
        {
            if (rectTransform.anchoredPosition != target)
            {
                bool closeEnough = Mathf.Approximately(target.x, rectTransform.anchoredPosition.x) && Mathf.Approximately(target.y, rectTransform.anchoredPosition.y);
                rectTransform.anchoredPosition = closeEnough ? target : Vector2.Lerp(rectTransform.anchoredPosition, target, Time.smoothDeltaTime * dragSmooth);
            }
        }

        protected virtual void Reset()
        {
            dragSmooth = 10f;
        }

        public virtual void Show()
        {
            canvasGroup.interactable = true;
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            canvasGroup.interactable = false;
            gameObject.SetActive(false);
        }
    }
}