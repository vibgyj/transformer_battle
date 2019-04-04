using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TransformerBattle.BusinessLayer;
using TransformerBattle.DataLayer;
using Xunit;

namespace TransformersBattleTests
{
    public class WarTests
    {
        [Fact]
        public void SimulateWar_Optimus_Predaking()
        {
            var war = new War(null);

            var transformers = new List<Transformer>
            {
                new Transformer { Name = "Optimus" },
                new Transformer { Name = "Predaking" }
            };

            var result = war.SimulateWar(transformers);

            Assert.Empty(result);
        }

        [Fact]
        public void SimulateWar_SplitsByAllegiance()
        {
            var transformerA = new Transformer { Id = Guid.NewGuid(), Name = "A1", Allegiance = Group.Autobot };
            var transformerD = new Transformer { Id = Guid.NewGuid(), Name = "D1", Allegiance = Group.Decepticon };
            var transformers = new List<Transformer> { transformerA, transformerD };

            var mockBattle = new Mock<IBattle>();
            var war = new War(mockBattle.Object);
            mockBattle.Setup(b => b.SimulateBattle(transformerD, transformerA)).Returns(transformerA);

            var victors = war.SimulateWar(transformers);

            Assert.Contains(transformerA, victors);
        }

        [Fact]
        public void SimulateWar_UnEvenCount()
        {
            var transformerA = new Transformer { Id = Guid.NewGuid(), Name = "A1", Allegiance = Group.Autobot };
            var transformerA2 = new Transformer { Id = Guid.NewGuid(), Name = "A2", Allegiance = Group.Autobot };
            var transformerD = new Transformer { Id = Guid.NewGuid(), Name = "D1", Allegiance = Group.Decepticon };
            var transformers = new List<Transformer> { transformerA, transformerD, transformerA2 };

            var mockBattle = new Mock<IBattle>();
            var war = new War(mockBattle.Object);
            mockBattle.Setup(b => b.SimulateBattle(transformerA, transformerD)).Returns(transformerA);

            var victors = war.SimulateWar(transformers);

            Assert.Contains(transformerA, victors);
            Assert.Contains(transformerA2, victors);
        }

        [Fact]
        public void SimulateWar_UnEvenWithRank()
        {
            var transformerA = new Transformer { Id = Guid.NewGuid(), Name = "A1", Allegiance = Group.Autobot, Rank = 4 };
            var transformerA2 = new Transformer { Id = Guid.NewGuid(), Name = "A2", Allegiance = Group.Autobot, Rank = 5 };
            var transformerD = new Transformer { Id = Guid.NewGuid(), Name = "D1", Allegiance = Group.Decepticon, Rank = 6 };
            var transformers = new List<Transformer> { transformerA, transformerD, transformerA2 };

            var mockBattle = new Mock<IBattle>();
            var war = new War(mockBattle.Object);
            mockBattle.Setup(b => b.SimulateBattle(transformerA2, transformerD)).Returns(transformerD);

            var victors = war.SimulateWar(transformers);

            Assert.Contains(transformerD, victors);
            Assert.Contains(transformerA, victors);
        }

        [Fact]
        public void SimulateWar_UnEvenCount_OnlyAutobot()
        {
            var transformerA = new Transformer { Id = Guid.NewGuid(), Name = "A1", Allegiance = Group.Autobot };
            var transformers = new List<Transformer> { transformerA };

            var mockBattle = new Mock<IBattle>();
            var war = new War(mockBattle.Object);

            var victors = war.SimulateWar(transformers);

            Assert.Contains(transformerA, victors);
        }

        [Fact]
        public void SimulateWar_UnEvenCount_OnlyDecepticon()
        {
            var transformerD = new Transformer { Id = Guid.NewGuid(), Name = "D1", Allegiance = Group.Decepticon };
            var transformers = new List<Transformer> { transformerD };

            var mockBattle = new Mock<IBattle>();
            var war = new War(mockBattle.Object);

            var victors = war.SimulateWar(transformers);

            Assert.Contains(transformerD, victors);
        }
    }
}
