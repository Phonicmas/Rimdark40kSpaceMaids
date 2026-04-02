using System.Collections.Generic;
using System.Linq;
using Genes40k;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI.Group;

namespace NyanDark40k;

public class Building_PicnicWorktableDrop : Building_WorkTable
{
    private bool hasDoneThing;

    private Lord lord;

    private const string LeaveSignal = "BEWH_PicnicLeaveSignal";

    [Unsaved]
    private Graphic cachedOpenGraphic;
        
    private Graphic OpenGraphic =>
        cachedOpenGraphic ??= GraphicDatabase.Get<Graphic_Single>(def.GetModExtension<DefModExtension_DropPod>().openGraphic, def.graphicData.shaderType.Shader,
            def.graphicData.drawSize, DrawColor, Color.white, DefaultGraphic.data, def.GetModExtension<DefModExtension_DropPod>().openGraphicMask);
        
    public override Graphic Graphic => hasDoneThing ? OpenGraphic : DefaultGraphic;

    protected override void TickInterval(int delta)
    {
        base.TickInterval(delta);
        if (hasDoneThing || !Spawned || Map == null || !this.IsHashIntervalTick(125, delta))
        {
            return;
        }
        
        hasDoneThing = true;

        OnDropPodOpen();
    }

    protected virtual void OnDropPodOpen()
    {
        var possibleCells = GenRadial.RadialCellsAround(Position, 10, useCenter: true)
            .Where(c => c.InBounds(Map) && !c.Fogged(Map) && c.GetEdifice(Map) == null).ToList();

        var newPossibleCells = new List<IntVec3>();
        
        foreach (var cell in possibleCells)
        {
            var northLoc = new IntVec3(cell.x, cell.y, cell.z + 1);
            var southLoc = new IntVec3(cell.x, cell.y, cell.z - 1);
            var eastLoc = new IntVec3(cell.x + 1, cell.y, cell.z);
            var westLoc = new IntVec3(cell.x - 1, cell.y, cell.z);
            if (!northLoc.InBounds(Map) || northLoc.Fogged(Map) || northLoc.GetEdifice(Map) != null)
            {
                continue;
            }
            if (!southLoc.InBounds(Map) || southLoc.Fogged(Map) || southLoc.GetEdifice(Map) != null)
            {
                continue;
            }
            if (!eastLoc.InBounds(Map) || eastLoc.Fogged(Map) || eastLoc.GetEdifice(Map) != null)
            {
                continue;
            }
            if (!westLoc.InBounds(Map) || westLoc.Fogged(Map) || westLoc.GetEdifice(Map) != null)
            {
                continue;
            }
            newPossibleCells.Add(cell);
        }

        if (newPossibleCells.Count <= 0)
        {
            return;
        }
        
        var cellsToSpawn = new List<IntVec3>();
        var initialCell = newPossibleCells.Where(c => c.GetEdifice(Map) == null).RandomElement();
        
        cellsToSpawn.Add(initialCell);
        newPossibleCells.Remove(initialCell);
        
        for (var i = 0; i < 4; i++)
        {
            var spawnCell = newPossibleCells.Where(c => cellsToSpawn.All(c2 => c2.DistanceTo(c) > 5)).RandomElement();
            cellsToSpawn.Add(spawnCell);
            newPossibleCells.Remove(spawnCell);
        }

        foreach (var cell in cellsToSpawn)
        {
            var projectile = (Projectile)GenSpawn.Spawn(NyanDark40kDefOf.BEWH_PicnicProjectile, Position, Map);
            projectile.Launch(this, Position.ToVector3(), cell, cell, ProjectileHitFlags.All, true);
        }
    }
    
    public override void Destroy(DestroyMode mode = DestroyMode.Vanish)
    {
        lord?.Notify_SignalReceived(new Signal(LeaveSignal));
        base.Destroy(mode);
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref hasDoneThing, "hasSpawnedMarines");
        Scribe_References.Look(ref lord, "lord");
    }
}