<p style="color:orange; text-align:center; text-size:30px;">프로젝트 디-버깅(동애등에-버깅)</p>

```
230614 - .json 라벨링 데이터 .yaml로 변환 (약 15000개)

230616 - 안드로이드 어플 구현

230619 - db서버 구축, nodeMCU 센서값 전달 성공
--
-- 데이터베이스: `testv1`
--

-- --------------------------------------------------------

--
-- 테이블 구조 `caterpillar`
--

DROP TABLE IF EXISTS `caterpillar`;
CREATE TABLE IF NOT EXISTS `caterpillar` (
  `Timestamp` datetime NOT NULL,
  `ObjectNumber` int NOT NULL,
  `ActivityLevel` float NOT NULL,
  PRIMARY KEY (`Timestamp`,`ObjectNumber`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- 테이블 구조 `controllog`
--

DROP TABLE IF EXISTS `controllog`;
CREATE TABLE IF NOT EXISTS `controllog` (
  `Timestamp` datetime NOT NULL,
  `ModuleName` varchar(40) NOT NULL,
  `ModuleStatus` varchar(40) NOT NULL,
  PRIMARY KEY (`Timestamp`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- 테이블 구조 `sensordata`
--

DROP TABLE IF EXISTS `sensordata`;
CREATE TABLE IF NOT EXISTS `sensordata` (
  `Timestamp` datetime NOT NULL,
  `Temperature` float NOT NULL,
  `Humidity` float NOT NULL,
  `Ammonium` float NOT NULL,
  `CO2` float NOT NULL,
  PRIMARY KEY (`Timestamp`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

 ** 접속 주소 http://175.205.128.40:9797
```
