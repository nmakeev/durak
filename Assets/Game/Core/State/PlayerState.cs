using System.Collections.Generic;
using Game.Core.Cards;

namespace Game.Core.State
{
    public class PlayerState
    {
        public PlayerId Id;
        public List<Card> Cards;
    }
}