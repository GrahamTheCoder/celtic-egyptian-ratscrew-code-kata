using System.Collections.Generic;

namespace ConsoleBasedGame
{
    internal interface IUserInterface
    {
        IEnumerable<PlayerInfo> GetPlayerInfoFromUserLazily();
        bool TryReadUserInput(out char userInput);
    }
}