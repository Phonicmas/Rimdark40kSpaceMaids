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
        return !def.HasModExtension<DefModExtension_IgnoreWorkTablePenalties>();
    }
}