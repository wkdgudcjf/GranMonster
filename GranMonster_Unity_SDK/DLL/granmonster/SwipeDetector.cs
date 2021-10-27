using UnityEngine;
using UnityEngine.EventSystems;

namespace granmonster
{
	class SwipeDetector : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		private enum Direction
		{
			None = 0,
			LeftToRight = 1,
			RightToLeft = -1,
		}

		private static float AnimationTime = 0.25f;
		private Sprite SelectedPointImage;
		private Sprite NotSelectedPointImage;

		private Direction direction;
		private float dragStartX;
		private float dragEndX;

		private float startPositionX;
		private float endPositionX;
		private float remainAniamtionTime;

		public int index;
		private RectTransform scrollViewContent;
		private Transform scrollPoints;

		private void FixedUpdate()
		{
			if (direction == Direction.None)
			{
				return;
			}

			remainAniamtionTime -= Time.deltaTime;
			if (remainAniamtionTime < 0.0f)
			{
				scrollViewContent.anchoredPosition3D = new Vector2(-endPositionX, scrollViewContent.anchoredPosition3D.y);
				for (int i = 0; i < scrollPoints.childCount; ++i)
				{
					Sprite sprite = (index + (int)direction) == i ? SelectedPointImage : NotSelectedPointImage;
					scrollPoints.GetChild(i).GetComponent<UnityEngine.UI.Image>().sprite = sprite;
				}

				direction = Direction.None;
				return;
			}

			float delta = (startPositionX - endPositionX) * (Time.deltaTime / AnimationTime);
			scrollViewContent.anchoredPosition = new Vector2(
				scrollViewContent.anchoredPosition3D.x + (int)delta,
				scrollViewContent.anchoredPosition3D.y);
		}

		public void Init()
		{
			scrollViewContent.anchoredPosition3D = new Vector2(0, scrollViewContent.anchoredPosition3D.y);

			Sprite sprite = index == 0 ? SelectedPointImage : NotSelectedPointImage;
			scrollPoints.GetChild(index).GetComponent<UnityEngine.UI.Image>().sprite = sprite;
		}

		public void SetScrollViewContent(int index, RectTransform scrollViewContent, Transform scrollPoints)
		{
			this.index = index;
			this.scrollViewContent = scrollViewContent;
			this.scrollPoints = scrollPoints;

			SelectedPointImage = scrollPoints.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite;
			NotSelectedPointImage = scrollPoints.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite;
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			Canvas canvas = GetComponentInParent<Canvas>();
			RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();

			Vector3 viewportPosition = canvas.worldCamera.ScreenToViewportPoint(eventData.position);
			viewportPosition.x = Mathf.Clamp01(viewportPosition.x);
			viewportPosition.y = Mathf.Clamp01(viewportPosition.y);

			RectTransform imageRectTransform = GetComponent<RectTransform>();
			Vector2 screenPosition = new Vector2(
				(viewportPosition.x * imageRectTransform.rect.width),
				(viewportPosition.y * imageRectTransform.rect.height)
			);

			dragStartX = screenPosition.x;
		}

		public void OnDrag(PointerEventData eventData)
		{
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			Canvas canvas = GetComponentInParent<Canvas>();
			RectTransform rectTransform = canvas.GetComponent<RectTransform>();

			Vector3 viewportPosition = canvas.worldCamera.ScreenToViewportPoint(eventData.position);
			viewportPosition.x = Mathf.Clamp01(viewportPosition.x);
			viewportPosition.y = Mathf.Clamp01(viewportPosition.y);

			RectTransform imageRectTransform = GetComponent<RectTransform>();
			Vector2 screenPosition = new Vector2(
				(viewportPosition.x * imageRectTransform.rect.width),
				(viewportPosition.y * imageRectTransform.rect.height)
			);

			dragEndX = screenPosition.x;
			if (Mathf.Abs(dragStartX - dragEndX) > imageRectTransform.rect.width / 4)
			{
				direction = dragStartX > dragEndX ? Direction.LeftToRight : Direction.RightToLeft;
				int toIndex = index + (int)direction;

				if (toIndex < 0 ||
					toIndex >= 3)
				{
					direction = Direction.None;
					return;
				}

				startPositionX = index * imageRectTransform.rect.width;
				endPositionX = toIndex * imageRectTransform.rect.width;
				remainAniamtionTime = AnimationTime;
			}
		}
	}
}
