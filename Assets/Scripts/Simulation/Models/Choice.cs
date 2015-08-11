using UnityEngine;
using System.Collections;

public class Choice {

  public static string Tower = "tower";
  public static string Shop = "shop";
  public static string Potion = "potion";
  public static string Continue = "continue";

  public enum Direction {
    Left,
    Right
  }

  public string key;
  public string label;
  public Direction direction;

  public static Choice PullLeft (string key, string label) {
    var c = new Choice();
    c.key = key;
    c.label = label;
    c.direction = Direction.Left;

    return c;
  }

  public static Choice PullRight (string key, string label) {
    var c = new Choice();
    c.key = key;
    c.label = label;
    c.direction = Direction.Right;

    return c;
  }

}
