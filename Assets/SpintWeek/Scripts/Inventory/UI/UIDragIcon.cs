using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Inventory.UI
{
    public class UIDragIcon : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI quantityText;
        [SerializeField] private Canvas canvasReference;

        private RectTransform _rectTransform;


        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();

            if (canvasReference == null)
            {
                canvasReference = GetComponentInParent<Canvas>();
            }

            Debug.Log("Canvas RenderMode: " + canvasReference.renderMode);
            Debug.Log("Canvas worldCamera: " + canvasReference.worldCamera);

            Hide();
        }


        public void Show(Sprite icon, int amount)
        {
            if (canvasReference == null)
            {
                canvasReference = GetComponentInParent<Canvas>();
                Debug.LogWarning("CanvasReference was null, re-assigned at runtime.");
            }

            iconImage.sprite = icon;
            quantityText.text = amount > 1 ? amount.ToString() : "";
            gameObject.SetActive(true);
            UpdatePosition();
        }



        public void UpdatePosition()
        {

            if (canvasReference == null)
            {
                Debug.LogWarning("Canvas reference is null. Can't update drag icon position.");
                return;
            }
            Camera camera = canvasReference.renderMode == RenderMode.ScreenSpaceOverlay
     ? null
     : canvasReference.worldCamera;


            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasReference.transform as RectTransform,
                Input.mousePosition,
                camera,
                out Vector2 localPoint
            );

            _rectTransform.anchoredPosition = localPoint;
        }


        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
