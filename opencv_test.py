import cv2
import numpy as np

# ONNX 모델 불러오기
net = cv2.dnn.readNetFromONNX('D:/InsectData/data/model.onnx')

# 이미지 불러오기
image = cv2.imread('D:/InsectData/data/Validation/rawData/VS1/PtecticusTenebrifer/Caterpillar/HighDefinition/Capture_20211117_13_45_52.jpg')
height, width = image.shape[:2]
resized_image = cv2.resize(image, (width//2, height//2))

# 전처리 (이미지 크기 조절 및 정규화)
blob = cv2.dnn.blobFromImage(image, scalefactor=1/255.0, size=(416, 416), swapRB=True, crop=False)
net.setInput(blob)

# 모델 실행
outputs = net.forward()

# 결과 처리
for detection in outputs[0, 0, :, :]:
    confidence = float(detection[2])
    if confidence > 0.5:
        left = int(detection[3] * image.shape[1])
        top = int(detection[4] * image.shape[0])
        right = int(detection[5] * image.shape[1])
        bottom = int(detection[6] * image.shape[0])
        cv2.rectangle(image, (left, top), (right, bottom), (255, 0, 0), 2)
cv2.imshow("image", resized_image)
cv2.waitKey(0)
