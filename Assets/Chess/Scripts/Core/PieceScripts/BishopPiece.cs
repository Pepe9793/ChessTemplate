using System.Collections.Generic;
using UnityEngine;

public class BishopPiece : BasePiece
{
    public override List<(int, int)> GetLegalMoves()
    {
        var possibleMoves = new List<(int, int)>();

        possibleMoves.AddRange(ScanDirection(1, 1));
        possibleMoves.AddRange(ScanDirection(1, -1));
        possibleMoves.AddRange(ScanDirection(-1, 1));
        possibleMoves.AddRange(ScanDirection(-1, -1));

        return possibleMoves;
    }

    private new List<(int, int)> ScanDirection(int rowStep, int colStep)
    {
        var directionalMoves = new List<(int, int)>();
        int tempRow = pieceRow;
        int tempCol = pieceColumn;

        while (true)
        {
            tempRow += rowStep;
            tempCol += colStep;

            if (!CheckBounds(tempRow, tempCol)) break;

            if (ChessBoardPlacementHandler.Instance.IsTileOccupied(tempRow, tempCol, out var blockingPiece))
            {
                if (blockingPiece.isWhiteSide != isWhiteSide)
                {
                    directionalMoves.Add((tempRow, tempCol));
                }
                break;
            }
            directionalMoves.Add((tempRow, tempCol));
        }

        return directionalMoves;
    }
}
