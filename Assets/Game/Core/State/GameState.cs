using System.Collections.Generic;
using Game.Core.Cards;

namespace Game.Core.State
{
    public class GameState
    {
        public List<PlayerState> Players;
        public List<Card> DrawPile;
        public List<Card> DiscardPile;
        public PlayerId CurrentPlayerId;
        public List<TableSlot> TableSlots;

        public Suit TrumpSuit;
    }
}