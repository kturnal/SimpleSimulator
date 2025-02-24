// using System.ComponentModel;
// using SimpleSimulator;
// using SimpleSimulator.ViewModels;

// namespace SimpleSimulator.ViewModels 
// {
//     public class AxisLabel : INotifyPropertyChanged
//     {
//         private string label;
//         private double value;

//         public string Label
//         {
//             get => label;
//             set
//             {
//                 if (label != value)
//                 {
//                     label = value;
//                     OnPropertyChanged(nameof(Label));
//                 }
//             }
//         }

//         public double Value
//         {
//             get => value;
//             set
//             {
//                 if (this.value != value)
//                 {
//                     this.value = value;
//                     OnPropertyChanged(nameof(Value));
//                 }
//             }
//         }



//         public event PropertyChangedEventHandler PropertyChanged;

//         protected virtual void OnPropertyChanged(string propertyName)
//         {
//             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//         }
//     }
// }