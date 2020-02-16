using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.ViewModels.Problems
{
    public class ProblemDetailsViewModel
    {
        public string Name { get; set; }

        public IEnumerable<SubmissionProblemDetailsViewModel> Submissions { get; set; }
    }
}
