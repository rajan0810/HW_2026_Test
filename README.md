# 🎮 Doofus - 3D Platformer Survival

A dynamic, endless 3D platformer built in Unity where the player must navigate a constantly shifting pathway of vanishing tiles. This project demonstrates core gameplay loops, JSON data parsing, custom UI systems, and advanced game-feel mechanics.

## 🎥 Gameplay Demo
https://github.com/user-attachments/assets/abf0645b-e131-4a88-aeee-6b1ace703e37

## 📸 Screenshots
| Start Screen | Gameplay | Game Over |
| :---: | :---: | :---: |
| <img width="960" height="537" alt="Screenshot 2026-08-21 at 7 57 00 PM" src="https://github.com/user-attachments/assets/077ca8b4-d1b0-405e-885c-1e464d6f4cda" /> | <img width="960" height="537" alt="Screenshot 2026-08-21 at 7 57 14 PM" src="https://github.com/user-attachments/assets/532d14b4-9a96-441b-bfe3-2794fe38f2f3" /> | <img width="960" height="537" alt="Screenshot 2026-08-21 at 7 57 21 PM" src="https://github.com/user-attachments/assets/30cee80a-cb3b-49a5-8482-cb67ddf83b66" /> |

## 🚀 Key Features

* **Dynamic JSON Configuration:** The player's speed and the platform spawn/destroy timers are dynamically loaded from a configuration file (`GameData.json`), making the game highly modular and easy to balance without changing code.
* **Algorithmic Platform Generation:** Platforms (Pulpits) spawn infinitely in random adjacent directions, forcing the player to react quickly.
* **Advanced "Game Feel":**
  * Smooth, interpolated Coroutine animations for platform scaling and warning-color blinking.
  * A `SmoothDamp` camera controller that glides gracefully behind the player, completely independent of the player's rigid hierarchy.
  * A classic "Mario-style" death sequence (time freeze, vertical hop, and fall) when the player drops out of bounds.
* **Global Audio Manager:** Implements a Singleton pattern to manage looping background music and overlapping sound effects, designed to persist seamlessly across level resets.
* **Modern UI/UX:** Features a fully responsive Canvas system using TextMeshPro (SDF fonts) with arcade-style drop shadows, hover states, and independent timescale management for menus.

## 🛠️ Technical Details

* **Engine:** Unity (2022.3 LTS or similar)
* **Language:** C#
* **Design Patterns Used:** Singleton (Audio Manager), Object Pooling logic (List management for Pulpits).

## 🕹️ How to Play

1. Clone this repository to your local machine.
2. Open the project in Unity.
3. Open the **Main** scene located in the `Assets/Scenes` folder.
4. Press **Play** in the editor.
5. Use **W, A, S, D** or **Arrow Keys** to move Doofus across the green platforms before they shrink and disappear!
