using System;
using System.Collections.Generic;
using System.Text;
using TransformerBattle.BusinessLayer;
using TransformerBattle.DataLayer;
using Xunit;

namespace TransformersBattleTests
{
    public class BattleTests
    {
        [Fact]
        public void Battle_OptimusInOneSide_Left()
        {
            var battle = new Battle();

            var transformerA = new Transformer { Id = Guid.NewGuid(), Name = "Optimus" };
            var transformerB = new Transformer { Id = Guid.NewGuid(), Name = "B1" };

            var victor = battle.SimulateBattle(transformerA, transformerB);

            Assert.Equal(transformerA, victor);
        }

        [Fact]
        public void Battle_OptimusInOneSide_Right()
        {
            var battle = new Battle();

            var transformerA = new Transformer { Id = Guid.NewGuid(), Name = "A1" };
            var transformerB = new Transformer { Id = Guid.NewGuid(), Name = "Optimus" };

            var victor = battle.SimulateBattle(transformerA, transformerB);

            Assert.Equal(transformerB, victor);
        }

        [Fact]
        public void Battle_PredakingInOneSide_Left()
        {
            var battle = new Battle();

            var transformerA = new Transformer { Id = Guid.NewGuid(), Name = "Predaking" };
            var transformerB = new Transformer { Id = Guid.NewGuid(), Name = "B1" };

            var victor = battle.SimulateBattle(transformerA, transformerB);

            Assert.Equal(transformerA, victor);
        }

        [Fact]
        public void Battle_PredakingInOneSide_Right()
        {
            var battle = new Battle();

            var transformerA = new Transformer { Id = Guid.NewGuid(), Name = "A1" };
            var transformerB = new Transformer { Id = Guid.NewGuid(), Name = "Predaking" };

            var victor = battle.SimulateBattle(transformerA, transformerB);

            Assert.Equal(transformerB, victor);
        }
    }
}
