using UnityEngine;
using System.Collections;

public class ExplorationQuestController : BaseBehaviour {

  enum InputState {
    Default,
    PendingSelection
  }

  public GameObject questMarkerPrefab;

  GameObject currentQuestMarker;
  InputState inputState = InputState.Default;

  // Use this for initialization
  void Start () {
    NotificationCenter.AddObserver(this, Constants.OnWorldSelection);
  }

  // Update is called once per frame
  void Update () {
    MoveQuestMarker();
  }

  public void OnActivateQuestPlacer () {
    CreateQuestMarker();
    EnterPendingSelectionState();
  }

  void EnterPendingSelectionState () {
    inputState = InputState.PendingSelection;
    NotificationCenter.PostNotification(Constants.OnEnterPendingSelection);
  }

  void OnWorldSelection () {
    if (inputState == InputState.PendingSelection) {
      DropQuestMarker();
      inputState = InputState.Default;
    }
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

  void DropQuestMarker () {
    sim.CreateExplorationQuest(currentQuestMarker.transform.position);
    currentQuestMarker = null;
  }


}
