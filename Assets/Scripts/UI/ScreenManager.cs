using UnityEngine;
using System.Collections;

public class ScreenManager : MonoBehaviour {

  public GameObject feedView;
  public GameObject equipmentView;

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

  void SwitchToCharacterView () {
    feedView.SetActive(false);
    equipmentView.SetActive(true);
    feedMenuView.SetActive(false);
    characterMenuView.SetActive(true);
  }

  public void OnFeedButtonTapped () {
    SwitchToFeedView();
  }

  void SwitchToFeedView () {
    feedView.SetActive(true);
    equipmentView.SetActive(false);
    feedMenuView.SetActive(true);
    characterMenuView.SetActive(false);
  }


}
