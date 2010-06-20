using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mveEngine
{
    [global::System.AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class LabelAttribute : Attribute
    {
        private string _label;

        public string Label { get { return _label; } }

        public LabelAttribute(string label)
        {
            string[] labs = label.Split('|');
            foreach (string s in labs)
            {
                if (s.StartsWith(System.Globalization.CultureInfo.CurrentCulture.Parent.ToString()))
                    this._label = GetLabel(s);
            }
            if (string.IsNullOrEmpty(this.Label))
                this._label = GetLabel(labs[0]);
        }

        private string GetLabel(string s)
        {
            if (s.Contains("&"))
                return s.Substring(s.IndexOf("&") + 1);
            return s;
        }
    }
}