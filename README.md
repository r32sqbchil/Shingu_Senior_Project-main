# Shingu_Senior_Project-main
1. 게임명
- Into the childhood

2. 소개
- 동화 속 마을을 성장시키세요! 평화를 위협하는 적들을 물리치세요! 

3. 장르
- 타이쿤(Tycoon),디펜스(Defence)

4. 제작 기간
- 2022.03.25 ~ 2022.06.20

5. 게임 엔진
- Unity

6. 협업 툴
- Sourcetree → Github Desktop

7. 참여자
- 기획자: 6명
- 프로젝트 매니저(PM): 1명
- 아티스트: 4명
- 개발자: 3명

***
1. 로딩 바(Loading Bar)
- 비동기 방식(Async)으로 로딩창을 제작
- AsynceOperation의 변수인 isDone을 활용하여 로딩이 완료되지 않았을 때 null을 반환하도록 함
- AsynceOperation의 변수인 progress를 활용하여 Slider의 value값을 실시간으로 바꿔주도록 함
- 게임 완성 후 로딩창이 필요할만큼 리소스가 크지 않아서 삭제
![image](https://user-images.githubusercontent.com/91232917/229370678-dc5c9743-6adf-4759-8073-cc1b050a1663.png)
***
2. 화면 전환 - Fade
- 코루틴을 활용한 Fade 효과 제작
- 알파값이 1이 될 때까지 Mathf.Lerp로 부드럽게 alpha값을 바꿔준다
***
3. 격자 타일(2D Grid)
- Tilemap을 활용하여 Grid 제작
- UnityEngine.Tilemaps 참조
- Tilemap.GetTilesBlock을 통해 타일 영역을 가져옴
- 가져온 영역으로 건물이 격자 위에 올바르게 올라가 있을 때는 초록, 그렇지 않을 때는 빨강으로 타일의 상태를 구분함
- 올바른 영역일 때 Tilemap.SetTilesBlock을 통해 타일 영역을 채움
- 3D Grid로 진행하기로 결정해서 삭제
![image](https://user-images.githubusercontent.com/91232917/229372848-a4c02122-c252-4c79-a714-57f6c09b4db2.png)
