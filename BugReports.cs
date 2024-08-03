using System;
using System.Collections.Generic;
using System.IO;
using Server;
using Server.Commands;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Misc
{
    public static class BugReports
    {
        //public static string docPath = "D:\\justi\\ServUO-pub57\\Scripts\\Custom Scripts\\Beta Stuff"; //Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string docPath = "Saves/Customs";

        public static List<BugReportEntry> Bugs = new List<BugReportEntry>();

        public static void Initialize()
        {
            CommandSystem.Register("Bug", AccessLevel.Player, new CommandEventHandler(Bug_OnCommand));
        }

        [Usage("Bug")]
        [Description("Opens an interface for allowing players to report bugs.")]
        public static void Bug_OnCommand(CommandEventArgs e)
        {
            if (e.Mobile is PlayerMobile)
            {
                PlayerMobile pm = (PlayerMobile)e.Mobile;
                pm.SendGump(new BugReportGump(pm));
                pm.SendMessage("Would you like to report a bug?");
            }
        }

        public static void WriteBug(string n, string s, string d)
        {
            StreamWriter sw = new StreamWriter(Path.Combine(docPath, "Bugs.txt"), true, encoding: System.Text.Encoding.ASCII);
            using (sw)
            {
                string name = n;
                string subject = s;
                string description = d;


                // time stamp
                int day = DateTime.Now.Day;
                int month = DateTime.Now.Month;
                int year = DateTime.Now.Year;
                int hour = DateTime.Now.Hour;
                int minute = DateTime.Now.Minute;

                // main stuff
                sw.Write("Name: " + name);
                sw.Write("\n");
                sw.Write(string.Format("{0}/{1}/{2} at {3}:{4}", day, month, year, hour, minute));
                sw.Write('\n');
                sw.Write("Subject: " + subject);
                sw.Write("\n");
                sw.Write("Description: " + description);
                sw.Write("\n");
                sw.Write("---------");
                sw.Write("\n \n");

                sw.Close();
            }
        }
    }

    public class BugReportEntry
    {
        public string Name;
        public string Subject;
        public string Description;

        public BugReportEntry(string name, string sub, string desc)
        {
            Name = name;
            Subject = sub;
            Description = desc;
        }

        public void AddEntry()
        {
            BugReports.Bugs.Add(this);
        }

        public void Serialize(GenericWriter writer)
        {
            writer.Write(Name);
            writer.Write(Subject);
            writer.Write(Description);
        }

        public void Deserialize(GenericReader reader)
        {
            Name = reader.ReadString();
            Subject = reader.ReadString();
            Description = reader.ReadString();
        }
    }
}
