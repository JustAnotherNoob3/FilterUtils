using HarmonyLib;
using UnityEngine;
using FilterUtils.CustomClasses;
using Game.Chat;
using Game.Interface;

namespace FilterUtils.Patches;


//[HarmonyPatch(typeof(PooledChatLogFilterController), "ApplyFilter")]
public static class CreateButtons{
    static public void Prefix(PooledChatLogFilterController __instance){
        
    }
}

