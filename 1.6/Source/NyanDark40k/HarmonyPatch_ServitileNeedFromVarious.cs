using HarmonyLib;
using Verse;
using Verse.AI;

namespace NyanDark40k;

[HarmonyPatch(typeof(JobDriver), "Cleanup")]
public class ServitileNeedFromVariousPatch
{
    public static void Postfix(JobCondition condition, Job ___job, Pawn ___pawn)
    {
        if (condition is not JobCondition.Succeeded)
        {
            return;
        }

        var gene = ___pawn?.genes?.GetFirstGeneOfType<Gene_SpaceMaidsServitile>();
        if (gene == null)
        {
            return;
        }

        var defMod = gene.def.GetModExtension<DefModExtension_ServitileNeedFill>();
        if (defMod == null)
        {
            return;
        }

        var need = ___pawn.needs.TryGetNeed<Need_Servitile>();
        if (need == null)
        {
            return;
        }

        if (defMod.jobDefs.Contains(___job.def))
        {
            need.CurLevelPercentage += 0.05f;
        }
    }
}