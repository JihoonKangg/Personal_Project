using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //Input output 데이터 출력
using UnityEngine.UIElements;

[System.Serializable] //직렬화
public class SaveData //데이터를 저장시킬 클래스
{
    public Vector3 playerPos; //캐릭터 위치
    public int worldLevel; //월드레벨
    public int WarriorLevel; //전사 무기레벨
    public int WizardLevel; //마법사 무기레벨
    public int EXP;         //월드레벨 경험치
    public float Warrior_hp; //전사 hp
    public float Wizard_hp; //마법사 hp

    //인벤토리 정보
    public List<int> invenArrayNum = new List<int>();
    public List<string> invenItemName = new List<string>();
    public List<int> invenItemNumber = new List<int>();

    //퀘스트 정보
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

        if(!Directory.Exists(SAVE_DATA_DIRECTORY)) //파일이 존재하는지 유무 확인하는 코드
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY); //없으면 파일이 생성됨
    }

    public void SaveData()
    {
        thePlayer = FindObjectOfType<CharacterChangeSystem>();
        inven = FindObjectOfType<Inventory>();
        quest = FindObjectOfType<QuestController>();
        exp = FindObjectOfType<PlayerLevel>();
        wiz = SceneData.Inst.wizard;
        war = SceneData.Inst.warrior;

        //월드레벨 저장
        saveData.worldLevel = SceneData.Inst.WorldLevel;

        //exp 저장
        saveData.EXP = exp.EXP;

        //hp 저장
        saveData.Warrior_hp = war.curHP;
        saveData.Wizard_hp = wiz.curHP;

        //무기레벨 저장
        saveData.WarriorLevel = war.W_LEVEL;
        saveData.WizardLevel = wiz.W_LEVEL;

        //캐릭터 위치 저장
        saveData.playerPos = thePlayer.myPlayer[0].transform.position;

        //인벤토리 데이터 저장과정
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

        //퀘스트 데이터 저장과정
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

        Debug.Log("저장 완료");
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

            //Json화 된 코드를 푸는 과정
            saveData = JsonUtility.FromJson<SaveData>(loadJson);
            inven = FindObjectOfType<Inventory>();
            quest = FindObjectOfType<QuestController>();
            thePlayer = FindObjectOfType<CharacterChangeSystem>();
            wiz = SceneData.Inst.wizard;   
            war = SceneData.Inst.warrior;
            exp = FindObjectOfType<PlayerLevel>();

            //월드레벨
            SceneData.Inst.WorldLevel = saveData.worldLevel;

            //exp
            SceneData.Inst.Saveexp = saveData.EXP;

            //hp
            SceneData.Inst.SavewarcurHP = saveData.Warrior_hp;
            SceneData.Inst.SavewizcurHP = saveData.Wizard_hp;

            //무기레벨
            SceneData.Inst.Savewarlevel = saveData.WarriorLevel;
            SceneData.Inst.Savewizlevel = saveData.WizardLevel;

            //플레이어 위치
            thePlayer.transform.position = saveData.playerPos;

            //인벤토리
            for(int i = 0; i < saveData.invenItemName.Count; i++)
                inven.LoadToInven(saveData.invenArrayNum[i], saveData.invenItemName[i], saveData.invenItemNumber[i]);

            //퀘스트
            for (int i = 0; i < saveData.questcode.Count; i++)
                quest.LoadToQuest(saveData.questArrayNum[i], saveData.questcode[i]);

            Debug.Log("로드 완료");
        }
        else Debug.Log("세이브파일이 없습니다");
    }
}
