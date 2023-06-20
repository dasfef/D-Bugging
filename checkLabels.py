import os
import glob

# .txt 파일이 있는 디렉토리 지정
directory = 'C:/Users/user/Desktop/InsectData/convertData/HighDefinition/labels/train'

# 디렉토리 내의 모든 .txt 파일 검사
for file in glob.glob(os.path.join(directory, '*.txt')):
    with open(file, 'r') as f:
        lines = f.readlines()

    new_lines = []
    for line in lines:
        parts = line.strip().split()

        # 바운딩 박스 좌표 가져오기
        x_center = float(parts[1])
        y_center = float(parts[2])
        width = float(parts[3])
        height = float(parts[4])

        # 좌표가 이미지 범위를 벗어나는지 확인
        if x_center >= 0 and x_center <= 1 and y_center >= 0 and y_center <= 1 and width >= 0 and width <= 1 and height >= 0 and height <= 1:
            new_lines.append(line)

    # 새 레이블을 파일에 쓰기
    with open(file, 'w') as f:
        f.write(''.join(new_lines))