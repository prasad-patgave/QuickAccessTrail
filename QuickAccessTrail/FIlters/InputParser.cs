using QuickAccessTrail.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace QuickAccessTrail.FIlters
{
    public static class InputParser
    {
        public static WorksetAssignmentRequest Parse(string input)
        {
            if (string.IsNullOrEmpty(input)) 
                throw new Exception("Please Enter Category and Workset information.");
            Dictionary<string, string> categoryWorksets = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            string[] separators = { Environment.NewLine };
            string[] lines = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                string[] parts = line.Split('\t');
                if (parts.Length != 2)
                {
                    throw new Exception($"Invalid input format. Each line must contain a category and a workset separated by a tab. Invalid line: {line}");
                }
                string category = parts[0].Trim();
                string workset = parts[1].Trim();

                if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(workset))
                {
                    throw new Exception($"Category and workset cannot be empty. Invalid line: {line}");
                }
                if (categoryWorksets.ContainsKey(category))
                {
                    throw new Exception($"Duplicate category found: {category}. Each category must be unique.");
                }
                categoryWorksets.Add(category, workset);
            }
            return new WorksetAssignmentRequest { CategoryWorksets = categoryWorksets };
        }
    }
}
