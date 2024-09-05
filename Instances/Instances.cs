using HarmonyLib;
using Game.Interface;

namespace FilterUtils.Instances;


[HarmonyPatch(typeof(PooledChatLogFilterController), "Start")]
public static class PooledChatLogFilterPatching{
    static public PooledChatLogFilterController instance;
    public static void Postfix(PooledChatLogFilterController __instance){
        instance = __instance;
    }
}
[HarmonyPatch(typeof(PooledChatViewSwitcher), "Start")]
public static class PooledChatViewPatcher{
    static public PooledChatViewSwitcher instance;
    public static void Postfix(PooledChatViewSwitcher __instance){
        instance = __instance;
    }
}