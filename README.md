# 🧩 Rat in a Maze (DFS Algorithm)

<p align="center">
  A Windows Forms application that visualizes the <b>Depth-First Search (DFS)</b> algorithm.
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-Windows%20Forms-blue" />
  <img src="https://img.shields.io/badge/Algorithm-DFS-orange" />
  <img src="https://img.shields.io/badge/Status-Active-success" />
</p>

---

## 📑 Table of Contents
- [📌 Overview](#-overview)
- [🎥 Demo](#-demo)
- [🚀 Features](#-features)
- [🛠️ Technical Details](#️-technical-details)
- [📂 Project Structure](#-project-structure)
- [🚦 How to Use](#-how-to-use)
- [🖼️ Assets](#️-assets)
- [💻 Requirements](#-requirements)

---

## 📌 Overview

A Windows Forms application that visualizes the **Depth-First Search (DFS)** algorithm. This project demonstrates how a stack-based approach can be used to navigate through a maze, featuring a "rat" character that explores paths, backtracks from dead ends, and eventually finds the exit.

---

## 🎥 Demo

<p align="center">
  <img src="demo.gif" alt="Maze Solver Demo" width="600" onerror="this.src='https://via.placeholder.com/600x400?text=Upload+Demo+GIF+to+Repo'"/>
</p>

---

## 🚀 Features

- **Real-time Visualization:** Watch the DFS algorithm work step-by-step with a controllable timer.  
- **Interactive Maze:** Click on any cell in the grid to toggle between a **Wall** (Black) and a **Path** (White).  
- **Randomization:** Generate new maze layouts instantly using the "Randomize" feature.  
- **Backtracking Logic:** Clearly distinguishes between the current active path (Orange) and cells that have been visited.  
- **Custom Assets:** Supports external images for the background and the "rat" character, with a fallback to geometric shapes if images are missing.  

---

## 🛠️ Technical Details

### 🔍 Algorithm: Depth-First Search (DFS)

The solver uses a `Stack<(int r, int c, int dir)>` to keep track of the current path and the direction being explored.

- **Exploration:** It tries to move in four directions (Right, Down, Left, Up).  
- **Backtracking:** When it hits a dead end (no safe adjacent cells), it pops the stack to return to the previous junction.  
- **State Management:** The maze grid uses integers to represent states:

| Value | Meaning |
| :--- | :--- |
| `0` | Unvisited Path |
| `1` | Wall |
| `2` | Active Path (Current Stack) |
| `3` | Visited/Dead End |

### 🎨 Graphics
- **Double Buffering:** Implemented via a `Bitmap off` buffer to prevent flickering during animation.
- **Modern UI:** Features a dark-themed sidebar with styled buttons and a status label.

---

## 📂 Project Structure

- **cimgactor** → Helper class for managing game objects, positions, and image rendering.  
- **Form1.cs** → Main logic (UI initialization, DFS execution, GDI+ drawing).

---

## 🚦 How to Use

1. **Launch the App:** The application starts with a default maze layout.
2. **Edit the Maze:** Use your mouse to click on the grid to create your own obstacles or clear paths.
3. **Randomize:** Click **RANDOMIZE** to generate a 30% density obstacle course.
4. **Start Solving:** Click **START / SOLVE**. The "rat" will begin moving from the top-left towards the bottom-right exit.
5. **Reset:** Use the **RESET MAZE** button to clear the current progress and return to the starting state.

---

## 🖼️ Assets

To use custom graphics, place the following files in your build directory (e.g., `bin/Debug`):

- **back.jpg** → Background image for the maze area.  
- **ratt.jpg** → Sprite for the actor (top-left pixel becomes transparent automatically).

---

## 💻 Requirements

- .NET Framework (Windows Forms support)
- Visual Studio 2019 or newer
