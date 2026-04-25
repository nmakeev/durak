using System.Collections.Generic;

namespace Game.Core.State
{
    public class RoundState
    {
        public PlayerId DefenderId;
        public PlayerId AttackerId;
        public List<TableSlot> TableSlots = new();
    }
}