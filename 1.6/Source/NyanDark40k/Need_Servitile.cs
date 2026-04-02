using RimWorld;
using Verse;

namespace NyanDark40k;

public class Need_Servitile : Need
{
    public Need_Servitile(Pawn newPawn) : base(newPawn)
    {
    }

    public override void NeedInterval()
    {
        if (!IsFrozen)
        {
            CurLevel -= 8.333333E-05f;
        }
    }
}