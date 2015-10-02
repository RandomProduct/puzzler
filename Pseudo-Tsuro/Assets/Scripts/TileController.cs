using UnityEngine;
using System.Collections;

public class TileController : MonoBehaviour {

	public static GameObject selected = null;
	static int inQueue = 4; //How many are still in the queue

	void OnMouseDown(){
		if (!GameObject.Find("GoButtonTrue").GetComponent<GameController>().move) { //If the player has confirmed their tiles, don't let them move.
		}
		else if(this.gameObject.tag.Equals("Respawn")){
			GameObject cam = GameObject.FindWithTag("MainCamera");
			cam.transform.position = new Vector3(0,0,-10);
		}
		else if (this.gameObject.tag.Equals ("Placeable")) { //Selecting a tile to move.
			if (selected != null) {
				selected.ScaleTo (new Vector3 (0.29f, 0.29f, 1), 0.5f, 0);
			}
			selected = this.gameObject;
			selected.ScaleTo (new Vector3 (0.35f, 0.35f, 1), 0.5f, 0);
		} else if (gameObject.tag.Equals ("Spot")) { //Selecting a tile to move to.
			if(selected == null){
				Debug.Log("Nothing selected");
			}
			else{
				if(selected.transform.position.x == 4.93f){
					inQueue--;
				}
				selected.ScaleTo(new Vector3 (0.29f, 0.29f, 1), 1.0f, 0);
				selected.MoveTo(new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -0.2f), 2, 0);
				switch(this.gameObject.name){
				case "Spot 1":
					GameObject.Find("GoButtonTrue").GetComponent<GameController>().spot1 = selected.name;
					break;
				case "Spot 2":
					GameObject.Find("GoButtonTrue").GetComponent<GameController>().spot2 = selected.name;
					break;
				case "Spot 3":
					GameObject.Find("GoButtonTrue").GetComponent<GameController>().spot3 = selected.name;
					break;
				case "Spot 4":
					GameObject.Find("GoButtonTrue").GetComponent<GameController>().spot4 = selected.name;
					break;
				}
				selected = null;
				if(inQueue == 0){
					setGo(true);
				}
			}
		}
		else if(gameObject.tag.Equals("Respawn")){ //Selecting the tile queue to put them back.
			if(selected != null && selected.transform.position.x != 4.93f){
				switch(selected.name){
				case "1":
					selected.MoveTo (new Vector3 (4.93f, 3.73f, -1), 1.5f, 0);
					selected.ScaleTo(new Vector3 (0.29f, 0.29f, 1), 0.5f, 0);
					selected = null;
					inQueue++;
					setGo(false);
					break;
				case "2":
					selected.MoveTo (new Vector3 (4.93f, 1.75f, -1), 1.0f, 0);
					selected.ScaleTo(new Vector3 (0.29f, 0.29f, 1), 0.5f, 0);
					selected = null;
					inQueue++;
					setGo(false);
					break;
				case "3":
					selected.MoveTo (new Vector3 (4.93f, -0.23f, -1), 1.0f, 0);
					selected.ScaleTo(new Vector3 (0.29f, 0.29f, 1), 0.5f, 0);
					selected = null;
					inQueue++;
					setGo(false);
					break;
				case "4":
					selected.MoveTo (new Vector3 (4.93f, -2.21f, -1), 1.0f, 0);
					selected.ScaleTo(new Vector3 (0.29f, 0.29f, 1), 0.5f, 0);
					selected = null;
					inQueue++;
					setGo(false);
					break;
				}
			}
			else if(selected != null){
				selected.ScaleTo(new Vector3 (0.29f, 0.29f, 1), 0.5f, 0);
				selected = null;
			}
		}
	}

	void setGo(bool ready){ //Start the game!
		GameObject goObject = GameObject.Find ("GoButtonTrue");
		if (ready) {
			goObject.transform.position = new Vector3(goObject.transform.position.x, goObject.transform.position.y, -1);
		} else {
			goObject.transform.position = new Vector3(goObject.transform.position.x, goObject.transform.position.y, 10);
		}
	}

	public void deselect(){ //Deselect the currently selected tile.
		if (selected != null) {
			selected.ScaleTo (new Vector3 (0.29f, 0.29f, 1), 0.5f, 0);
			selected = null;
		}
	}
}
