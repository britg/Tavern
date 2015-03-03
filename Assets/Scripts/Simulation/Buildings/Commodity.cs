
public struct Commodity {

  public string Name { get; set; }
  public string Need { get; set; } // "hunger" is the Need for "food"
  public float Amount { get; set; }
  public float MaxAmount { get; set; }
  public float Percentage {
    get {
      return Amount / MaxAmount;
    }
  } // to implement

}