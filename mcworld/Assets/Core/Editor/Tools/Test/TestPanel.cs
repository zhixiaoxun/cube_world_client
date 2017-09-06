using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using System.IO;


[System.Serializable]
public class RoleParameter
{
    public string roleName;  // 角色名  
    public string roleID;    //角色ID  

    public float Hp;     // 血量  
    public float Spd;   //速度  
    public float Atk;  //攻击力  
    public float Def;
    public float att1Atk;    // 攻击1 的攻击力  
    public float att2Atk;    // 攻击2 的攻击力  
    public float att3Atk;    // 攻击3的攻击力  
    public float att4Atk;    // 技能攻击的攻击力  

    public RoleAttackParameter[] attackArr;

    public RoleAppearAnimationType[] roleAppearAnimationArr;

    public RoleParameter()
    {
        roleName = "0";
        roleID = "0";
        Hp = 0;
        Spd = 0;
        Atk = 0;
        Def = 0;
        att1Atk = 0;
        att2Atk = 0;
        att3Atk = 0;
        att4Atk = 0;

        attackArr = new RoleAttackParameter[4];
        for (int i = 0; i < 4; i++)
        {
            attackArr[i] = new RoleAttackParameter();
        }

        roleAppearAnimationArr = new RoleAppearAnimationType[3];
        for (int i = 0; i < 3; i++)
        {
            roleAppearAnimationArr[i] = RoleAppearAnimationType.Standbyaction_1;
        }
    }
}

[System.Serializable]
public class RoleAttackParameter // 角色攻击参数  
{
    public string attackName;  // 攻击名  
    public FxParameter[] FxArr;
    public string soundID;   //攻击音乐ID  
    public RoleAttackParameter()
    {
        attackName = "0";
        soundID = "0";
        FxArr = new FxParameter[3];
        for (int i = 0; i < 3; i++)
        {
            FxArr[i] = new FxParameter();
        }
    }
}

[System.Serializable]
public class FxParameter  // 特效ID pos  time  
{
    public string fxID;       //特效ID  
    public float time;     //特效生成时间  
    public string path; // 子物体节点路径  

    public FxParameter()
    {
        fxID = "0";
        time = 0;
        path = "0";
    }
}

[System.Serializable]
public enum RoleAppearAnimationType  // 角色出场动画类型  
{
    Standbyaction_1 = 0,
    Standbyaction_2,
    Standbyaction_3,
    Walk,
    Run,
    Attack_1,
    Attack_2,
    Attack_3,
    Beaten,
    Deathaction,
    Thevictoryofthebattle,
    Thecaster,
}

public enum CHARACTORSELECT
{
    ONE,
    TWO,
    THREE,
}


public class CreateMyWindown : EditorWindow
{
    private static bool isOnce = true; //是否是第一次打开窗口  
    private static Rect windowRect = new Rect(100, 100, 1600, 600);  //定义窗口位置和宽高  

    private static CHARACTORSELECT charSelect = CHARACTORSELECT.ONE;  //实例化选择角色枚举  
    private string[] FxEditor = { "FxEditor", "FxChecker" }; //Toolbar 选项  
    private int FxEditorSelect = 0;  //Fx  Toolbar选择的项  
    private Vector3 scrollPos = new Vector2(0, 0); // scrollView坐标  

    private int num = 0; //记录选择角色枚举的值  
    private RoleParameter CurrentRoleParameter = new RoleParameter(); // 当前角色参数  

    [MenuItem("扩展工具/Core/编辑器参考/CreateWindow")] //创建角色按钮  
    static void Init()
    {
        CreateMyWindown window = (CreateMyWindown)EditorWindow.GetWindow(typeof(CreateMyWindown));  //实例化窗口  
        window.position = windowRect;  //设置窗口坐标和宽高  
        window.Show();  //显示窗口  
    }

