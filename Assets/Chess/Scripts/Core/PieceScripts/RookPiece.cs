using Chess.Scripts.Core;
using System.Collections.Generic;
using UnityEngine;

public class RookPiece : BasePiece
{
    public override List<(int, int)> GetLegalMoves()
    {
        var possibleMoves = new List<(int, int)>();

        possibleMoves.AddRange(ExplorePath(1, 0));    
        possibleMoves.AddRange(ExplorePath(-1, 0));   
        possibleMoves.AddRange(ExplorePath(0, 1));    
        possibleMoves.AddRange(ExplorePath(0, -1));   

        return possibleMoves;
    }

    private List<(int, int)> ExplorePath(int rowIncrement, int colIncrement)
    {
        var pathMoves = new List<(int, int)>();
        int targetRow = pieceRow;
        int targetCol = pieceColumn;

        while (true)
        {
            targetRow += rowIncrement;
            targetCol += colIncrement;

            if (!CheckBounds(targetRow, targetCol)) break;

            if (ChessBoardPlacementHandler.Instance.IsTileOccupied(targetRow, targetCol, out var blockingPiece))
            {
                if (blockingPiece.isWhiteSide != isWhiteSide)
                {
                    pathMoves.Add((targetRow, targetCol)); 
                }
                break;
            }

            pathMoves.Add((targetRow, targetCol));
        }

        return pathMoves;
    }
}
