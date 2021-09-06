# test.pngを見て文字を判別するプログラム

import os
from PIL import Image
import pyocr

# インストールしたTesseract-OCRのパスを環境変数「PATH」へ追記する。
# OS自体に設定してあれば以下の2行は不要
path = ';C:\\Program Files\\Tesseract-OCR'
os.environ['PATH'] = os.environ['PATH'] + path
# pyocrへ利用するOCRエンジンをTesseractに指定する。
tools = pyocr.get_available_tools()
tool = tools[0]

# OCR対象の画像ファイルを読み込む
img = Image.open("zoomImage2.png")
print(type(img))
img_rgb = img.convert("RGB")
pixels = img_rgb.load()

# ファイルが送信されたら通知
def FileNotification():
    red = 230
    green = 241
    blue = 248
    flag = False
    for j in range(img_rgb.size[1]):
        for i in range(img_rgb.size[0]):
            if (pixels[i, j][0] > red - 5 and pixels[i, j][1] < red + 5):
                if (pixels[i, j][0] > green - 5 and pixels[i, j][1] < green + 5):
                    if (pixels[i, j][0] > blue - 5 and pixels[i, j][1] < blue + 5):
                        flag = True
    
    if (flag):
        print("ファイルが送られました")

# 画像を加工
def Recolor():
    colormin = 80
    bluemin = 225
    for j in range(img_rgb.size[1]):
        for i in range(img_rgb.size[0]):
            if (pixels[i, j][0] > colormin and pixels[i, j][1] > colormin and pixels[i, j][2] > colormin):
                pixels[i, j] = (255, 255, 255)
            if (pixels[i, j][2] > bluemin):
                pixels[i, j] = (255, 255, 255)

FileNotification()

Recolor()
# 画像から文字を読み込む
builder = pyocr.builders.TextBuilder(tesseract_layout=6)
text = tool.image_to_string(img_rgb, lang="jpn", builder=builder)

print(text)
