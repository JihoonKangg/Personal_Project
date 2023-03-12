using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //Input output ������ ���
using UnityEngine.UIElements;

[System.Serializable] //����ȭ
public class SaveData //�����͸� �����ų Ŭ����
{
    public Vector3 playerPos; //ĳ���� ��ġ
    public int worldLevel; //���巹��
    public int WarriorLevel; //���� ���ⷹ��
    public int WizardLevel; //������ ���ⷹ��
    public int EXP;         //���巹�� ����ġ
    public float Warrior_hp; //���� hp
    public float Wizard_hp; //������ hp

    //�κ��丮 ����
    public List<int> invenArrayNum = new List<int>();
    public List<string> invenItemName = new List<string>();
    public List<int> invenItemNumber = new List<int>();

    //����Ʈ ����
    public List<int> questArrayNum = new List<int>();
    public List<int> questcode = new List<int>();
}

public class SaveLoad : MonoBehaviour
{
    private SaveData saveData = new SaveData();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveFile.txt";

    private CharacterChangeSystem thePlayer;
    private Inventory inven;
    private QuestController quest;
    private Warrier war;
    private Wizard wiz;
    private PlayerLevel exp;

    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves";

        if(!Directory.Exists(SAVE_DATA_DIRECTORY)) //������ �����ϴ��� ���� Ȯ���ϴ� �ڵ�
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY); //������ ������ ������
    }

    public void SaveData()
    {
        thePlayer = FindObjectOfType<CharacterChangeSystem>();
        inven = FindObjectOfType<Inventory>();
        quest = FindObjectOfType<QuestController>();
        exp = FindObjectOfType<PlayerLevel>();
        wiz = SceneData.Inst.wizard;
        war = SceneData.Inst.warrior;

        //���巹�� ����
        saveData.worldLevel = SceneData.Inst.WorldLevel;

        //exp ����
        saveData.EXP = exp.EXP;

        //hp ����
        saveData.Warrior_hp = war.curHP;
        saveData.Wizard_hp = wiz.curHP;

        //���ⷹ�� ����
        saveData.WarriorLevel = war.W_LEVEL;
        saveData.WizardLevel = wiz.W_LEVEL;

        //ĳ���� ��ġ ����
        saveData.playerPos = thePlayer.myPlayer[0].transform.position;

        //�κ��丮 ������ �������
        InventorySlot[] invenslots = inven.GetInvenSlots();
        for(int i = 0; i< invenslots.Length; i++)
        {
            if (invenslots[i].item != null)
            {
                saveData.invenArrayNum.Add(i);
                saveData.invenItemName.Add(invenslots[i].item.itemName);
                saveData.invenItemNumber.Add(invenslots[i].itemCount);
            }
        }

        //����Ʈ ������ �������
        QuestSlot[] questslots = quest.GetQuestSlots();
        for (int i = 0; i < questslots.Length; i++)
        {
            if (questslots[i].quest != null)
            {
                saveData.questArrayNum.Add(i);
                saveData.questcode.Add(questslots[i].quest.QuestCode);
            }
        }

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);

        Debug.Log("���� �Ϸ�");
        Debug.Log(json);
    }

    public void LoadScene()
    {
        SaveLoadingSceneController.LoadScene();
    }

    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);

            //Jsonȭ �� �ڵ带 Ǫ�� ����
            saveData = JsonUtility.FromJson<SaveData>(loadJson);
            inven = FindObjectOfType<Inventory>();
            quest = FindObjectOfType<QuestController>();
            thePlayer = FindObjectOfType<CharacterChangeSystem>();
            wiz = SceneData.Inst.wizard;   
            war = SceneData.Inst.warrior;
            exp = FindObjectOfType<PlayerLevel>();

            //���巹��
            SceneData.Inst.WorldLevel = saveData.worldLevel;

            //exp
            SceneData.Inst.Saveexp = saveData.EXP;

            //hp
            SceneData.Inst.SavewarcurHP = saveData.Warrior_hp;
            SceneData.Inst.SavewizcurHP = saveData.Wizard_hp;

            //���ⷹ��
            SceneData.Inst.Savewarlevel = saveData.WarriorLevel;
            SceneData.Inst.Savewizlevel = saveData.WizardLevel;

            //�÷��̾� ��ġ
            thePlayer.transform.position = saveData.playerPos;

            //�κ��丮
            for(int i = 0; i < saveData.invenItemName.Count; i++)
                inven.LoadToInven(saveData.invenArrayNum[i], saveData.invenItemName[i], saveData.invenItemNumber[i]);

            //����Ʈ
            for (int i = 0; i < saveData.questcode.Count; i++)
                quest.LoadToQuest(saveData.questArrayNum[i], saveData.questcode[i]);

            Debug.Log("�ε� �Ϸ�");
        }
        else Debug.Log("���̺������� �����ϴ�");
    }
}
