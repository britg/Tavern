using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class SimulationConfig {
  
  const string CONFIG_PATH = "Assets/Scripts/Simulation/Config";

  public void LoadAll () {
    foreach (string dir in Directory.GetDirectories(CONFIG_PATH)) {
      LoadDirectory(dir);
    }
  }

  public void LoadDirectory (string dir) {

    foreach (string subdir in Directory.GetDirectories(dir)) {
      LoadDirectory(subdir);
    }

    foreach (string filename in Directory.GetFiles(dir)) {
      LoadFile(filename);
    }
  }

  public void LoadFile (string filename) {
    var fileInfo = new FileInfo(filename);
    var ext = fileInfo.Extension;

    if (ext == ".json") {
      Debug.Log ("Parsing json file " + filename);
      string contents = File.ReadAllText(filename);
      ParseContents(contents);
    } else {
      Debug.Log ("Ignoring file " + filename);
    }

  }

  public void ParseContents (string json) {
    Debug.Log ("Parsing contents " + json);
  }

}