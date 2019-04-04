using Microsoft.EntityFrameworkCore;
using System;
using TransformerBattle.DataLayer;
using TransformerBattle.Repositories;
using Xunit;

namespace TransformersBattleTests
{
    public class TransformerRepositoryTests
    {
        [Fact]
        public void Add()
        {
            var repo = GetInMemoryRepo("Add_Database");

            var transformer = repo.Add(new Transformer { Name = "test", Allegiance = Group.Autobot });

            Assert.NotNull(transformer);
        }

        [Fact]
        public void Update()
        {
            var repo = GetInMemoryRepo("Update_Database");

            var transformer = repo.Add(new Transformer { Name = "test", Allegiance = Group.Autobot });

            transformer.Name = "newName";
            transformer.Allegiance = Group.Decepticon;
            transformer.Strength = 1;
            transformer.Intelligence = 2;
            transformer.Speed = 3;
            transformer.Endurance = 4;
            transformer.Rank = 5;
            transformer.Courage = 6;
            transformer.Firepower = 7;
            transformer.Skill = 8;

            var id = transformer.Id;
            repo.Update(id, transformer);

            var updatedTransformer = repo.Get(id);

            Assert.Equal("newName", updatedTransformer.Name);
            Assert.Equal(Group.Decepticon, updatedTransformer.Allegiance);
            Assert.Equal(1, updatedTransformer.Strength);
            Assert.Equal(2, updatedTransformer.Intelligence);
            Assert.Equal(3, updatedTransformer.Speed);
            Assert.Equal(4, updatedTransformer.Endurance);
            Assert.Equal(5, updatedTransformer.Rank);
            Assert.Equal(6, updatedTransformer.Courage);
            Assert.Equal(7, updatedTransformer.Firepower);
            Assert.Equal(8, updatedTransformer.Skill);
        }

        [Fact]
        public void Delete()
        {
            var repo = GetInMemoryRepo("Delete Database");

            var transformer = repo.Add(new Transformer { Name = "test", Allegiance = Group.Autobot });
            var id = transformer.Id;

            repo.Delete(id);

            transformer = repo.Get(id);

            Assert.Null(transformer);
        }

        [Fact]
        public void GetAutobots()
        {
            var repo = GetInMemoryRepo("Get_Autobots_Database");

            repo.Add(new Transformer { Name = "test1", Allegiance = Group.Autobot });
            repo.Add(new Transformer { Name = "test2", Allegiance = Group.Autobot });
            repo.Add(new Transformer { Name = "test3", Allegiance = Group.Decepticon });

            var transformers = repo.GetAll(Group.Autobot);
            Assert.Equal(2, transformers.Count);
        }

        [Fact]
        public void GetDecepticons()
        {
            var repo = GetInMemoryRepo("Get_Decepticons_Database");

            repo.Add(new Transformer { Name = "test1", Allegiance = Group.Autobot });
            repo.Add(new Transformer { Name = "test2", Allegiance = Group.Decepticon });
            repo.Add(new Transformer { Name = "test3", Allegiance = Group.Decepticon });

            var transformers = repo.GetAll(Group.Decepticon);
            Assert.Equal(2, transformers.Count);
        }

        [Fact]
        public void GetScore()
        {
            var repo = GetSqlServerRepo();

            var transformer = repo.Add(new Transformer { Name = "test01", Allegiance = Group.Autobot });

            var score = repo.GetScore(transformer.Id);

            repo.Delete(transformer.Id);

            Assert.Equal(0, score);
        }

        private TransformerRepository GetInMemoryRepo(string dbName)
        {
            var options = new DbContextOptionsBuilder<TransformerContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            var context = new TransformerContext(options);
            return new TransformerRepository(context);
        }

        private TransformerRepository GetSqlServerRepo()
        {
            var options = new DbContextOptionsBuilder<TransformerContext>()
                .UseSqlServer("Server=.;Database=TransformerDB;User Id=sa;Password=Summer@123;MultipleActiveResultSets=true")
                .Options;
            var context = new TransformerContext(options);
            return new TransformerRepository(context);
        }
    }
}
