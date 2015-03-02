using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CashflowProcessor : Processor {

  float totalHourlyExpenses = 0f;
  float totalHourlyIncome = 0f;

  public void CalcHourlyExpenses () {
    totalHourlyExpenses = 0f;
    foreach (Building building in sim.Player.Buildings) {
      totalHourlyExpenses += building.HourlyExpenses;
    }

    sim.Player.Expenses = totalHourlyExpenses;
  }

  public void CalcHourlyIncome () {
    totalHourlyIncome = 0f;
    sim.Player.Income = totalHourlyIncome;
  }
}