using System.Collections.Generic;
using UnityEngine;

public class KingPiece : BasePiece
{
    public override List<(int, int)> GetLegalMoves()
    {
        var possibleMoves = new List<(int, int)>();
        int[][] moveDirections = {
            new int[] { 1, 0 }, new int[] { -1, 0 },
            new int[] { 0, 1 }, new int[] { 0, -1 },
            new int[] { 1, 1 }, new int[] { 1, -1 },
            new int[] { -1, 1 }, new int[] { -1, -1 }
        };

        foreach (var moveDir in moveDirections)
        {
            int targetRow = pieceRow + moveDir[0];
            int targetCol = pieceColumn + moveDir[1];
            if (CheckBounds(targetRow, targetCol) && (IsTileClear(targetRow, targetCol) || HasEnemyPiece(targetRow, targetCol)))
            {
                possibleMoves.Add((targetRow, targetCol));
            }
        }

        return possibleMoves;
    }
}
