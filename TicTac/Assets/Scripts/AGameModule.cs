using System;
using UnityEngine;

namespace TicTacGame
{
    public abstract class AGameModule : ScriptableObject
    {
        public ACell CellPrefab;

        public Vector2Int FieldSize;

        public Action<Vector2Int> OnCellClicked;

        public abstract void SetupGame();

        public abstract bool PlayerSelectedCell(Vector2Int cellId, int playerId);

        public abstract bool CheckWinningCondition(Vector2Int changedObject);

        public abstract bool AnyEligibleMoves();
    }
}