    private static string getSelectObjNodePath = "";
    [MenuItem("扩展工具/Core/编辑器参考/GetNodePath")]
    static void CopyPosition()
    {
        GameObject obj = Selection.activeGameObject;
        if (obj != null)
        {  //myTransform.FindChild("Banda/_model_Banda");  
            getSelectObjNodePath = GetGameObjectPath(obj);
        }
    }

    public static void ShowDisplayDialog()
    {
        if (EditorUtility.DisplayDialog("error", "Cannot find the GameController, Please go to the firse scene", "Yes", "No")) { } //在编辑状态下不要使用Application.LoadLevel(0); 不然会将整个scene转换为 0 场景  
    }

    public static string GetGameObjectPath(GameObject obj)
    {
        string path = "/" + obj.name;
        while (obj.transform.parent != null)
        {
            obj = obj.transform.parent.gameObject;
            path = "/" + obj.name + path;
        }
        return path;
    }

    void OnGUI()
    {
        CreateUi();
    }

    void OnDisable()  // 关闭窗口调用该方法  
    {
        isOnce = true;  //关闭窗口时重新设置 第一次打开 值为 true  
    }

    private void CreateUi() //创建界面  
    {
        CurrentRoleParameter.roleName = Enum.GetName(typeof(CHARACTORSELECT), CreateRoleFxItems());  //CreateRoleFxItems ().ToString().Split('_')[1];  //选择角色区域        

        EditorGUILayout.BeginVertical();
        //循环3次，依次创建 3个  
        for (int i = 0; i < 3; i++)
        {
            CurrentRoleParameter = RoleScrollow();
        }
        EditorGUILayout.EndVertical();
    }

    private CHARACTORSELECT CreateRoleFxItems()  // 选择角色区域  
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();// 开始水平布局，选择角色枚举  

        string roleNam = Enum.GetName(typeof(CHARACTORSELECT), charSelect); //获取角色名字  
        GUILayout.Label("" + roleNam, GUILayout.Width(150), GUILayout.Height(25));
        CHARACTORSELECT oldRoleName = charSelect;
        charSelect = (CHARACTORSELECT)EditorGUILayout.EnumPopup("", charSelect, GUILayout.Width(400), GUILayout.Height(25));

        if (oldRoleName != charSelect)
        {
            roleNam = Enum.GetName(typeof(CHARACTORSELECT), charSelect);
        }

        if (isOnce)  // 第一次打开窗口时读取数据  
        {
            isOnce = !isOnce;
        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Before", GUILayout.Width(100), GUILayout.Height(25)))  //上一个角色按钮  
        {
            num = (int)charSelect;
            if (num > 1)
            {
                num--;
            }
            else
            {
                num = 34;
            }

            charSelect = (CHARACTORSELECT)(num);
            roleNam = Enum.GetName(typeof(CHARACTORSELECT), charSelect); //获取角色名字  
        }

        if (GUILayout.Button("Next", GUILayout.Width(100), GUILayout.Height(25))) // 下一个角色按钮  
        {
            num = (int)charSelect;
            if (num < 34)
            {
                num++;
            }
            else
            {
                num = 0;
            }

            charSelect = (CHARACTORSELECT)(num);
            roleNam = Enum.GetName(typeof(CHARACTORSELECT), charSelect); //获取角色名字  
        }
        // Fx  Toolbar  
        FxEditorSelect = GUILayout.Toolbar(FxEditorSelect, FxEditor, GUILayout.Width(200), GUILayout.Height(25));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        return charSelect;
    }

    private RoleParameter RoleScrollow()
    {
        scrollPos = GUI.BeginScrollView(new Rect(0, 80, position.width - 0, position.height - 80), scrollPos, new Rect(0, 80, 2100, 650));  // 创建ScrollView  
        EditorGUILayout.Space();
        CurrentRoleParameter = RoleParameter();

        GUI.EndScrollView();
        return CurrentRoleParameter;
    }

