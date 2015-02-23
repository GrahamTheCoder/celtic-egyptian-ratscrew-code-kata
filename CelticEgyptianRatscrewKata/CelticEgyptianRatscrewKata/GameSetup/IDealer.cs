using System.Collections.Generic;

namespace CelticEgyptianRatscrewKata.GameSetup
{
    public interface IDealer
    {
        List<Cards> Deal(uint numberOfHands, Cards deck);
    }
}