using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.ViewModels
{
    public class ActionButtonViewModel
    {
        public string Text { get; set; }
        public string Color { get; set; }
        public ButtonAction Action { get; set; }
        public int Identifier { get; set; }
        public ActionButtonViewModel()
        {
        }
        public ActionButtonViewModel(string text, string color, ButtonAction action, int ID)
        {
            this.Text = text;
            this.Color = color;
            this.Action = action;
            this.Identifier = ID;
        }
    }
}