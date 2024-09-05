using HarmonyLib;
using Game.Chat;
using Game.Interface;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using UnityEngine;
using Server.Shared.Messages;

namespace FilterUtils.Patches;


[HarmonyPatch(typeof(PooledChatLogFilterController), "ApplyFilter")]
public static class ApplyChanges
{
    [HarmonyTranspiler]
    static public IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);

        for (int i = 0; i < codes.Count; i++)
        {
            if (codes[i].opcode == OpCodes.Ldfld &&
                (codes[i].operand as System.Reflection.FieldInfo)?.Name == "LimitToFilteredPlayer")
            {
                if (codes[i + 1].opcode == OpCodes.Brtrue_S)
                {
                    yield return codes[i];
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(ApplyChanges), nameof(Check)));
                    yield return new CodeInstruction(OpCodes.Or);
                    yield return new CodeInstruction(OpCodes.Brtrue_S, codes[i + 1].operand);
                    i++;
                }
                else
                {
                    yield return codes[i];
                }
            }
            else
            {
                yield return codes[i];
            }
        }
        yield break;
    }

    static bool Check()
    {
        Debug.LogWarning("OMGGG");
        return false;
    }
}

[HarmonyPatch(typeof(PooledChatController), "PopulateChat")]
public static class ModifyMsgPool
{
    [HarmonyTranspiler]
    static public IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        for (int i = 0; i < codes.Count; i++)
        {
            if (codes[i].opcode == OpCodes.Ldfld &&
                (codes[i].operand as FieldInfo)?.Name == "showInChatLog")
            {
                yield return codes[i];
                yield return new CodeInstruction(OpCodes.Ldloc_S, 3); 
                yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(ModifyMsgPool), nameof(AddMessage)));
                yield return new CodeInstruction(OpCodes.Or);
                i++;
                yield return new CodeInstruction(OpCodes.Brfalse_S, codes[i].operand);
            }
            else
            {
                yield return codes[i];
            }
        }

    }
    static bool AddMessage(ChatLogMessage chatLog)
    {
        Debug.LogWarning("This runs yeyy");
        return false;
    }
}
