import sys
import mysql.connector
import json
import os
import tkinter
import tkinter.filedialog
import tkinter.messagebox
cnx = None
args=sys.argv
try:
    cnx = mysql.connector.connect(
        user='docker_user',  # ユーザー名
        password='docker_pass',  # パスワード
        host='daitokai.mydns.jp:4001',  # ホスト名(IPアドレス）
        port= '4001',
        database='test'  # データベース名
    )

    if cnx.is_connected():
        print("connect ok")

    cur = cnx.cursor()
    mail = args[1]
    with open(args[2], mode='rt', encoding='utf-8') as file:
        print('file: ' + str(file))   

    # 辞書オブジェクト(dictionary)を取得
    data = json.load(file)
    
    sql = "INSERT INTO Json (mail,json) VALUES ({},{})"
    
    sqli = sql.format(mail,data)
    cur.execute(sql)


except Exception as e:
    print(f"Error Occurred: {e}")

finally:
    if cnx is not None and cnx.is_connected():
        cnx.close()
