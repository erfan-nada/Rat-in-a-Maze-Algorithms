Here is the raw Markdown code for your GitHub `README.md`. I have optimized the formatting with clear headers, lists, and code blocks so it looks professional on your profile.

```markdown
# Maze Solver (DFS Algorithm)

A Windows Forms application that visualizes the **Depth-First Search (DFS)** algorithm. This project demonstrates how a stack-based approach can be used to navigate through a maze, featuring a "rat" character that explores paths, backtracks from dead ends, and eventually finds the exit.

---

## 🚀 Features

* **Real-time Visualization:** Watch the DFS algorithm work step-by-step with a controllable timer.
* **Interactive Maze:** Click on any cell in the grid to toggle between a **Wall** (Black) and a **Path** (White).
* **Randomization:** Generate new maze layouts instantly using the "Randomize" feature.
* **Backtracking Logic:** Clearly distinguishes between the current active path (Orange) and cells that have been visited.
* **Custom Assets:** Supports external images for the background and the "rat" character, with a fallback to geometric shapes if images are missing.

---

## 🛠️ Technical Details

### Algorithm: Depth-First Search (DFS)
The solver uses a `Stack<(int r, int c, int dir)>` to keep track of the current path and the direction being explored.

* **Exploration:** It tries to move in four directions (Right, Down, Left, Up).
* **Backtracking:** When it hits a dead end (no safe adjacent cells), it pops the stack to return to the previous junction.
* **State Management:** The maze grid uses integers to represent states:
    * `0`: Unvisited Path
    * `1`: Wall
    * `2`: Active Path (Current Stack)
    * `3`: Visited/Dead End

### Graphics
* **Double Buffering:** Implemented via a `Bitmap off` buffer to prevent flickering during animation.
* **Modern UI:** Features a dark-themed sidebar with styled buttons and a status label.

---

## 📂 Project Structure

* **cimgactor:** A helper class for managing game objects, positions, and image rendering.
* **Form1.cs:** The main logic containing the UI initialization, the DFS step-by-step execution, and the GDI+ drawing code.

---

## 🚦 How to Use

1.  **Launch the App:** The application starts with a default maze layout.
2.  **Edit the Maze:** Use your mouse to click on the grid to create your own obstacles or clear paths.
3.  **Randomize:** Click **RANDOMIZE** to generate a 30% density obstacle course.
4.  **Start Solving:** Click **START / SOLVE**. The "rat" will begin moving from the top-left towards the bottom-right exit.
5.  **Reset:** Use the **RESET MAZE** button to clear the current progress and return to the starting state.

---

## 🖼️ Assets

To use custom graphics, place the following files in your build directory (e.g., `bin/Debug`):

* **back.jpg:** The background image for the maze area.
* **ratt.jpg:** The sprite for the actor (the code automatically makes the top-left pixel color transparent).

---

## 💻 Requirements

* .NET Framework (Windows Forms support)
* Visual Studio 2019 or newer
```
