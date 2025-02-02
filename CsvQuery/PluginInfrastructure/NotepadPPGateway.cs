﻿// NPP plugin platform for .Net v0.93.96 by Kasper B. Graversen etc.
using System;
using System.Text;
using NppPluginNET.PluginInfrastructure;

namespace CsvQuery.PluginInfrastructure
{
	public interface INotepadPPGateway
	{
		void FileNew();

		string GetCurrentFilePath();
		unsafe string GetFilePath(IntPtr bufferId);
		void SetCurrentLanguage(LangType language);
	}

	/// <summary>
	/// This class holds helpers for sending messages defined in the Msgs_h.cs file. It is at the moment
	/// incomplete. Please help fill in the blanks.
	/// </summary>
	public class NotepadPPGateway : INotepadPPGateway
	{
		private const int Unused = 0;

		public void FileNew()
		{
			Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_MENUCOMMAND, Unused, NppMenuCmd.IDM_FILE_NEW);
		}

		/// <summary>
		/// Gets the path of the current document.
		/// </summary>
		public string GetCurrentFilePath()
		{
			var path = new StringBuilder(2000);
			Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_GETFULLCURRENTPATH, 0, path);
			return path.ToString();
		}

		/// <summary>
		/// Gets the path of the current document.
		/// </summary>
		public unsafe string GetFilePath(IntPtr bufferId)
		{
			var path = new StringBuilder(2000);
			Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_GETFULLPATHFROMBUFFERID, bufferId, path);
			return path.ToString();
		}

		public void SetCurrentLanguage(LangType language)
		{
			Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_SETCURRENTLANGTYPE, Unused, (int) language);
		}

        /// <summary>
        /// Gets the ID of the current tab in Notepad++
        /// </summary>
        /// <returns></returns>
	    public static IntPtr GetCurrentBufferId()
        {
            return Win32.SendMessage(PluginBase.nppData._nppHandle, (uint)NppMsg.NPPM_GETCURRENTBUFFERID, Unused, Unused);
        }

		public static int GetNppMajorVersion()
        {
			var version = Win32.SendMessage(PluginBase.nppData._nppHandle, (uint)NppMsg.NPPM_GETNPPVERSION, Unused, Unused).ToInt32();
			return version >> 16;
		}

	    public void OpenFileInNpp(string filename)
	    {
            // NPPM_DOOPEN
	        Win32.SendMessage(PluginBase.nppData._nppHandle, (uint)NppMsg.NPPM_DOOPEN, Unused, filename);
        }
    }

	/// <summary>
	/// This class holds helpers for sending messages defined in the Resource_h.cs file. It is at the moment
	/// incomplete. Please help fill in the blanks.
	/// </summary>
	class NppResource
	{
		private const int Unused = 0;

		public void ClearIndicator()
		{
			Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) Resource.NPPM_INTERNAL_CLEARINDICATOR, Unused, Unused);
		}
	}
}
