using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class GUI_menu : MonoBehaviour {
	public	String myextension = ".*";//defualt
	public	String myplayer = "C:\\Program Files (x86)\\Windows Media Player\\wmplayer.exe";//default
	public DirectoryInfo dir = new DirectoryInfo("C:\\Users\\Public\\Music\\Sample Music");
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI(){
		int w = 500;
	
		//sets the filetype and player preferences for this folder
		try
        {
            using (StreamReader sr = new StreamReader(dir + "\\folder_media_info.txt"))
            {
                String line = sr.ReadLine();
                myextension = line;
				line = sr.ReadLine();
                myplayer = line;
            }
        }
        catch (Exception e)
        {
            print("The file could not be read:");
            print(e.Message);
			myextension = ".*";
        }
		
		
		
		
		int i = 10;
		int h = 20;
		//back button
		if(dir.ToString() != "C:"){
			if(GUI.Button(new Rect(10,i,w,h), "Back")) {
				String k = dir.ToString().Substring(0,dir.ToString().LastIndexOf("\\"));
				dir = new DirectoryInfo(k);
			}
		}
		
		//Displays folder list as buttons
		DirectoryInfo[] dinfo = dir.GetDirectories();		
		foreach (DirectoryInfo d in dinfo) {
			i+=h;
			string str = d.ToString();
			str = str.Substring(str.LastIndexOf("\\") + 1);
			if(GUI.Button(new Rect(10,i,w,h), str)) {
				String k = dir.ToString() + "\\" + str;
				dir = new DirectoryInfo(k);
			}
			
		}
		
		//Displays file list as buttons
		FileInfo[] info = dir.GetFiles("*"+ myextension);
		foreach (FileInfo f in info) {
			//print("for");
			i+=h;
			string str = f.ToString();
			str = str.Substring(str.LastIndexOf("\\") + 1);
			//print("str = " + str);
			if(GUI.Button(new Rect(10,i,w,h), str)) {
				System.Diagnostics.Process.Start(myplayer.ToString() ,  " \"" + f.ToString() + "\"");
			}
			
		}		

	}
}
