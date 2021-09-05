# pyautoguiを使えるかの確認用
# AttributeError: module 'pyscreeze' has no attribute 'locateOnWindow'が出たら
# ・pip install pyautogui==0.9.51でpyautoguiのversionを下げるか
# ・pip install -U pyscreezeをする

import pyautogui
import time

"""
info: https://pyautogui.readthedocs.io/en/latest/quickstart.html
info: https://pypi.org/project/PyAutoGUI/0.9.50/
"""


def get_status():
    # マウスの位置を取得する
    print(pyautogui.position())
    # 画面のサイズを取得する
    print(pyautogui.size())


def move_cursor():
    # カーソルを動かす(絶対座標へ移動, duration=移動にかける秒数)
    pyautogui.moveTo(500, 500, duration=1)
    # 上へ移動する(絶対座標へ移動)
    pyautogui.mouseUp(x=300, y=300, button="left")
    # 下へ移動する(絶対座標へ移動)
    pyautogui.mouseDown(x=700, y=700, button="left")


def crick():
    # 左クリック
    pyautogui.click(x=700, y=700, clicks=1, interval=1, button="left")
    # 右クリック
    pyautogui.rightClick(x=700, y=700)
    # ダブルクリック
    pyautogui.doubleClick()


def send_key():
    # 文字列の書き込み
    pyautogui.write("Hello world!", interval=0.25)
    # ホットキー入力
    pyautogui.hotkey("ctrl", "a")


if __name__ == "__main__":
    print(pyautogui.position())
    print(pyautogui.size())
    pyautogui.rightClick(x=10, y=10)
    time.sleep(1)
    pyautogui.hotkey("win")
