# 選択した範囲を読み、文字を認識する
import os
import numpy
from numpy.lib.type_check import imag

import pyocr
import pyocr.builders
import pyautogui
import pyperclip

from PIL import Image
from difflib import SequenceMatcher
from time import sleep

# インストールしたTesseract-OCRのパスを環境変数「PATH」へ追記する。
# OS自体に設定してあれば以下の2行は不要
path = ';C:\\Program Files\\Tesseract-OCR'
TESSDATA_PATH = 'C:\\Program Files\\Tesseract-OCR\\tessdata'

os.environ['PATH'] = os.environ['PATH'] + path
os.environ['TESSDATA_PREFIX'] = TESSDATA_PATH

# 変数
tools = pyocr.get_available_tools()
tool = tools[0]

img1 = None
img_rgb = None
pixels = None
img2 = None

student_id = '19k1131'

# 座標選択　(後で消す)
def PosGet():
    print("左上隅の座標を取得します")
    sleep(3)
    x1, y1 = pyautogui.position()

    print("右下隅の座標を取得します")
    sleep(3)
    x2, y2 = pyautogui.position()

    x2 -= x1
    y2 -= y1

    return(x1, y1, x2, y2)

# 画像比較
def ImageCompare(img1, img2):
    return numpy.array_equal(img1, img2)

# 選択した範囲のスクリーンショットを撮る
def ScreenShot(x1, y1, x2, y2):
    sc = pyautogui.screenshot(region=(x1, y1, x2, y2))
    sc.save('TransActor.jpg')
    img = Image.open("TransActor.jpg")
    
    return img

# ファイルが送信されたら通知 (色で判断)
def FileNotification():
    red = 230
    green = 241
    blue = 248
    cnt = 0
    for j in range(img_rgb.size[1]):
        for i in range(img_rgb.size[0]):
            if (pixels[i, j][0] > red - 2 and pixels[i, j][1] < red + 2):
                if (pixels[i, j][0] > green - 2 and pixels[i, j][1] < green + 2):
                    if (pixels[i, j][0] > blue - 2 and pixels[i, j][1] < blue + 2):
                        cnt+=1
    
    if (cnt > 100):
        print("ファイルが送られました")
        print(cnt)

# 画像を加工 (必要な文字のみ入手できるようにする)
def Recolor():
    colormin = 80
    bluemin = 225
    for j in range(img_rgb.size[1]):
        for i in range(img_rgb.size[0]):
            if (pixels[i, j][0] > colormin and pixels[i, j][1] > colormin and pixels[i, j][2] > colormin):
                pixels[i, j] = (255, 255, 255)
            if (pixels[i, j][2] > bluemin):
                pixels[i, j] = (255, 255, 255)

#画像から文字を起こし、単語ごとに分割し、リストに格納して返却
def TranslationActors():
    builder = pyocr.builders.TextBuilder(tesseract_layout=6)
    text = tool.image_to_string(img_rgb, lang="jpn", builder=builder)
    split = text.split()

    return split

# 特定の条件になった場合Trueを返す
def JudgeTypeID(split, num):
    cnt = 0
    for text in split:
        s = SequenceMatcher(None, student_id, text)
        if s.ratio() > 0.3:
            cnt += 1
    if cnt > 3 and num > 5:
        return True
    else:
        return False

# 自動で学籍番号を入力する
def AutoTypeID(x1, y1, x2, y2):
    pyautogui.click(x1 + (x2 / 2), y1 + y2 - 10)
    pyperclip.copy(student_id)
    pyautogui.hotkey('ctrl', 'v')
    pyautogui.hotkey('enter')


x1, y1, x2, y2 = PosGet()
print(x1, y1)
print(x2, y2)

count = 0
while True:
    img1 = ScreenShot(x1, y1, x2, y2)
    img_rgb = img1.convert("RGB")
    pixels = img_rgb.load()

    if img2 is not None:
        if not ImageCompare(img1, img2): # 画面が変わっていたら...
            count += 1
            FileNotification()
            Recolor()
            textlis = TranslationActors()
            if JudgeTypeID(textlis, count):
                AutoTypeID(x1, y1, x2, y2)
                count = 0
        else:
            count = 0
    img2 = img1

    sleep(1)
