using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace NyanDark40k;

public class DefModExtension_ServitileNeedFill : DefModExtension
{
    public List<JobDef> jobDefs = [];

    public float fillPerJob = 0.01f;

    public int referenceJobTicks = 600;

    public FloatRange durationFactorRange = new FloatRange(0.25f, 2f);

    /// <summary>
    /// Scales the reward by how long the job actually took, so hauling a single item is not worth as much
    /// as cooking a meal.
    /// </summary>
    public float FillForJob(int jobTicks)
    {
        if (referenceJobTicks <= 0)
        {
            return fillPerJob;
        }

        var factor = Mathf.Clamp(jobTicks / (float)referenceJobTicks, durationFactorRange.min, durationFactorRange.max);

        return fillPerJob * factor;
    }
}
