using System.Collections.Generic;

namespace ConsoleBasedGame
{
    internal interface IGamePlayUserInterface
    {
        bool TryReadUserInput(out char userInput);
    }

    internal interface IGameSetupUserInterface
    {
        IEnumerable<PlayerInfo> GetPlayerInfoFromUserLazily();
    }
}