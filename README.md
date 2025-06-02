## SP_7_3dTeamProject
스파르타 7조, 숙련 팀프로젝트 과제입니다.


- 플레이어 조작   
  -캐릭터 이동 : WASD   
  -캐릭터 공격 : 마우스 좌클릭  
  -캐릭터 및 화면 회전 : 마우스 좌우 이동   
  -캐릭터 점프 : 스페이스바   
  -인벤토리 : I   
  -건축메뉴 : Tap   
![image](https://github.com/user-attachments/assets/1aa2f398-b825-43d4-99e1-5a3b644022f7)



- 컨텐츠_자원   
  -생성된 자원물(나무, 돌, 철, 버섯)에 접근 시, 건축재료 및 음식 드랍   
  -채취시 자원물 파괴, 일정 시간 후 재생성   

![image](https://github.com/user-attachments/assets/1c4c5156-7e19-467a-897e-f225e8545442)

- 컨텐츠_빌드   
   -건축메뉴(Tap)을 열고, 원하는 건축 아이콘을 클릭 시 '빌드 Preview'를 생성   
   -생성된 '빌드 preview'를 올바른 위치에 두고, 마우스 좌클릭시 건축재료를 소모하며 건축완료
  
   -생성 된 건축물은 몬스터의 접근을 막는데 사용.   
   -건축 된 집 안에 있을 시 Hp 회복.

![image](https://github.com/user-attachments/assets/92e49592-71bd-4e06-b1ee-34f7e38c7f0e)


- 컨텐츠_몬스터   
  -여러 종류의 곰이 맵 내에 생성되며, 그 수는 시간의 경과에 비례해 늘어남.   
  -곰들은 일정범위 내 플레이어를 탐지해 쫒아가며, 만약 그 범위를 벗어나거나, 플레이어가 쫒아갈 수 없는 위치(집, 물 등)로 도망간다면 포기함   
  -곰들은 평소에는 맵을 배회하며, 이동 속도에 따라 다양한 모션을 가짐.   
  -곰들이 플레이어를 공격 시, 여러가지 모션 중 랜덤한 모션을 보여줌.

   -백곰의 Hp가 30%이하가 되면 '광폭화'상태에 돌입해 크기가 커지고 강력해짐.   
   -백곰은 Hp가 특정 수치로 내려가면, 도망을 친 후 컨디션을 회복하여 다시 플레이어를 노림.   

   -곰을 사냥하는데 성공 시, 아이템 드랍   
   -드랍 된 아이템은 인벤토리 내에서 장착 가능   
![image](https://github.com/user-attachments/assets/d1a45304-e45a-4633-9506-15bdea9e54ed)
![image](https://github.com/user-attachments/assets/6cd48ff5-6271-42f5-9c5f-009d159cb647)



- 컨텐츠_UI 및 사운드   
  -화면 우측 상단 톱니바퀴 버튼을 누르면 옵션 조절 가능. BGM 및 효과음 크기, 재시작 게임끄기 등   
  -Hp가 0이 될 시 GameOver UI에서 플레이 선택가능   

![image](https://github.com/user-attachments/assets/087c29bd-d913-46d1-93b1-e1fe1880196f)

![image](https://github.com/user-attachments/assets/2f11cb1f-6252-40ec-af69-b973c8597f37)


- 컨텐츠_NPC   
  -맵 내에 Npc에게 접근 시 대화 가능     


- 그외    
  -물에 접근시 Hydration 수치 회복   
  -인벤토리(I)를 열고, 음식 아이콘을 클릭 후, 사용하기 버튼을 누르면 Hp와 Hunger수치가 회복   


