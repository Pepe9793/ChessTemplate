using System.Collections.Generic;
using UnityEngine;

public class KnightPiece : BasePiece
{
    public override List<(int, int)> GetLegalMoves()
    {
        var possibleMoves = new List<(int, int)>();

        possibleMoves.AddRange(VerifyKnightMove(2, 1));
        possibleMoves.AddRange(VerifyKnightMove(2, -1));
        possibleMoves.AddRange(VerifyKnightMove(-2, 1));
        possibleMoves.AddRange(VerifyKnightMove(-2, -1));
        possibleMoves.AddRange(VerifyKnightMove(1, 2));
        possibleMoves.AddRange(VerifyKnightMove(1, -2));
        possibleMoves.AddRange(VerifyKnightMove(-1, 2));
        possibleMoves.AddRange(VerifyKnightMove(-1, -2));

        return possibleMoves;
    }

    private List<(int, int)> VerifyKnightMove(int rowShift, int colShift)
    {
        var moveOptions = new List<(int, int)>();

        int targetRow = pieceRow + rowShift;
        int targetCol = pieceColumn + colShift;

        if (CheckBounds(targetRow, targetCol) &&
            (IsTileClear(targetRow, targetCol) || HasEnemyPiece(targetRow, targetCol)))
        {
            moveOptions.Add((targetRow, targetCol));
        }

        return moveOptions;
    }
}

