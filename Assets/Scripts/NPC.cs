using UnityEngine;

[System.Serializable]
public class NPC : MonoBehaviour
{

    public Transform ChatBackGround;
    public Transform NPCCharacter;

    private DialogSystem dialogSystem;

    public string Name;
    public Quest givenQuest;

    [TextArea(5, 10)]
    public string[] sentences;

    void Start()
    {
        dialogSystem = FindObjectOfType<DialogSystem>();
    }

    void Update()
    {
        Vector3 Pos = Camera.main.WorldToScreenPoint(NPCCharacter.position);
        Pos.y += 100;
        ChatBackGround.position = Pos;
    }

    public void OnTriggerStay(Collider other)
    {
        this.gameObject.GetComponent<NPC>().enabled = true;
        FindObjectOfType<DialogSystem>().EnterRangeOfNPC();
        if ((other.gameObject.tag == "Player")) //  && Input.GetKeyDown(KeyCode.F)
        {
            this.gameObject.GetComponent<NPC>().enabled = true;
            dialogSystem.Names = Name;
            dialogSystem.dialogLines = sentences;
            dialogSystem.givenQuest = givenQuest;
            FindObjectOfType<DialogSystem>().NPCName();
        }
    }

    public void OnTriggerExit()
    {
        FindObjectOfType<DialogSystem>().OutOfRange();
        this.gameObject.GetComponent<NPC>().enabled = false;
    }
}

