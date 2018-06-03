using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using B18_Ex02;

namespace Ex05.WindowsUI
{
     public class BoardButton : Button
     {
          private Square.SquarePosition m_Position = new Square.SquarePosition();
          private bool m_IsActive = false;

          public BoardButton(int i_Row, int i_Col)
          {
               m_Position.y = i_Row;
               m_Position.x = i_Col;
               if ((m_Position.y + m_Position.x) % 2 == 0)
               {
                    this.BackColor = System.Drawing.Color.Peru;
                    this.Enabled = false;
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