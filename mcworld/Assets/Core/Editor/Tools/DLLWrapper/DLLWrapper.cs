using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Core.Utils;

namespace Assets.Core.Editor.Tools.DllWrapper
{
    public class DllWrapper : EditorWindow
    {
        public class WrapperFunction
        {
			public string EntryPoint = null; // 入口点
            public string FuncName = null; // 函数名
			public int DeclareStartLineNum = 0; // 函数声明的起始行
			public int DeclareEndLineNum = 0; // 函数声明的结束行
			public List<string> FuncBodyLines = new List<string>(); //函数声明内容集合
            public List<string> OriginalLines = new List<string>(); //原始内容行集合
			public string ReturnType = null; // 返回值字符串
			public string DelegateSurffix = "_delegate";

			public List<string> GetParamList()
			{
				string content = null; // 拼成一行
				foreach (var str in FuncBodyLines)
				{
					content += (str.Trim(new char[] { ' ', '\t' }));
				}

				// 取()之间的内容
				string strStart = "(";
				int subStartIndex = content.LastIndexOf(strStart) + strStart.Length;
				string strEnd = ")";
				int subEndIndex = content.LastIndexOf(strEnd);
				string paramStr = content.Substring(subStartIndex, subEndIndex - subStartIndex).Replace(", ", ",");
				paramStr = paramStr.Replace(",", " ");
				var splitStr = paramStr.Split(' ');

				List<string> ret = new List<string>();
				for (int i = 0; i < splitStr.Length; i++)
				{
					if (i % 2 != 0)
						ret.Add(splitStr[i]);
				}
				return ret;
			}

            public List<string> MakeDelegateFuncAndBody()
            {
                List<string> content = new List<string>();
				string firstLine = FuncBodyLines[0];
				int spaceCount = firstLine.IndexOf("public");
				string spaces = null;
				for (int i = 0; i < spaceCount; i++)
					spaces += " ";

				// delegate
				foreach (var str in FuncBodyLines)
				{
					string tmp = str.Replace("extern ", "").Replace("static ", "delegate ");
					tmp = tmp.Replace(FuncName, EntryPoint + DelegateSurffix);
					content.Add(tmp);
				}

				// function
				foreach (var str in FuncBodyLines)
                {
					content.Add(str.Replace("extern ", "").Replace(";",""));
                }
				content.Add(spaces + "{");

				string paramStr = "";
				var paramList = GetParamList();
				foreach (var p in paramList)
				{
					paramStr += (", " + p);
				}

				if (ReturnType == "void")
				{
					content.Add(string.Format("{0}{1}RunningDimensions.Native.Invoke<{2}>(WrapHelper.LibPtr{3});",
						spaces, spaces, EntryPoint + DelegateSurffix, paramStr));
				}
				else
				{
					content.Add(string.Format("return {0}{1}RunningDimensions.Native.Invoke<{2}, {3}>(WrapHelper.LibPtr{4});",
						spaces, spaces, ReturnType, EntryPoint + DelegateSurffix, paramStr));
				}
				content.Add(spaces + "}");
				return content;
            }
        }

        public static Dictionary<string, WrapperFunction> FunctionMap = new Dictionary<string, WrapperFunction>();

