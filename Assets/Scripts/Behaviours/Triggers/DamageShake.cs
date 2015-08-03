using UnityEngine;
using System.Collections;

public class DamageShake : MonoBehaviour {

  void Start () {
    NotificationCenter.AddObserver(this, Constants.OnTakeDamage);
  }

  void OnTakeDamage () {
    Shake();
  }

  void Shake () {
    iTween.ShakePosition(gameObject, Vector3.right*100, 0.2f);
  }
}
