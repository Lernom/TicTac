using System;
using UnityEngine;

namespace TicTacGame
{
    public class TicTacCell : ACell
    {
        [SerializeField]
        private Animator _cellAnimator;

        [SerializeField]
        private string _cellChangeOwnerTrigger = "triggered";

        [SerializeField]
        private TMPro.TMP_Text _ownerLabel;

        private TicTacGameModule _module;

        protected override void SetupInternal(Vector2Int id)
        {
            _module = GameManager.Instance.GameModule as TicTacGameModule;
            if (!_module)
            {
                throw new Exception("[TIC TAC] Cells are designed to work only for TicTacGameModule");
            }
        }

        protected override void SetOwnerInternal(int playerId)
        {
            _cellAnimator.SetTrigger(_cellChangeOwnerTrigger);
        }

        public void UpdateOwnerText()
        {
            _ownerLabel.text = _module.GetPlayerLabel(CellOwner);
        }
    }
}