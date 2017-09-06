# -*- coding: utf-8 -*-

import subprocess

class Utils():
	def ExecSystemCmd(cmd):
		#print("cmd:%s" % cmd)
		ps = subprocess.Popen(cmd, shell=True)
		ret = ps.wait()    #让程序阻塞
		#print("cmd:%s ret=%d" % (cmd, ret))
		return (0 == ret)