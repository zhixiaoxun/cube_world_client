# -*- coding: utf-8 -*-

import os
import shutil

class FileHelper():
	def __init__(self):
		pass

	def __del__(self):
		pass

	@classmethod
	def createDir(cls, dir):
		if os.path.exists(dir):
			return
		os.makedirs(dir)

	@classmethod
	def clearDir(cls, rootDir):
		if not os.path.exists(rootDir):
			return
		fileList=os.listdir(rootDir)
		for f in fileList:
			filePath = os.path.join(rootDir, f)
			if os.path.isfile(filePath):
				os.remove(filePath)
			elif os.path.isdir(filePath):
				shutil.rmtree(filePath, True)

	@classmethod
	def removeDir(cls, dir):
		if not os.path.exists(dir):
			return
		shutil.rmtree(dir)

	@classmethod
	def copyDir(cls, src, dst):
		if os.path.exists(dst):
			cls.removeDir(dst)
		shutil.copytree(src, dst)

	@classmethod
	def moveFile(cls, src, dst):
		shutil.move(src, dst) 

	#获取文件的后缀名
	@classmethod
	def getFileSuffix(cls, file):
		return os.path.splitext(file)[1]
	@classmethod
	def getFileNameWithoutSuffix(cls, file):
		return os.path.splitext(file)[0]
	@classmethod
	def renameDirFile(cls, dir, oldSuffix, newSuffix):
		for i in os.listdir(dir):
			oldfile = os.path.join(dir, i)
			if os.path.isdir(oldfile):
				#print(oldfile+" is dir")
				cls.renameDirFile(oldfile, oldSuffix, newSuffix)
			else:
				suffix = cls.getFileSuffix(oldfile)
				if suffix == oldSuffix:
					newfile = cls.getFileNameWithoutSuffix(oldfile) + newSuffix
					#print("old={0} new={1}".format(oldfile, newfile))
					shutil.move(oldfile, newfile) 
	@classmethod
	def removeSpecialSuffixFileInDir(cls, dir, suffixList):
		if not os.path.exists(dir):
			return
		for i in os.listdir(dir):
			oldfile = os.path.join(dir, i)
			if os.path.isdir(oldfile):
				cls.removeSpecialSuffixFileInDir(oldfile, suffixList)
			else:
				suffix = cls.getFileSuffix(oldfile)
				if suffix in suffixList:
					os.remove(oldfile)