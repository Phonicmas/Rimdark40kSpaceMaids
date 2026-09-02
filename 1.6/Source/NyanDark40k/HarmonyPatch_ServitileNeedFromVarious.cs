using System.Collections.Generic;
using Verse;
using HarmonyLib;
using Verse.AI;

namespace NyanDark40k;

[HarmonyPatch(typeof(JobDriver), "Cleanup")]
public class ServitileNeedFromVariousPatch
{
    public static void Postfix(JobCondition condition, Job ___job, Pawn ___pawn, int ___startTick)
    {
        if (condition is not JobCondition.Succeeded || ___job?.def == null)
        {
            return;
        }

        if (!ServitileNeedFillCache.IsFillingJob(___job.def))
        {
            return;
        }

        var need = ___pawn?.needs?.TryGetNeed<Need_Servitile>();
        if (need == null)
        {
            return;
        }

        var gene = ___pawn.genes?.GetFirstGeneOfType<Gene_SpaceMaidsServitile>();
        var defMod = gene?.def.GetModExtension<DefModExtension_ServitileNeedFill>();
        if (defMod == null || !defMod.jobDefs.Contains(___job.def))
        {
            return;
        }

        var jobTicks = ___startTick < 0 ? 0 : Find.TickManager.TicksGame - ___startTick;

        need.CurLevelPercentage += defMod.FillForJob(jobTicks);
    }
}

/// <summary>
/// Every job def that any gene fills the devotion need from, so the patch can reject the vast majority of
/// job completions without touching genes or needs at all.
/// </summary>
[StaticConstructorOnStartup]
public static class ServitileNeedFillCache
{
    private static readonly HashSet<JobDef> FillingJobs = new HashSet<JobDef>();

    static ServitileNeedFillCache()
    {
        foreach (var geneDef in DefDatabase<GeneDef>.AllDefsListForReading)
        {
            var defMod = geneDef.GetModExtension<DefModExtension_ServitileNeedFill>();
            if (defMod?.jobDefs == null)
            {
                continue;
            }

            foreach (var jobDef in defMod.jobDefs)
            {
                if (jobDef != null)
                {
                    FillingJobs.Add(jobDef);
                }
            }
        }
    }

    public static bool IsFillingJob(JobDef jobDef)
    {
        return FillingJobs.Contains(jobDef);
    }
}
