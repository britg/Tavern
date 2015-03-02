using UnityEngine;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;

class BuildingSetup {

  public static List<Building> BuildingTemplates () {
    List<Building> buildingTemplates = new List<Building>();
    foreach (JSONNode bldg in Config.Buildings) {
      var buildingTemplate = new Building();
      buildingTemplate.BuildingType = Utilities.StringToEnum<Building.Type>(bldg["type"]);
      buildingTemplate.HourlyExpenses = bldg["basic"]["hourly_expenses"].AsFloat;
      buildingTemplates.Add(buildingTemplate);
    }

    return buildingTemplates;
  }
}