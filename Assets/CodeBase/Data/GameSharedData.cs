namespace CodeBase.Data
{
  public class GameSharedData
  {
    public PlayerData PlayerData;
    public ElevatorData ElevatorData;

    public GameSharedData()
    {
      PlayerData = new PlayerData();
      ElevatorData = new ElevatorData();
    }
  }
}