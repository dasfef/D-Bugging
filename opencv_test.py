import cv2
import numpy as np

# ONNX 모델 불러오기
net = cv2.dnn.readNetFromONNX('D:/InsectData/data/model.onnx')

# 이미지 불러오기
# image = cv2.imread('http://192.168.0.46:5702/mjpeg/1')
url = "http://192.168.0.46:5702/mjpeg/1"
webcam = cv2.VideoCapture(url)
height, width = webcam.shape[:2]
resized_image = cv2.resize(webcam, (width//2, height//2))

# 전처리 (이미지 크기 조절 및 정규화)
blob = cv2.dnn.blobFromImage(webcam, scalefactor=1/255.0, size=(416, 416), swapRB=True, crop=False)
net.setInput(blob)

# 모델 실행
outputs = net.forward()

# 결과 처리
for detection in outputs[0, 0, :, :]:
    confidence = float(detection[2])
    if confidence > 0.5:
        left = int(detection[3] * webcam.shape[1])
        top = int(detection[4] * webcam.shape[0])
        right = int(detection[5] * webcam.shape[1])
        bottom = int(detection[6] * webcam.shape[0])
        cv2.rectangle(webcam, (left, top), (right, bottom), (255, 0, 0), 2)
cv2.imshow("webcam", resized_image)
cv2.waitKey(0)