import pyautogui as pag
import pyperclip

pag.click(100, 100)
pyperclip.copy('自動で文字書くでーー')
pag.hotkey('ctrl', 'v')
pag.hotkey('enter')
