import cv2
import pyautogui
import pyperclip
from time import sleep
from PIL import Image

pdfimg = cv2.imread('./Image/pdfImage.png')
wordimg = cv2.imread('./Image/wordImage.png')
excelimg = cv2.imread('./Image/excelImage.png')

zoomiconimg = cv2.imread('./Image/zoomIcon.png')
chatbuttonimg = cv2.imread('./Image/chatButtonImage.png')
chatimg = cv2.imread('./Image/chatImage.png')
startimg = cv2.imread('./Image/start.png')

main = 'main'
chat = 'chat'

width = pyautogui.size().width
height = pyautogui.size().height

# ScreenShotを撮る
def ScreenShot(x1, y1, x2, y2, name):
    sc = pyautogui.screenshot(region=(x1, y1, x2, y2))
    sc.save('./Data/' + name + '.png')

# tmplの座標を指定する
def CoordinateSearch(img, tmpl):
    result = cv2.matchTemplate(img, tmpl, cv2.TM_CCORR_NORMED)
    minVal, maxVal, minLoc, maxLoc = cv2.minMaxLoc(result)
    x = maxLoc[0]
    y = maxLoc[1]
    
    print(img.shape, tmpl.shape, result.shape)
    print(cv2.minMaxLoc(result))
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
    ScreenShot(x_c, y_c, w, h, chat)
    print(x_c, y_c, w, h)
    return x_c, y_c, w, h

Start()
sleep(1)
zoomFullScreen()
sleep(1)
zoomChatClick()
sleep(1)
x1, y1, x2, y2 = SaveChatLocation()

