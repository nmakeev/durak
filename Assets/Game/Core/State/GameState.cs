using System.Collections.Generic;
using Game.Core.Cards;

namespace Game.Core.State
{
    public class GameState
    {
        public List<PlayerState> Players;
        public List<Card> DrawPile;
        public List<Card> DiscardPile;
        public Suit TrumpSuit;

        public RoundState RoundState;

        public List<PlayerId> AllowedPlayers;
    }
}