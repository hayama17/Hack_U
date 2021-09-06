import cv2

pdfimg = cv2.imread('./Image/pdfImage.png')
wordimg = cv2.imread('./Image/wordImage.png')
excelimg = cv2.imread('./Image/excelImage.png')

result = cv2.matchTemplate(wordimg, pdfimg, cv2.TM_CCORR_NORMED)
minVal, maxVal, minLoc, maxLoc = cv2.minMaxLoc(result)

print(maxVal)