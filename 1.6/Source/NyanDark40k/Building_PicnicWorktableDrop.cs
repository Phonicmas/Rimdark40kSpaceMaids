using System.Collections.Generic;
using System.Linq;
using Genes40k;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace NyanDark40k;

public class Building_PicnicWorktableDrop : Building_WorkTable
{
    private const int TableCount = 5;

    private const int SearchRadius = 10;

    private const float PreferredTableSpacing = 5f;

    private const float FallbackTableSpacing = 2f;

    private bool hasDoneThing;

    [Unsaved]
    private Graphic cachedOpenGraphic;

    private DefModExtension_DropPod DropPodExtension => def.GetModExtension<DefModExtension_DropPod>();

    private Graphic OpenGraphic
    {
        get
        {
            if (cachedOpenGraphic != null)
            {
                return cachedOpenGraphic;
            }

            var extension = DropPodExtension;
            if (extension?.openGraphic == null)
            {
                return DefaultGraphic;
            }

            cachedOpenGraphic = GraphicDatabase.Get<Graphic_Single>(extension.openGraphic, def.graphicData.shaderType.Shader,
                def.graphicData.drawSize, DrawColor, Color.white, DefaultGraphic.data, extension.openGraphicMask);

            return cachedOpenGraphic;
        }
    }

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
        DropPodExtension?.openSound?.PlayOneShot(new TargetInfo(Position, Map));

        var possibleCells = GenRadial.RadialCellsAround(Position, SearchRadius, useCenter: true)
            .Where(CellCanHoldTable).ToList();

        foreach (var cell in SelectTableCells(possibleCells))
        {
            var projectile = GenSpawn.Spawn(NyanDark40kDefOf.BEWH_PicnicProjectile, Position, Map) as Projectile;
            projectile?.Launch(this, Position.ToVector3(), cell, cell, ProjectileHitFlags.All, true);
        }
    }

    /// <summary>
    /// A table needs its own cell plus all four orthogonal neighbours, so the stools have somewhere to land.
    /// </summary>
    private bool CellCanHoldTable(IntVec3 cell)
    {
        return CellIsFree(cell)
               && CellIsFree(cell + IntVec3.North)
               && CellIsFree(cell + IntVec3.South)
               && CellIsFree(cell + IntVec3.East)
               && CellIsFree(cell + IntVec3.West);
    }

    private bool CellIsFree(IntVec3 cell)
    {
        return cell.InBounds(Map) && !cell.Fogged(Map) && cell.GetEdifice(Map) == null;
    }

    /// <summary>
    /// Picks up to TableCount spread out cells, relaxing the spacing rather than giving up when the area is cramped.
    /// </summary>
    private static List<IntVec3> SelectTableCells(List<IntVec3> possibleCells)
    {
        var cellsToSpawn = new List<IntVec3>();

        while (cellsToSpawn.Count < TableCount && possibleCells.Count > 0)
        {
            var spacing = cellsToSpawn.Count <= 0 ? 0f : PreferredTableSpacing;

            if (!TryPickCell(possibleCells, cellsToSpawn, spacing, out var cell)
                && !TryPickCell(possibleCells, cellsToSpawn, FallbackTableSpacing, out cell))
            {
                break;
            }

            cellsToSpawn.Add(cell);
            possibleCells.Remove(cell);
        }

        return cellsToSpawn;
    }

    private static bool TryPickCell(List<IntVec3> possibleCells, List<IntVec3> chosenCells, float minSpacing, out IntVec3 cell)
    {
        return possibleCells.Where(c => chosenCells.All(c2 => c2.DistanceTo(c) > minSpacing)).TryRandomElement(out cell);
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref hasDoneThing, "hasSpawnedMarines");
    }
}
