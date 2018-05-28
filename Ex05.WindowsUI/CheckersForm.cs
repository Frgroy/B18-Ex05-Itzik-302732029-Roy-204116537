using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace Ex05.WindowsUI
{
     public class CheckersForm : Form
     {
          private BoardForm m_Board;
          private Label m_Player1Label;
          private Label m_Player2Label;

          public CheckersForm(int i_BoardSize, string i_Player1Name, string i_Player2Name)
          {
               this.Text = "Checkers";
               this.StartPosition = FormStartPosition.CenterScreen;
               this.Size = new System.Drawing.Size(540, 640);
               this.m_Player1Label = new Label();
               this.m_Player2Label = new Label();
               initControls(i_BoardSize, i_Player1Name, i_Player2Name);
               m_Board.ShowDialog();
          }

          private void initControls(int i_BoardSize, string i_Player1Name, string i_Player2Name)
          {
               this.m_Player1Label.Location = new System.Drawing.Point(80, 24);
               this.m_Player1Label.Text = i_Player1Name + ": " + 0.ToString();

               this.m_Player2Label.Location = new System.Drawing.Point(200, 24);
               this.m_Player2Label.Text = i_Player2Name + ": " + 0.ToString();

               this.m_Board = new BoardForm(i_BoardSize);
               this.m_Board.Top = 60;
               this.m_Board.Size = new System.Drawing.Size(54 * i_BoardSize, 54 * i_BoardSize);

               this.Controls.Add(this.m_Player2Label);
               this.Controls.Add(this.m_Player1Label);
          }

          private void label1_Click(object sender, EventArgs e)
          {

          }

          private void Damka_Load(object sender, EventArgs e)
          {

          }

          private void label1_Click_1(object sender, EventArgs e)
          {

          }

          private void m_lblPlayer2Score_Click(object sender, EventArgs e)
          {

          }

     }

     public class BoardButton : Button
     {
          public int m_Row { get; set; }
          public int m_Col { get; set; }
     }

     public class BoardForm : Form
     {
          BoardButton[,] m_SquareButtons;
          public BoardForm (int i_BoardSize)
          {
               m_SquareButtons = new BoardButton[i_BoardSize, i_BoardSize];
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
               BoardButton button = new BoardButton();
               button.Left = i_ColPosition * 54;
               button.Top = i_RowPosition * 54;
               button.m_Row = i_RowPosition;
               button.m_Col = i_ColPosition;
               button.Size = new System.Drawing.Size(54, 54);
               // this.m_GameBoard[row][col].Click += new System.EventHandler(this.label1_Click);
               this.Controls.Add(button);
               m_SquareButtons[i_RowPosition, i_ColPosition] = button;
          }

     }
}
