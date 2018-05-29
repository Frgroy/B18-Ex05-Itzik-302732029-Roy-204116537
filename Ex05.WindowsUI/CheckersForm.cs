using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using B18_Ex02;

namespace Ex05.WindowsUI
{
     public class CheckersForm : Form
     {
          private Label Player1Label = new Label();
          private Label Player2Label = new Label();
          private BoardButton[,] SquareButtons;
          private CheckersGame m_Game;
          private GameSettingsForm m_GameSettingsForm;
          private Move m_RequestedMove = new Move();

          public CheckersForm()
          {
               this.Text = "Checkers";
               this.StartPosition = FormStartPosition.CenterScreen;
               this.AutoSize = true;
               m_GameSettingsForm = new GameSettingsForm();
               m_GameSettingsForm.ShowDialog();
               initControls(m_GameSettingsForm.BoardSize, m_GameSettingsForm.Player1Name, m_GameSettingsForm.Player2Name);
               m_Game = new CheckersGame(m_GameSettingsForm.Player1Name, m_GameSettingsForm.Player2Name, m_GameSettingsForm.BoardSize, m_GameSettingsForm.GameMode);
               assignMenToButtons();
               updateSourceButtonsAvailability();
          }

          private void initControls(int i_BoardSize, string i_Player1Name, string i_Player2Name)
          {
               Player1Label.Location = new System.Drawing.Point(80, 6);
               Player1Label.Text = i_Player1Name + ": " + 0.ToString();
               this.Controls.Add(this.Player1Label);

               Player2Label.Location = new System.Drawing.Point(200, 6);
               Player2Label.Text = i_Player2Name + ": " + 0.ToString();
               this.Controls.Add(this.Player2Label);

               SquareButtons = new BoardButton[i_BoardSize, i_BoardSize];
               addSquareButtons(i_BoardSize);
          }

          private void addSquareButtons(int i_BoardSize)
          {
               for (int i = 0; i < i_BoardSize; i++)
               {
                    for (int j = 0; j < i_BoardSize; j++)
                    {
                         addSquare(i, j);
                    }
               }
          }

          private void addSquare(int i_RowPosition, int i_ColPosition)
          {
               BoardButton button = new BoardButton(i_RowPosition, i_ColPosition);
               button.Left = 6 + i_ColPosition * 54;
               button.Top = 32 + i_RowPosition * 54;
               button.Size = new System.Drawing.Size(54, 54);
               button.Click += new System.EventHandler(BoardButton_Click);
               button.Enabled = false;
               this.Controls.Add(button);
               SquareButtons[i_RowPosition, i_ColPosition] = button;
               if ((i_RowPosition + i_ColPosition) % 2 == 0)
               {
                    button.BackColor = System.Drawing.Color.Black;
               }
               else
               {
                    button.BackColor = System.Drawing.Color.White;
               }
          }

          private void assignMenToButtons()
          {
               foreach (Man man in m_Game.ActiveTeam.ArmyOfMen)
               {
                    SquareButtons[man.CurrentPosition.Position.y, man.CurrentPosition.Position.x].AddManToButton(man);
               }

               foreach (Man man in m_Game.InactiveTeam.ArmyOfMen)
               {
                    SquareButtons[man.CurrentPosition.Position.y, man.CurrentPosition.Position.x].AddManToButton(man);
               }
          }

          private void updateSourceButtonsAvailability()
          {
               foreach (Move attackMove in m_Game.ActiveTeam.AttackMoves)
               {
                    SquareButtons[attackMove.SourceSquare.Position.y, attackMove.SourceSquare.Position.x].Enabled = true;
               }
               
               if (m_Game.ActiveTeam.AttackMoves.Count == 0)
               {
                    foreach (Move regularMove in m_Game.ActiveTeam.RegularMoves)
                    {
                         SquareButtons[regularMove.SourceSquare.Position.y, regularMove.SourceSquare.Position.x].Enabled = true;
                    }
               }
          }

          private void updateDestinationButtonsAvailability(BoardButton i_BoardButton)
          {
               foreach (BoardButton button in SquareButtons)
               {
                    button.Enabled = false;
               }

               SquareButtons[i_BoardButton.Position.y, i_BoardButton.Position.x].Enabled = true;

               foreach (Move attackMove in m_Game.ActiveTeam.AttackMoves)
               {
                    if (attackMove.SourceSquare.Position.Equals(i_BoardButton.Position))
                    {
                         SquareButtons[attackMove.DestinationSquare.Position.y, attackMove.DestinationSquare.Position.x].Enabled = true;
                    }
               }

               if (m_Game.ActiveTeam.AttackMoves.Count == 0)
               {
                    foreach (Move regularMove in m_Game.ActiveTeam.RegularMoves)
                    {
                         if (regularMove.SourceSquare.Position.Equals(i_BoardButton.Position))
                         {
                              SquareButtons[regularMove.DestinationSquare.Position.y, regularMove.DestinationSquare.Position.x].Enabled = true;
                         }
                    }
               }
          }

          private void BoardButton_Click(object sender, EventArgs e)
          {
               BoardButton button = sender as BoardButton;
               button.BackColor = System.Drawing.Color.LightBlue;
               m_RequestedMove.SourceSquare = m_Game.Board.GetSquare(button.Position.y, button.Position.x);
               updateDestinationButtonsAvailability(button);
               button.Click -= new System.EventHandler(BoardButton_Click);
               button.Click += new System.EventHandler(BoardButton_SecondClick);
          }

          private void BoardButton_SecondClick(object sender, EventArgs e)
          {
               BoardButton button = sender as BoardButton;
               if (button.BackColor == System.Drawing.Color.LightBlue)
               {
                    button.BackColor = System.Drawing.Color.White;
                    m_RequestedMove.SourceSquare = null;
                    button.Click -= new System.EventHandler(BoardButton_SecondClick);
                    button.Click += new System.EventHandler(BoardButton_Click);
               }
               else
               {
                    m_RequestedMove.DestinationSquare = m_Game.Board.GetSquare(button.Position.y, button.Position.x);
                    m_Game.MakeAMoveProcess(m_RequestedMove);
                    m_Game.SwapActiveTeam();
                    assignMenToButtons();
                    updateSourceButtonsAvailability();
                    button.Click -= new System.EventHandler(BoardButton_SecondClick);
                    button.Click += new System.EventHandler(BoardButton_Click);
               }
          }


          private void Damka_Load(object sender, EventArgs e)
          {

          }
     }



     public class BoardButton : Button
     {
          Square.SquarePosition m_Position = new Square.SquarePosition();

          public BoardButton(int i_Row, int i_Col)
          {
               m_Position.y = i_Row;
               m_Position.x = i_Col;
          }

          public int Row
          {
               get { return m_Position.y; }
          }

          public int Col
          {
               get
               { return m_Position.x; }
          }

          public Square.SquarePosition Position
          {
               get { return m_Position; }
          }

          public void AddManToButton (Man i_Man)
          {
               this.Text = i_Man.Sign.ToString();
          }
     }
}
