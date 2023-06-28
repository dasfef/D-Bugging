import cv2
from ultralytics import YOLO
from ultralytics.yolo.utils import ROOT, yaml_load
from ultralytics.yolo.utils.checks import check_requirements, check_yaml

# ESP32 영상 스트림 URL
stream_url = 'http://192.168.0.46:5702/mjpeg/1'

# 영상 스트림 받아오기
cap = cv2.VideoCapture(stream_url)
model = YOLO('yolov8n.pt')

# 클래스 레이블 로딩
classes = yaml_load(check_yaml('coco128.yaml'))['names']

while True:
    # 프레임 읽기
    ret, frame = cap.read()
    height, width, channels = frame.shape

    if not ret:
        break

    # Yolov5를 이용한 객체 감지 수행
    # TODO: Yolov5 코드 작성
    results = model(frame)
    labeled_frame = results.render()

    # 결과 표시
    cv2.imshow('Object Detection', frame)

    # 'q' 키를 누르면 종료
    if cv2.waitKey(1) == ord('q'):
        break

# 자원 해제
cap.release()
cv2.destroyAllWindows()