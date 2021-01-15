using UnityEngine;
using UnityEngine.UI;
using System.Collections;       //引用 系統.集合 API (包含協同程序)

public class NPC : MonoBehaviour
{
    [Header("NPC 資料")]
    public NPCData data;
    [Header("對話框")]
    public GameObject dialog;
    [Header("對話內容")]
    public Text textContent;
    [Header("對話者名稱")]
    public Text textName;
    [Header("對話間隔")]
    public float interval = 0.2f;




    /// <summary>
    /// 玩家是否進入感應區
    /// </summary>
    public bool playerInArea;
    private Animator ani;



    // 定義列舉 eunm (下拉式選單 - 只能選一個)
    public enum NPCSate
    {
        第一段對話, 任務進行中, 完成任務
    }

    //列舉欄位
    //修飾詞 列舉名稱 自訂欄位名稱 指定 預設值;
    public NPCSate state = NPCSate.第一段對話;   // if後面不加,預設值為第一個.


    /* 協同程序
    private void Start()
    {
        // 啟動協同程序
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
         print("");
         yield return new WaitForSeconds(1.5f);
        print("1.5秒後");
        yield return new WaitForSeconds(2f);
        print("2秒後");
    }
    */
    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Yong")
        {
            playerInArea = true;
            StartCoroutine(Dialog());
            ani.SetBool("對話開關", true);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Yong")
        {
            playerInArea = false;
            Stopdialog();
            ani.SetBool("對話開關", false);
        }
    }

    /// <summary>
    /// 停止對話
    /// </summary>
    private void Stopdialog()
    {
        dialog.SetActive(false);    //關閉對話框
        StopAllCoroutines();        //停止所有協程
    }

    /// <summary>
    /// 開始對話
    /// </summary>
    private IEnumerator Dialog()
    {
        /**
        //print(data.dialogA);       // 取得字串全部資料
        //print(data.dialogA[3]);    //取得字串局部資料:語法[編號]

        for 迴圈:重複處理相同程式
        for (int i = 0; i < 10; i++)
        {
                print("我是迴圈：" + i);
        }
        
        for (int apple = 1; apple < 100; apple++)
        {
            print("迴圈：" + apple);
        }
        */

        //顯示對話框
        dialog.SetActive(true);
        // 清空文字
        textContent.text = "";
        // 對話者名稱 指定為 此物件的名稱
        textName.text = name;

        //要說的對話
        string dialogString = data.dialogA;
        

        switch (state)
        {
            case NPCSate.第一段對話:
                dialogString = data.dialogA;
                break;
            case NPCSate.任務進行中:
                dialogString = data.dialogB;
                break;
            case NPCSate.完成任務:
                dialogString = data.dialogC;
                break;
        }

        // 字串的長度 dialogA.Length
        for (int i = 0; i < dialogString.Length; i++)
        {
            // print(data.dialogA[i]);
            // 文字 串聯
            textContent.text += dialogString[i] + "";
            yield return new WaitForSeconds(interval);
        }

    }
}
