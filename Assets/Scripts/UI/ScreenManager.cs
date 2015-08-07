using UnityEngine;
using System.Collections;

public class ScreenManager : MonoBehaviour {

  public GameObject feedView;
  public GameObject equipmentView;
  public GameObject inventoryView;
  public GameObject questView;

  public GameObject feedMenuView;
  public GameObject characterMenuView;

	// Use this for initialization
	void Start () {
//    SwitchToCharacterView();
    SwitchToFeedView();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void OnCharacterButtonTapped () {
    SwitchToCharacterView();
  }

  public void OnFeedButtonTapped () {
    SwitchToFeedView();
  }

  public void OnInventoryButtonTapped () {
    SwitchToInventoryView();
  }

  public void OnQuestButtonTapped () {
    SwitchToQuestView();
  }

  void ActivateCharacterMenu () {
    feedMenuView.SetActive(false);
    characterMenuView.SetActive(true);
  }

  void ActivateFeedMenu () {
    feedMenuView.SetActive(true);
    characterMenuView.SetActive(false);
  }

  void SwitchToCharacterView () {
    ActivateCharacterMenu();

    equipmentView.SetActive(true);

    feedView.SetActive(false);
    inventoryView.SetActive(false);
    questView.SetActive(false);
  }

  void SwitchToFeedView () {
    ActivateFeedMenu();

    feedView.SetActive(true);

    equipmentView.SetActive(false);
    inventoryView.SetActive(false);
    questView.SetActive(false);
  }

  void SwitchToQuestView () {
    ActivateCharacterMenu();

    questView.SetActive(true);

    feedView.SetActive(false);
    equipmentView.SetActive(false);
    inventoryView.SetActive(false);
  }

  void SwitchToInventoryView () {
    ActivateCharacterMenu();

    inventoryView.SetActive(true);

    feedView.SetActive(false);
    equipmentView.SetActive(false);
    questView.SetActive(false);
  }


}
