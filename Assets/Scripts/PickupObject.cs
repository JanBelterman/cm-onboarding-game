using System;
using UnityEngine;

public class PickupObject : MonoBehaviour {
	// GameObject player;
	// bool carrying;
	// GameObject carriedObject; 
	// public float distance;
	// public float smooth;
	// private PlayerInputActions _inputAction;
	// // Use this for initialization
	// void Start () {
	// 	 _inputAction = new PlayerInputActions();
	// 	_inputAction.PlayerControls.Interact.performed += _ => pickupAction();
	// 	player = GameObject.FindWithTag("Player"); 
	// }
	
	// // Update is called once per frame
	// // void Update () {
	// // 	if(carrying) {
	// // 		carry(carriedObject);
	// // 		checkDrop();
	// // 		//rotateObject();
	// // 	} else {
	// // 		pickup();
	// // 	}
	// // }
	// void pickupAction() {
	// 	Debug.Log("Pickup action");
	// }
	// private void OnEnable() {
    //     _inputAction.Enable();
    // }

    // private void OnDisable() {
    //     _inputAction.Disable();
    // }
	
	// void rotateObject() {
	// 	carriedObject.transform.Rotate(5,10,15);
	// }
	
	// void carry(GameObject o) {
	// 	o.transform.position = Vector3.Lerp (o.transform.position, player.transform.position + player.transform.forward * distance, Time.deltaTime * smooth);
	// }
	
	// void pickup() {
	// 	if(_inputAction.PlayerControls.Interact) {
	// 		int x = Screen.width / 2;
	// 		int y = Screen.height / 2;
			
	// 		Ray ray = player.GetComponent<Camera>().ScreenPointToRay(new Vector3(x,y));
	// 		RaycastHit hit;
	// 		if(Physics.Raycast(ray, out hit)) {
	// 			Pickupable p = hit.collider.GetComponent<Pickupable>();
	// 			if(p != null) {
	// 				carrying = true;
	// 				carriedObject = p.gameObject;
	// 				p.gameObject.GetComponent<Rigidbody>().isKinematic = true;
	// 			}
	// 		}
	// 	}
	// }
	
	// void checkDrop() {
	// 	if(Input.GetMouseButtonDown(1)) {
	// 		dropObject();
	// 	}
	// }
	
	// void dropObject() {
	// 	carrying = false;
	// 	carriedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
	// 	carriedObject = null;
	// }
}

