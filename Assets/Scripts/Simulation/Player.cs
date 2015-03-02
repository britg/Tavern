using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {

  public int Gold { get; set; }
  public List<Building> Buildings { get; set; }

  // daily
  public float Expenses { get; set; }

  // daily
  public float Income { get; set; }

}
