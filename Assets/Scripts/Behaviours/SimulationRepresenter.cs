using UnityEngine;
using System.Collections;

public class SimulationRepresenter : BaseBehaviour {

  // Use this for initialization
  public GameObject tavernPrefab;
  public GameObject adventurerPrefab;

  void Awake () {
    NotificationCenter.AddObserver(this, Constants.OnAdventurerCreated);
    NotificationCenter.AddObserver(this, Constants.OnTavernCreated);
  }

	void Start () {
	}


  void OnAdventurerCreated (Notification n) {
    //Debug.Log("Receiving adventurer created " + n);
    Adventurer adv = (Adventurer)n.data["adventurer"];
    GameObject advObj = (GameObject)Instantiate(adventurerPrefab);
    advObj.transform.SetParent(transform);
    advObj.GetComponent<AdventurerView>().SetAdventurer(adv);
  }

  void OnTavernCreated (Notification n) {
    Building tavern = (Building)n.data["tavern"];
    //Debug.Log("On Tavern created " + tavern);
    GameObject tavernObj = (GameObject)Instantiate(tavernPrefab);
    tavernObj.transform.SetParent(transform);
    tavernObj.GetComponent<TavernView>().SetTavern(tavern);
  }

}
