from os import lseek

import subprocess #サブプロセスを実行するライブラリ→コマンドを実行する際に上書きしないようにプロセスを分ける

meetingID = 83655493910#meetingID指定してください

meetingPass = "WEVILzJwM2sxTzJZN21BUU9jRk14Zz09"#meetingpwdを指定して下さい。


Auto_zoom_command = "start zoommtg:\"//zoom.us/join?\"confno="+ str(meetingID) + "&pwd=" +str(meetingPass) +"\""#cliで実行起動するコマンド


subprocess.Popen(Auto_zoom_command,shell=True)#shell=TrueにしないとWindows にあらかじめ組み込まれているコマンドが実行できない