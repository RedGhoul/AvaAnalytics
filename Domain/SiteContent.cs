using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class SiteContent
    {
        public int Id { get; set; }
        public string LargeHeader { get; set; }
        public string BlurgUnderLargerHeader { get; set; }
        public string Header1 { get; set; }
        public string Header1Content { get; set; }
        public string Header1CodeSnip { get; set; }
        public string Header2 { get; set; }
        public string Header2Content { get; set; }
        public string Header3 { get; set; }
        public string Header3Content { get; set; }
        public string FinalHeader { get; set; }
        public string FinalHeaderContent { get; set; }
        public string WhatWeCollect { get; set; }
        public string PrivacyPolicy { get; set; }
        public string Documentation { get; set; }

    }
}