    private RoleParameter RoleParameter()
    {
        EditorGUILayout.BeginVertical("box", GUILayout.Width(1530));
        EditorGUILayout.BeginHorizontal("box", GUILayout.Width(1530), GUILayout.Height(80)); //  
        CurrentRoleParameter.roleName = RoleNameID("RoleName", CurrentRoleParameter.roleName); // 角色名字 输入框  
        CurrentRoleParameter.roleID = RoleNameID("RoleID", CurrentRoleParameter.roleID);          // 角色ID输入框  

        AddHpSpdSlider();

        string[] attackNameArr = { "Attack1", "Attack2", "Attack3", "Skill" };
        for (int i = 0; i < 4; i++)  // 攻击参数  
        {
            CurrentRoleParameter.attackArr[i] = RoleAttackFx(attackNameArr[i], CurrentRoleParameter.attackArr[i]);
        }

        CurrentRoleParameter.roleAppearAnimationArr = AppearAnimationParameter(CurrentRoleParameter.roleAppearAnimationArr);  // 角色出场动画  

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();  // 最下方的保存按钮  
        GUILayout.Label("", GUILayout.Width(750));
        if (GUILayout.Button("Save", GUILayout.Width(120), GUILayout.Height(25)))
        {
            SaveRoleParameter(CurrentRoleParameter);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        return CurrentRoleParameter;
    }

    private void AddHpSpdSlider() // 添加角色血量，攻击力  
    {
        string[] roleHpTitle = { "Hp", "Spd", "Atk", "Def", "att1Atk", "att2Atk", "att3Atk", "att4Atk" };
        HpSpdSlider(roleHpTitle);
    }

    private void HpSpdSlider(string[] roleHpTitle) // 角色血量，攻击力，速度等 Slider  
    {
        EditorGUILayout.BeginVertical("box", GUILayout.Width(200));
        GUILayout.Label("        parameter");

        EditorGUILayout.BeginHorizontal("box");
        EditorGUILayout.BeginVertical();
        foreach (string title in roleHpTitle)
        {
            GUILayout.Label(title, GUILayout.Width(50), GUILayout.Height(20));
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        CurrentRoleParameter.Hp = EditorGUILayout.Slider("", CurrentRoleParameter.Hp, 0, 1, GUILayout.Height(20));  // 血量  
        CurrentRoleParameter.Spd = EditorGUILayout.Slider("", CurrentRoleParameter.Spd, 0, 1, GUILayout.Height(20));  // 速度  
        CurrentRoleParameter.Atk = EditorGUILayout.Slider("", CurrentRoleParameter.Atk, 0, 1, GUILayout.Height(20)); //攻击力  
        CurrentRoleParameter.Def = EditorGUILayout.Slider("", CurrentRoleParameter.Def, 0, 1, GUILayout.Height(20));  //   

        CurrentRoleParameter.att1Atk = EditorGUILayout.Slider("", CurrentRoleParameter.att1Atk, 0, 1, GUILayout.Height(20));     // 攻击1 的攻击力  
        CurrentRoleParameter.att2Atk = EditorGUILayout.Slider("", CurrentRoleParameter.att2Atk, 0, 1, GUILayout.Height(20));     //攻击2的攻击力  
        CurrentRoleParameter.att3Atk = EditorGUILayout.Slider("", CurrentRoleParameter.att3Atk, 0, 1, GUILayout.Height(20));      //攻击3的攻击力  
        CurrentRoleParameter.att4Atk = EditorGUILayout.Slider("", CurrentRoleParameter.att4Atk, 0, 1, GUILayout.Height(20));     //技能攻击的攻击力  
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
    }

    private void SaveRoleParameter(RoleParameter role) // 将信息保存至 txt 中, 参数为 当前角色  
    {

    }

    private string RoleNameID(string title, string valueID)  // 左侧副本名， 副本ID， 参数1 为标题，参数2 为需要填写的ID, ID 为string类型  
    {
        EditorGUILayout.BeginVertical("box", GUILayout.Width(110), GUILayout.Height(60));
        EditorGUILayout.BeginVertical("box", GUILayout.Width(130)); //标题区域  
        GUILayout.Label(title, GUILayout.Width(100), GUILayout.Height(40));
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("box", GUILayout.Width(130), GUILayout.Height(60));
        valueID = EditorGUILayout.TextField("", valueID, GUILayout.Width(110), GUILayout.Height(20));  // 填写的ID  
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
        return valueID;
    }

    private RoleAttackParameter RoleAttackFx(string attackName, RoleAttackParameter roleattackParameter)
    {
        FxIdTimePos();
        roleattackParameter = RoleAttack("                           " + attackName, roleattackParameter);

        return roleattackParameter;
    }

    private void FxIdTimePos()  // 攻击名左边 的 提示Lable  
    {
        EditorGUILayout.BeginVertical("box", GUILayout.Width(40));

        GUILayout.Label("", GUILayout.Height(60));

        string[] fxArr = { "Trans", "FxID", "Time ", "Path", "", "Sound" };
        for (int i = 0; i < 6; i++)
        {
            GUILayout.Label(fxArr[i]);
        }
        EditorGUILayout.EndVertical();
    }

    private RoleAttackParameter RoleAttack(string title, RoleAttackParameter roleattackParameter)  //一个攻击对应的三种特效类型， 参数为攻击名  
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal("box");
        EditorGUILayout.BeginVertical(GUILayout.Width(200));
        EditorGUILayout.BeginHorizontal("box");
        GUILayout.Label(title);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginHorizontal("box");
        GUILayout.Label("      自 身", GUILayout.Width(80));  //提示Lable 在自身位置显示的 特效  
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal("box");
        GUILayout.Label("      追 踪", GUILayout.Width(80));   //提示Lable 追踪目标的特效  
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal("box");
        GUILayout.Label("      目 标", GUILayout.Width(80));   //提示Lable 在目标位置生成的特效  
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        for (int i = 0; i < 3; i++)
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(70));
            roleattackParameter.FxArr[i] = FxParameter(roleattackParameter.FxArr[i]);   //循环3 次生成 一个攻击对应的 3 种特效的参数  
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();

        roleattackParameter.soundID = EditorGUILayout.TextField("", roleattackParameter.soundID, GUILayout.Width(278));  // 攻击对应的音效ID  

        EditorGUILayout.EndVertical();

        return roleattackParameter;
    }

