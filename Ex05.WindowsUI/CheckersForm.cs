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
          private Square m_SourceSquare = null;

          public CheckersForm()
          {
               this.Text = "Checkers";
               this.StartPosition = FormStartPosition.CenterScreen;
               this.BackColor = System.Drawing.Color.FloralWhite;
               this.AutoSize = true;
               m_GameSettingsForm = new GameSettingsForm();
               m_GameSettingsForm.ShowDialog();
               m_Game = new CheckersGame(m_GameSettingsForm.Player1Name, m_GameSettingsForm.Player2Name, m_GameSettingsForm.BoardSize, m_GameSettingsForm.GameMode);
               initControls();
               assignMenToButtons();
               updateSourceButtonsAvailability();
          }

          private void initControls()
          {
               Player1Label.Location = new System.Drawing.Point(80, 6);
               Player1Label.Text = m_Game.ActiveTeam.Name + ": " + m_Game.ActiveTeam.Score.ToString();
               this.Controls.Add(this.Player1Label);

               Player2Label.Location = new System.Drawing.Point(200, 6);
               Player2Label.Text = m_Game.InactiveTeam.Name + ": " + m_Game.InactiveTeam.Score.ToString();
               this.Controls.Add(this.Player2Label);

               SquareButtons = new BoardButton[m_Game.Board.BoardSize, m_Game.Board.BoardSize];
               addSquareButtons(m_Game.Board.BoardSize);
          }

          private void updateScore()
          {
               Player1Label.Text = m_Game.ActiveTeam.Name + ": " + m_Game.ActiveTeam.Score.ToString();
               Player2Label.Text = m_Game.InactiveTeam.Name + ": " + m_Game.InactiveTeam.Score.ToString();
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
          }

          private void assignMenToButtons()
          {
               foreach (BoardButton button in SquareButtons)
               {
                    if (button.Active)
                    {
                         resetActiveButton(button);
                    }
               }

               foreach (Man man in m_Game.ActiveTeam.ArmyOfMen)
               {
                    SquareButtons[man.CurrentPosition.Position.y, man.CurrentPosition.Position.x].AddManToButton(man);
               }

               foreach (Man man in m_Game.InactiveTeam.ArmyOfMen)
               {
                    SquareButtons[man.CurrentPosition.Position.y, man.CurrentPosition.Position.x].AddManToButton(man);
               }
          }

          private void resetActiveButton(BoardButton i_BoardButton)
          {
               i_BoardButton.Text = null;
               i_BoardButton.Enabled = false;
               i_BoardButton.BackColor = System.Drawing.Color.NavajoWhite;
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
               if (m_SourceSquare == null)
               {
                    chooseDestinationSquare(button);
               }
               else if (m_SourceSquare.Position.Equals(SquareButtons[button.Position.y, button.Position.x].Position))
               {
                    endUserTurn(button);
               }
               else
               {
                    Move requestedMove = moveCreation(m_SourceSquare, m_Game.Board.GetSquare(button.Position.y, button.Position.x));
                    m_Game.MakeAMoveProcess(requestedMove);
                    if (m_Game.IsProgressiveMoveAvailable(requestedMove))
                    {
                         handleProgressiveMove(button);
                    }

                    else
                    {
                         if (m_Game.IsEndOfRound())
                         {
                              handleEndOfRound();
                         }
                         else
                         {
                              m_Game.SwapActiveTeam();
                              endUserTurn(button);
                         }
                    } 
               }
          }


          private void handleProgressiveMove(BoardButton i_BoardButton)
          {
               i_BoardButton.BackColor = System.Drawing.Color.NavajoWhite;
               SquareButtons[m_SourceSquare.Position.y, m_SourceSquare.Position.x].BackColor = System.Drawing.Color.NavajoWhite;
               m_SourceSquare = null;
               assignMenToButtons();
               updateSourceButtonsAvailability();
               chooseDestinationSquare(i_BoardButton);
               i_BoardButton.BackColor = System.Drawing.Color.Blue;
               i_BoardButton.Enabled = false;
          }

          private void chooseDestinationSquare(BoardButton i_BoardButton)
          {
               m_SourceSquare = m_Game.Board.GetSquare(i_BoardButton.Position.y, i_BoardButton.Position.x);
               i_BoardButton.BackColor = System.Drawing.Color.LightBlue;
               updateDestinationButtonsAvailability(i_BoardButton);
          }

          private void endUserTurn(BoardButton i_BoardButton)
          {
               i_BoardButton.BackColor = System.Drawing.Color.NavajoWhite;
               SquareButtons[m_SourceSquare.Position.y, m_SourceSquare.Position.x].BackColor = System.Drawing.Color.NavajoWhite;
               m_SourceSquare = null;
               assignMenToButtons();
               updateSourceButtonsAvailability();
               if (m_Game.ActiveTeam.Type == Team.eTeamType.Computer)
               {
                    makeComputerMove();
               }
          }

          private void endComputerTurn()
          {
               m_Game.SwapActiveTeam();
               assignMenToButtons();
               updateSourceButtonsAvailability();
          }

          private void makeComputerMove()
          {
               Move requestedMove = m_Game.GenerateMoveRequest();
               m_Game.MakeAMoveProcess(requestedMove);
               assignMenToButtons();
               updateSourceButtonsAvailability();
               while (m_Game.IsProgressiveMoveAvailable(requestedMove))
               {
                    m_Game.GenerateProgressiveAttack(ref requestedMove);
                    m_Game.MakeAMoveProcess(requestedMove);
                    assignMenToButtons();
                    updateSourceButtonsAvailability();
               }
               if (m_Game.IsEndOfRound())
               {
                    handleEndOfRound();

               }
               else
               {
                    endComputerTurn();
               }
          }

          private void handleEndOfRound()
          {
               string endOfRoundMessage;
               if (m_Game.Status == CheckersGame.eGameStatus.RoundEndWithDraw)
               {
                    endOfRoundMessage = string.Format(@"Tie!{1}Another Round?", m_Game.Status.ToString(), Environment.NewLine);
               }
               else
               {
                    endOfRoundMessage = string.Format(@"{0} Won!{1}Another Round?", m_Game.ActiveTeam.Name, Environment.NewLine);
               }

               DialogResult dialogResult = MessageBox.Show(endOfRoundMessage , "Checkers", MessageBoxButtons.YesNo);
               if (dialogResult == DialogResult.Yes)
               {
                    handleNewRoundRequest();
               }
               else
               {
                    this.Close();
               }
          }

          private void handleNewRoundRequest()
          {
               m_Game.CreateNewRound();
               assignMenToButtons();
               updateSourceButtonsAvailability();
               m_SourceSquare = null;
               updateScore();
          }

          private Move moveCreation(Square i_SourceSquare, Square i_DestinationSquare)
          {
               Move requestedMove = new Move();
               foreach (Move attackMove in m_Game.ActiveTeam.AttackMoves)
               {
                    if (i_SourceSquare.Position.Equals(attackMove.SourceSquare.Position) &&
                         i_DestinationSquare.Position.Equals(attackMove.DestinationSquare.Position))
                    {
                         requestedMove = attackMove;
                         break;
                    }
               }

               foreach (Move regularMove in m_Game.ActiveTeam.RegularMoves)
               {
                    if (i_SourceSquare.Position.Equals(regularMove.SourceSquare.Position) &&
                         i_DestinationSquare.Position.Equals(regularMove.DestinationSquare.Position))
                    {
                         requestedMove = regularMove;
                         break;
                    }
               }

               return requestedMove;
          }
     }



     public class BoardButton : Button
     {
          Square.SquarePosition m_Position = new Square.SquarePosition();
          bool m_IsActive = false;

          public BoardButton(int i_Row, int i_Col)
          {
               m_Position.y = i_Row;
               m_Position.x = i_Col;
               if ((m_Position.y + m_Position.x) % 2 == 0)
               {
                    this.BackColor = System.Drawing.Color.Peru;
               }
               else
               {
                    this.BackColor = System.Drawing.Color.NavajoWhite;
                    m_IsActive = true;
               }
          }

          public bool Active
          {
               get { return m_IsActive; }
          }

          public int Row
          {
               get { return m_Position.y; }
          }

          public int Col
          {
               get { return m_Position.x; }
          }

          public Square.SquarePosition Position
          {
               get { return m_Position; }
          }

          public void AddManToButton(Man i_Man)
          {
               this.Text = i_Man.Sign.ToString();
          }
     }
}
