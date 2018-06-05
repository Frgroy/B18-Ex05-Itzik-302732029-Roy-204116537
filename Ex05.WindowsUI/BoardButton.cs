using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using B18_Ex02;
using System.Drawing;

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
               this.BackgroundImageLayout = ImageLayout.Stretch;
               if ((m_Position.y + m_Position.x) % 2 == 0)
               {
                    this.BackColor = System.Drawing.Color.LightSlateGray;
                    this.Enabled = false;
               }
               else
               {
                    this.BackColor = System.Drawing.Color.LightGoldenrodYellow;
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
               Image manImage;

               switch (i_Man.Sign)
               {
                    case 'O':
                         manImage = Image.FromFile(@"C:\Users\user\Documents\Visual Studio 2015\Projects\B18 Ex05 Itzik 302732029 Roy 204116537\whiteMan.png");
                         this.BackgroundImage = manImage;
                         break;
                    case 'X':
                         manImage = Image.FromFile(@"C:\Users\user\Documents\Visual Studio 2015\Projects\B18 Ex05 Itzik 302732029 Roy 204116537\blackMan.png");
                         this.BackgroundImage = manImage;
                         break;
                    case 'K':
                         manImage = Image.FromFile(@"C:\Users\user\Documents\Visual Studio 2015\Projects\B18 Ex05 Itzik 302732029 Roy 204116537\blackKing.png");
                         this.BackgroundImage = manImage;
                         break;
                    case 'U':
                         manImage = Image.FromFile(@"C:\Users\user\Documents\Visual Studio 2015\Projects\B18 Ex05 Itzik 302732029 Roy 204116537\whiteKing.png");
                         this.BackgroundImage = manImage;
                         break;
               }

          }
     }
}