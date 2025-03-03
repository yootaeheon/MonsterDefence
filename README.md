# MonsterDefence - 개인 프로젝트

# **Ⅰ.** 개요

## 한 줄 소개

스폰된 몬스터가 골인 지점까지 가기 전에 캐릭터를 배치하여 막아내는 2D 타워디펜스형 게임입니다.

## 개발 환경

개발기간 : 2025.02.18 ~ 2025.02.26

인원 : 1인

사용 툴 : Unity Engine 2021.3.42f1, Visual Studio 2022, Git, Github, Firebase

## 링크

PPT : [**피피티 링크**](https://drive.google.com/file/d/1Q8a6hZTWTYxpq6uT7T2g-h2f_L3Cj9Er/view?usp=drive_link)

Github : [**깃허브 링크**](https://github.com/yootaeheon/UnityLastProject)

시연영상 : [**시연영상 링크**](https://youtu.be/G4_rCyhQjaw)

## 목차

### 구현 컨텐츠

1. A* 이용한 몬스터 경로 탐색 구현
2. ScriptableObject 이용한 캐릭터 생성 프레임 워크 구현
3. 상태 패턴 이용한 캐릭터 상태 및 기능 구현
4. CSV 파싱 이용한 맵 생성기 구현
5. Firebase 이용한 로그인/회원가입 구현 및 유저데이터 Database에 저장
6. UI 관리

### 이슈 및 해결사항

1. Firebase 실시간 데이터 UI 연동 미적용 문제

![스크린샷 2025-02-28 000616.png](attachment:5c394201-0ae3-4088-bbe0-6844d3c96d63:스크린샷_2025-02-28_000616.png)

![image.png](attachment:998c420b-c86e-4282-84b5-03e7b39c3d84:image.png)

![스크린샷 2025-02-28 000855.png](attachment:ecbafe1a-eeb8-4cd3-bdf5-dd897031759f:스크린샷_2025-02-28_000855.png)

![image.png](attachment:c861d869-1b99-4124-a7ef-a928c1b31dae:image.png)

# **Ⅱ**. 구현 컨텐츠

# **1.** A* 이용한 몬스터 경로 탐색 구현

---

## 1.1 구상

- 스폰된 몬스터가 도착 지점까지 장애물을 피해 최단 경로를 탐색하여 이동
- 휴리스틱 함수를 이용하여 도착 지점 방향으로만 탐색 진행

## 1.2 설계 방법

### A* 알고리즘으로 최단 경로 탐색

- **구현 기술**
    - 휴리스틱 함수를 이용한 목표 방향으로의 최상의 경로를 추정
        - 다음 탐색할 노드 중 F가 가장 낮은 노드(F가 같다면 H가 가장 낮은 노드)를 선택
        - F(총 비용) = G(시작 점-현재 지점)+ H(현재 지점 - 도착 지점)
        - 장애물이 존재하여도 장애물을 피해 최단 경로를 탐색
- **구현 내용**
    1. 초기 설정
        1. 탐색할 정점들을 보관할 리스트 생성
            1. 정점의 크기가 일정하지 않아 동적으로 배열의 크기 변경이 요구되어 리스트 사용
        2. 탐색을 완료한 정점들을 보관할 딕셔너리 생성
            1. 위치값(Vector2Int)과 탐색여부(bool)를 키-값 쌍으로 하는 딕셔너리를 생성
            2. . 탐색할 정점을 리스트에 넣고 탐색
    2. . 탐색할 정점을 리스트에 넣고 탐색 - 탐색할 정점이 없을 때 까지 반복
        1. 탐색할 정점을 딕셔너리에 추가
    3. 주변 정점들 계산
        1. 휴리스틱 함수를 이용하여 도착 지점까지 방향으로만 계산
    4. 만약 다음 탐색할 정점이 도착지인 경우 성공
        1. 최단 경로를 반환할 리스트 생성하여 백 트래킹 과정을 거쳐 추가
- **이 방식을 사용한 이유**
    - 최단 경로 탐색
        - 도착 지점까지 최단 경로를 탐색하기 위해 A*알고리즘 사용, 다익스트라 알고리즘과 BFS를 이용할 수 있었지만 계산을 최소화하기 위해 도착 지점까지의 방향으로만 탐색하고자 A* 사용
    - 최적화
        - 복잡한 연산을 최소한으로 하기 위해 PathManager 생성하여 경로 탐색은 1회만 진행, 생성된 몬스터들이  탐색한 최단 경로를 받아서 이동
        
        ![<최단 경로 탐색 결과>](attachment:4f29ec2f-607d-4ff6-9d45-6648e97cf9f7:image.png)
        
        <최단 경로 탐색 결과>
        

# 2. ScriptableObject 이용한 캐릭터 생성 프레임 워크 구현

---

## 2.1 구상

- 능력이 없는 캐릭터 틀(마네킹)에 프리팹을 참조시켜 캐릭터를 쉽게 생성할 수 있는 프레임 워크 구현
- 다양한 캐릭터를 양산할 수 있는 생산성을 향상

## 2.2 설계 방법

### ScriptableObject를 참조시킬 Adapter 구현

[<투표 순서도>](https://lh7-rt.googleusercontent.com/docsz/AD_4nXdX2yOdVHQ0ixkGQbWDFNMCBYZuh-IlUJoqMTMU1sjnqbxhlneWYI3XGuZ9knHc2vvsLGDS2j6WdP6EQO6Q9q6jaJkPwVh-G1OaOqmvCqHtZkbU6QehZiJCq6j-TYoAag6m1vP3Gg?key=QLb5grh6-g01snpKXaMdZBiy)

<투표 순서도>

- 직렬화를 이용하여 Status, Weapon, Skill 데이터를 넣어줄 Adapter 구현
- 게임 실행 시 각 카테고리의 데이터를 캐릭터 모델에 연동하여 초기화
- ScriptableObject를 이용하여 각 카테고리의 데이터를 저장
    - Status : 캐릭터의 기본 정보, 능력치
    - Weapon : 사용할 무기의 정보, 능력치, 각 상태의 애니메이션 클립
    - Skill : 스킬의 타입과 능력치, 비용케ㅐ

![<캐릭터 틀>](attachment:0ce6f141-ecaf-424e-85c1-e40ce83546bf:스크린샷_2025-03-02_213515.png)

<캐릭터 틀>

![<Status 예시>](attachment:92ca0630-4434-475b-b254-45ce1d7af5a1:스크린샷_2025-02-26_222331.png)

<Status 예시>

![<Weapon 예시>](attachment:487f28dc-b007-4f4f-8e3b-27d76175532b:스크린샷_2025-02-26_222400.png)

<Weapon 예시>

![<Skill 예시>](attachment:a0ce98ad-4b94-4015-bf73-3c73cd96179e:스크린샷_2025-02-26_222317.png)

<Skill 예시>

- 마네킹 프리팹에 각 카테고리에 맞는 데이터를 참조만하여 빠르게 캐릭터를 생성 가능
    - 마네킹 : CharacterContorller와 Adapter 컴포넌트만 소유한 캐릭터 틀
        
        ![<캐릭터 생성 과정>](attachment:c16cf754-967b-4f64-9d42-22b0677a589b:UnityLastProject_-_SampleScene_-_Windows_Mac_Linux_-_Unity_2021.3.42f1_Personal___DX11__2025-02-26_22-34-19.gif)
        
        <캐릭터 생성 과정>
        
- 이 방식을 사용한 이유
    - 생산성
        - 캐릭터 생성 과정을 압축시켜 생산성을 향상하고자 이 프레임 워크를 구축하였음
        - 프로젝트의 모든 팀원이 쉽게 캐릭터를 생성시킬 수 있음
    - 경량(FlyWeight)패턴 적용
        - 같은 데이터(Status, Weapon, Skill)를 여러 캐릭터가 공유할 수 있도록 ScriptableObject를 사용하여 메모리 사용량을 줄이고 성능을 최적화함
        - ScriptableObject를 활용함으로써 동일한 데이터를 참조하여 불필요한 복제를 방지
        - 게임이 실행되는 동안 데이터가 유지되므로, 런타임 중 불필요한 인스턴스 생성을 방지하고 효율적인 데이터 관리를 가능하게 함

### 애니메이터 오버라이드 컨트롤러 사용

- **구현 기술**
    - 애니메이터 오버라이드를 사용하여 한 개의 캐릭터 애니메이터를  기반으로 사용
    - 각 캐릭터의 애니메이션을 ScriptableObject 기반 데이터와 연동하여 동적으로 변경 가능
    - 애니메이터 오버라이드 컨트롤러를 통해 기본 애니메이터를 유지하면서, 캐릭터별로 고유한 애니메이션 적용
        
        ![image.png](attachment:31bee0e1-1c9a-4d93-bc67-9f9953823bfe:image.png)
        
- **이 방식을 사용한 이유**
    - 캐릭터 생성 과정 압축
        - 애니메이터 작업 과정을 생략함으로써 캐릭터 생성 과정 단순화 및 속도 향상
    - 유지보수성 및 확장성
        - 별도의 애니메이터 컨트롤러를 추가할 필요 없이, 기존 애니메이터를 기반으로 캐릭터별 애니메이션을 쉽게 변경 가능
        - 특정 캐릭터의 애니메이션을 수정할 때, 개별 애니메이션만 교체하여 수정 가능
    - 메모리 사용 최적화
        - 모든 캐릭터가 개별 애니메이터를 갖지 않고, 하나의 애니메이터를 공유하여 메모리 사용량 절감
        - 필요할 때만 특정 애니메이션을 교체하여 동적 변경 가능

# **3. 상태 패턴** 이용한 캐릭터 상태 및 기능 구현

---

## 3.1 구상

- 각 상태에서 캐릭터의 기능을 구현하고 조건에 맞게 상태를 전환시켜줌
- StateMachine이 조건에 맞는 각 상태로 전환
- Idle 상태에서 공격 범위 내에 몬스터 탐지 시 Attack 상태, 일정 마나 이상일 때 Skill 상태

## 3.2 설계 방법

### 각 상태를 독립적인 클래스로 분리

[](https://lh7-rt.googleusercontent.com/docsz/AD_4nXeyAxUhkJL1KGr28qrGo2cGqaMKC-qyvKz-tKNjcSIKT_RtCLLPEhJ-hf2EPogA0u9Euq6uN6p1iX2tmi9uAVX-zt83NuhX32ZqObUoRJLG6KA56kP5wCvSKpWGGdU9yrRJIDZU5Q?key=yQ6zY4mO39wxZSdIKEQBKM23)

[<투표 순서도>](https://lh7-rt.googleusercontent.com/docsz/AD_4nXdX2yOdVHQ0ixkGQbWDFNMCBYZuh-IlUJoqMTMU1sjnqbxhlneWYI3XGuZ9knHc2vvsLGDS2j6WdP6EQO6Q9q6jaJkPwVh-G1OaOqmvCqHtZkbU6QehZiJCq6j-TYoAag6m1vP3Gg?key=QLb5grh6-g01snpKXaMdZBiy)

<투표 순서도>

- **구현 기술**
    - Idle, Attack, Skill 상태를 각각의 클래스에서 관리
    - 모든 상태는 추상 클래스인 CharacterState 상속
        - 부모클래스를 상속하여 계층적 구조를 사용
        - 부모 클래스의 모든 속성과 추상, 가상 메서드를 자식클래스에서 오버라이드하여  필요한 기능을 구체적으로 구현 가능
- **이 방식을 사용한 이유**
    - 유지보수성 및 확장성 향상
        - 특정 상태의 로직을 추가/수정할 때 다른 상태에 영향을 주지 않음
    - 디버깅 용이
        - 문제 발생 시 특정 상태에서 직관적으로 디버깅 가능하여 쉽게 문제를 찾아 해결 가능
    
    ![스크린샷 2025-02-28 000306.png](attachment:6331687e-5145-4e6a-93a4-a4fb5fb59b44:스크린샷_2025-02-28_000306.png)
    

### 각 조건에 맞게 StateMachine에서 상태 전환

- **구현 기술**
    - 모든 상태를 저장하고 전환해주는 역할의 머신 구현
    - 상태를 딕셔너리에 저장하여 빠르게 탐색하여 전활 수 있게 설계
    - 상태 전환 시, 그 상태의 Enter()에 진입
- **이 방식을 사용한 이유**
    - 유연한 상태 제어
        - StateMachine을 통해 동적으로 상태를 변경할 수 있어 유연한 상태 전환 가능

### 오버랩박스와 인터페이스를 이용한 캐릭터 공격 로직

- **구현 기술**
    - 오버랩박스로 캐릭터의 공격 가능 범위 내의 충돌체를 탐지
    - 충돑체가 IDamgeable 인터페이스를 상속하고 있다면 공격
- **이 방식을 사용한 이유**
    - 유지보수성
        - 공격 대상이 추가/변경되어도 **기존 캐릭터 공격 로직을 수정할 필요가 없음**
    - 확장성
        - 스킬, 크리티컬 히트, 특수 피격 반응 등의 다른 기능을 구현하여도 인터페이스 확장을 통해 유연하게 관리 가능
    - 오버랩박스 사용 이유
        - 사각형 타일 맵 구조에서 몬스터 판정 정확도 향상
        - 범위 수치를 달리하여 캐릭터의 다양성 구현 고려
    
    ![스크린샷 2025-02-27 235105.png](attachment:56725b53-4b4d-4211-a526-74f9e6cd1bc9:6874a907-532c-474e-99b7-884093f684d5.png)
    

# **4.** CSV 파싱 이용한 맵 생성기 구현

---

## 4.1 구상

- CSV 파일을 파싱하여 타일맵을 자동으로 생성해주는 기능
- 맵을 손쉽게 디자인 가능한 기능
- CSV 파일 내의 데이터에 따라 벽(Wall), 시작 지점(Start), 종료 지점(End)을 자동으로 배치

## 4.2 설계 방법

### CSV Parser 구현 (CSV 파일을 읽고, 데이터를 저장 및 조회하는 기능을 담당)

[](https://lh7-rt.googleusercontent.com/docsz/AD_4nXeyAxUhkJL1KGr28qrGo2cGqaMKC-qyvKz-tKNjcSIKT_RtCLLPEhJ-hf2EPogA0u9Euq6uN6p1iX2tmi9uAVX-zt83NuhX32ZqObUoRJLG6KA56kP5wCvSKpWGGdU9yrRJIDZU5Q?key=yQ6zY4mO39wxZSdIKEQBKM23)

[<투표 순서도>](https://lh7-rt.googleusercontent.com/docsz/AD_4nXdX2yOdVHQ0ixkGQbWDFNMCBYZuh-IlUJoqMTMU1sjnqbxhlneWYI3XGuZ9knHc2vvsLGDS2j6WdP6EQO6Q9q6jaJkPwVh-G1OaOqmvCqHtZkbU6QehZiJCq6j-TYoAag6m1vP3Gg?key=QLb5grh6-g01snpKXaMdZBiy)

<투표 순서도>

- **구현 기술**
    - CSV 파일을 읽어 데이터를 2차원 배열로 저장
        - CSV 파일의 행과 열의 크기의 2차원 배열을 생성
        - 인덱서를 사용하여 특정 (y, x) 위치의 데이터를 저장
        - 쉼표, 탭 으로 구분된 데이터를 파싱하여 저장
    - 저장된 파일의 경로는 GameManager에 보내어 MapGenerator에서  받아 맵을 생성
- **이 방식을 사용한 이유**
    - 배열 사용 이유
        - 인덱스 기반으로 데이터 접근에 특화된 배열 자료구조를 사용하여 맵 생성 로직을 더 빠르게 구현 가능
    - 간편화
        - CSV 파일을 간편하게 파싱하고 데이터를 저장 및 조회하는 기능을 구현하여 넘겨주기 위해 구현
    - 데이터 중심 설계로 쉬운 맵 디자인
        - CSV 파일 수정만으로 다양한 맵 생성하여 추가적인 오브젝트 배치 및 맵 로직 적용 가능하며 코드 수정과 에디터 없이 맵 변경 가능

### Map 생성기 구현

[](https://lh7-rt.googleusercontent.com/docsz/AD_4nXeyAxUhkJL1KGr28qrGo2cGqaMKC-qyvKz-tKNjcSIKT_RtCLLPEhJ-hf2EPogA0u9Euq6uN6p1iX2tmi9uAVX-zt83NuhX32ZqObUoRJLG6KA56kP5wCvSKpWGGdU9yrRJIDZU5Q?key=yQ6zY4mO39wxZSdIKEQBKM23)

[<투표 순서도>](https://lh7-rt.googleusercontent.com/docsz/AD_4nXdX2yOdVHQ0ixkGQbWDFNMCBYZuh-IlUJoqMTMU1sjnqbxhlneWYI3XGuZ9knHc2vvsLGDS2j6WdP6EQO6Q9q6jaJkPwVh-G1OaOqmvCqHtZkbU6QehZiJCq6j-TYoAag6m1vP3Gg?key=QLb5grh6-g01snpKXaMdZBiy)

<투표 순서도>

- **구현 기술**
    - CSV Parser에서 경로를 저장한 변수를 받아 맵을 생성하는 식으로 진행하여 게임 씬은 1개를 사용
        - GameManager에서 파일 경로를 받아 GameScene에서 맵을 생성
    - 이중 For 반복문을 통해 CSV 데이터의 X,Y 좌표를 순회하며 데이터를 읽음
        - “Wall” 이면 타일맵 생성
        - “Start” / “End” 이면 프리팹 생성
    - Vector3Int(x, -y, 0)을 사용하여 2D 좌표계를 변환
- **이 방식을 사용한 이유**
    - 좌표 변환 이유
        - y 좌표를 반전하여 Unity 좌표계에서 올바르게 배치되도록 함. (CSV에서는 위에서 아래로 저장되지만, Unity에서는 아래에서 위로 증가하는 방식)
    - 메모리 최적화
        - 여러 스테이지 씬을 생성하지 않고 1개의 게임 씬에서 CSV 파일을 읽어들여 생성하는 방식이기 때문에 메모리 최적화 가능
    
    ![<생성할 CSV 파일>](attachment:7c7797fd-1385-4d9a-9f5c-5b6885372d2b:e8454a63-093f-45d7-999d-1caf9d14154b.png)
    
    <생성할 CSV 파일>
    
    ![<파싱하여 생성한 맵>](attachment:6b39bbf5-81ca-4d8e-93a9-c313377b0b03:image.png)
    
    <파싱하여 생성한 맵>
    
    ### 캐릭터 생성 시 타일 맵 위에만 생성 가능
    
    [](https://lh7-rt.googleusercontent.com/docsz/AD_4nXeyAxUhkJL1KGr28qrGo2cGqaMKC-qyvKz-tKNjcSIKT_RtCLLPEhJ-hf2EPogA0u9Euq6uN6p1iX2tmi9uAVX-zt83NuhX32ZqObUoRJLG6KA56kP5wCvSKpWGGdU9yrRJIDZU5Q?key=yQ6zY4mO39wxZSdIKEQBKM23)
    
    [<투표 순서도>](https://lh7-rt.googleusercontent.com/docsz/AD_4nXdX2yOdVHQ0ixkGQbWDFNMCBYZuh-IlUJoqMTMU1sjnqbxhlneWYI3XGuZ9knHc2vvsLGDS2j6WdP6EQO6Q9q6jaJkPwVh-G1OaOqmvCqHtZkbU6QehZiJCq6j-TYoAag6m1vP3Gg?key=QLb5grh6-g01snpKXaMdZBiy)
    
    <투표 순서도>
    
    - **구현 기술**
        - 타일맵을 기준으로 캐릭터 생성 위치 제한
            - CSV 데이터를 기반으로 유효한 타일 좌표위에 버튼을 생성
            - 타일이 존재하는 위치에만 캐릭터를 생성하도록 로직 구현
        - 버튼 프리팹을 타일맵과 동일한 위치에 배치
            - 유저가 캐릭터를 배치할 수 있는 타일에만 버튼 UI 활성화
            - 버튼 클릭 시 해당 위치에 캐릭터 인스턴스 생성
        - 버튼 프리팹만 관리하는 캔버스를 따로 분리
            - 드로우콜을 줄여 UI 최적화를 위해 버튼 프리팹만을 관리하는 캔버스를 따로 분리
    - **이 방식을 사용한 이유**
        - 비정상적인 캐릭터 배치 방지
            - 공중이나 벽 속에 캐릭터가 생성되지 않도록 타일 존재 여부 확인
        - 직관적인 UI/UX
            - 클릭 가능한 위치를 시각적으로 안내하여 직관적인 캐릭터 배치 가능
    
    ![몬스터 생성 (1).gif](attachment:96385d6d-bba3-4807-aa35-81163513414e:몬스터_생성_(1).gif)
    

# **5.** Firebase 이용한 로그인/회원가입 구현 및 유저데이터 데이터베이스에 저장

---

## 5.1 구상

- 이메일 인증을 통한 회원가입 및 로그인 기능 구현
- 계정의 유저데이터를 Firebase의 RealTime Database에 저장

## 5.2 설계 방법

### 회원가입 및 로그인

[](https://lh7-rt.googleusercontent.com/docsz/AD_4nXeyAxUhkJL1KGr28qrGo2cGqaMKC-qyvKz-tKNjcSIKT_RtCLLPEhJ-hf2EPogA0u9Euq6uN6p1iX2tmi9uAVX-zt83NuhX32ZqObUoRJLG6KA56kP5wCvSKpWGGdU9yrRJIDZU5Q?key=yQ6zY4mO39wxZSdIKEQBKM23)

[<투표 순서도>](https://lh7-rt.googleusercontent.com/docsz/AD_4nXdX2yOdVHQ0ixkGQbWDFNMCBYZuh-IlUJoqMTMU1sjnqbxhlneWYI3XGuZ9knHc2vvsLGDS2j6WdP6EQO6Q9q6jaJkPwVh-G1OaOqmvCqHtZkbU6QehZiJCq6j-TYoAag6m1vP3Gg?key=QLb5grh6-g01snpKXaMdZBiy)

<투표 순서도>

- **구현 기술**
    - Firebase의 이메일 인증 기능 이용하여 회원가입 및 로그인 기능 구현
    - 이메일 인증 과정으로 본인 명의 이메일
    - 회원가입
        - 사용자가 이메일과 비밀번호를 입력하여 Firebase에 계정을 생성
        - 이메일 인증을 통해 본인 명의의 이메일만을 사용하여 사칭 계정 예방
    - 로그인
        - 사용자가 입력한 이메일과 비밀번호를 Firebase와 비교하여 인증
        - 인증 성공 시 게임 내에서 사용자 정보를 유지하도록 처리
- **이 방식을 사용한 이유**
    - 유지보수성
        - 각 기능의 로직을 분리하여 관리하여 유지보수성을 향상
        - 기능 문제 발생 시 각 기능 로직에서 수정 가능

![<로그인 창>](attachment:ff0270d0-b8d2-4710-a05c-25b8279a127b:스크린샷_2025-02-28_000616.png)

<로그인 창>

![<비밀번호 재설정 창>](attachment:8111202e-32e1-47ce-99c2-dbdc39cf90fd:스크린샷_2025-02-28_000640.png)

<비밀번호 재설정 창>

![<회원가입 창>](attachment:e3e12043-9568-46df-a57f-dd447d368d18:스크린샷_2025-02-28_000624.png)

<회원가입 창>

![<이메일 인증 대기 창>](attachment:19aa77b4-5aec-4118-80c3-c4bda1ae16cd:스크린샷_2025-02-28_000811.png)

<이메일 인증 대기 창>

### 유저데이터 Firebase의 RealTime Database에 저장

[](https://lh7-rt.googleusercontent.com/docsz/AD_4nXeyAxUhkJL1KGr28qrGo2cGqaMKC-qyvKz-tKNjcSIKT_RtCLLPEhJ-hf2EPogA0u9Euq6uN6p1iX2tmi9uAVX-zt83NuhX32ZqObUoRJLG6KA56kP5wCvSKpWGGdU9yrRJIDZU5Q?key=yQ6zY4mO39wxZSdIKEQBKM23)

[<투표 순서도>](https://lh7-rt.googleusercontent.com/docsz/AD_4nXdX2yOdVHQ0ixkGQbWDFNMCBYZuh-IlUJoqMTMU1sjnqbxhlneWYI3XGuZ9knHc2vvsLGDS2j6WdP6EQO6Q9q6jaJkPwVh-G1OaOqmvCqHtZkbU6QehZiJCq6j-TYoAag6m1vP3Gg?key=QLb5grh6-g01snpKXaMdZBiy)

<투표 순서도>

- **구현 기술**
    - 이벤트식 연결로 유저데이터 실시간으로 데이터베이스와 연동
        - JSON 기반의 실시간 데이터베이스를 사용하여 변경 사항이 즉시 반영되도록 설정
    - 이벤트 기반 데이터 연동
        - Firebase의 ValueChanged 이벤트를 활용하여 데이터가 변경될 때마다 클라이언트에서 자동으로 갱신되도록 처리
        - 이를 통해 유저가 게임을 진행하면서 데이터가 실시간으로 저장되고, 다른 기기에서 접속해도 동일한 데이터를 유지할 수 있도록 함
    - 스테이지 해금 시스템 구현
        - 유저가 특정 도전할 스테이지의 이전 스테이지를 클리어했다는 조건을 충족하면 다음 스테이지가 해금 되도록 구현
        - 해금된 데이터는 Firebase에 즉시 저장되어, 게임을 재시작하거나 다른 기기에서 접속해도 동일한 상태를 유지할 수 있도록 구현
- **Firebase를 사용한 이유**
    - 실시간 동기화
        - 데이터를 변경하면 자동으로 반영되므로 별도의 요청 없이도 최신 상태를 유지 가능
    - 클라우드 기반 데이터 관리
        - 기기별 저장이 아니라 클라우드에 저장되므로, 다양한 환경에서 같은 데이터를 사용할 수 있음
    - 자동 저장 및 백업
        - 유저 데이터가 클라우드에 저장되므로 기기 변경이나 데이터 손실 시에도 복구가 가능

![<Firebase 실시간 데이터 저장중>](attachment:22b7059f-38ad-4091-b2dc-fda9e23fd4d0:image.png)

<Firebase 실시간 데이터 저장중>

![<Firebase 데이터 UI 연동>](attachment:3ced62f4-7c67-43bc-8a6f-2c053a044ee8:싨기간데이터연동.jpg)

<Firebase 데이터 UI 연동>

### BackendManager, DatabaseManager 싱글톤으로 구현

[](https://lh7-rt.googleusercontent.com/docsz/AD_4nXeyAxUhkJL1KGr28qrGo2cGqaMKC-qyvKz-tKNjcSIKT_RtCLLPEhJ-hf2EPogA0u9Euq6uN6p1iX2tmi9uAVX-zt83NuhX32ZqObUoRJLG6KA56kP5wCvSKpWGGdU9yrRJIDZU5Q?key=yQ6zY4mO39wxZSdIKEQBKM23)

[<투표 순서도>](https://lh7-rt.googleusercontent.com/docsz/AD_4nXdX2yOdVHQ0ixkGQbWDFNMCBYZuh-IlUJoqMTMU1sjnqbxhlneWYI3XGuZ9knHc2vvsLGDS2j6WdP6EQO6Q9q6jaJkPwVh-G1OaOqmvCqHtZkbU6QehZiJCq6j-TYoAag6m1vP3Gg?key=QLb5grh6-g01snpKXaMdZBiy)

<투표 순서도>

- **구현 기술**
    - 싱글톤 패턴 적용
        - 언제든지 전역적으로 접근 가능하도록 구현
        - DontDestroyOnLoad를  사용하여 씬이 변경되어도 유지되도록 설정
    - BackendManager는 로그인 및 인증 등의 백엔드 관련 기능 수행하며,
    - DataManager와 연계하여 유저데이터를 관리
- **이 방식을 사용한 이유**
    - 전역적 접근 필요
        - 로그인, 유저데이터, 데이터 동기화 등의 기능은 게임 전쳉에서 지속적으로 접근해야하므로 싱글톤을 사용하여 전역적으로 접근 가능하게 구현
    - 데이터 일관성 유지
        - 하나의 인스턴스만을 생성하여 데이터의 일관성을 보장
    - 유지보수성
        - 특정 기능(로그인, 데이터 저장 등)을 한 곳에서 관리하여 코드 유지보수가 용이함

# **6. UI 관리**

---

## 6.1 구상

- Canvas 분할을 통한 드로우콜 줄이기
- View 클래스에서 UI 관리
- UI 바인딩을 통한 분산된 UI 오브젝트 관리

## 6.2 설계 방법

### Canvas 분할

[](https://lh7-rt.googleusercontent.com/docsz/AD_4nXeyAxUhkJL1KGr28qrGo2cGqaMKC-qyvKz-tKNjcSIKT_RtCLLPEhJ-hf2EPogA0u9Euq6uN6p1iX2tmi9uAVX-zt83NuhX32ZqObUoRJLG6KA56kP5wCvSKpWGGdU9yrRJIDZU5Q?key=yQ6zY4mO39wxZSdIKEQBKM23)

[<투표 순서도>](https://lh7-rt.googleusercontent.com/docsz/AD_4nXdX2yOdVHQ0ixkGQbWDFNMCBYZuh-IlUJoqMTMU1sjnqbxhlneWYI3XGuZ9knHc2vvsLGDS2j6WdP6EQO6Q9q6jaJkPwVh-G1OaOqmvCqHtZkbU6QehZiJCq6j-TYoAag6m1vP3Gg?key=QLb5grh6-g01snpKXaMdZBiy)

<투표 순서도>

- **구현 기술**
    - UI의 각 기능을 분리하여 개별적인 Canvas로 구성
    - UI 변화가 적은 부분과 잦은 부분을 분리하여 최적화
    - 필요할 때만 특정 Canvas를 활성화하여 렌더링 비용 절감
- **이 방식을 사용한 이유**
    - 최적화
        - Unity의 Canvas는 변경이 발생할 때마다 전체를 다시 그리는 특성이 있음
        - UI 변경이 발생하는 영역만 다시 그리도록 Canvas를 분할하여 드로우콜 감소
        - 전체적인 성능 향상 및 프레임 유지
            
            ![<Game 씬 에시>](attachment:4c418f6e-17c4-49d9-a26f-ec09fd68a1dc:캔버스분할예시.jpg)
            
            <Game 씬 에시>
            

### View 클래스에서 UI 관리

[](https://lh7-rt.googleusercontent.com/docsz/AD_4nXeyAxUhkJL1KGr28qrGo2cGqaMKC-qyvKz-tKNjcSIKT_RtCLLPEhJ-hf2EPogA0u9Euq6uN6p1iX2tmi9uAVX-zt83NuhX32ZqObUoRJLG6KA56kP5wCvSKpWGGdU9yrRJIDZU5Q?key=yQ6zY4mO39wxZSdIKEQBKM23)

[<투표 순서도>](https://lh7-rt.googleusercontent.com/docsz/AD_4nXdX2yOdVHQ0ixkGQbWDFNMCBYZuh-IlUJoqMTMU1sjnqbxhlneWYI3XGuZ9knHc2vvsLGDS2j6WdP6EQO6Q9q6jaJkPwVh-G1OaOqmvCqHtZkbU6QehZiJCq6j-TYoAag6m1vP3Gg?key=QLb5grh6-g01snpKXaMdZBiy)

<투표 순서도>

- **구현 기술**
    - MVC(Model-View-Controller) 패턴을 적용하여 UI 관리
    - View 클래스에서 UI 요소를 관리하고, 데이터와 로직은 별도의 클래스에서 처리
    - UI 요소를 직접 참조하지 않고 **이벤트 기반**으로 UI 업데이트
- **이 방식을 사용한 이유**
    - 유지보수성
        - UI 관련 코드와 로직을 분리하여 코드의 가독성을 향상
        - 다른 UI 요소 간 의존성을 최소화하여 유지보수 용이
    - 확장성
        - UI 변경이 발생해도 View 클래스만 수정하면 되므로 확장성이 뛰어남

### **UI 바인딩**

- **구현 기술**
    - UIBinder 클래스를 상속 받아 Bind() 매서드를 호출하여 UI 오브젝트와 컴포넌트를 바인딩하고 GetUI<T>(String name) 메서드를 사용하여 특정 UI 컴포넌트를 동적으로 가져와 관리 가능
    - UI 요소 탐색 결과를 딕셔너리에 저장하여 반복 탐색 비용을 줄임
    - 모든 이벤트를 EventReciver를 통해 통합 관리하며, 필요할 때 동적으로 추가/제거 가능
- **해당 방식을 사용한 이유**
    - 편의성/유연성
        - 계속 많아지는 게임 오브젝트와 복잡한 UI 요소를 인스펙터에서 직접 참조하는 방식이 아닌 스크립트 상에서 동적으로 바인딩 및 관리하고자 사용

# Ⅲ. 이슈 및 해결 사항

## 1. Firebase 실시간 데이터 UI 연동 미적용 문제  ****

---

### 원인

Firebase 데이터베이스 에 저장하고 있는 유저 데이터를 UI에 연결하는 과정에서 값 변경 시 UI 연동이 끊기는 문제가 발생

DatabaseManager에서 값 변경 시 데이터베이스에 저장은 하지만 데이터베이스와 UI가 실시간으로 연결되지 않는 문제 파악

### 해결 방법

업데이트에서 프레임마다 확인하는 방법보다 이벤트를 사용하여 값 변경 시 UI에 연결
 Firebase 데이터베이스의 ValueChanged 이벤트를 활용하여 데이터가 변경될 때 자동으로 변수 값을 업데이트하고, UnityEvent를 통해 변경된 값을 UI와 연결시킬 수 있게 구현

![<이벤트를 활용하여 데이터 변경시 실시간 업뎅이트>](attachment:4b6f9f9a-f39d-40a1-84ce-a58a4fba000c:image.png)

<이벤트를 활용하여 데이터 변경시 실시간 업뎅이트>
