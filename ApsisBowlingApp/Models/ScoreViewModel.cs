using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApsisBowlingApp.Models
{
    public class ScoreViewModel
    {
        public List<FrameViewModel> Frames { get; set; }
        public int? TotalScore { get; set; }
    }
}