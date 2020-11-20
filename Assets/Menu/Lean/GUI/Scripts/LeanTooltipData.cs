using UnityEngine;
using UnityEngine.EventSystems;
using Lean.Common;
using UnityEngine.SceneManagement;

namespace Lean.Gui
{
	/// <summary>This component allows you to associate text with this GameObject, allowing it to be displayed from a tooltip.</summary>
	[HelpURL(LeanGui.HelpUrlPrefix + "LeanTooltipData")]
	[AddComponentMenu(LeanGui.ComponentMenuPrefix + "Tooltip Data")]
	public class LeanTooltipData : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		/// <summary>If you want this tooltip to hide when a selectable (e.g. Button) is disabled or non-interactable, then specify it here.</summary>
		public UnityEngine.UI.Selectable Selectable { set { selectable = value; } get { return selectable; } } [SerializeField] private UnityEngine.UI.Selectable selectable;

		/// <summary>This allows you to set the tooltip text string that is associated with this object.</summary>
		public string Text { set { text = value; } get { return text; } } [Multiline] [SerializeField] private string text;

		protected virtual void Update()
      	{
         if (LeanTooltip.HoverData == this)
         {
            if (selectable != null)
            {
               LeanTooltip.HoverShow = selectable.enabled == true && selectable.interactable == true;
            }
         }
         if (SceneManager.GetActiveScene().name == "Golf")
            {
            text = "<color=#0000ff>" + "<격방>" + "</color>" + "  \n  공을 쳐서 정해진 구멍에 들어가게 하는 공치기 경기" +
               "\n \n " +
               "<color=#ff0000>" + "<게임 방법>" + "</color>" + " \n버튼/손잡이 누르면 누르는 만큼 힘이 가해지고 떼면 공을 칠 수 있다. 총 기회는 3번! 구멍(와아)에 공을 넣으면 게임 성공!";
            }
         if (SceneManager.GetActiveScene().name == "Dice")
         {
            text = "<color=#0000ff>" + "<종경도>" + "</color>" + "  \n 옛 벼슬의 이름을 종이에 도표로 만들어놓고 놀던 어린이놀이" +
               "\n \n " +
               "<color=#ff0000>" + "<게임 방법>"+"</color>"+"  \n 15칸이상이면 3점만점, 10칸이상이면 2점 5칸 이상이면 1점으로 주사위 굴려서 나온 점수 만큼 보드 위의 말을 움직일 수 있고 총 18칸을 움직이면 성공!.";
         }
         if (SceneManager.GetActiveScene().name == "Tuho")
         {
            text = "<color=#0000ff>" + "<투호치기>" + "</color>" + " \n 병을 일정한 거리에 놓고, 그 속에 화살을 던져 넣은 후 그 개수로 승부를 가리는 성인남녀놀이. 승부놀이" +
               "\n \n " +
               "<color=#ff0000>" + "<게임 방법>" + "</color>" + "  \n ARROW 버튼을 눌러 투호 화살을 불러오고, 투호 화살을 위로 밀어서 통에 넣으면 점수가 1점 증가합니다. 5점을 얻으면 성공!.";
         }
         if (SceneManager.GetActiveScene().name == "Arrow")
         {
            text = "<color=#0000ff>" + "<활쏘기>" + "</color>" + " \n 활과 화살을 사용하여 표적을 맞히는 전통무술 또는 민속경기" +
               "\n \n " +
               "<color=#ff0000>" + "<게임 방법>" + "</color>" + " ???.";
         }
      	}

		public void OnPointerEnter(PointerEventData eventData)
		{
			LeanTooltip.HoverPointer = eventData;
			LeanTooltip.HoverData    = this;
			LeanTooltip.HoverShow    = true;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (LeanTooltip.HoverData == this)
			{
				LeanTooltip.HoverPointer = null;
				LeanTooltip.HoverData    = null;
				LeanTooltip.HoverShow    = false;
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			LeanTooltip.PressPointer = eventData;
			LeanTooltip.PressData    = this;
			LeanTooltip.PressShow    = true;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (LeanTooltip.PressData == this)
			{
				LeanTooltip.PressPointer = null;
				LeanTooltip.PressData    = null;
				LeanTooltip.PressShow    = false;
			}
		}
	}
}

#if UNITY_EDITOR
namespace Lean.Gui.Inspector
{
	using UnityEditor;

	[CanEditMultipleObjects]
	[CustomEditor(typeof(LeanTooltipData))]
	public class LeanTooltipData_Inspector : LeanInspector<LeanTooltipData>
	{
		protected override void DrawInspector()
		{
			Draw("selectable", "If you want this tooltip to hide when a selectable (e.g. Button) is disabled or non-interactable, then specify it here.");
			Draw("text", "This allows you to set the tooltip text string that is associated with this object.");
		}
	}
}
#endif