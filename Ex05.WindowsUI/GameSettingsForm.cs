using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using B18_Ex02;

namespace Ex05.WindowsUI
{
     public class GameSettingsForm : Form
     {
          private const int k_SmallBoardSize = 6;
          private const int k_MediumBoardSize = 8;
          private const int k_BigBoardSize = 10;
          private Label m_BoardSizeLabel = new Label();
          private RadioButton m_6x6Button = new RadioButton();
          private RadioButton m_8x8Button = new RadioButton();
          private RadioButton m_10x10Button = new RadioButton();
          private Label m_PlayersLabel = new Label();
          private Label m_Player1Label = new Label();
          private Label m_Player2Label = new Label();
          private TextBox m_Player1TextBox = new TextBox();
          private TextBox m_Player2TextBox = new TextBox();
          private CheckBox m_AgainstUserModeCheckBox = new CheckBox();
          private Button m_DoneButton = new Button();

          public int BoardSize
          {
               get
               {
                    int boardSize = 0;
                    if (m_6x6Button.Checked)
                    {
                         boardSize = k_SmallBoardSize;
                    }

                    else if (m_8x8Button.Checked)
                    {
                         boardSize = k_MediumBoardSize;
                    }

                    else if (m_10x10Button.Checked)
                    {
                         boardSize = k_BigBoardSize;
                    }

                    return boardSize;
               }
          }

          public string Player1Name
          {
               get { return m_Player1TextBox.Text; }
          }

          public string Player2Name
          {
               get { return m_Player2TextBox.Text; }
          }

          public CheckersGame.eGameMode GameMode
          {
               get { return m_AgainstUserModeCheckBox.Checked ? CheckersGame.eGameMode.VersusAnotherPlayer : CheckersGame.eGameMode.VersusComputer; }
          }

          public GameSettingsForm()
          {
               this.Name = "GameSettings";
               this.Text = "Game Settings";
               this.StartPosition = FormStartPosition.CenterScreen;
               initControls();
          }

          private void initControls()
          {
               m_BoardSizeLabel.Location = new System.Drawing.Point(12, 10);
               m_BoardSizeLabel.Name = "m_BoardSizeLabel";
               m_BoardSizeLabel.Size = new System.Drawing.Size(80, 14);
               m_BoardSizeLabel.Text = "Board Size: ";

               m_6x6Button.Location = new System.Drawing.Point(25, 25);
               m_6x6Button.Name = "m_6x6Button";
               m_6x6Button.Size = new System.Drawing.Size(48, 17);
               m_6x6Button.TabStop = true;
               m_6x6Button.Text = "6 x 6";
               m_6x6Button.UseVisualStyleBackColor = true;
               m_6x6Button.Checked = true;

               m_8x8Button.Location = new System.Drawing.Point(80, 25);
               m_8x8Button.Name = "m_8x8Button";
               m_8x8Button.Size = new System.Drawing.Size(48, 17);
               m_8x8Button.TabStop = true;
               m_8x8Button.Text = "8 x 8";
               m_8x8Button.UseVisualStyleBackColor = true;

               m_10x10Button.Location = new System.Drawing.Point(135, 25);
               m_10x10Button.Name = "m_10x10Button";
               m_10x10Button.Size = new System.Drawing.Size(60, 17);
               m_10x10Button.TabStop = true;
               m_10x10Button.Text = "10 x 10";
               m_10x10Button.UseVisualStyleBackColor = true;

               m_PlayersLabel.Location = new System.Drawing.Point(12, 45);
               m_PlayersLabel.Name = "m_PlayersLabel";
               m_PlayersLabel.Size = new System.Drawing.Size(50, 14);
               m_PlayersLabel.Text = "Players: ";

               m_Player1Label.Location = new System.Drawing.Point(22, 68);
               m_Player1Label.Name = "m_Player1Label";
               m_Player1Label.Size = new System.Drawing.Size(50, 14);
               m_Player1Label.Text = "Player 1: ";

               m_Player1TextBox.Location = new System.Drawing.Point(109, 65);
               m_Player1TextBox.Name = "m_Player1TextBox";
               m_Player1TextBox.Size = new System.Drawing.Size(117, 20);

               m_AgainstUserModeCheckBox.Location = new System.Drawing.Point(22, 97);
               m_AgainstUserModeCheckBox.Name = "m_AgainstUserModeCheckBox";
               m_AgainstUserModeCheckBox.Size = new System.Drawing.Size(15, 14);
               m_AgainstUserModeCheckBox.UseVisualStyleBackColor = true;
               m_AgainstUserModeCheckBox.Click += new EventHandler(AgainstUserModeCheckBox_CheckedChanged);

               m_Player2Label.Location = new System.Drawing.Point(42, 97);
               m_Player2Label.Name = "m_Player2Label";
               m_Player2Label.Size = new System.Drawing.Size(50, 14);
               m_Player2Label.Text = "Player 2: ";

               m_Player2TextBox.Enabled = false;
               m_Player2TextBox.Location = new System.Drawing.Point(109, 94);
               m_Player2TextBox.Name = "m_Player2TextBox";
               m_Player2TextBox.Size = new System.Drawing.Size(117, 20);
               m_Player2TextBox.Text = "[Computer]";
               m_DoneButton.Location = new System.Drawing.Point(180, 130);
               m_DoneButton.Name = "m_DoneButton";
               m_DoneButton.Size = new System.Drawing.Size(75, 23);
               m_DoneButton.TabIndex = 10;
               m_DoneButton.Text = "Done";
               m_DoneButton.UseVisualStyleBackColor = true;
               m_DoneButton.Click += new System.EventHandler(DoneButton_Click);

               this.ClientSize = new System.Drawing.Size(284, 176);
               this.Controls.Add(this.m_DoneButton);
               this.Controls.Add(this.m_AgainstUserModeCheckBox);
               this.Controls.Add(this.m_Player2TextBox);
               this.Controls.Add(this.m_Player1TextBox);
               this.Controls.Add(this.m_Player2Label);
               this.Controls.Add(this.m_Player1Label);
               this.Controls.Add(this.m_PlayersLabel);
               this.Controls.Add(this.m_10x10Button);
               this.Controls.Add(this.m_8x8Button);
               this.Controls.Add(this.m_6x6Button);
               this.Controls.Add(this.m_BoardSizeLabel);
          }

          public void AgainstUserModeCheckBox_CheckedChanged(object sender, EventArgs e)
          {
               m_Player2TextBox.Enabled = !m_Player2TextBox.Enabled;
          }

          public void DoneButton_Click(object sender, EventArgs e)
          {
               if (isFormFulfilled())
               {
                    this.Close();
               }
               else
               {
                    MessageBox.Show("Invalid input.");
               }
          }

          private bool isFormFulfilled()
          {
               return (m_Player1TextBox.Text != string.Empty && m_Player2TextBox.Text != string.Empty) ? true : false;
          }
     }
}
