using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleLine : MonoBehaviour {
	private static ConsoleLine console;

    public ScrollRect scroll;
	public List<Text> lines;
	public Transform lineRoot;
	public GameObject linePrefab;

	private void Start(){
		ConsoleLine.console = this;
	}
	public static void WriteLine(string text){
		if(ConsoleLine.console != null){
			var go = Instantiate(console.linePrefab, console.lineRoot);
			go.GetComponent<Text>().text = text;
			console.lines.Add(go.GetComponent<Text>());
            console.scroll.normalizedPosition = new Vector2(0, 0);
        }
	}
}
