using Verse;

namespace NyanDark40k;

public class DamageWorker_ForceFeed : DamageWorker_AddInjury
{
    public override DamageResult Apply(DamageInfo dinfo, Thing thing)
    {
		if (thing is Pawn pawn)
		{
			pawn.needs.food.CurLevelPercentage += 0.05f;
		}
		
		return base.Apply(dinfo, thing);
	}

	protected override BodyPartRecord ChooseHitPart(DamageInfo dinfo, Pawn pawn)
	{
		return pawn.health.hediffSet.GetBodyPartRecord(NyanDark40kDefOf.Jaw);
	}
}