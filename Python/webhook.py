<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 18700d49e71576db031ac2f800348322e725d19c
import requests,json

WEB_HOOK_URL = "https://discordapp.com/api/webhooks/881070399167823903/UO78Qq6IFllqSni-SjoDdHN69FKIxvOLgCxN1PYkILW6YbK0wY0Yp6RqFpUr8b71gGXd"

main_content = {
                'content':'CS配下です。',#表示テキスト
                'avatar_url':'https://cdn.icon-icons.com/icons2/887/PNG/512/Python_icon-icons.com_68975.png',#アイコン
                'username':'πson'#名前
                }
header = {'content-Type':'application/json'}#json形式で送信するよって教えるヘッダー

response = requests.post(WEB_HOOK_URL,json.dumps(main_content),headers=header)#webhookのURLにpost形式で送信する。


<<<<<<< HEAD
=======
=======
import sys
import requests,json
args=sys.argv
WEB_HOOK_URL = sys.argv[1]

main_content = {
                'content':sys.argv[2],#表示テキスト
                'avatar_url':'https://cdn.icon-icons.com/icons2/887/PNG/512/Python_icon-icons.com_68975.png',#アイコン
                'username':'πson'#名前
                }
header = {'content-Type':'application/json'}#json形式で送信するよって教えるヘッダー

response = requests.post(WEB_HOOK_URL,json.dumps(main_content),headers=header)#webhookのURLにpost形式で送信する。


>>>>>>> fa0238d4140818b1a1c81c4e3123b76571c74de6
>>>>>>> 18700d49e71576db031ac2f800348322e725d19c
#メンションを付けてテキストを送信する場合　"<@&roleID>"or"<@&userID>"で送れる