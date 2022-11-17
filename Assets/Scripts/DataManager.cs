using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class DataManager : MonoBehaviour
{
	public Dictionary<string, Dictionary<string, Question[]>> questions;

	void Start() {
		string json = ReadFromFile("QData.json");
		questions = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Question[]>>>(json);
	}

	string ReadFromFile(string fileName) {
		string path = Application.streamingAssetsPath + "/" + fileName;
		if (File.Exists(path)) {
			using (StreamReader reader = new StreamReader(path)) {
				string json = reader.ReadToEnd();
				return json;
			}
		}
		else {
			Debug.Log("No such file");
		}

		return "";
	}

	public void SaveJson(string fileName) {
		string path = Application.streamingAssetsPath + "/" + fileName;
		string content = JsonConvert.SerializeObject(questions);
		File.WriteAllText(path, content);
	}
}
