using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransformerBattle.DataLayer;

namespace TransformerBattle.BusinessLayer
{
    public interface IWar
    {
        List<Guid> SimulateWar(List<Transformer> transformers);
    }

    public interface IBattle
    {
        Guid SimulateBattle(Transformer transformerA, Transformer transformerB);
    }

    public class War : IWar
    {
        private readonly IBattle battle;
        public War(IBattle battle)
        {
            this.battle = battle;
        }

        public List<Guid> SimulateWar(List<Transformer> transformers)
        {
            if (transformers.Any(t => t.Name.Equals("Optimus")) && transformers.Any(t => t.Name.Equals("Predaking")))
                return new List<Guid>();

            var autobots = transformers.Where(t => t.Allegiance.Equals(Group.Autobot)).OrderByDescending(t => t.Rank);
            var decepticons = transformers.Where(t => t.Allegiance.Equals(Group.Decepticon)).OrderByDescending(t => t.Rank);

            var result = new List<Guid>();

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
                    result.Add(largerGroup.ElementAt(i).Id);
                }
                else
                {
                    var victor = battle.SimulateBattle(largerGroup.ElementAt(i), smallerGroup.ElementAt(i));
                    result.Add(victor);
                }                    
            }
            //var autobotsCount = autobots.Count();
            //var decepticonsCount = decepticons.Count();
            //var minLenth = autobotsCount < decepticonsCount ? autobotsCount : decepticonsCount;

            
            //for(int i = 0; i < minLenth; i++)
            //{
            //    var victor = battle.SimulateBattle(autobots[i], decepticons[i]);
            //    result.Add(victor);
            //}

            //if()

            return result;
        }
    }

    public class Battle : IBattle
    {
        public Guid SimulateBattle(Transformer transformerA, Transformer transformerB)
        {
            throw new NotImplementedException();
        }
    }
}
