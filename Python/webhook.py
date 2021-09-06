import requests,json

WEB_HOOK_URL = "https://discordapp.com/api/webhooks/881070399167823903/UO78Qq6IFllqSni-SjoDdHN69FKIxvOLgCxN1PYkILW6YbK0wY0Yp6RqFpUr8b71gGXd"

main_content = {
                'content':'CS配下です。',#表示テキスト
                'avatar_url':'https://cdn.icon-icons.com/icons2/887/PNG/512/Python_icon-icons.com_68975.png',#アイコン
                'username':'πson'#名前
                }
header = {'content-Type':'application/json'}#json形式で送信するよって教えるヘッダー

response = requests.post(WEB_HOOK_URL,json.dumps(main_content),headers=header)#webhookのURLにpost形式で送信する。


#メンションを付けてテキストを送信する場合　"<@&roleID>"or"<@&userID>"で送れる