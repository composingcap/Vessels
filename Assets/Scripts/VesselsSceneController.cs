using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyButtons;
public class VesselsSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public sceneInfo[] scenes;
    public int nextScene = 0;
    public float titleWaitTime = 5;
    public float fadeTime = 3;
    bool fading;
    bool fadeOut;
    bool fadeIn;
    public bool maxConnected;
    float pingTimer;
    bool receivedPing;
    sceneInfo lastScene;
    public OSC osc;
    OscMessage m;
    public fadeController fadeController;
    public Transform titlesContainer;
    bool showTitleFlag;
    private void Start()
    {
        osc = GameObject.FindGameObjectWithTag("OSC").GetComponent<OSC>();
        m = new OscMessage();
        osc.SetAddressHandler("/maxReady", setMaxReady);
        osc.SetAddressHandler("/nextSpell", setNextScene);
        osc.SetAddressHandler("/unloadScene", unloadScene);
        osc.SetAddressHandler("/loadScene", nextSpell);
        osc.SetAddressHandler("/ping", receivePing);
        osc.SetAddressHandler("/showTitle", showTitle);
        osc.SetAddressHandler("/hideTitle", hideTitle);
        //fadeController.fade = 1;
        fadeController.fadeOut(0);
        //loadNextScene();
    }

    [Button]
    void showTitle()
    {
        titlesContainer.gameObject.SetActive(false);
        StartCoroutine(showTitleCR());
    }

    IEnumerator showTitleCR()
    {
        hideTitle();
        yield return new WaitForEndOfFrame();
        hideTitle();
        yield return new WaitForEndOfFrame();
        hideTitle();
        yield return new WaitForEndOfFrame();
        scenes[nextScene].showTitle();
        titlesContainer.gameObject.SetActive(true);
    }
    void showTitle(OscMessage m)
    {
        if ((float)m.values[0] > 0f)
        {
            showTitle();
        }
    }

    void hideTitle(OscMessage m)
    {
        if ((float)m.values[0] > 0f)
        {
            hideTitle();
        }
    }
    [Button]
    void hideTitle()
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            sceneInfo s = scenes[i];
            s.hideTitle();
        }
    }

    void receivePing(OscMessage m)
    {
        receivedPing = true;
    }

    public void setNextScene(OscMessage v)
    {
        int thisScene = (int)(float)v.values[0];

        for (int i= 0; i < scenes.Length; i++)
        {
            sceneInfo s = scenes[i];
            if (s.spellNumber == thisScene)
            {
                nextScene = i;
                break;
            }
        }
    }

    public void unloadScene(OscMessage m)
    {
        if (lastScene != null || lastScene != scenes[nextScene])
        {
            lastScene.unloadScene();
            lastScene = null;
        }
    }

    [Button]
    public void loadNextScene()
    {
        
        if (lastScene != null && lastScene != scenes[nextScene])
        {
            lastScene.unloadScene();
            lastScene = null;
        }

        m.values.Clear();
        m.address = "/loadingScene";
        m.values.Add(scenes[nextScene].spellNumber);
        osc.Send(m);
        StartCoroutine(scenes[nextScene].loadScene(osc));
        lastScene = scenes[nextScene];


    }
    void nextSpell(OscMessage m)
    {
        if ((float)m.values[0] > 0)
        {
            loadNextScene();
        }
    }
    void setMaxReady(OscMessage m)
    {
        if ((float)m.values[0] > 0 && lastScene != null)
        {
            lastScene.maxReady = true;
        }
    }


    [System.Serializable]
    public class sceneInfo
    {
        public string sceneName;
        public Transform avitar;
        public RectTransform VRTitle;
        public RectTransform screenTitle;
        public bool maxReady;
        public int spellNumber;


        public void showTitle()
        {
            VRTitle.gameObject.SetActive(true);
            screenTitle.gameObject.SetActive(true);
        }

        public void hideTitle()
        {
            VRTitle.gameObject.SetActive(false);
            screenTitle.gameObject.SetActive(false);
        }

        public IEnumerator loadScene(OSC osc)
        {
            maxReady = false; 

            if (sceneName == "" || sceneName == "none")
            {
                
                yield return null;
            }
            else
            {
                AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                async.allowSceneActivation = false;




                yield return new WaitForSeconds(5);
                OscMessage m = new OscMessage();
                m.address = "/sceneStart";
                m.values.Add(spellNumber);
                osc.Send(m);
                avitar.gameObject.SetActive(true);
                yield return new WaitUntil(() => maxReady);
                async.allowSceneActivation = true;
            }

        }

        public void unloadScene()
        {
            if (avitar)
            {
                avitar.gameObject.SetActive(false);
            }
            if (sceneName == "" || sceneName == "none")
            {
                VRTitle.gameObject.SetActive(false);
                screenTitle.gameObject.SetActive(false);
                return;
            }
            else
            {
                
            }
            try
            {
                SceneManager.UnloadSceneAsync(sceneName);
            }
            catch { };
        }



    }
}
