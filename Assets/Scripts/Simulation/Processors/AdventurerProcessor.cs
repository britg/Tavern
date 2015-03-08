public class AdventurerProcessore : Processor {

  AdventurerConfig config;

  public override void Start () {
    config = sim.config.AdventurerConfig;
    CreateInitialAdventurers();
  }

  void CreateInitialAdventurers () {
    for (int i = 0; i < config.start_count; i++) {
      CreateAdventurer(sim.Player.CurrentLevel);
    }
  }

  void CreateAdventurer (int level) {
  }

}