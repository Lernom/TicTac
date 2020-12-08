using System;
using UnityEngine;

namespace TicTacGame
{
    [CreateAssetMenu(fileName = "TicTacModule.asset",menuName = "Tic Tac Toe Game Module",order = 0)]
    public class TicTacGameModule : AGameModule
    {
        public int WinningRowLength = 3;

        [SerializeField]
        private string[] _playerLabels;

        private ACell[,] _cells;

        private Vector2Int[] _directions;

        public override bool CheckWinningCondition(Vector2Int cellId)
        {
            var cell = _cells[cellId.x, cellId.y];
            var owner = cell.CellOwner;
            for (int i = 0; i < _directions.Length; i++)
            {
                var directionToCheck = _directions[i];
                var step = 1;
                var streak = 1;
                bool nextSame = true, prevSame = true;
                while (true)
                {
                    if (nextSame)
                    {
                        var next = cellId + directionToCheck * step;
                        nextSame = SameOwner(cell, next);
                    }
                    if (prevSame)
                    {
                        var prev = cellId - directionToCheck * step;
                        prevSame = SameOwner(cell, prev);
                    }

                    if (!nextSame && !prevSame)
                        break;

                    if (nextSame) streak++;
                    if (prevSame) streak++;
                    if (streak >= WinningRowLength)
                        return true;
                    step++;
                }
            }
            return false;
        }

        private bool SameOwner(ACell cellBase, Vector2Int cellidToCheck)
        {
            if (IsValidId(cellidToCheck))
            {
                var nextCell = _cells[cellidToCheck.x, cellidToCheck.y];
                if (nextCell.CellOwner == cellBase.CellOwner)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsValidId(Vector2Int cellid)
        {
            return cellid.x >= 0 && cellid.x < _cells.GetLength(0) && cellid.y >= 0 && cellid.y < _cells.GetLength(1);
        }

        public override bool PlayerSelectedCell(Vector2Int cellId, int playerId)
        {
            var cell = _cells[cellId.x, cellId.y];
            if (cell.CellOwner >= 0)
                return false;
            _cells[cellId.x, cellId.y].SetOwner(playerId);
            return true;
        }

        public override bool AnyEligibleMoves()
        {
            for (int x = 0; x < _cells.GetLength(0); x++)
            {
                for (int y = 0; y < _cells.GetLength(1); y++)
                {
                    if (_cells[x, y].CellOwner < 0)
                        return true;
                }
            }
            return false;
        }

        public string GetPlayerLabel(int playerId)
        {
            if (playerId < 0)
                return "";
            return (_playerLabels[playerId]);
        }

        public override void SetupGame()
        {
            _directions = new Vector2Int[] { new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(-1, 1), new Vector2Int(0, 1) };

            if (_cells != null)
            {
                for (int x = 0; x < _cells.GetLength(0); x++)
                {
                    for (int y = 0; y < _cells.GetLength(1); y++)
                    {
                        var cell = _cells[x, y];
                        cell.CellClicked -= CellClicked;
                        GameObject.Destroy(cell.gameObject);
                    }
                }
            }

            _cells = new ACell[FieldSize.x, FieldSize.y];
            for (int x = 0; x < _cells.GetLength(0); x++)
            {
                for (int y = 0; y < _cells.GetLength(1); y++)
                {
                    var newCell = GameObject.Instantiate(CellPrefab);
                    newCell.transform.position = new Vector3(x - _cells.GetLength(0) / 2, 0, y - _cells.GetLength(1) / 2);
                    newCell.Setup(new Vector2Int(x, y));
                    newCell.CellClicked += CellClicked;
                    newCell.SetOwner(-1);
                    _cells[x, y] = newCell;
                }
            }
        }

        private void CellClicked(Vector2Int cellId)
        {
            OnCellClicked?.Invoke(cellId);
        }

      
    }
}