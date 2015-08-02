using UnityEngine;
using System.Collections;

public class Roll {

  /*
    In the format of
    iTween.Hash(
      (string)key, float,
      (string)key, float
    )
  */
  public static string Hash (Hashtable hash) {
    float sum = 0f;
    foreach (DictionaryEntry pair in hash) {
      Debug.Log("hash value is " + pair.Value + " for " + pair.Key);
      sum += (float)pair.Value;
    }

    float rand = Random.Range(0f, sum);

    float running = 0;
    string chosen = null;
    foreach (DictionaryEntry pair in hash) {
      running += (float)pair.Value;
      if (rand <= running) {
        chosen = (string)pair.Key;
        break;
      }
    }

    return chosen;
  }
}
