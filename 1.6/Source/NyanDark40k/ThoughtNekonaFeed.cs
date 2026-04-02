using RimWorld;
using Verse;

namespace NyanDark40k;

public class ThoughtNekonaFeed : Thought_Memory
{
    private MentalBreakDef mentalBreak;

    private MentalStateDef stateDef;

    private string reason;

    private bool causedByMood;

    private Pawn mentalOtherPawn;

    private bool transitionSilently;

    private bool causedByDamage;

    private bool causedByPsycast;

    public void SetMentalState(MentalStateDef stateDef, string reason = null, bool causedByMood = false, Pawn mentalOtherPawn = null, bool transitionSilently = false, bool causedByDamage = false, bool causedByPsycast = false)
    {
        this.reason = reason;
        this.stateDef = stateDef;
        mentalBreak = null;
        this.causedByMood = causedByMood;
        this.mentalOtherPawn = mentalOtherPawn;
        this.transitionSilently = transitionSilently;
        this.causedByDamage = causedByDamage;
        this.causedByPsycast = causedByPsycast;
    }

    public void SetMentalBreak(MentalBreakDef mentalBreak, string reason, bool causedByMood)
    {
        this.reason = reason;
        this.mentalBreak = mentalBreak;
        this.causedByMood = causedByMood;
        stateDef = null;
        mentalOtherPawn = null;
        transitionSilently = false;
        causedByDamage = false;
        causedByPsycast = false;
    }

    public void TryDoMentalBreak()
    {
        if (stateDef != null)
        {
            pawn.mindState.mentalStateHandler.TryStartMentalState(stateDef, reason, forced: false, forceWake: false, causedByMood: causedByMood, mentalOtherPawn, transitionSilently, causedByDamage, causedByPsycast);
        }
        else if (mentalBreak != null)
        {
            mentalBreak.Worker.TryStart(pawn, reason, causedByMood);
        }
        else
        {
            return;
        }
        ResetData();
    }

    private void ResetData()
    {
        stateDef = null;
        mentalBreak = null;
        reason = null;
        causedByMood = false;
        mentalOtherPawn = null;
        transitionSilently = false;
        causedByDamage = false;
        causedByPsycast = false;
    }
    
    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Defs.Look(ref mentalBreak, "mentalBreak");
        Scribe_Defs.Look(ref stateDef, "stateDef");
        Scribe_Values.Look(ref reason, "reason");
        Scribe_Values.Look(ref causedByMood, "causedByMood", false);
        Scribe_References.Look(ref mentalOtherPawn, "mentalOtherPawn");
        Scribe_Values.Look(ref transitionSilently, "transitionSilently", false);
        Scribe_Values.Look(ref causedByDamage, "causedByDamage", false);
        Scribe_Values.Look(ref causedByPsycast, "causedByPsycast", false);
    }
}