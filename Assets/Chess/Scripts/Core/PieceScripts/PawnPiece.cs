using Chess.Scripts.Core;
using System.Collections.Generic;
using UnityEngine;

public class PawnPiece : BasePiece
{
    public override List<(int, int)> GetLegalMoves()
    {
        var possibleMoves = new List<(int, int)>();
        int moveDirection = isWhiteSide ? -1 : 1;

        int stepRow = pieceRow + moveDirection;
        if (CheckBounds(stepRow, pieceColumn) && IsTileClear(stepRow, pieceColumn))
        {
            possibleMoves.Add((stepRow, pieceColumn));

            int startRow = isWhiteSide ? 1 : 6;
            if (pieceRow == startRow && IsTileClear(stepRow + moveDirection, pieceColumn))
            {
                possibleMoves.Add((stepRow + moveDirection, pieceColumn));
            }
        }

        AddDiagonalCaptures(possibleMoves, stepRow, pieceColumn + 1);
        AddDiagonalCaptures(possibleMoves, stepRow, pieceColumn - 1);

        Debug.Log($"Pawn at ({pieceRow}, {pieceColumn}) can move to: {string.Join(", ", possibleMoves)}");

        return possibleMoves;
    }

    private void AddDiagonalCaptures(List<(int, int)> possibleMoves, int targetRow, int targetCol)
    {
        if (CheckBounds(targetRow, targetCol))
        {
            var tileObject = ChessBoardPlacementHandler.Instance.GetTile(targetRow, targetCol);
            if (tileObject != null)
            {
                ChessPlayerPlacementHandler occupantPiece = tileObject.GetComponent<ChessPlayerPlacementHandler>();
                if (occupantPiece != null && occupantPiece.isWhiteSide != isWhiteSide)
                {
                    possibleMoves.Add((targetRow, targetCol));
                    Debug.Log($"Pawn can capture opponent at ({targetRow}, {targetCol})");
                }
                else if (occupantPiece != null)
                {
                    Debug.Log($"Tile ({targetRow}, {targetCol}) is occupied by friendly piece.");
                }
            }
            else
            {
                Debug.Log($"Tile ({targetRow}, {targetCol}) does not exist.");
            }
        }
        else
        {
            Debug.Log($"Tile ({targetRow}, {targetCol}) is out of bounds.");
        }
    }
}

