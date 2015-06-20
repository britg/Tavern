﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equipment {

  public EquipmentType Type { get; set; }
  public int Level { get; set; }

  public List<Stat> Stats = new List<Stat>();

}
