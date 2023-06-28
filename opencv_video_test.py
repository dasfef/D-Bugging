import cv2
import numpy as np

# YOLO 모델 로드
net = cv2.dnn.readNet("C:/Users/user/Desktop/WORK/ultralytics/yolov8n.pt", "yolov3.cfg")  # 이 부분에 본인이 다운로드 받은 모델의 경로를 입력해야 합니다.
layer_names = net.getLayerNames()
output_layers = [layer_names[i[0] - 1] for i in net.getUnconnectedOutLayers()]

# URL에서 비디오 받아오기
cap = cv2.VideoCapture('http://192.168.0.46:5702/mjpeg/1')  # 이 부분에 본인의 URL을 입력해야 합니다.

while(True):
    ret, frame = cap.read()
    height, width, channels = frame.shape

    # YOLO에 필요한 형태로 이미지 변환
    blob = cv2.dnn.blobFromImage(frame, 0.00392, (416, 416), (0, 0, 0), True, crop=False)
    net.setInput(blob)
    outs = net.forward(output_layers)

    # 객체 인식 결과 처리
    for out in outs:
        for detection in out:
            scores = detection[5:]
            class_id = np.argmax(scores)
            confidence = scores[class_id]
            if confidence > 0.5:
                # 객체를 인식한 경우, 해당 객체에 대한 정보를 출력 또는 처리할 수 있습니다.
                pass

    # 프레임을 화면에 표시
    cv2.imshow('frame', frame)

    # 'q' 키를 누르면 종료
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

# 종료
cap.release()
cv2.destroyAllWindows()
