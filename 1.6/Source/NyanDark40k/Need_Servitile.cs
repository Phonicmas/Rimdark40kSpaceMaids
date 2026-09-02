using System.Collections.Generic;
using RimWorld;
using Verse;

namespace NyanDark40k;

public class Need_Servitile : Need
{
    public const float UnservedThreshold = 0.15f;

    public const float IdleThreshold = 0.35f;

    public const float ServingThreshold = 0.65f;

    public const float WellServingThreshold = 0.9f;

    private const float IntervalsPerDay = 400f;

    public Need_Servitile(Pawn newPawn) : base(newPawn)
    {
        threshPercents =
        [
            UnservedThreshold,
            IdleThreshold,
            ServingThreshold,
            WellServingThreshold,
        ];
    }

    public override void NeedInterval()
    {
        if (!IsFrozen)
        {
            CurLevel -= def.fallPerDay / IntervalsPerDay;
        }
    }
}
