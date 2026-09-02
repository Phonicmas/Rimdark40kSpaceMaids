using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Verse;
using Verse.AI;

namespace NyanDark40k;

[HarmonyPatch]
public class NekonaFeedStopMentalBreak
{
    /// <summary>
    /// TryStart is virtual and several workers override it, so every declared implementation has to be
    /// patched rather than just the one on MentalBreakWorker itself.
    /// </summary>
    public static IEnumerable<MethodBase> TargetMethods()
    {
        var parameters = new[] { typeof(Pawn), typeof(string), typeof(bool) };

        var baseMethod = AccessTools.DeclaredMethod(typeof(MentalBreakWorker), nameof(MentalBreakWorker.TryStart), parameters);
        if (baseMethod != null)
        {
            yield return baseMethod;
        }

        foreach (var type in typeof(MentalBreakWorker).AllSubclasses())
        {
            var method = AccessTools.DeclaredMethod(type, nameof(MentalBreakWorker.TryStart), parameters);
            if (method != null)
            {
                yield return method;
            }
        }
    }

    public static bool Prefix(ref bool __result, MentalBreakWorker __instance, Pawn pawn, string reason, bool causedByMood)
    {
        var nekonaThought = ThoughtNekonaFeed.ActiveOn(pawn);
        if (nekonaThought == null)
        {
            return true;
        }

        nekonaThought.SetMentalBreak(__instance.def, reason, causedByMood);
        __result = false;
        return false;
    }
}
