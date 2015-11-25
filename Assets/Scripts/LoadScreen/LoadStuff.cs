using UnityEngine;
using System.Collections;

public class LoadStuff : MonoBehaviour {

    string levelToLoad;

    public GameObject loadingScreen;
    public GameObject progressBar;
    public GameObject text;

    private int loadProgress = 0;

    void Start() {
        loadingScreen.SetActive(false);
        progressBar.SetActive(false);
        text.SetActive(false);
    }

    IEnumerator DisplayLoadingScreen(string level) {
        loadingScreen.SetActive(true);
        progressBar.SetActive(true);
        text.SetActive(true);

        text.GetComponent<GUIText>().text = "Progress " + loadProgress + "%";
        progressBar.transform.localScale = new Vector3(loadProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);

        AsyncOperation async = Application.LoadLevelAsync(level);
        while (!async.isDone)
        {
            loadProgress =(int) (async.progress * 100);
            text.GetComponent<GUIText>().text = "Progress " + loadProgress + "%";
            progressBar.transform.localScale = new Vector3(async.progress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);

            yield return null;
        }
    }


    void OnGUI() {
        GUILayout.Box("Current scene: " + Application.loadedLevelName );

        if (GUILayout.Button("Load Scene 1")) {
            StartCoroutine( DisplayLoadingScreen("Scene1"));
           // Application.LoadLevel("Scene1");
        }

        if (GUILayout.Button("Load Scene 2"))
        {
           StartCoroutine( DisplayLoadingScreen("Scene2"));
           // Application.LoadLevel("Scene2");
        }
    }
}
