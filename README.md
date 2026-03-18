# 2D Platformer Game — Unity

A 2D platformer game built with Unity as part of a university game development course.
The player navigates through a level, collects coins, avoids hazards, and reaches the finish line.

---

## Gameplay

- Collect all coins to unlock the finish
- Avoid damage zones that drain your energy
- Fall off the map — lose a life and respawn at the last checkpoint
- Run out of lives — Game Over

---

## Project Structure
```
Assets/
├── Scripts/
│   ├── GameManager.cs       # Core game logic, UI, win/lose conditions
│   ├── FallRespawn.cs       # Fall detection and checkpoint respawn
│   ├── Coin.cs              # Coin pickup logic
│   ├── Checkpoint.cs        # Checkpoint activation
│   ├── DamageZone.cs        # Continuous energy drain trigger
│   ├── MovingPlatform.cs    # Back-and-forth platform movement
│   ├── SlipperyPlatform.cs  # Applies low-friction physics material
│   ├── BouncePlatform.cs    # Applies bounce-on-contact force
│   └── AudioManager.cs      # Centralized audio management
├── Scenes/
├── Audio/
│   ├── Music/
│   ├── SFX/
│   └── Ambience/
└── Prefabs/
```

---

## Requirements

- **Unity** 2022.3 LTS or newer
- **TextMeshPro** (included via Unity Package Manager)
- No external dependencies required

---

## Getting Started

1. Clone the repository:
```bash
   git clone https://github.com/D0naL1ka/2D-Game.git
```
2. Open the project in **Unity Hub**
3. Open the main scene from `Assets/Scenes/`
4. Press **Play** ▶️

---

## How to Win

1. Collect **all coins** in the level
2. Reach the **finish zone**

You lose if all 5 lives are depleted (by falling or standing in damage zones).

---

## Author

**D0naL1ka** — [github.com/D0naL1ka](https://github.com/D0naL1ka)
