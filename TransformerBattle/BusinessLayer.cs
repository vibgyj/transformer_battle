using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransformerBattle.DataLayer;

namespace TransformerBattle.BusinessLayer
{
    public interface IWar
    {
        List<Transformer> SimulateWar(List<Transformer> transformers);
    }

    public interface IBattle
    {
        Transformer SimulateBattle(Transformer transformerA, Transformer transformerB);
    }

    public class War : IWar
    {
        private readonly IBattle battle;
        public War(IBattle battle)
        {
            this.battle = battle;
        }

        public List<Transformer> SimulateWar(List<Transformer> transformers)
        {
            if (transformers.Any(t => t.Name.Equals("Optimus")) && transformers.Any(t => t.Name.Equals("Predaking")))
                return new List<Transformer>();

            var autobots = transformers.Where(t => t.Allegiance.Equals(Group.Autobot)).OrderByDescending(t => t.Rank);
            var decepticons = transformers.Where(t => t.Allegiance.Equals(Group.Decepticon)).OrderByDescending(t => t.Rank);

            var result = new List<Transformer>();

            IEnumerable<Transformer> largerGroup;
            IEnumerable<Transformer> smallerGroup;
            if (autobots.Count() > decepticons.Count())
            {
                largerGroup = autobots;
                smallerGroup = decepticons;
            }
            else
            {
                largerGroup = decepticons;
                smallerGroup = autobots;
            }

            for(int i = 0; i < largerGroup.Count(); i++)
            {
                if (smallerGroup.Count() <= i)
                {
                    result.Add(largerGroup.ElementAt(i));
                }
                else
                {
                    var victor = battle.SimulateBattle(largerGroup.ElementAt(i), smallerGroup.ElementAt(i));
                    result.Add(victor);
                }                    
            }

            return result;
        }
    }

    public class Battle : IBattle
    {
        public Transformer SimulateBattle(Transformer transformerA, Transformer transformerB)
        {
            if (transformerA.Name.Equals("Optimus") || transformerA.Name.Equals("Predaking"))
                return transformerA;
            if (transformerB.Name.Equals("Optimus") || transformerB.Name.Equals("Predaking"))
                return transformerB;

            if (transformerA.Strength - transformerB.Strength > 2 && transformerB.Courage < 5)
                return transformerA;
            if (transformerB.Strength - transformerA.Strength > 2 && transformerA.Courage < 5)
                return transformerB;

            if (transformerA.Skill - transformerB.Skill > 4) return transformerA;
            if (transformerB.Skill - transformerA.Skill > 4) return transformerB;

            if (transformerA.GetOverAll() > transformerB.GetOverAll()) return transformerA;
            if (transformerB.GetOverAll() > transformerA.GetOverAll()) return transformerB;

            if (transformerA.Allegiance == Group.Autobot) return transformerA;
            return transformerB;
        }
    }
}
