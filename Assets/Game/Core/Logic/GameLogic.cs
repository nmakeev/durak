using System;
using System.Collections.Generic;
using Game.Core.Cards;
using Game.Core.Commands;
using Game.Core.State;
using Game.Extensions;

namespace Game.Core.Logic
{
    public class GameLogic
    {
        private const int TargetHandSize = 6;
        
        public GameState GameState;

        public GameLogic()
        {
            GameState = new GameState();

            var drawPile = new List<Card>();
            var suits = (Suit[])Enum.GetValues(typeof(Suit));
            var ranks = (Rank[])Enum.GetValues(typeof(Rank));
            
            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    var card = new Card(suit, rank);
                    drawPile.Add(card);
                }
            }

            drawPile.Shuffle();

            GameState.DiscardPile = new List<Card>();
            GameState.DrawPile = drawPile;
            GameState.CurrentPlayerId = PlayerId.P1;
            
            GameState.Players = new List<PlayerState>();
            var firstPlayer = new PlayerState
            {
                Id = PlayerId.P1,
                Cards = new List<Card>()
            };
            DrawCards(drawPile, firstPlayer.Cards, TargetHandSize);

            var secondPlayer = new PlayerState
            {
                Id = PlayerId.P2,
                Cards = new List<Card>()
            };
            DrawCards(drawPile, secondPlayer.Cards, TargetHandSize);
            GameState.Players.Add(firstPlayer);
            GameState.Players.Add(secondPlayer);
            
            GameState.TableSlots = new List<TableSlot>();

            GameState.TrumpSuit = DecideTrumpSuit();
            GameState.CurrentPlayerId = FindStartingPlayer(GameState);
        }

        private void ApplyCommands(IGameCommand command)
        {
            
        }

        private Suit DecideTrumpSuit()
        {
            return GameState.DrawPile[0].Suit;
        }

        private static PlayerId FindStartingPlayer(GameState gameState)
        {
            var playerWithLowestTrump = FindPlayerWithLowestTrump(gameState.Players, gameState.TrumpSuit);
            return playerWithLowestTrump ?? FindPlayerWithLowestRankCard(gameState.Players);
        }

        private static PlayerId? FindPlayerWithLowestTrump(List<PlayerState> players, Suit trumpSuit)
        {
            Card? minCard = null;
            PlayerId? playerWithMinCard = null;
            
            foreach (var player in players)
            {
                var card = FindLowestCardOfSuit(player, trumpSuit);
                if (card == null)
                {
                    continue;
                }

                if (minCard == null)
                {
                    minCard = card;
                    playerWithMinCard = player.Id;
                    continue;
                }

                if (card.Value.Rank < minCard.Value.Rank)
                {
                    minCard = card;
                    playerWithMinCard = player.Id;
                }
            }

            return playerWithMinCard;
        }

        //TODO: rewrite, compare rank and suit
        private static PlayerId FindPlayerWithLowestRankCard(List<PlayerState> players)
        {
            var firstPlayer = players[0];
            var minCard = FindLowestRankCard(firstPlayer);
            var playerIdWithMinCard = firstPlayer.Id;

            for (var i = 1; i < players.Count; i++)
            {
                var currentPlayerMinCard = FindLowestRankCard(players[i]);
                if (currentPlayerMinCard.Rank < minCard.Rank)
                {
                    minCard = currentPlayerMinCard;
                    playerIdWithMinCard = players[i].Id;
                }
            }

            return playerIdWithMinCard;
        }

        private static Card? FindLowestCardOfSuit(PlayerState state, Suit suit)
        {
            Card? result = null;
            foreach (var card in state.Cards)
            {
                if (card.Suit == suit)
                {
                    if (result == null)
                    {
                        result = card;
                        continue;
                    }

                    if (card.Rank < result.Value.Rank)
                    {
                        result = card;
                    }
                }
            }

            return result;
        }

        private static Card FindLowestRankCard(PlayerState state)
        {
            var result = state.Cards[0];
            for (var i = 1; i < state.Cards.Count; i++)
            {
                if (result.Rank > state.Cards[i].Rank)
                {
                    result = state.Cards[i];
                }
            }

            return result;
        }

        private static void DrawCards(List<Card> drawPile, List<Card> hand, int targetCount)
        {
            while (hand.Count < targetCount && drawPile.Count > 0)
            {
                hand.Add(drawPile[^1]);
                drawPile.RemoveAt(drawPile.Count - 1);
            }
        }
    }
}