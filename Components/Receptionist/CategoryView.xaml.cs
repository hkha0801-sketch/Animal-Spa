using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Animal_Spa.Components.Receptionist
{
    public partial class CategoryView : UserControl
    {
        private readonly Brush _normalBackground =
            new SolidColorBrush(Color.FromRgb(169, 203, 233));

        private readonly Brush _selectedBackground =
            new SolidColorBrush(Color.FromRgb(67, 139, 221));

        private readonly Brush _selectedBorder =
            new SolidColorBrush(Color.FromRgb(57, 126, 214));

        public event Action<string>? CategorySelected;

        public CategoryView()
        {
            InitializeComponent();
            ResetCategoryCards();
        }

        public void SetCustomerMode()
        {
            AddCategoryButton.Visibility = Visibility.Collapsed;
            NailCategoryButton.Visibility = Visibility.Visible;
            ResetCategoryCards();
        }

        private void Category_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button || button.Tag is not string category)
            {
                return;
            }

            ResetCategoryCards();
            button.Background = _selectedBackground;
            button.BorderBrush = _selectedBorder;
            CategorySelected?.Invoke(category);
        }

        private void ResetCategoryCards()
        {
            BathCategoryButton.Background = _normalBackground;
            HairCategoryButton.Background = _normalBackground;
            EarCategoryButton.Background = _normalBackground;
            NailCategoryButton.Background = _normalBackground;

            BathCategoryButton.BorderBrush = Brushes.Transparent;
            HairCategoryButton.BorderBrush = Brushes.Transparent;
            EarCategoryButton.BorderBrush = Brushes.Transparent;
            NailCategoryButton.BorderBrush = Brushes.Transparent;
        }
    }
}
