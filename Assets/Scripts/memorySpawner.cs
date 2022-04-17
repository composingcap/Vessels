using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class memorySpawner : MonoBehaviour
{
    public GameObject memoryPrototype;
    float startTime;
    float endTime;
    public int memoryVoices = 16;
    List<MemoryController> memories;
    public Camera player;
    Vector3 randomPosition;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("VRPlayer").GetComponent<Camera>();

        memories = new List<MemoryController>();
        for (int i = 0; i < memoryVoices; i++)
        {
            GameObject memory = GameObject.Instantiate(memoryPrototype);
            memory.SetActive(false);
            memory.transform.parent = transform;
            memory.name = "memory/" + (i + 1);
            memories.Add(memory.GetComponent <MemoryController>());


        }
    }
    public void initializeMemory()
    {
        foreach (MemoryController memory in memories)
        {
            if (memory.gameObject.activeInHierarchy) continue;

            memory.gameObject.SetActive(true);

            memory.fadeIn = true;
            memory.explode = false;
            memory.memoryStartTime = startTime;
            memory.memoryEndTime = endTime;

                randomPosition.x = Random.Range(-1f, 1f);
                randomPosition.z = Random.Range(-1f, 1f);
                randomPosition.y = Random.Range(-0.5f, 0.5f);

           
            float radius = Random.Range(0.5f, 1.4f);

            memory.transform.position = player.transform.position + randomPosition.normalized*radius;
            return;
           
        }

    }
    public void setStartTime(float v)
    {
        startTime = v;
    }

    public void setEndTime(float v)
    {
        endTime = v;
    }
}
