using System;
using System.Collections.Generic;
using System.Text;
using TransformerBattle.BusinessLayer;
using TransformerBattle.DataLayer;
using Xunit;

namespace TransformersBattleTests
{
    public class BattleSkillTests
    {
        [Theory]
        [InlineData(6, 1, 0)]
        [InlineData(1, 6, 1)]
        public void Battle_Skill5OrMore(int transformerASkill, int transformerBSkill, int victorIndex)
        {
            var battle = new Battle();

            var transformers = new List<Transformer>
            {
                new Transformer { Id = Guid.NewGuid(), Name = "A1", Skill = transformerASkill },
                new Transformer { Id = Guid.NewGuid(), Name = "B1", Skill = transformerBSkill }
            };

            var victor = battle.SimulateBattle(transformers[0], transformers[1]);

            Assert.Equal(transformers[victorIndex], victor);
        }
    }
}
