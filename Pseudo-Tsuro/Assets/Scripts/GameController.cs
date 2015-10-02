using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	GameObject player;
	public iTween.EaseType easeType = iTween.EaseType.punch;
	public bool move = true;
	public string spot1;
	public string spot2;
	public string spot3;
	public string spot4;
	private Queue line;
	private bool pressGo = true;
	private bool win = false;
	GameObject tile1;
	GameObject tile2;
	GameObject tile3;
	GameObject tile4;	
	GameObject tile1flip;
	GameObject tile2flip;
	GameObject tile3flip;
	GameObject tile4flip;

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		tile1 = GameObject.Find ("1");
		tile2 = GameObject.Find ("2");
		tile3 = GameObject.Find ("3");
		tile4 = GameObject.Find ("4");
		tile1flip = GameObject.Find ("1flip");
		tile2flip = GameObject.Find ("2flip");
		tile3flip = GameObject.Find ("3flip");
		tile4flip = GameObject.Find ("4flip");
	}

	void OnMouseDown(){
		if (pressGo) {
			/*GameObject[] arr = GameObject.FindGameObjectsWithTag("Placeable");
			for(int i = 0; i < arr.Length; i++){
				arr[i].GetComponent<SpriteRenderer>().sprite = arr[i].GetComponent<TileController>().flippedSprite;
			}*/
			GetComponent<TileController> ().deselect ();
			move = false;
			line = compileLine ();
			StartCoroutine (runLine ());
		}
	}

	private Queue compileLine(){
		Queue tempLine = new Queue ();
		tempLine.Enqueue ("Start");

		switch (spot1) {
		case "1":
			tempLine.Enqueue("1horizontal");
			tempLine.Enqueue("0212uphorizontal");
			switch(spot2){
			case "2":
				tempLine.Enqueue("2vertical");
				tempLine.Enqueue("00lower");
				switch(spot3){
				case "3":
					tempLine.Enqueue("3horizontal");
					switch(spot4){
					case "4":
						tempLine.Enqueue("4loopdown");
						tempLine.Enqueue("end");
						win = true;
						break;
					}
					break;
				case "4":
					tempLine.Enqueue("01down");
					tempLine.Enqueue("11rightloop");
					tempLine.Enqueue("11downrightloop");
					tempLine.Enqueue("12downleft");
					tempLine.Enqueue("13downleft");
					break;
				}
				break;
			case "3":
				tempLine.Enqueue("3end");
				win = true;
				break;
			case "4":
				tempLine.Enqueue("014bottomright");
				tempLine.Enqueue("11topleft");
				switch(spot3){
				case "2":
					tempLine.Enqueue("012upleft");
					win = true;
					break;
				case "3":
					tempLine.Enqueue("143end");
					win = true;
					break;
				}
				break;
			}
			break;
		case "2":
			tempLine.Enqueue("2start");
			tempLine.Enqueue("23leftloop");
			tempLine.Enqueue("23across");
			tempLine.Enqueue("33upperleft");
			tempLine.Enqueue("32leftvertical");
			tempLine.Enqueue("31lowerleft");
			tempLine.Enqueue("21upperright");
			tempLine.Enqueue("2upperleft");
			tempLine.Enqueue("12lowerhorizontal");
			tempLine.Enqueue("02lowerirght");
			switch(spot2){
			case "1":
				tempLine.Enqueue("012lowerright");
				tempLine.Enqueue("11topleft");
				switch(spot3){
				case "3":
					tempLine.Enqueue("143end");
					win = true;
					break;
				case "4":
					tempLine.Enqueue("104bottomright");
					tempLine.Enqueue("023horizontal");
					tempLine.Enqueue("end");
					win = true;
					break;
				}
				break;
			case "3":
				tempLine.Enqueue("013vertical");
				tempLine.Enqueue("00vertical");
				win = true;
				break;
			case "4":
				tempLine.Enqueue("014bottomleft");
				win = true;
				break;
			}
			break;
		case "3":
		case "4":
			tempLine.Enqueue ("224upperright");
			tempLine.Enqueue ("21lowerright");
			tempLine.Enqueue ("4lose");
			break;
		}

		return tempLine;
	}

	private IEnumerator runLine(){

		GameObject compile = GameObject.Find ("compiling");
		compile.transform.position = new Vector2 (4.93f, -3.35f);
		//x 4.93 y -3.35

		yield return new WaitForSeconds (2);
		compile.transform.position = new Vector2 (8.28f, -4.04f);

		tile1flip.transform.position = new Vector3(tile1.transform.position.x , tile1.transform.position.y, tile1.transform.position.z);
		tile1.transform.position += Vector3.forward;
		tile2flip.transform.position = new Vector3(tile2.transform.position.x , tile2.transform.position.y, tile2.transform.position.z);
		tile2.transform.position += Vector3.forward;
		tile3flip.transform.position = new Vector3(tile3.transform.position.x , tile3.transform.position.y, tile3.transform.position.z);
		tile3.transform.position += Vector3.forward;
		tile4flip.transform.position = new Vector3(tile4.transform.position.x , tile4.transform.position.y, tile4.transform.position.z);
		tile4.transform.position += Vector3.forward;

		pressGo = false;
		while (line.Count != 0) {
			string path = (string)line.Dequeue();
			iTween.MoveTo(player, iTween.Hash("path", iTweenPath.GetPath(path), "time", 1.5f, "EaseType", easeType));
			yield return new WaitForSeconds(1.5f);
		}
		endGame (win);
	}

	private void endGame(bool won){
		if (won) {
			GameObject win = GameObject.Find("VICTORY");
			win.transform.position += Vector3.back*10;
		} else {
			GameObject def = GameObject.Find("DEFEAT");
			def.transform.position += Vector3.back*10;
		}
	}
}