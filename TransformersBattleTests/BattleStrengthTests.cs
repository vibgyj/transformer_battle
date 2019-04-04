using System;
using System.Collections.Generic;
using System.Text;
using TransformerBattle.BusinessLayer;
using TransformerBattle.DataLayer;
using Xunit;

namespace TransformersBattleTests
{
    public class BattleStrengthTests
    {
        [Theory]
        [InlineData(4, 1, 4, 4, 0)]
        [InlineData(1, 4, 4, 4, 1)]
        public void Battle_A_Strength_Diff3orMore_B_Courage_LessThan5(int transformerAStrength, int transformerBStrength, 
            int transformerACourage, int transformerBCourage, int victorIndex)
        {
            var battle = new Battle();

            var transformers = new List<Transformer>
            {
                new Transformer { Id = Guid.NewGuid(), Name = "A1", Strength = transformerAStrength, Courage = transformerACourage },
                new Transformer { Id = Guid.NewGuid(), Name = "B1", Strength = transformerBStrength, Courage = transformerBCourage }
            };

            var victor = battle.SimulateBattle(transformers[0], transformers[1]);

            Assert.Equal(transformers[victorIndex], victor);
        }
    }
}
