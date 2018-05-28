using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05.WindowsUI
{
     public class CheckersWindowsUI
     {
          private int m_BoardSize;
          private string m_Player1Name;
          private string m_Player2Name;
          private bool m_IsAgainstUser;

          public void Run()
          {
               GameSettingsForm gameSettingsForm = new GameSettingsForm();
               gameSettingsForm.ShowDialog();
               getSettings(gameSettingsForm);
               CheckersForm checkersForm = new CheckersForm(m_BoardSize, m_Player1Name, m_Player2Name);
               checkersForm.ShowDialog();
          }

          private void getSettings(GameSettingsForm i_GameSettingsForm)
          {
               m_BoardSize = i_GameSettingsForm.BoardSize;
               m_Player1Name = i_GameSettingsForm.Player1Name;
               m_Player2Name = i_GameSettingsForm.Player2Name;
               m_IsAgainstUser = i_GameSettingsForm.IsAgainstUser;
          }
     }
}
