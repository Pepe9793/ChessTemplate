using System.Collections.Generic;
using UnityEngine;

public class QueenPiece : BasePiece
{
    public override List<(int, int)> GetLegalMoves()
    {
        var possibleMoves = new List<(int, int)>();

        possibleMoves.AddRange(ScanPath(1, 0));
        possibleMoves.AddRange(ScanPath(-1, 0));
        possibleMoves.AddRange(ScanPath(0, 1));
        possibleMoves.AddRange(ScanPath(0, -1));
        possibleMoves.AddRange(ScanPath(1, 1));
        possibleMoves.AddRange(ScanPath(1, -1));
        possibleMoves.AddRange(ScanPath(-1, 1));
        possibleMoves.AddRange(ScanPath(-1, -1));

        return possibleMoves;
    }

    private List<(int, int)> ScanPath(int rowStep, int colStep)
    {
        var pathMoves = new List<(int, int)>();
        int currentRow = pieceRow;
        int currentCol = pieceColumn;

        while (true)
        {
            currentRow += rowStep;
            currentCol += colStep;

            if (!CheckBounds(currentRow, currentCol)) break;

            if (ChessBoardPlacementHandler.Instance.IsTileOccupied(currentRow, currentCol, out var blockingPiece))
            {
                if (blockingPiece.isWhiteSide != isWhiteSide)
                {
                    pathMoves.Add((currentRow, currentCol));
                }
                break;
            }
            pathMoves.Add((currentRow, currentCol));
        }

        return pathMoves;
    }
}
