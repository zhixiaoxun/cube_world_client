# -*- coding: utf-8 -*-

import os, sys, getopt, time
import build_android
import config

def usage():
	print('''Usage: python build.py [options]
		Options:
		--help            Print this message and exit.
		--platform        Set build platform, can be android or ios, default is android.
		--mode            Set debug or release build, default is release.
		--backend         Set app run backend, can be mono or il2cpp, default is mono.
		--name            Set app name, default is mcworld.
		--version         Set app version, default is 0.0.0
	''')


if "__main__" == __name__:
	buildPlatform = "android"
	buildMode = "release"
	buildBackend = "mono"
	buildVersion = "0.0.0"

	print("解析命令行参数 ...")
	opts, args = getopt.getopt(sys.argv[1:], "", ["help", "platform=", "mode=", "backend=", "version="])
	for op, value in opts:
		if op == "--help":
			usage()
			sys.exit()
		elif op == "--platform":
			if value != "ios" and value != "android":
				print("platform {0} not support!".format(value))
				system.exit()
			if value == "ios":
				print("platform ios not support for now!")
				system.exit()
			buildPlatform = value
		elif op == "--mode":
			if value != "debug" and value != "release":
				print("mode {0} not support!".format(value))
				system.exit()
			buildMode = value
		elif op == "--backend":
			if value != "mono" and value != "il2cpp":
				print("backend {0} not support!".format(value))
				system.exit()
			buildBackend = value
		elif op == "--version":
			buildVersion = value

	print("解析命令行参数完成：name={0} platform={1} mode={2} backend={3} version={4}".format(config.PRODUCT_NAME, buildPlatform, buildMode, buildBackend, buildVersion))

	if buildPlatform == "android":
		builder = build_android.BuildAndroid(buildPlatform, buildMode, buildBackend, buildVersion)
		builder.buildApp()
	elif platform == "ios":
		print("platform ios not support for now!")
		system.exit()
