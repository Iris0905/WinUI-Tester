using System;

namespace WinUITester.Models
{
    public class TestResult
    {
        public string Barcode { get; set; }

        public string Assay { get; set; }

        public string Result { get; set; }

        public DateTime CompletedAt { get; set; }
    }
}