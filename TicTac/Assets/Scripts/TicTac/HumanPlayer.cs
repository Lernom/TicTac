using System.Collections;
using UnityEngine;

namespace TicTacGame
{
    public class HumanPlayer : APlayer
    {
        private AGameModule _module;

        private bool _done = false;

        public override IEnumerator MakeTurn()
        {
            var size = GameManager.Instance.GameModule.FieldSize;
            _done = false;
            while (!_done)
            {
                yield return null;
            }
            yield break;
        }

        public override void Setup(AGameModule module)
        {
            if (module != _module)
            {
                _module = module;
                _module.OnCellClicked += CellClicked;
            }
        }

        private void CellClicked(Vector2Int obj)
        {
            SelectedCell = obj;
            _done = true;
        }
    }
}