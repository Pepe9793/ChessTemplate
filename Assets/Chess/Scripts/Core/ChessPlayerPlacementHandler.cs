using System;
using UnityEngine;

namespace Chess.Scripts.Core {
    public class ChessPlayerPlacementHandler : MonoBehaviour
    {
        public int row, column;
        public bool isWhiteSide;

        private void Start()
        {
            transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
        }

        private void Update()
        {
            transform.position = ChessBoardPlacementHandler.Instance.GetTile(row,column).transform.position;
        }

        public bool IsPlayerWhite()
        {
            return isWhiteSide;
        }
    }
}