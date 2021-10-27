using UnityEngine;
using UnityEngine.EventSystems;

namespace granmonster
{
	class FloatingButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		private Canvas canvas = null;
		private Camera canvasCamera = null;
		private RectTransform canvasRectTransform = null;

		private Animator animator = null;

		private void Start()
		{
			animator = GetComponent<Animator>();
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			// 드래그를 시작하는 순간 버튼 클릭이 먹히지 않도록 한다.
			UnityEngine.UI.Button button = GetComponent<UnityEngine.UI.Button>();
			button.enabled = false;

			animator.Play("FocusIn");
		}

		public void OnDrag(PointerEventData eventData)
		{
			// 프리팹을 객체화하는 과정에서 초기화 시점이 애매한 부분이 있어,
			// 버튼 생성 후 업데이트 함수에서 한 번만 처리하도록 한다.
			if (canvas == null)
			{
				canvas = GetComponentInParent<Canvas>();
				if (canvas == null)
				{
					return;
				}
				else
				{
					canvasCamera = canvas.worldCamera;
					canvasRectTransform = canvas.GetComponent<RectTransform>();
				}
			}

			// 터치 포인트는 스크린 포인트(픽셀)로 들어온다. 이것을 뷰 포트로 변환한다.
			Vector3 viewportPosition = canvasCamera.ScreenToViewportPoint(eventData.position);
			viewportPosition.x = Mathf.Clamp01(viewportPosition.x);
			viewportPosition.y = Mathf.Clamp01(viewportPosition.y);

			// 뷰 포트는 카메라가 보는 영역을 0부터 1사이의 값을 사용하여 비율로 나타낸 것이다.
			// 이 비율을 활용하여 캔버스의 크기(sizeDelta)에 해당하는 값을 계산한다.
			RectTransform buttonRectTransform = GetComponent<RectTransform>();
			Vector2 screenPosition = new Vector2(
				(viewportPosition.x * canvasRectTransform.sizeDelta.x),
				(viewportPosition.y * canvasRectTransform.sizeDelta.y)
			);

			buttonRectTransform.anchoredPosition3D = screenPosition;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			// 드래그가 끝나는 순간 버튼을 다시 활성화하여 클릭할 수 있도록 한다.
			UnityEngine.UI.Button button = GetComponent<UnityEngine.UI.Button>();
			button.enabled = true;

			animator.Play("FocusOut");
		}

		public void OnButtonClick()
		{
			animator.Play("Click");
		}
	}
}
