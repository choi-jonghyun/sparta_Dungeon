# 개인과제 스파르타 던전모험 
## 프로젝트 소개
-이 프로젝트는 Unity 3D로 개발된 생존형 RPG 게임입니다. </br>플레이어는 배고픔, 체력, 스태미나를 관리하며 다양한 아이템을 획득하고 사용할 수 있습니다.</br>
또한 인터랙션 시스템, 인벤토리 시스템, 점프 발판 등 다양한 기능을 통해 기본적인 생존 메커니즘과 UI 연동을 경험할 수 있도록 설계되어 있습니다.

📷 스크린샷</br>
![image](https://github.com/user-attachments/assets/b5d38f0b-89b4-4d6e-9b91-771587fd9cbc)


## 개발 환경</br>
- Unity 2022.3.17 f1</br>
- TextMeshPro 사용</br>
- C# </br>

## 주요 기능
- 생존 시스템 (PlayerCondition)
- 체력, 허기, 스태미나 수치 관리

- 허기가 0일 때 체력 감소

- 시간이 지날수록 허기 감소, 스태미나 회복

- 피해 처리 / 힐 / 아이템 섭취 / 사망 처리 등 포함

- UI 연동 시스템 (Condition, UICondition)
체력, 허기, 스태미나를 실시간으로 UI Bar에 표시

- Image.fillAmount로 연동

### 인벤토리 시스템 (UIInventory, ItemSlot)
- 아이템 획득 및 드랍

- 아이템 스택 가능 (중첩 수량 표시)

- 소모 아이템 사용으로 능력치 회복 및 스피드 버프

- UI에서 아이템 이름, 설명, 능력치 효과 확인 가능

### 아이템 시스템 (ItemData, ItemObject)
- ScriptableObject 기반 아이템 데이터 관리

- ItemType, ConsumableType 기반으로 확장 가능

- 아이템 드랍 시 Prefab 생성

### 상호작용 시스템 (Interaction)
- 플레이어 중심 레이캐스트로 상호작용 가능 오브젝트 감지

- 텍스트로 오브젝트 설명 출력

- E 키로 아이템 획득

### 점프 발판 시스템 (Jump)
- 트리거 충돌 시 Rigidbody에 순간적인 힘 적용

- 다양한 방향 설정 가능 (Vector3.up, Vector3.forward 등)

## 폴더구조
Assets/</br>
// ├── Externals/              
// ├── Fonts/                 
// ├── InputAction/            
// ├── Materials/             
// ├── Prefabs/                
// │   └── UI/                 
// │   ├── Item_Carrot         
// │   └── Item_Rock          
// ├── Scenes/                
// ├── Scripts/               
// │   ├── Item/              
// │   ├── Player/             
// │   ├── ScriptableObject/  
// │   ├── UI/                 
// └── └── Jump/              
 
