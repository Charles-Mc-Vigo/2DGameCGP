namespace Simple2D
{
    public partial class Form1 : Form
    {
        //game variables
        bool goRight, goLeft, goJump, hasKey;
        int jumpSpeed = 2;
        int force = 8;
        int score = 0;
        int playerSpeed = 10;
        int backgroundMovement = 12;

        public Form1()
        {
            InitializeComponent();
        }

        //game controls
        private void keyIsDown(object sender, KeyEventArgs e)
        {
            //condition if key is down
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Space && goJump == false)
            {
                goJump = true;
            }
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            //condition if key is up
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (goJump == true)
            {
                goJump = false;
            }
        }

        private void formIsClose(object sender, FormClosedEventArgs e)
        {

        }

        //game mechanics and conditions
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //left and right movements
            if (goRight == true && pbPlayer.Left + (pbPlayer.Width + 30) < this.ClientSize.Width)
            {
                pbPlayer.Left += playerSpeed;
                pbPlayer.Image = Properties.Resources.player;
            }
            if (goLeft == true && pbPlayer.Left > 30)
            {
                pbPlayer.Left -= playerSpeed;
                pbPlayer.Image = Properties.Resources.inverted_player;
            }

            //background movement
            if (goLeft == true && pbBackground.Left < 0)
            {
                pbBackground.Left += backgroundMovement;
                assetsBehaviour("forward");
            }
            if (goRight == true && pbBackground.Left > -1350)
            {
                pbBackground.Left -= backgroundMovement;
                assetsBehaviour("backward");
            }

            //jumping movement
            pbPlayer.Top += jumpSpeed;
            if (goJump == true && pbPlayer.Top > 0)
            {
                jumpSpeed = -12;
                force -= 8;

            }
            else
            {
                jumpSpeed = 12;
            }

            //collision between player and platform
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "platform")
                {
                    if (pbPlayer.Bounds.IntersectsWith(x.Bounds))
                    {
                        force = 8;
                        pbPlayer.Top = x.Top - pbPlayer.Height;
                        jumpSpeed = 0;
                    }
                    x.BringToFront();
                }
                if (x is PictureBox && (string)x.Tag == "coin")
                {
                    if (pbPlayer.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                    {
                        x.Visible = false;
                        score += 1;
                        lbScore.Text = "Score :" + score.ToString();
                    }
                }
            }
            if (pbPlayer.Bounds.IntersectsWith(pbKey.Bounds))
            {
                pbKey.Visible = false;
                hasKey = true;
                pbDoorClose.Image = Properties.Resources.door_open;
            }
            if (pbPlayer.Bounds.IntersectsWith(pbDoorClose.Bounds) && hasKey == true)
            {
                pbDoorClose.Image = Properties.Resources.door_open;
                gameTimer.Stop();
                MessageBox.Show("You Win the Game!"+ Environment.NewLine + "Do you want to play again?");

                Form1 restartGame = new Form1();
                restartGame.Show();
                this.Hide();
            }
        }

        //assets behaviour
        private void assetsBehaviour(string direction)
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "platform")
                {
                    if (direction == "backward")
                    {
                        x.Left -= backgroundMovement;
                    }
                    if (direction == "forward")
                    {
                        x.Left += backgroundMovement;
                    }
                }
                if (x is PictureBox && (string)x.Tag == "coin")
                {
                    if (direction == "backward")
                    {
                        x.Left -= backgroundMovement;
                    }
                    if (direction == "forward")
                    {
                        x.Left += backgroundMovement;
                    }
                }
                if (x is PictureBox && (string)x.Tag == "key")
                {
                    if (direction == "backward")
                    {
                        x.Left -= backgroundMovement;
                    }
                    if (direction == "forward")
                    {
                        x.Left += backgroundMovement;
                    }
                }
                if (x is PictureBox && (string)x.Tag == "doorClose")
                {
                    if (direction == "backward")
                    {
                        x.Left -= backgroundMovement;
                    }
                    if (direction == "forward")
                    {
                        x.Left += backgroundMovement;
                    }
                }
            }
        }

    }
}