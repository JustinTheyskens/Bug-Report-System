using Server.Misc;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Gumps
{
    internal class BugReportGump : Gump
    {
        Mobile User;
        public BugReportGump(Mobile user)
            : base(250, 150)
        {
            User = user;
            AddGumpLayout();
        }

        public void AddGumpLayout()
        {
            AddPage(0);
            int width = 500;
            int height = 500;
            AddBackground(0, 0, width, height, 0xA28);

            AddHtml(230, 15, 500, 16, "Bug report", false, false);


            AddHtml(55, 45, 500, 16, "Subject", false, false);
            string subject = "";
            AddImageTiled(50, 65, height - 100, 21, 0xBBC);
            AddTextEntry(55, 65, width - 105, 21, 0, 1, subject);


            AddHtml(55, 105, 500, 16, "Description", false, false);
            string desc = "";
            AddImageTiled(50, 125, width - 100, 300, 0xBBC);
            AddTextEntry(55, 125, width - 106, 300, 0, 2, desc);

            AddButton(85, 445, 0xFB7, 0xFB9, 1, GumpButtonType.Reply, 0);
            AddHtml(120, 445, 400, 16, "Submit Report", false, false);

        }

        public void AddEntry(RelayInfo info, Mobile from)
        {
            TextRelay sub = info.GetTextEntry(1); // Subject
            TextRelay des = info.GetTextEntry(2); // Description

            string subject = sub.Text;
            string desc = des.Text;
            BugReports.WriteBug(User.Name, subject, desc);

        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (info.ButtonID == 0)
                return;

            if (info.ButtonID == 1)
            {
                AddEntry(info, User);
            }
        }
    }
}
