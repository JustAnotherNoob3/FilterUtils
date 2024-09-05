using HarmonyLib;
using UnityEngine;
using FilterUtils.CustomClasses;
using Game.Chat;
using Game.Interface;

namespace FilterUtils.Patches;


[HarmonyPatch(typeof(ChatItemFactory), "Start")]
public static class AddCustomClass{
    static public void Prefix(ChatItemFactory __instance){
        __instance.ChatItemTemplate.transform.Find("Words").gameObject.AddComponent<TMProChatClicked>();
    }
}
