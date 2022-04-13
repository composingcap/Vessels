using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using Valve.VR;
using UnityEngine.XR;
using System.IO;
using Klak.Spout;
public class VesslesSettings : MonoBehaviour
{

    public VesselsConfig config;
    [Space]
    public Transform VRPlayerContainer;
    public Transform spoutCam;
    public OSC osc;
    public Transform debugCamera;
    RenderTexture spoutTexture;

    bool loadingSettings;

    [Button]
    void readConfig()
    {
        config= config.loadJSON();
    }
    [Button]
    void writeConfig()
    {
        config.writeJSON();
    }
    [Button]
    void loadSettings()
    {
        spoutCam.gameObject.SetActive(config.enableScreens);
        VRPlayerContainer.gameObject.SetActive(config.enableVR);
        debugCamera.gameObject.SetActive(!config.enableVR);
        VRPlayerContainer.transform.position = config.playerTranslate;
        osc.outIP = config.audioIp;
        osc.outPort = config.audioPort;
        Quaternion rot = new Quaternion();
        rot.eulerAngles = new Vector3(0f, 180f + config.playerRotation, 0f);
        VRPlayerContainer.rotation = rot;

        //spoutTexture.width = (int)(config.ScreenSize.x * 3);
        //spoutTexture.height = (int)config.ScreenSize.y;
        if (spoutTexture)
        {
            spoutTexture.Release();
        }
        spoutTexture = new RenderTexture((int)(config.ScreenSize.x * 3), (int)config.ScreenSize.y, 16);
        spoutTexture.name = "SpoutTexture_" + spoutTexture.width + "x" + spoutTexture.height;
        spoutCam.GetComponent<Camera>().targetTexture = spoutTexture;
        spoutCam.GetComponent<SpoutSender>().sourceTexture = spoutTexture;
    }

    private void Start()
    {
        spoutCam = GameObject.FindGameObjectWithTag("Spout").transform;
        VRPlayerContainer = GameObject.FindGameObjectWithTag("VRPlayerContainer").transform;
        osc = GameObject.FindGameObjectWithTag("OSC").GetComponent<OSC>();
        debugCamera = GameObject.FindGameObjectWithTag("DebugCamera").transform;

        readConfig();
        loadSettings();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1) && !loadingSettings)
        {
            loadingSettings = true;
            readConfig();
            loadSettings();
        }
        if (Input.GetKeyUp(KeyCode.F1) && loadingSettings)
        {
            loadingSettings = false;
        }
    }
}

[System.Serializable]
public class VesselsConfig
{
    public int audioPort = 4000;
    public string audioIp = "127.0.0.1";
    public bool enableScreens = true;
    public bool enableVR = true;
    public Vector3 playerTranslate = new Vector3(0,0,0);
    public Vector2 ScreenSize = new Vector2(1920,1080);
    public float playerRotation = 180f;
    public  VesselsConfig loadJSON()
    {

        string textjson = File.ReadAllText(Application.streamingAssetsPath + "/config.json");

        return JsonUtility.FromJson<VesselsConfig>(textjson);
    }
    public void writeJSON()
    {
        string jsonString = JsonUtility.ToJson(this);
        string filePath = Application.streamingAssetsPath + "/config.json";
        Debug.Log(filePath);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        File.WriteAllText(filePath, jsonString);
        

    }
}

