namespace CelticEgyptianRatscrewKata.GameSetup
{
    public interface IGamePlayUserInterface
    {
        bool TryReadUserInput(out char userInput);
    }
}