# 選択した範囲を読み、文字を認識する

import os

import pyocr
import pyocr.builders
import pyautogui
import cv2

from PIL import Image

from time import sleep

# インストールしたTesseract-OCRのパスを環境変数「PATH」へ追記する。
# OS自体に設定してあれば以下の2行は不要
path = ';C:\\Program Files\\Tesseract-OCR'
TESSDATA_PATH = 'C:\\Program Files\\Tesseract-OCR\\tessdata'

os.environ['PATH'] = os.environ['PATH'] + path
os.environ['TESSDATA_PREFIX'] = TESSDATA_PATH

tools = pyocr.get_available_tools()

tool = tools[0]

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


def ScreenShot(x1, y1, x2, y2):
    sc = pyautogui.screenshot(region=(x1, y1, x2, y2))
    sc.save('TransActor.jpg')

    img = cv2.imread('TransActor.jpg')
    gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
    tmp = cv2.resize(gray, (gray.shape[1]*2, gray.shape[0]*2), interpolation=cv2.INTER_LINEAR)
    cv2.imwrite('TransActor.jpg', tmp)

    return

def TranslationActors():
    builder = pyocr.builders.TextBuilder(tesseract_layout=6)
    text = tool.image_to_string(Image.open('TransActor.jpg'), lang="jpn", builder=builder)
    print(text)

    return

x1, y1, x2, y2 = PosGet()

while True:
    ScreenShot(x1, y1, x2, y2)

    TranslationActors()
    sleep(1)