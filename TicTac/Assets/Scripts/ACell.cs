using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TicTacGame
{
    public abstract class ACell : MonoBehaviour, IPointerClickHandler
    {
        public int CellOwner;

        public Vector2Int Id;

        public Action<Vector2Int> CellClicked;

        public void Setup(Vector2Int id)
        {
            Id = id;
            SetupInternal(id);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            CellClicked?.Invoke(Id);
        }

        public void SetOwner(int playerId)
        {
            CellOwner = playerId;
            SetOwnerInternal(playerId);
        }

        protected virtual void SetupInternal(Vector2Int id)
        {

        }

        protected virtual void SetOwnerInternal(int playerId)
        {

        }
    }
}