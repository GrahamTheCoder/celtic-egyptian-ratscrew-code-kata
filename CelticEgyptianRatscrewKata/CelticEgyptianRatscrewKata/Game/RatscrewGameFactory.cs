using CelticEgyptianRatscrewKata.GameSetup;
using CelticEgyptianRatscrewKata.SnapRules;

namespace CelticEgyptianRatscrewKata.Game
{
    public class RatscrewGameFactory : IGameFactory
    {
        public IGameController Create(ILog log)
        {
            var callingOutTracker = new LoggedCallingOutTracker(new CallingOutTracker(), log);
            ISnapRule[] rules =
            {
                new DarkQueenSnapRule(),
                new SandwichSnapRule(),
                new StandardSnapRule(),
                new CallingOutSnapRule(callingOutTracker)
            };

            var penalties = new Penalties();
            var loggedPenalties = new LoggedPenalties(penalties, log);
            var gameController = new GameController(new GameState(), new SnapValidator(rules), new Dealer(), new Shuffler(), loggedPenalties, new PlayerSequence());
            var callingOutGameController = new CallingOutSnapGameController(gameController, callingOutTracker);
            return new LoggedGameController(callingOutGameController, log);
        }
    }
}