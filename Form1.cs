using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AlgorithmsMazeProject
{
    public class cimgactor
    {
        public int x, y;
        public Bitmap img;
        public Rectangle rsrc, rdst;
        public Color fallbackColor;
    }

    public partial class Form1 : Form
    {
        Bitmap off;
        cimgactor rat;
        cimgactor background;
        Timer t = new Timer();

        
        int[,] maze;
        int rows;
        int cols;

        int cellW = 50;
        int cellH = 50;
        int startX = 450;
        int startY = 25;

        Stack<(int r, int c, int dir)> st = new Stack<(int r, int c, int dir)>();
        bool solved = false;
        bool isRunning = false;
        int ratRow = 1;
        int ratCol = 1;

        Label lblStatus;

        public Form1()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;
            this.Text = "MSA Algorithm Project - Maze Solver (DFS)";

            InitializeModernGUI();
            ResetMaze();

            this.DoubleBuffered = true;
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.MouseClick += new MouseEventHandler(Form1_MouseClick);

            t.Interval = 100; 
            t.Tick += T_Tick;
        }

        private void Form1_Load(object sender, EventArgs e) { }
        private void Form1_KeyDown(object sender, KeyEventArgs e) { }

        void InitializeModernGUI()
        {
            Panel sidebar = new Panel();
            sidebar.Dock = DockStyle.Left;
            sidebar.Width = 250;
            sidebar.BackColor = Color.Black;
            this.Controls.Add(sidebar);

            Label title = new Label();
            title.Text = "MAZE CONTROLS";
            title.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            title.ForeColor = Color.White;
            title.AutoSize = false;
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.Location = new Point(0, 30);
            title.Size = new Size(250, 40);
            sidebar.Controls.Add(title);

            lblStatus = new Label();
            lblStatus.Text = "Ready";
            lblStatus.Font = new Font("Segoe UI", 11, FontStyle.Italic);
            lblStatus.ForeColor = Color.Orange;
            lblStatus.AutoSize = false;
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            lblStatus.Location = new Point(0, 70);
            lblStatus.Size = new Size(250, 30);
            sidebar.Controls.Add(lblStatus);

            Button btnSolve = CreateStyledButton("START / SOLVE", Color.FromArgb(46, 204, 113));
            btnSolve.Location = new Point(25, 130);
            btnSolve.Click += (s, e) => StartSolving();
            sidebar.Controls.Add(btnSolve);

            Button btnReset = CreateStyledButton("RESET MAZE", Color.FromArgb(231, 76, 60));
            btnReset.Location = new Point(25, 200);
            btnReset.Click += (s, e) => ResetMaze();
            sidebar.Controls.Add(btnReset);

            Button btnRandom = CreateStyledButton("RANDOMIZE", Color.FromArgb(52, 152, 219));
            btnRandom.Location = new Point(25, 270);
            btnRandom.Click += (s, e) => RandomizeMaze();
            sidebar.Controls.Add(btnRandom);

            Label lblInstr = new Label();
            lblInstr.Text = "INSTRUCTIONS:\n\n• Orange = Active Path\n• White = Visited/Empty\n• Click Grid to Toggle Walls";
            lblInstr.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            lblInstr.ForeColor = Color.LightGray;
            lblInstr.AutoSize = true;
            lblInstr.Location = new Point(25, 350);
            sidebar.Controls.Add(lblInstr);
        }

        Button CreateStyledButton(string text, Color color)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Size = new Size(200, 50);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            return btn;
        }

        void ResetMaze()
        {
            t.Stop();
            isRunning = false;
            solved = false;
            st.Clear();
            ratRow = 1;
            ratCol = 1;

            if (lblStatus != null) lblStatus.Text = "Ready";
            if (lblStatus != null) lblStatus.ForeColor = Color.Orange;

            maze = new int[,]
            {
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,0,0,0,0,0,0,0,0,0,0,1,0,0,1},
                {1,0,1,1,1,1,0,1,1,1,0,1,0,1,1},
                {1,0,1,0,1,1,0,0,0,1,0,0,0,1,1},
                {1,0,1,0,1,1,1,1,0,1,1,1,1,0,1},
                {1,0,0,0,0,0,0,1,0,0,0,0,1,0,1},
                {1,1,1,1,1,1,0,1,1,1,1,0,1,0,1},
                {1,0,0,0,0,1,0,0,0,0,1,0,1,0,1},
                {1,0,1,1,0,1,1,1,0,1,1,1,1,0,1},
                {1,0,0,1,0,0,0,0,0,1,0,0,1,0,1},
                {1,1,0,1,1,1,1,1,1,1,1,0,1,0,1},
                {1,0,0,0,0,0,0,0,0,0,1,0,1,0,1},
                {1,0,1,1,1,1,1,1,1,0,1,0,0,0,1},
                {1,0,0,0,0,0,0,0,1,1,1,1,1,0,1},
                {1,1,1,1,1,1,1,0,0,0,0,0,0,0,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,0,0}
            };

            rows = maze.GetLength(0);
            cols = maze.GetLength(1);

            LoadAssets();
            this.Invalidate();
        }

        void RandomizeMaze()
        {
            Random rnd = new Random();
            for (int r = 1; r < rows - 1; r++)
            {
                for (int c = 1; c < cols - 1; c++)
                {
                    maze[r, c] = (rnd.Next(0, 100) < 30) ? 1 : 0;
                }
            }
            maze[1, 1] = 0;
            maze[rows - 1, cols - 2] = 0;
            maze[rows - 1, cols - 1] = 0;
            ResetVisualsOnly();
            lblStatus.Text = "Maze Randomized";
            this.Invalidate();
        }

        void ResetVisualsOnly()
        {
            st.Clear();
            ratRow = 1;
            ratCol = 1;
            solved = false;
            isRunning = false;
            t.Stop();
            updateRatPosition();
        }

        void StartSolving()
        {
            if (isRunning) return;
            st.Push((1, 1, 0));
            maze[1, 1] = 2; 
            isRunning = true;
            lblStatus.Text = "Solving (DFS)...";
            lblStatus.ForeColor = Color.Orange;
            t.Start();
        }

        private void T_Tick(object sender, EventArgs e)
        {
            if (!isRunning || solved) return;
            PerformDFSStep();
            this.Invalidate();
        }

        void PerformDFSStep()
        {
            if (st.Count == 0)
            {
                t.Stop();
                lblStatus.Text = "No Solution!";
                MessageBox.Show("No solution found!");
                return;
            }

            var (r, c, dir) = st.Pop();

            // Check if we hit the last cell
            if (r == rows - 1 && c == cols - 1)
            {
                ratRow = r;
                ratCol = c;
                updateRatPosition();
                this.Refresh();

                solved = true;
                t.Stop();
                lblStatus.Text = "Target Reached!";
                lblStatus.ForeColor = Color.LightGreen;
                MessageBox.Show("Maze Solved!");
                return;
            }

            int[] dr = { 0, 1, 0, -1 };
            int[] dc = { 1, 0, -1, 0 };

            for (int i = dir; i < 4; i++)
            {
                int nr = r + dr[i];
                int nc = c + dc[i];

                if (isSafe(nr, nc))
                {
                    // Mark as Active Path (Orange)
                    maze[nr, nc] = 2;

                    // Push current state to backtrack later
                    st.Push((r, c, i + 1));

                    // Push new state
                    st.Push((nr, nc, 0));

                    ratRow = nr;
                    ratCol = nc;
                    updateRatPosition();
                    return;
                }
            }

            
            maze[r, c] = 3;

            // move rat back to previous stack item
            if (st.Count > 0)
            {
                var parent = st.Peek();
                ratRow = parent.r;
                ratCol = parent.c;
            }
            updateRatPosition();
        }

        bool isSafe(int r, int c)
        {
            if (r < 0 || c < 0 || r >= rows || c >= cols) return false;
            return maze[r, c] == 0;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isRunning) return;

            int c = (e.X - startX) / cellW;
            int r = (e.Y - startY) / cellH;

            if (r >= 0 && r < rows && c >= 0 && c < cols)
            {
                if (maze[r, c] == 1) maze[r, c] = 0;
                else if (maze[r, c] == 0) maze[r, c] = 1;
                this.Invalidate();
            }
        }

        void LoadAssets()
        {
            background = new cimgactor();
            try
            {
                background.img = new Bitmap("back.jpg");
                background.rdst = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            }
            catch { background.img = null; }

            rat = new cimgactor();
            rat.fallbackColor = Color.Red;
            try
            {
                rat.img = new Bitmap("ratt.jpg");
                rat.img.MakeTransparent(rat.img.GetPixel(0, 0));
            }
            catch { rat.img = null; }

            updateRatPosition();
        }

        void updateRatPosition()
        {
            if (rat == null) return;
            rat.rdst = new Rectangle(
                startX + (ratCol * cellW),
                startY + (ratRow * cellH),
                cellW,
                cellH
            );
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (off == null || off.Width != this.ClientSize.Width || off.Height != this.ClientSize.Height)
            {
                off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            }

            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            e.Graphics.DrawImage(off, 0, 0);
        }

        void DrawScene(Graphics g)
        {
            g.Clear(Color.White);

            if (background != null && background.img != null)
                g.DrawImage(background.img, 0, 0, this.Width, this.Height);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    int x = startX + (c * cellW);
                    int y = startY + (r * cellH);

                    // 1 = WALL (Black)
                    if (maze[r, c] == 1)
                    {
                        g.FillRectangle(Brushes.Black, x, y, cellW, cellH);
                        g.DrawRectangle(Pens.Gray, x, y, cellW, cellH);
                    }
                    // 0 = EMPTY (White)
                    else if (maze[r, c] == 0)
                    {
                        g.FillRectangle(Brushes.White, x, y, cellW, cellH);
                        g.DrawRectangle(Pens.Gray, x, y, cellW, cellH);
                    }
                    // 2 = ACTIVE PATH (Orange)
                    else if (maze[r, c] == 2)
                    {
                        g.FillRectangle(Brushes.Orange, x, y, cellW, cellH);
                        g.DrawRectangle(Pens.Gray, x, y, cellW, cellH);
                    }
                    // 3 = DEAD END
                    else if (maze[r, c] == 3)
                    {
                        g.FillRectangle(Brushes.White, x, y, cellW, cellH);
                        g.DrawRectangle(Pens.Gray, x, y, cellW, cellH);
                    }
                }
            }

            if (rat != null)
            {
                if (rat.img != null)
                    g.DrawImage(rat.img, rat.rdst);
                else
                    g.FillEllipse(new SolidBrush(rat.fallbackColor), rat.rdst);
            }
        }
    }
}