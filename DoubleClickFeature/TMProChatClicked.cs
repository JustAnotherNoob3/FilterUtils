using FilterUtils.Instances;
using Game.Chat;
using Game.Interface;
using Server.Shared.State.Chat;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FilterUtils.CustomClasses;

public class TMProChatClicked : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI TMPro;
    public Camera camera;
    public void Start()
    {
        TMPro = gameObject.GetComponent<TextMeshProUGUI>();
        camera = Service.Game.Interface.uiCamera.GetComponent<Camera>();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(!Pepper.IsGamePhasePlay()) return;
        if(!(eventData.clickCount == 2 && eventData.button == PointerEventData.InputButton.Left) && eventData.button != PointerEventData.InputButton.Middle) return;
        if (!TMP_TextUtilities.IsIntersectingRectTransform(TMPro.rectTransform, eventData.position, camera)) return;
        if (!PooledChatViewPatcher.instance.chatLogCanvasGroup.interactable) PooledChatViewPatcher.instance.SetViewToChatLog();
        PooledChatLogFilterController controller = PooledChatLogFilterPatching.instance;
        int speaker = ((ChatLogChatMessageEntry)transform.parent.GetComponent<HudChatPoolItem>()._chatLogMessage.chatLogEntry).speakerId;
        try{
        controller.PositionFilter = speaker + 1;
        } catch {
            controller.PositionFilter = speaker + 1;
        }
    }
}