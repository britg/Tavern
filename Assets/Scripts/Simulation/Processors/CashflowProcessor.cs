using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CashflowProcessor : Processor {

  float totalHourlyExpenses = 0f;
  float totalHourlyIncome = 0f;
  float timeSinceLastAccumulation = 0f;
  float accumulatedCashFlow = 0f;

  float currentPercentageOfHour {
    get {
      return timeSinceLastAccumulation / (float)GameTime.HOUR_SECONDS;
    }
  }

  public override void Update (float deltaTime) {
    timeSinceLastAccumulation += deltaTime;
  }

  public override void OnMinute () {
    CalcHourlyExpenses();
    CalcHourlyIncome();
    ProRateCashFlow();
    ApplyAccumulatedGold();
  }

  public override void OnHour () {
    Simulation.Log("Committing expenses and income!");
  }

  void CalcHourlyExpenses () {

  }

  void CalcHourlyIncome () {
    totalHourlyIncome = sim.Player.BaseHourlyIncome;
    sim.Player.Income = totalHourlyIncome;
  }

  void ProRateCashFlow () {
    accumulatedCashFlow += currentPercentageOfHour * (totalHourlyIncome - totalHourlyExpenses);
    timeSinceLastAccumulation = 0f;
  }

  void ApplyAccumulatedGold () {
    Simulation.Log("Accumulated cash flow: " + accumulatedCashFlow);
    sim.Player.Gold += accumulatedCashFlow;
    accumulatedCashFlow = 0f;
  }
}