namespace CelticEgyptianRatscrewKata.Game
{
    public interface IGameFactory
    {
        GameController Create();
        bool AddPlayer(IPlayer player);
        Cards CreateFullDeckOfCards();
    }
}