        [MenuItem("扩展工具/Core/DLLWrapper/生成动态加载卸载DLL支持文件")]
        public static void BuildDynamicDLLWrapper()
        {
            FunctionMap.Clear();

			List<string> fileLines = new List<string>();

            // 源文件路径
            string srcWrapperFilePath = Application.dataPath.Replace("Assets", "Assets/Core/Scripts/CppCore/SwigWrapper/mcworld_client_corePINVOKE.cs");
            string srcWrapperFileTxtPath = Application.dataPath.Replace("Assets", "Assets/Core/Scripts/CppCore/SwigWrapper/mcworld_client_corePINVOKE.cs.txt");
            // 目标文件路径
            string dstWrapperFilePath = Application.dataPath.Replace("Assets", "Assets/Core/Scripts/CppCore/SwigWrapper/mcworld_client_corePINVOKE_Editor.cs");
            string dstWrapperFileTxtPath = Application.dataPath.Replace("Assets", "Assets/Core/Scripts/CppCore/SwigWrapper/mcworld_client_corePINVOKE_Editor.cs.txt");

            // 复制源文件
            if (File.Exists(srcWrapperFileTxtPath))
                File.Delete(srcWrapperFileTxtPath);
            File.Copy(srcWrapperFilePath, srcWrapperFileTxtPath);

            // 复制目标文件
            if (File.Exists(dstWrapperFileTxtPath))
                File.Delete(dstWrapperFileTxtPath);
            File.Copy(srcWrapperFilePath, dstWrapperFileTxtPath);

            // 获取源文件内容
            byte[] srcContent;
            FileHelper.LoadFileByFileStream(srcWrapperFilePath, out srcContent);
            Debug.Log(string.Format("srcpath={0} content={1}", srcWrapperFilePath, srcContent.Length));

            // 修改源文件
            string srcHeadLine = "#if !UNITY_EDITOR\n";
            string srcTailLine = "\n#endif";
            byte[] srcHeadBytes = System.Text.Encoding.UTF8.GetBytes(srcHeadLine);
            byte[] srcTailBytes = System.Text.Encoding.UTF8.GetBytes(srcTailLine);
            byte[] srcContent2 = new byte[srcContent.Length + srcHeadBytes.Length + srcTailLine.Length];
            srcHeadBytes.CopyTo(srcContent2, 0);
            srcContent.CopyTo(srcContent2, srcHeadBytes.Length);
            srcTailBytes.CopyTo(srcContent2, srcContent.Length + srcHeadBytes.Length);
            File.WriteAllBytes(srcWrapperFileTxtPath, srcContent2);

            // 修改目标文件
            string dstHeadLine = "#if UNITY_EDITOR\n";
            string dstTailLine = "\n#endif";
            byte[] dstHeadBytes = System.Text.Encoding.UTF8.GetBytes(dstHeadLine);
            byte[] dstTailBytes = System.Text.Encoding.UTF8.GetBytes(dstTailLine);
            byte[] dstContent2 = new byte[srcContent.Length + dstHeadBytes.Length + dstTailBytes.Length];
            dstHeadBytes.CopyTo(dstContent2, 0);
            srcContent.CopyTo(dstContent2, dstHeadBytes.Length);
            dstTailBytes.CopyTo(dstContent2, srcContent.Length + dstHeadBytes.Length);
            File.WriteAllBytes(dstWrapperFileTxtPath, dstContent2);

			// 将要处理的内容分行读入列表中
			FileStream dstFileStream = new FileStream(dstWrapperFileTxtPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			StreamReader fileReader = new StreamReader(dstFileStream);
			while (!fileReader.EndOfStream)
			{
				fileLines.Add(fileReader.ReadLine());
			}
			fileReader.Close();
			dstFileStream.Close();

			// 分析每一行，收集函数定义信息
			int lineNum = 0;
			while (lineNum < fileLines.Count)
			{
				string trimLine = fileLines[lineNum].Trim(new char[] { ' ', '\t' });

				if (trimLine.StartsWith("[") && trimLine.EndsWith("]") && trimLine.Contains("DllImport"))
				{
					WrapperFunction func = new WrapperFunction();
					func.DeclareStartLineNum = lineNum;
					func.OriginalLines.Add(fileLines[lineNum]);

					// 获得入口点
					string strStart = "EntryPoint=\"";
					int subStartIndex = fileLines[lineNum].LastIndexOf(strStart) + strStart.Length;
					string strEnd = "\")]";
					int subEndIndex = fileLines[lineNum].LastIndexOf(strEnd);
					func.EntryPoint = fileLines[lineNum].Substring(subStartIndex, subEndIndex - subStartIndex);

					while (true) // 继续往下查找直到这个声明结束
					{
						lineNum++;

						if (fileLines[lineNum].Contains("public static extern"))
						{
							// 获得函数名
							func.FuncName = fileLines[lineNum].Trim(new char[] { ' ', '\t' }).Replace('(', ' ').Split(' ')[4];

							// 获得返回值
							var splitStr = fileLines[lineNum].Trim(new char[] { ' ', '\t' }).Split(' ');
							func.ReturnType = splitStr[3];
						}

						func.OriginalLines.Add(fileLines[lineNum]);
						func.FuncBodyLines.Add(fileLines[lineNum]);
						if (fileLines[lineNum].Trim(new char[] { ' ', '\t' }).EndsWith(");"))
						{
							func.DeclareEndLineNum = lineNum;
							break;
						}
					}

					FunctionMap.Add(func.FuncName, func);
				}
				lineNum++;
			}

			// 将dllimport的函数定义转成委托函数
			List<string> NewFileLineList = new List<string>();
			for (int lineno = 0; lineno < fileLines.Count; lineno++)
			{
				bool hasAdd = false;
				foreach (var pairs in FunctionMap)
				{
					var func = pairs.Value;
					if (lineno >= func.DeclareStartLineNum && lineno <= func.DeclareEndLineNum)
					{
						NewFileLineList.Add("//" + fileLines[lineno]); // 将原来的函数定义注释掉

						if (lineno == func.DeclareEndLineNum)
						{
							// 在此处插入函数委托定义实现
							var newFuncLineList = func.MakeDelegateFuncAndBody();
							foreach (var l in newFuncLineList)
							{
								NewFileLineList.Add(l);
							}
						}
						hasAdd = true;
					}
				}

				if (!hasAdd)
					NewFileLineList.Add(fileLines[lineno]);
			}

			File.Delete(srcWrapperFilePath);
			File.Copy(srcWrapperFileTxtPath, srcWrapperFilePath);
			File.Delete(srcWrapperFileTxtPath);

			// 写入文件
			string newContent = null;
			foreach (var l in NewFileLineList)
				newContent += (l + "\n");
			File.WriteAllBytes(dstWrapperFilePath, System.Text.Encoding.UTF8.GetBytes(newContent));

			File.Delete(dstWrapperFileTxtPath);
		}
	}
}