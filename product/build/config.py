# -*- coding: utf-8 -*-
import platform

if platform.system() == 'Darwin':
    UNITY_PATH = r'"/Applications/Unity/Unity.app/Contents/MacOS/Unity"'
elif platform.system() == 'Windows':
    UNITY_PATH = r'"c:/Program Files/Unity/Editor/Unity.exe"'

PRODUCT_NAME = "mcworld"

#生成的apk或ipa全名
#产品名_发行公司_v版本号_运行框架_发行版或可调试版
#mcworld_yy_v1.1.1_mono_release_1970_01_01.apk
#mcworld_yy_v1.1.1_il2cpp_debug_1970_01_01.apk

GAME_PRODUCT_NAME = "{0}_v{1}_{2}_{3}_{4}"

def GetAndroidProductName(product, version, devmode, backend, time_str):
    return GAME_PRODUCT_NAME.format(product, version,
                                    backend, devmode, time_str) + ".apk"
