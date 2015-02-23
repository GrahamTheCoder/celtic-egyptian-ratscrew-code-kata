using System.Collections.Generic;

namespace CelticEgyptianRatscrewKata.GameSetup
{
    public interface IGameSetupUserInterface
    {
        IEnumerable<PlayerInfo> GetPlayerInfoFromUserLazily();
    }
}