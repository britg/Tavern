using UnityEngine;
using System.Collections;

public class ExplorationQuestController : MonoBehaviour {

  public GameObject questMarkerPrefab;

  GameObject currentQuestMarker;

  // Use this for initialization
  void Start () {

  }

  // Update is called once per frame
  void Update () {
    MoveQuestMarker();
  }

  public void OnActivateQuestPlacer () {
    Debug.Log("On activate quest placer");
    CreateQuestMarker();
  }

  void CreateQuestMarker () {
    currentQuestMarker = (GameObject)Instantiate(questMarkerPrefab);
  }

  void MoveQuestMarker () {
    if (currentQuestMarker == null) {
      return;
    }

    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit[] hits = Physics.RaycastAll(ray);
    foreach (RaycastHit hit in hits) {
      int layer = hit.collider.gameObject.layer;
      string layerName = LayerMask.LayerToName(layer);
      if (Constants.GroundLayer == layerName) {
        currentQuestMarker.transform.position = hit.point;
        return;
      }
    }
  }


}
