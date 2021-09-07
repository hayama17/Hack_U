# 選択した範囲を読み、文字を認識する
# -> IDが流れたら自分もIDを入力する
# -> ファイルが流れたら送信されたことを伝える
import os
import numpy
from numpy.lib.type_check import imag
import cv2

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

pdfimg = cv2.imread('./Image/pdfImage.png')
wordimg = cv2.imread('./Image/wordImage.png')
excelimg = cv2.imread('./Image/excelImage.png')
zoomiconimg = cv2.imread('./Image/zoomIcon.png')
chatbuttonimg = cv2.imread('./Image/chatButtonImage.png')
chatimg = cv2.imread('./Image/chatImage.png')
startimg = cv2.imread('./Image/start.png')

pdfflag = False
wordflag = False
excelflag = False

student_id = '19k1131'
idflag = False

main = 'main'
chat = 'chat'

width = pyautogui.size().width
height = pyautogui.size().height

# 画像比較
def ImageCompare(img1, img2):
    return numpy.array_equal(img1, img2)

# 選択した範囲のスクリーンショットを撮る
def ScreenShot(x1, y1, x2, y2, name):
    sc = pyautogui.screenshot(region=(x1, y1, x2, y2))
    sc.save('./Data/' + name + '.png')

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
    global idflag
    pyautogui.click(x1 + (x2 / 2), y1 + y2 - 10)
    pyperclip.copy(student_id)
    pyautogui.hotkey('ctrl', 'v')
    pyautogui.hotkey('enter')
    idflag = True

# pdfファイルが送られたらTrueを返す
def pdfFile(cv2img):
    result = cv2.matchTemplate(pdfimg, cv2img, cv2.TM_CCORR_NORMED)
    minVal, maxVal, minLoc, maxLoc = cv2.minMaxLoc(result)
    if maxVal > 0.99:
        return True
    else:
        return False

# wordファイルが送られたらTrueを返す
def wordFile(cv2img):
    result = cv2.matchTemplate(wordimg, cv2img, cv2.TM_CCORR_NORMED)
    minVal, maxVal, minLoc, maxLoc = cv2.minMaxLoc(result)
    if maxVal > 0.99:
        return True
    else:
        return False

# excelファイルが送られたらTrueを返す
def excelFile(cv2img):
    result = cv2.matchTemplate(excelimg, cv2img, cv2.TM_CCORR_NORMED)
    minVal, maxVal, minLoc, maxLoc = cv2.minMaxLoc(result)
    if maxVal > 0.99:
        return True
    else:
        return False

# いずれかのファイルが送られたらメッセージを送る(出来たらwebhookを使う)
def FileNotification():
    global pdfflag, wordflag, excelflag
    cv2img = cv2.imread('TransActor.png')
    if pdfFile(cv2img):
        if not pdfflag:
            print('pdfファイルが送信されました')
            pdfflag = True
    else:
        pdfflag = False
    
    if wordFile(cv2img):
        if not wordflag:
            print('wordファイルが送信されました')
            wordflag = True
    else:
        wordflag = False

    if excelFile(cv2img):
        if not excelflag:
            print('excelファイルが送信されました')
            excelflag = True
    else:
        excelflag = False

# tmplの座標を指定する
def CoordinateSearch(img, tmpl):
    result = cv2.matchTemplate(img, tmpl, cv2.TM_CCORR_NORMED)
    minVal, maxVal, minLoc, maxLoc = cv2.minMaxLoc(result)
    x = maxLoc[0]
    y = maxLoc[1]
    
    return x, y

# zoom開始のオーディオ設定をクリック
def Start():
    ScreenShot(0, 0, width, height, main)
    img = cv2.imread('./Data/' + main + '.png')
    x, y = CoordinateSearch(img, startimg)
    pyautogui.click(x + 10, y + 10)

# zoomをフルスクリーンにする
def zoomFullScreen():
    ScreenShot(0, 0, width, height, main)
    img = cv2.imread('./Data/' + main + '.png')
    x, y = CoordinateSearch(img, zoomiconimg)
    pyautogui.click(x + 10, y + 10)
    pyautogui.hotkey('winleft', 'up')

# チャットをクリック
def zoomChatClick():
    ScreenShot(0, 0, width, height, main)
    img = cv2.imread('./Data/' + main + '.png')
    x, y = CoordinateSearch(img, chatbuttonimg)
    pyautogui.click(x + 5, y + 5)

# チャット画面の座標を選択
def SaveChatLocation():
    ScreenShot(0, 0, width, height, main)
    img = cv2.imread('./Data/' + main + '.png')
    x_c, y_c = CoordinateSearch(img, chatimg)
    w = chatimg.shape[1]
    h = chatimg.shape[0]
    sleep(1)
    ScreenShot(x_c, y_c, w, h, chat)
    return x_c, y_c, w, h

count = 0

Start()
sleep(1)
zoomFullScreen()
sleep(1)
zoomChatClick()
sleep(1)
x1, y1, x2, y2 = SaveChatLocation()
while True:
    ScreenShot(x1, y1, x2, y2, chat)
    img1 = Image.open('./Data/chat.png')
    img_rgb = img1.convert("RGB")
    pixels = img_rgb.load()
    print(idflag)

    if img2 is not None:
        if ImageCompare(img1, img2) == False: # 画面が変わっていたら...
            count += 1
            FileNotification()
            Recolor()
            textlis = TranslationActors()
            print(textlis)
            if JudgeTypeID(textlis, count):
                print(12)
                if idflag == False:
                    AutoTypeID(x1, y1, x2, y2)
                    count = 0
        else:
            count = 0
    img2 = img1

    sleep(1)
