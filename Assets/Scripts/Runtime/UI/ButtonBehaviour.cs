using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField][Tooltip("How much the button should scale by.")] private float scaleMultiplier = 1.1f;
        private Vector3 defaultScale;

        private void Start()
        {
            defaultScale = transform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale *= scaleMultiplier;
            SoundEffectsHandler.Instance.PlayEffect("UI-Hover");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = defaultScale;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            transform.localScale = defaultScale;
            SoundEffectsHandler.Instance.PlayEffect("UI-Select");
        }
    }
