# -*- coding: utf-8 -*-

#制作Android全包版本

import os, shutil, datetime, time
import config
from file_helper import FileHelper
from utils import Utils

class BuildAndroid():
	def __init__(self, buildPlatform, buildMode, buildBackend, buildVersion):
		self.buildPlatform = buildPlatform
		self.buildMode = buildMode
		self.buildBackend = buildBackend
		self.buildVersion = buildVersion

		self.clientProjectPath = os.path.join(os.getcwd(), "../../mcworld")
		self.productPath = os.path.join(os.getcwd(), "../app/android")

		time_str = datetime.datetime.now().strftime('%Y%m%d%H%M%S')
		self.apkName = config.GetAndroidProductName(
			config.PRODUCT_NAME, self.buildVersion, self.buildMode, self.buildBackend, time_str)

		self.apkFullPath = os.path.join(self.productPath, self.apkName)

		print("[初始化] 准备构建Android版本")
		print("[初始化] 工作路径{0}".format(os.getcwd()))
		print("[初始化] client工程路径{0}".format(self.clientProjectPath))
		print("[初始化] apk路径{0}".format(self.productPath))
		print("[初始化] apk文件{0}".format(self.apkName))
		print("[初始化] apk文件全路径{0}".format(self.apkFullPath))

	def __del__(self):
		pass

	def buildApp(self):
		print("[预处理] 进入编译Android版本预处理工作")

		print("[预处理] 将StreamingAssets/Lua目录移动到Assets/Core/Resources/目录下")
		FileHelper.copyDir(os.path.join(self.clientProjectPath, "Assets/StreamingAssets/Lua"), 
			os.path.join(self.clientProjectPath, "Assets/Core/Resources/Lua"))

		print("[预处理] 将Assets/Core/Resources/Lua目录下的lua改成txt文件")
		FileHelper.renameDirFile(os.path.join(self.clientProjectPath, "Assets/Core/Resources/Lua"), ".lua", ".txt")
		
		print("[预处理] 删除StreamingAssets/Lua目录")
		# TODO
		
		print("[编译中] 开始生成app {0}".format(self.apkName))

		self.unityLogFile = "unity_build_{0}.log".format(self.apkName)
		self.unityLogFile = os.path.join(self.productPath, self.unityLogFile)
		print("[编译中] 指定unity日志文件 {0}".format(self.unityLogFile))

		commandStr = "{0} -batchmode -projectPath {1} -executeMethod Assets.Core.Editor.Tools.Build.BuildTools.BuildApp \
			 --app={2} --platform={3} --mode={4} --backend={5} --version={6} -quit -logFile {7}"
		command = commandStr.format(config.UNITY_PATH, self.clientProjectPath, self.apkFullPath, self.buildPlatform, 
			self.buildMode, self.buildBackend, self.buildVersion, self.unityLogFile)

		result = Utils.ExecSystemCmd(command)
		if result:
			print("[编译完成] 生成apk成功 路径={0}".format(self.apkFullPath))
		else:
			print("[编译完成] 生成apk失败")