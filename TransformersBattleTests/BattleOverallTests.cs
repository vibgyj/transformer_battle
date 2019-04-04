using System;
using System.Collections.Generic;
using TransformerBattle.BusinessLayer;
using TransformerBattle.DataLayer;
using Xunit;

namespace TransformersBattleTests
{
    public class BattleOverallTests
    {
        [Theory]
        [InlineData(2, 1, 0)]
        [InlineData(1, 2, 1)]
        public void Battle_A_OverallMore(int transformerAStrength, int transformerBStrength, int victorIndex)
        {
            var battle = new Battle();

            var transformers = new List<Transformer>
            {
                new Transformer { Id = Guid.NewGuid(), Name = "A1", Skill = transformerAStrength },
                new Transformer { Id = Guid.NewGuid(), Name = "B1", Skill = transformerBStrength }
            };
            var victor = battle.SimulateBattle(transformers[0], transformers[1]);

            Assert.Equal(transformers[victorIndex], victor);
        }

        [Theory]
        [InlineData(1, 2, 0)]
        [InlineData(2, 1, 1)] // When passing enum, test is not getting executed. Bug with VS?
        public void Battle_OverallEqual_AutobotCinematicPowerWins(int transformerAAllegiance, int transformerBAllegiance, int victorIndex)
        {
            var battle = new Battle();

            var transformers = new List<Transformer>
            {
                new Transformer { Id = Guid.NewGuid(), Name = "A1", Allegiance = (Group)transformerAAllegiance, Strength = 1 },
                new Transformer { Id = Guid.NewGuid(), Name = "B1", Allegiance = (Group)transformerBAllegiance, Strength = 1 }
            };
            var victor = battle.SimulateBattle(transformers[0], transformers[1]);

            Assert.Equal(transformers[victorIndex], victor);
        }
    }
}
