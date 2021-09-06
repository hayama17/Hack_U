<<<<<<< HEAD
from os import lseek

import subprocess #サブプロセスを実行するライブラリ→コマンドを実行する際に上書きしないようにプロセスを分ける

meetingID = 312321#meetingID指定してください

meetingPass = "282849"#meetingpwdを指定して下さい。


Auto_zoom_command = "start zoommtg:\"//zoom.us/join?confno="+ str(meetingID) + "&pwd=" +str(meetingPass) +"\""#cliで実行起動するコマンド


=======
from os import lseek
import sys
import subprocess #サブプロセスを実行するライブラリ→コマンドを実行する際に上書きしないようにプロセスを分ける
args=sys.argv
meetingID = args[1]#meetingID指定してください

meetingPass = args[2]#meetingpwdを指定して下さい。


Auto_zoom_command = "start zoommtg:\"//zoom.us/join?confno="+ meetingID + "&pwd=" +meetingPass+"\""#cliで実行起動するコマンド


>>>>>>> e06b549ab86baf54f0bb3944cd1d3903b9d6dea2
subprocess.Popen(Auto_zoom_command,shell=True)#shell=TrueにしないとWindows にあらかじめ組み込まれているコマンドが実行できない