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
        public List<Guid> SimulateWar(List<Transformer> transformers)
        {
            throw new NotImplementedException();
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
