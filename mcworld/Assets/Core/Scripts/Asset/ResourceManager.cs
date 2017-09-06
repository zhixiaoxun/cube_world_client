//////////////////////////////////////////////////////////////////////////
// Resources目录
// 在项目根目录中创建Resources文件夹来保存文件。
// 可以使用Resources.Load("文件名字，注：不包括文件后缀名");把文件夹中的对象加载出来。
// 注：此方可实现对文件实施“增删查改”等操作，但打包后不可以更改了。
//////////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////////////
// Application.dataPath 目录
// 
// 在直接使用Application.dataPath来读取文件进行操作。
// 注：移动端是没有访问权限的。
// 
// 编辑器模式下 "file://E:/MyProject/Assets"
// 
// windows程序 "file://E:/MyProjectBin/MyProject_Data"
//      E:/MyProject/        ----此目录下是Unity工程
//      E:/MyProjectBin/     ----此目录下是Unity工程生成的Windows程序
//      E:/MyProjectBin/MyProject.exe     ----exe程序
//      E:/MyProjectBin/MyProject_Data    ----Unity资源目录 也就是Application.dataPath
//      
// Android     
//      /data/app/com.baiyun.test-1.apk   ---- com.baiyun.test 为包名 1为编译选项中的Bundle Version Code
//      
// IOS
//      Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/xxx.app/Data -- xxxx为guid xxx为应用名
//////////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////////////
// Application.streamingAssetsPath 目录
// 
// 电脑中可实现对文件实施“增删查改”等操作，但在移动端只支持读取操作。
// 
// 编辑器模式下 "file://E:/MyProject/Assets/StreamingAssets"
// 
// windows程序 "file://E:/MyProjectBin/MyProject_Data/StreamingAssets"
//      E:/MyProject/        ----此目录下是Unity工程
//      E:/MyProjectBin/     ----此目录下是Unity工程生成的Windows程序
//      E:/MyProjectBin/MyProject.exe     ----exe程序
//      E:/MyProjectBin/MyProject_Data    ----Unity资源目录 也就是Application.dataPath
//      E:/MyProjectBin/MyProject_Data/StreamingAssets    ----Application.streamingAssetsPath
//      
// Android     
//      jar:file:///data/app/com.baiyun.test-1.apk!/assets   ---- com.baiyun.test 为包名 1为编译选项中的Bundle Version Code
//
// IOS
//      Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/xxx.app/Data/Raw
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
// Application.persistentDataPath

// 该文件存在手机沙盒中，因为外部不能直接存放文件，
// app可以实现对此目录的增删改
// 1.通过服务器直接下载保存到该位置，也可以通过Md5码比对下载更新新的资源
// 2.没有服务器的，只有间接通过文件流的方式从本地读取并写入Application.persistentDataPath文件下，然后再通过Application.persistentDataPath来读取操作。
// 注：在Pc/Mac电脑 以及Android跟Ipad、ipone都可对文件进行任意操作，另外在IOS上该目录下的东西可以被iCloud自动备份。
// 
// 编辑器模式下 "file://C:/Users/baiyun/AppData/LocalLow/DefaultCompany/credits kit stripped and ready to publish"
// 
// windows程序 "file://C:/Users/baiyun/AppData/LocalLow/DefaultCompany/credits kit stripped and ready to publish"
// 
// Android     
//      /storage/emulated/0/Android/data/com.baiyun.test/files   ---- com.baiyun.test 为包名
//      
// IOS
//      Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/Documents
//////////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////////////
// Application.temporaryCachePath
//
// 操作方式跟上面Application.persistentDataPath类似。除了在IOS上不能被iCloud自动备份。
//
// 编辑器模式下 "file://C:/Users/baiyun/AppData/LocalLow/DefaultCompany/credits kit stripped and ready to publish"
// 
// windows程序 "file://C:/Users/baiyun/AppData/LocalLow/DefaultCompany/credits kit stripped and ready to publish"
//
// Android     
//      /storage/emulated/0/Android/data/com.baiyun.test/cache   ---- com.baiyun.test 为包名
//
// IOS
//      Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/Library/Caches
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;
using SLua;

// 统一的资源加载，所以资源从这里过，只需传入资源url，无需要关心资源在resources中，或是在哪个bundle中
namespace Core.Asset
{
    [CustomLuaClass]
    public partial class ResourceManager : Singleton<ResourceManager>
    {
        public static new ResourceManager Instance
        {
            get { return _instance; }
        }
        protected override IEnumerator OnInitCoroutine()
        {
            yield return 1;
        }

        // 判断是否为Core资源
        public bool IsCoreResource(string url)
        {
            return url.ToLower().StartsWith(ResourceDefine.CORE_RESOURCE_HEADER.ToLower());
        }

        // 从Resources目录下加载文本文件 不带后缀，但必须是Unity可以识别的资源文件
        // path as "Core/Lua/Core"
        public byte[] LoadTextAssetFromResource(string path)
        {
           return (Resources.Load(path) as TextAsset).bytes;
        }

		public GameObject LoadPrefabAssetFromResource(string path)
		{
			return GameObject.Instantiate(Resources.Load(path)) as GameObject;
		}

        // 根据项目名和资源名返回资源在resource目录下的相对路径（不带后缀）
        // 例如：传入 Demo, ui/bag 返回Demo/ui/bag
        public string GetResourceName(string gameName, string fileName)
        {
            return string.Format("{0}/{1}", gameName, fileName);
        }

        public T LoadFromResources<T>(string path) where T : Object
        {
            //if (ResourceMgrRuntime.Instance != null)
            //{
            //    return ResourceMgrRuntime.Instance.LoadFromResources<T>(path, true);
            //}
            return Resources.Load<T>(path);
        }
        public Object LoadFromResources(string path, Type type)
        {
            //if (ResourceMgrRuntime.Instance != null)
            //{
            //    return ResourceMgrRuntime.Instance.LoadFromResources(path, type, true);
            //}
            return Resources.Load(path, type);
        }
        public string LoadXMLText(string resourceName)
        {
            string configText = ((TextAsset)Resources.Load(resourceName)).text;

#if !UNITY_WEBPLAYER

            if (Application.isEditor == false && Application.isWebPlayer == false)
            {
                try
                {
                    string exePath = System.IO.Directory.GetParent(Application.dataPath).FullName;
                    string fileConfigPath = System.IO.Path.Combine(exePath, resourceName + ".xml");

                    if (System.IO.File.Exists(fileConfigPath))
                        configText = System.IO.File.ReadAllText(fileConfigPath);
                }
                catch (System.Exception ex)
                {
                    Debug.Log(ex.ToString());
                }
            }
#endif

            return configText;
        }

        public Shader LoadShader(string path)
        {
            return Resources.Load(path, typeof(Shader)) as Shader;
        }
    }
}