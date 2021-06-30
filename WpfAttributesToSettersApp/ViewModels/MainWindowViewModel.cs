using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfAttributesToSettersApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _sourceText;
        private string _resultText;

        public string SourceText
        {
            get => _sourceText;
            set
            {
                _sourceText = value;
                UpdateResult();
            }
        }

        public string ResultText
        {
            get => _resultText;
            set
            {
                _resultText = value;
                OnPropertyChanged();
            }
        }

        private void UpdateResult()
        {
            //var sourceText = SourceText
            //    .Replace("\n", " ")
            //    .Replace("  ", " ")
            //    .Replace("  ", " ");

            var regex = new Regex(@"(\w+)\=\""([^\""]*)\""");
            var matches = regex.Matches(SourceText).ToList();
            var setters = matches
                .Select(match => match.Groups)
                .Select(groups => (Key: groups[1].Value, Value: groups[2].Value))
                .ToList();
            ResultText =
                string.Join("\n", setters.Select(x => $"<Setter Property=\"{x.Key}\" Value=\"{x.Value}\"/>").ToArray());
        }
    }
}
