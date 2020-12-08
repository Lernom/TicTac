using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TicTacGame
{
    public class RandomAIPlayer : APlayer
    {
        private Vector2Int _size;

        public override IEnumerator MakeTurn()
        {            
            SelectedCell = new Vector2Int(Random.Range(0, _size.x), Random.Range(0, _size.y));
            yield return new WaitForSeconds(0.3f);
            yield break;
        }

        public override void Setup(AGameModule module)
        {
            _size = module.FieldSize;
        }
    }
}