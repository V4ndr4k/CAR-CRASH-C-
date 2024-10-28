﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAR_CRASH
{
    public partial class Form1 : Form
    {
        int roadSpeed;
        int trafficSpeed;
        int playerSpeed = 12;
        int score;
        int carImage;

        Random rand = new Random();
        Random carPosition = new Random();
        bool isGameOver = false;
        bool goleft, goright;

        private SoundPlayer backgroundMusicPlayer;
        private System.Windows.Forms.MenuStrip menuStrip;



        public Form1()
        {
            InitializeComponent();
            InitializeGame();
            InitializeBackgroundMusic();
            PlayBackgroundMusicLoop();

            InitializeMenuStrips();


            InitializeCarMovement();


            this.KeyDown += keyisdown;
            this.KeyUp += keyisup;


        }

        private void InitializeGame()
        {
            ResetGame();
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;
            score++;

            if (goleft == true && player.Left > 10)
            {
                player.Left -= playerSpeed;
            }
            if (goright == true && player.Left < 370)
            {
                player.Left += playerSpeed;
            }

            roadGrey1.Top += roadSpeed;
            roadGrey2.Top += roadSpeed;

            if (roadGrey2.Top > 520)
            {
                roadGrey2.Top = -520;
            }
            if (roadGrey1.Top > 520)
            {
                roadGrey1.Top = -520;
            }

            AI1.Top += trafficSpeed;
            AI2.Top += trafficSpeed;

            if (AI1.Top > 530)
            {
                changeAIcars(AI1);
            }

            if (AI2.Top > 530)
            {
                changeAIcars(AI2);
            }

            if (player.Bounds.IntersectsWith(AI1.Bounds) || player.Bounds.IntersectsWith(AI2.Bounds))
            {
                gameOver();
            }

            if (score > 40 && score < 500)
            {
                award.Image = Properties.Resources.game_over;
            }

            if (score > 500 && score < 2000)
            {
                award.Image = Properties.Resources.game_over;
                roadSpeed = 21;
                trafficSpeed = 24;
            }

            if (score > 4000)
            {
                award.Image = Properties.Resources.gold;
                trafficSpeed = 24;
                roadSpeed = 27;
            }
        }

        private void changeAIcars(PictureBox tempCar)
        {
            carImage = rand.Next(1, 8);

            switch (carImage)
            {
                case 1:
                    tempCar.Image = Properties.Resources.Truckgreen;
                    break;
                case 2:
                    tempCar.Image = Properties.Resources.carorange;
                    break;
                case 3:
                    tempCar.Image = Properties.Resources.Truckred;
                    break;
                case 4:
                    tempCar.Image = Properties.Resources.carGrey;
                    break;
                case 5:
                    tempCar.Image = Properties.Resources.CarRed;
                    break;
                case 6:
                    tempCar.Image = Properties.Resources.Cardiff;
                    break;
                case 7:
                    tempCar.Image = Properties.Resources.Truckgrey;
                    break;
                case 8:
                    tempCar.Image = Properties.Resources.carblue;
                    break;


            }

            tempCar.Top = carPosition.Next(100, 400) * -1;

            if ((string)tempCar.Tag == "carLeft")
            {
                tempCar.Left = carPosition.Next(5, 200);
            }
            if ((string)tempCar.Tag == "carRight")
            {
                tempCar.Left = carPosition.Next(235, 400);
            }
        }

        private void gameOver()
        {
            playSound();
            gameTimer.Stop();
            explosion.Visible = true;
            player.Controls.Add(explosion);
            explosion.Location = new Point(-8, 5);
            explosion.BringToFront();
            explosion.BackColor = Color.Transparent;

            award.Visible = true;
            award.BringToFront();

            btnStart.Enabled = true;
        }

        private void ResetGame()
        {
            btnStart.Enabled = false;
            explosion.Visible = false;
            award.Visible = false;
            goleft = false;
            goright = false;
            score = 0;
            award.Image = Properties.Resources.game_over;

            roadSpeed = 12;
            trafficSpeed = 15;

            AI1.Top = carPosition.Next(200, 500) * -1;
            AI1.Left = carPosition.Next(5, 200);

            AI2.Top = carPosition.Next(200, 500) * -1;
            AI2.Left = carPosition.Next(245, 400);

            gameTimer.Start();
        }

        private void restartGame(object sender, EventArgs e)
        {
            InitializeGame();
            PlayBackgroundMusicLoop();
            InitializeCarMovement();
        }

        private void playSound()
        {
            System.Media.SoundPlayer playCrash = new System.Media.SoundPlayer(Properties.Resources.hit);
            playCrash.Play();
        }


        private void PlayBackgroundMusicLoop()
        {
            backgroundMusicPlayer.PlayLooping();
        }

        private void InitializeBackgroundMusic()
        {
            backgroundMusicPlayer = new SoundPlayer(Properties.Resources.backgroundMusic);
        }

        private void InitializeCarMovement()
        {
            goleft = false;
            goright = false;
        }

        private void InitializeMenuStrips()
        {
            Controls.Add(menuStrip1);
            Controls.Add(menuStrip2);
        }

        private void OHreMenuItem_Click(object sender, EventArgs e)
        {
           
            string textOHre = "\r\nVítejte v CAR CRASH, světě rychlosti a dovedností! Ovládání je jednoduché – používejte šipky doleva/doprava, vyhýbejte se srážkám s AI-vozy. Získejte co nejvyšší skóre na nekonečné silnici. " +
                "Připravte se na adrenalinovou jízdu, kde každá sekunda a rychlá reakce rozhodují. Posaďte se za volant a zkuste překonat všechny výzvy!";
            MessageBox.Show(textOHre, "O HŘE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void award_Click(object sender, EventArgs e)
        {

        }

        private void OAutorceMenuItem_Click(object sender, EventArgs e)
        {
           
            string textOAutorce = "CAR CRASH je hra vyvinutá jako závěrečná práce pro předmět NUR studentkou L.K. v roce 2024."; 
            MessageBox.Show(textOAutorce, "O AUTORCE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



    }


}

