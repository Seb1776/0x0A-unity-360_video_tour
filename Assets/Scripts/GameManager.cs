using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RoomLocation[] locations;
    [SerializeField] private string debugLocation;
    [SerializeField] private float fadeInDur;
    [SerializeField] private Animator fadeAnimator;
    private GameObject activatedSphere;
    [SerializeField] private GameObject initSphere;

    void Start()
    {
        activatedSphere = initSphere;
        StartCoroutine(StartLoad());
    }

    IEnumerator StartLoad()
    {
        yield return new WaitForSeconds(.1f);

        foreach (RoomLocation rl in locations)
            if (rl.roomSphere != initSphere)
                rl.roomSphere.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ChangeRoom(debugLocation);
    }

    public void ChangeRoom(string name)
    {
        foreach (RoomLocation rl in locations)
        {
            if (rl.roomName == name)
            {
                StartCoroutine(TriggerFade(activatedSphere, rl.roomSphere));
                break;
            }
        }
    }

    public void ToggleObj(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }

    IEnumerator TriggerFade(GameObject dis, GameObject en)
    {
        fadeAnimator.SetTrigger("fade");
        activatedSphere = en;
        yield return new WaitForSeconds(fadeInDur);
        fadeAnimator.ResetTrigger("fade");
        dis.SetActive(false);
        en.SetActive(true);
    }
}

[System.Serializable]
public class RoomLocation
{
    public string roomName;
    public GameObject roomSphere;
}
