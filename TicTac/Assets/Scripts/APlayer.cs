using System;
using System.Collections;
using UnityEngine;

namespace TicTacGame
{
    public abstract class APlayer
    {
        public Vector2Int SelectedCell { get; protected set; }

        public abstract IEnumerator MakeTurn();

        public abstract void Setup(AGameModule module);
    }
}


