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


#メンションを付けてテキストを送信する場合　"<@&roleID>"or"<@&userID>"で送れる