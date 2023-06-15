import os
import json
import glob

def convert(size, box):
    dw = 1./size[0]
    dh = 1./size[1]
    x = (box[0] + box[1])/2.0
    y = (box[2] + box[3])/2.0
    w = abs(box[1] - box[0])
    h = abs(box[3] - box[2])
    x = x*dw
    w = w*dw
    y = y*dh
    h = h*dh
    return [x,y,w,h]

def convert_annotation(json_path, output_path):
    with open(json_path, 'r') as json_file:
        data = json.load(json_file)
        image_name = os.path.splitext(data['META']['Image File Name'])[0]
        width = data['META']['Image Width MAGNITUDE']                   # 이미지 넓이 키값 위치 지정
        height = data['META']['Image Height MAGNITUDE']                 # 이미지 높이 키값 위치 지정

        try :
            annotations = data['ANNOTATION']['Anntation Regions']       # ANNOTATION 키 값이 'Anntation Regions'일 경우
        except KeyError:
            annotations = data['ANNOTATION']['Annotation Regions']      # ANNOTATION 키 값이 'Annotation Regions'일 경우

        with open(os.path.join(output_path, f'{image_name}.txt'), 'w') as outfile:
            for annotation in annotations:
                category_id = 0  
                x_coordinates = annotation['Polygon X Coordinate']
                y_coordinates = annotation['Polygon Y Coordinate']
                bbox = [min(x_coordinates), min(y_coordinates), max(x_coordinates), max(y_coordinates)] 
                bb = convert((width,height), bbox)

                if any([coord > 1 or coord < 0 for coord in bb]):
                    print(f"이상한 좌표값 발견: {bb}, 파일: {json_path}")

                outfile.write(f"{category_id} {bb[0]} {bb[1]} {bb[2]} {bb[3]}\n")

# json 파일 위치 경로 입력
json_paths = glob.glob('C:/Users/User/Desktop/InsectData/Data/Training/labelData/TL1/PtecticusTenebrifer/Caterpillar/Cam2/*.json')  
# 저장될 위치 경로 입력
output_path = 'C:/Users/User/Desktop/InsectData/ConvertData/Cam2' 

os.makedirs(output_path, exist_ok=True)

for json_path in json_paths:
    convert_annotation(json_path, output_path)