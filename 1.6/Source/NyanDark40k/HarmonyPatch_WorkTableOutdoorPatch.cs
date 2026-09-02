using HarmonyLib;
using RimWorld;
using Verse;

namespace NyanDark40k;

[HarmonyPatch(typeof(StatPart_WorkTableOutdoors), "Applies", [
    typeof(ThingDef),
    typeof(Map),
    typeof(IntVec3)
], [
    ArgumentType.Normal,
    ArgumentType.Normal,
    ArgumentType.Normal,
])]
public class HarmonyPatch_WorkTableOutdoorPatch
{
    public static bool Prefix(ref bool __result, ThingDef def)
    {
        if (def == null || !def.HasModExtension<DefModExtension_IgnoreWorkTablePenalties>())
        {
            return true;
        }

        __result = false;
        return false;
    }
}
