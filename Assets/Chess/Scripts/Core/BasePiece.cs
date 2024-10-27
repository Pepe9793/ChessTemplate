using Chess.Scripts.Core;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePiece : MonoBehaviour
{
    public int pieceRow, pieceColumn;
    public bool isWhiteSide;

    public abstract List<(int, int)> GetLegalMoves();

    public void ShowMoves()
    {
        ChessBoardPlacementHandler.Instance.ClearHighlights();

        foreach (var (targetRow, targetCol) in GetLegalMoves())
        {
            var targetTile = ChessBoardPlacementHandler.Instance.GetTile(targetRow, targetCol);
            if (targetTile == null) continue;

            if (ChessBoardPlacementHandler.Instance.IsTileOccupied(targetRow, targetCol, out var detectedPiece))
            {
                if (detectedPiece.isWhiteSide != isWhiteSide)
                {
                    ChessBoardPlacementHandler.Instance.HighlightRed(targetRow, targetCol);
                }
            }
            else
            {
                ChessBoardPlacementHandler.Instance.Highlight(targetRow, targetCol);
            }
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Piece selected: Highlighting moves");
        ShowMoves();
    }

    protected bool CheckBounds(int row, int col)
    {
        return row >= 0 && row < 8 && col >= 0 && col < 8;
    }

    protected bool IsTileClear(int row, int col)
    {
        var tile = ChessBoardPlacementHandler.Instance.GetTile(row, col);
        return tile != null && tile.GetComponent<ChessPlayerPlacementHandler>() == null;
    }

    protected bool HasEnemyPiece(int row, int col)
    {
        var piece = ChessBoardPlacementHandler.Instance.GetTile(row, col)?.GetComponent<ChessPlayerPlacementHandler>();
        return piece != null && piece.isWhiteSide != isWhiteSide;
    }

    protected IEnumerable<(int, int)> ScanDirection(int rowChange, int colChange)
    {
        var moveList = new List<(int, int)>();
        int currentRow = pieceRow + rowChange;
        int currentCol = pieceColumn + colChange;

        while (CheckBounds(currentRow, currentCol))
        {
            if (ChessBoardPlacementHandler.Instance.IsTileOccupied(currentRow, currentCol, out var blockingPiece))
            {
                if (blockingPiece.isWhiteSide != isWhiteSide)
                {
                    moveList.Add((currentRow, currentCol));
                }
                break;
            }
            moveList.Add((currentRow, currentCol));

            currentRow += rowChange;
            currentCol += colChange;
        }

        return moveList;
    }
}