    private FxParameter FxParameter(FxParameter fxParameter)   // 一个 特效对应的 ID， 生成时间， 生成位置  
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(90));

        Transform roleTransform = null;
        roleTransform = EditorGUILayout.ObjectField(roleTransform, typeof(Transform), true, GUILayout.Width(90)) as Transform;

        if (roleTransform != null)
        {
            fxParameter.fxID = roleTransform.name;
        }
        fxParameter.fxID = EditorGUILayout.TextField("", fxParameter.fxID, GUILayout.Width(90));
        fxParameter.time = EditorGUILayout.FloatField("", fxParameter.time, GUILayout.Width(90));
        fxParameter.path = EditorGUILayout.TextField("", fxParameter.path, GUILayout.Width(90));
        if (GUILayout.Button("粘贴Pos", GUILayout.Width(60), GUILayout.Height(15)))
        {
            fxParameter.path = getSelectObjNodePath;
        }

        EditorGUILayout.EndVertical();
        return fxParameter;
    }

    private RoleAppearAnimationType[] AppearAnimationParameter(RoleAppearAnimationType[] roleAppearAnimationTypeArr) // 出场动画  
    {
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("      AppearAnimation");
        for (int i = 0; i < roleAppearAnimationTypeArr.Length; i++)
        {
            roleAppearAnimationTypeArr[i] = (RoleAppearAnimationType)EditorGUILayout.EnumPopup("", roleAppearAnimationTypeArr[i], GUILayout.Width(150), GUILayout.Height(40));
        }

        EditorGUILayout.EndVertical();
        return roleAppearAnimationTypeArr;
    }

    void OnInspectorUpdate()
    {
        Repaint(); //重新绘制  
    }
}