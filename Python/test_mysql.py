import mysql.connector
from mysql.connector.locales.eng import client_error

import json
import codecs
import sys
args=sys.argv
cnx = None



    
def search_mail(mail):
    try:
        mail = '"'+mail+'"'
        print(mail)
        cnx = mysql.connector.connect(
        user='docker_user',  # ユーザー名
        password='docker_pass',  # パスワード
        host='daitokai.mydns.jp',  # ホスト名(IPアドレス）
        port='4001',
        database='test'  # データベース名
        )
        
        cur = cnx.cursor()
        sql = 'SELECT mail FROM Json WHERE mail = {}'
        sql_var = sql.format(mail)
        print("---sql_var---")
        print(sql_var)
        cur.execute(sql_var)
        result=cur.fetchall()
        print("---result---")
        print (result[0])
        return result[0]
    except Exception as e:
        print(f"Error Occurred: {e}")


    finally:
        if cnx is not None and cnx.is_connected():
            cnx.close()




try:    

    mail = args[1]
    cnx = mysql.connector.connect(
        user='docker_user',  # ユーザー名
        password='docker_pass',  # パスワード
        host='daitokai.mydns.jp',  # ホスト名(IPアドレス）
        port='4001',
        database='test'  # データベース名
    )

    if cnx.is_connected():
        print("connect ok")

    cur = cnx.cursor()
    
    

    with open(args[2], mode='rt', encoding='utf-8') as file:

        # 辞書オブジェクト(dictionary)を取得
        data = json.load(file)
        # data = str(file)
    jsonstr = "'" + json.dumps(data, sort_keys=True, ensure_ascii=False, indent=2) + "'"
    # print(jsonstr)
    aru = search_mail(mail)
    print("---aru---")
    print(aru)
    print("---判定---")
    if aru is None:
        mail = '"'+mail+'"'
        sql = 'INSERT INTO `Json` (`mail`,`json`) VALUES ({},{})'
        sqli = sql.format(mail, jsonstr)
        print("null")
    else:
        mail = '"'+mail+'"'
        sql = 'UPDATE `Json` SET `json`= {} WHERE `mail`={}'
        sqli = sql.format(jsonstr,mail)
        print("aru")
    cur.execute(sqli)
    cnx.commit()

    


except Exception as e:
    print(f"Error Occurred: {e}")

finally:
    if cnx is not None and cnx.is_connected():
        cnx.close()
