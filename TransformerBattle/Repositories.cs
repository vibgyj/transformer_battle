using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TransformerBattle.DataLayer;

namespace TransformerBattle.Repositories
{
    public interface ITransformerRepository
    {
        Transformer Add(Transformer transformer);
        Transformer Update(Guid id, Transformer transformer);
        void Delete(Guid id);
        Transformer Get(Guid id);
        List<Transformer> GetAll();
        List<Transformer> GetAll(Group allegiance);
        int GetScore(Guid id);
    }

    public class TransformerRepository : ITransformerRepository
    {
        private readonly TransformerContext context;

        public TransformerRepository(TransformerContext context)
        {
            this.context = context;
        }

        public Transformer Add(Transformer transformer)
        {
            transformer.Id = Guid.NewGuid();
            context.Add(transformer);
            context.SaveChanges();

            return transformer;
        }

        public void Delete(Guid id)
        {
            var toRemove = context.Transformers.FirstOrDefault(t => t.Id == id);
            if (toRemove != null)
            {
                context.Transformers.Remove(toRemove);
                context.SaveChanges();
            }
        }

        public Transformer Get(Guid id)
        {
            return context.Transformers.FirstOrDefault(t => t.Id == id);
        }

        public List<Transformer> GetAll()
        {
            return context.Transformers.ToList();
        }

        public List<Transformer> GetAll(Group allegiance)
        {
            return context.Transformers.Where(t => t.Allegiance == allegiance).ToList();
        }

        public int GetScore(Guid id)
        {
            var transformerScore = context.Set<TransformerScore>().FromSql("dbo.GetScore @Id = {0}", id).FirstOrDefault();
            return (int)transformerScore?.Score;
        }

        public Transformer Update(Guid id, Transformer transformer)
        {
            var toUpdate = context.Transformers.FirstOrDefault(t => t.Id == id);
            if (toUpdate != null)
            {
                if (transformer.Name != toUpdate.Name) toUpdate.Name = transformer.Name;
                if (transformer.Allegiance != toUpdate.Allegiance) toUpdate.Allegiance = transformer.Allegiance;
                if (transformer.Strength != toUpdate.Strength) toUpdate.Strength = transformer.Strength;
                if (transformer.Intelligence != toUpdate.Intelligence) toUpdate.Intelligence = transformer.Intelligence;
                if (transformer.Speed != toUpdate.Speed) toUpdate.Speed = transformer.Speed;
                if (transformer.Endurance != toUpdate.Endurance) toUpdate.Endurance = transformer.Endurance;
                if (transformer.Rank != toUpdate.Rank) toUpdate.Rank = transformer.Rank;
                if (transformer.Courage != toUpdate.Courage) toUpdate.Courage = transformer.Courage;
                if (transformer.Firepower != toUpdate.Firepower) toUpdate.Firepower = transformer.Firepower;
                if (transformer.Skill != toUpdate.Skill) toUpdate.Skill = transformer.Skill;

                context.SaveChanges();
            }

            return toUpdate;
        }
    }
}
