using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PunchOut
{
    public partial class Form1 : Form
    {

        bool blockAttack = false; // this is the player block boolean

        // below is the enemy attack lists
        List<string> enemyAttacks = new List<string> { "left", "right", "block" };

        // rnd is the new random class used to generate random numbers
        Random rnd = new Random();

        // below is the enemy speed variable
        int enemySpeed = 5;

        // below int i will be used to check the enemy moves
        int i = 0;

        bool enemyBlocked; // this is the enemy attack block boolean

        int playerHealth = 100; // players total health
        int enemyHealth = 100; // enemys total health

        public Form1()
        {
            InitializeComponent();

            playerLife.ForeColor = Color.Blue; // change the players progress bar to blue
            enemyLife.ForeColor = Color.Red; // change the enemy progress bar to red
        }

        private void enemyMoveEvent(object sender, EventArgs e)
        {
            enemyBoxer.Left += enemySpeed;//move enemy to the left
            if(playerHealth > 1)
            {
                playerLife.Value = Convert.ToInt32(playerHealth);
            }
            if (enemyHealth > 1)
            {
                enemyLife.Value = Convert.ToInt32(enemyHealth);
            }

            if(enemyBoxer.Left > 480)
            {
                enemySpeed = -5;
            }
            if(enemyBoxer.Left < 315)
            {
                enemySpeed = 5;
            }

            if(enemyHealth < 1)
            {
                enemyTimer.Stop();
                enemyMove.Stop();

                MessageBox.Show("You win, click OK to play again.");
                resetGame();
            }
            if (playerHealth < 1)
            {
                enemyTimer.Stop();
                enemyMove.Stop();

                MessageBox.Show("You lose, click OK to retry.");
                resetGame();
            }
        }

        private void enemyPunchEvent(object sender, EventArgs e)
        {
            i = rnd.Next(0, enemyAttacks.Count);

            switch (enemyAttacks[i].ToString())
            {
                // if the attack is left
                case "left":
                    // then we change the enemy to the punch left image
                    enemyBoxer.Image = Properties.Resources.oppleftpunch;

                    // we will also check if the player and enemy colliding and the blocking is set to false

                    if (enemyBoxer.Bounds.IntersectsWith(boxer.Bounds) && !blockAttack)
                    {
                        // if so then we reduce 20 from the players health
                        playerHealth -= 20;
                    }
                    enemyBlocked = false; // set the blocking to false
                    break;

                // if the attack is right
                case "right":
                    // then we change the nenemy to the punch 2 image
                    enemyBoxer.Image = Properties.Resources.opprightupper;

                    // we will also check if the player and enemy colliding and the blocking is set to false
                    if (enemyBoxer.Bounds.IntersectsWith(boxer.Bounds) && !blockAttack)
                    {
                        // if so then we reduce 20 from the players health
                        playerHealth -= 20;
                    }
                    enemyBlocked = false; // set the blocking to false
                    break;

                // if the attack is block
                case "block":
                    // then we change the enemy picture to block
                    enemyBoxer.Image = Properties.Resources.oppblock;
                    // we change the boolean to true
                    enemyBlocked = true;
                    break;
            }
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                // we change the player image to the block image
                boxer.Image = Properties.Resources.block;
                blockAttack = true; // change the block attack boolean to true
            }

            // if the player hits the left key
            if (e.KeyCode == Keys.Left)
            {
                // change the image to left punching picture
                boxer.Image = Properties.Resources.leftpunch;

                // if the player and enemy collide with and the enemy blocked boolean is false
                if (enemyBoxer.Bounds.IntersectsWith(boxer.Bounds) && !enemyBlocked)
                {
                    // the we do 5 damage to the enemy
                    enemyHealth -= 5;
                }
            }

            // if the player hits the right key
            if (e.KeyCode == Keys.Right)
            {
                // change the player image to the righ punching one
                boxer.Image = Properties.Resources.rightpunch;

                // if the player and enemy collide with and the enemy blocked boolean is false
                if (enemyBoxer.Bounds.IntersectsWith(boxer.Bounds) && !enemyBlocked)
                {
                    // then we do 5 damage to the enemy
                    enemyHealth -= 5;
                }
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            boxer.Image = Properties.Resources.stand;
            blockAttack = false;
        }

        private void resetGame()
        {
            enemyTimer.Start(); // start the enemy punch timer
            enemyMove.Start(); // start the enemy move timer

            enemyBoxer.Left = 385; // reset the enemy left to 385
            enemyBoxer.Top = 297; // reset the enemy top to 297

            enemyBoxer.Image = Properties.Resources.oppstand; // show the enemy stand image
            boxer.Image = Properties.Resources.stand; // show the boxer stand image

            playerHealth = 100; // reset player health to 100
            enemyHealth = 100; // reset enemy health to 100
        }
    }
}