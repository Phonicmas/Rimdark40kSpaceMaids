using HarmonyLib;
using RimWorld;
using Verse;

namespace NyanDark40k;

[HarmonyPatch(typeof(StatPart_WorkTableTemperature), "Applies", [
    typeof(ThingDef),
    typeof(Map),
    typeof(IntVec3)
], [
    ArgumentType.Normal,
    ArgumentType.Normal,
    ArgumentType.Normal,
])]
public class HarmonyPatch_WorkTableTemperaturePatch
{
    public static bool Prefix(ref bool __result, ThingDef tDef)
    {
        return !tDef.HasModExtension<DefModExtension_IgnoreWorkTablePenalties>();
    }
}