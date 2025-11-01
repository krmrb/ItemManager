using System.Collections.ObjectModel;

namespace ItemManagerClient
{
   public partial class MainPage : ContentPage
   {
      public ObservableCollection<Models.Item> Items { get; set; } = new();

      public MainPage()
      {
         InitializeComponent();

         // Ajouter les gestes de tap sur les menus
         var itemsTap = new TapGestureRecognizer();
         itemsTap.Tapped += (s, e) => ShowItemsView();
         ItemsMenu.GestureRecognizers.Add(itemsTap);

         var categoriesTap = new TapGestureRecognizer();
         categoriesTap.Tapped += (s, e) => ShowCategoriesView();
         CategoriesMenu.GestureRecognizers.Add(categoriesTap);

         // Charger les données d'exemple
         LoadSampleData();
      }

      private void LoadSampleData()
      {
         var sampleItems = new List<Models.Item>
            {
                new Models.Item
                {
                    Id = 1,
                    Title = "Réunion d'équipe hebdomadaire",
                    Description = "Discussion des projets en cours et planification",
                    Date = DateTime.Today.AddDays(1),
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(10, 30, 0),
                    Status = Enums.ItemStatus.Open,
                    CategoryId = 1
                },
                new Models.Item
                {
                    Id = 2,
                    Title = "Rapport mensuel de performance",
                    Description = "Préparation et analyse des indicateurs clés",
                    Date = DateTime.Today.AddDays(2),
                    IsAllDay = true,
                    Status = Enums.ItemStatus.Pending,
                    CategoryId = 5
                },
                new Models.Item
                {
                    Id = 3,
                    Title = "Maintenance système serveurs",
                    Description = "Mise à jour des correctifs de sécurité",
                    Date = DateTime.Today.AddDays(-1),
                    StartTime = new TimeSpan(14, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    Status = Enums.ItemStatus.Closed,
                    CategoryId = 3
                },
                new Models.Item
                {
                    Id = 4,
                    Title = "Formation nouvelles technologies",
                    Description = "Session de formation sur les derniers frameworks",
                    Date = DateTime.Today.AddDays(3),
                    StartTime = new TimeSpan(13, 0, 0),
                    EndTime = new TimeSpan(17, 0, 0),
                    Status = Enums.ItemStatus.Open,
                    CategoryId = 4
                }
            };

         foreach (var item in sampleItems)
         {
            Items.Add(item);
         }

         DisplayItems();
      }

      private void DisplayItems()
      {
         var tableContent = new VerticalStackLayout { Spacing = 1 };

         // En-tête du tableau
         var headerGrid = new Grid
         {
            BackgroundColor = Color.FromArgb("#f8f9fa"),
            Padding = new Thickness(15, 12),
            ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
         };

         var headers = new[] { "TÂCHE", "DATE", "HEURE", "STATUT", "CATÉGORIE", "ACTIONS" };
         for (int i = 0; i < headers.Length; i++)
         {
            var headerLabel = new Label
            {
               Text = headers[i],
               TextColor = Color.FromArgb("#2c3e50"),
               FontAttributes = FontAttributes.Bold,
               FontSize = 12,
               VerticalOptions = LayoutOptions.Center
            };
            Grid.SetColumn(headerLabel, i);
            headerGrid.Children.Add(headerLabel);
         }

         tableContent.Children.Add(headerGrid);

         // Lignes du tableau
         foreach (var item in Items)
         {
            var rowGrid = new Grid
            {
               BackgroundColor = Colors.White,
               Padding = new Thickness(15, 12),
               ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                    }
            };

            // Titre avec description
            var titleStack = new VerticalStackLayout { Spacing = 2 };
            titleStack.Children.Add(new Label
            {
               Text = item.Title,
               TextColor = Colors.Black,
               FontSize = 14,
               FontAttributes = FontAttributes.Bold
            });
            if (!string.IsNullOrEmpty(item.Description))
            {
               titleStack.Children.Add(new Label
               {
                  Text = item.Description.Length > 50 ? item.Description.Substring(0, 50) + "..." : item.Description,
                  TextColor = Color.FromArgb("#7f8c8d"),
                  FontSize = 12
               });
            }
            Grid.SetColumn(titleStack, 0);
            rowGrid.Children.Add(titleStack);

            // Date
            var dateLabel = new Label
            {
               Text = item.Date.ToString("dd/MM/yyyy"),
               TextColor = Colors.Black,
               FontSize = 13,
               VerticalOptions = LayoutOptions.Center
            };
            Grid.SetColumn(dateLabel, 1);
            rowGrid.Children.Add(dateLabel);

            // Heure
            var timeLabel = new Label
            {
               Text = item.IsAllDay ? "🕒 Toute la journée" :
                      $"{item.StartTime:hh\\:mm} - {item.EndTime:hh\\:mm}",
               TextColor = Colors.Black,
               FontSize = 13,
               VerticalOptions = LayoutOptions.Center
            };
            Grid.SetColumn(timeLabel, 2);
            rowGrid.Children.Add(timeLabel);

            // Statut avec badge
            var statusBadge = new Frame
            {
               BackgroundColor = GetStatusColor(item.Status),
               Padding = new Thickness(12, 6),
               HasShadow = false,
               CornerRadius = 12,
               HorizontalOptions = LayoutOptions.Start,
               Content = new Label
               {
                  Text = GetStatusText(item.Status),
                  TextColor = Colors.White,
                  FontSize = 11,
                  FontAttributes = FontAttributes.Bold
               }
            };
            Grid.SetColumn(statusBadge, 3);
            rowGrid.Children.Add(statusBadge);

            // Catégorie avec icône
            var categoryLabel = new Label
            {
               Text = GetCategoryText(item.CategoryId),
               TextColor = Colors.Black,
               FontSize = 13,
               VerticalOptions = LayoutOptions.Center
            };
            Grid.SetColumn(categoryLabel, 4);
            rowGrid.Children.Add(categoryLabel);

            // Actions
            var actionsLayout = new HorizontalStackLayout
            {
               Spacing = 8,
               VerticalOptions = LayoutOptions.Center,
               Children =
                    {
                        new Button
                        {
                            Text = "✏️",
                            BackgroundColor = Colors.Transparent,
                            TextColor = Color.FromArgb("#3498db"),
                            FontSize = 14,
                            Padding = new Thickness(8),
                            CornerRadius = 5
                        },
                        new Button
                        {
                            Text = "🗑️",
                            BackgroundColor = Colors.Transparent,
                            TextColor = Color.FromArgb("#e74c3c"),
                            FontSize = 14,
                            Padding = new Thickness(8),
                            CornerRadius = 5
                        }
                    }
            };
            Grid.SetColumn(actionsLayout, 5);
            rowGrid.Children.Add(actionsLayout);

            // Séparateur
            var separator = new BoxView
            {
               BackgroundColor = Color.FromArgb("#ecf0f1"),
               HeightRequest = 1
            };

            tableContent.Children.Add(rowGrid);
            tableContent.Children.Add(separator);
         }

         ContentFrame.Content = tableContent;
      }

      private void ShowItemsView()
      {
         // Activer le menu Items
         ItemsIndicator.IsVisible = true;
         CategoriesIndicator.IsVisible = false;

         // Mettre à jour le style des menus
         UpdateMenuStyle(ItemsMenu, true);
         UpdateMenuStyle(CategoriesMenu, false);

         // Mettre à jour le contenu
         ContentTitle.Text = "Tableau de suivi des items";
         ContentDescription.Text = "Suivez l'état d'avancement de vos items";

         DisplayItems();
      }

      private void ShowCategoriesView()
      {
         // Activer le menu Catégories
         ItemsIndicator.IsVisible = false;
         CategoriesIndicator.IsVisible = true;

         // Mettre à jour le style des menus
         UpdateMenuStyle(ItemsMenu, false);
         UpdateMenuStyle(CategoriesMenu, true);

         // Mettre à jour le contenu
         ContentTitle.Text = "Gestion des catégories";
         ContentDescription.Text = "Organisez vos items par catégories";

         // Afficher message pour catégories
         ContentFrame.Content = new Label
         {
            Text = "🔄 Interface catégories en cours de développement...",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontSize = 16
         };
      }

      private void UpdateMenuStyle(Grid menu, bool isActive)
      {
         var label = menu.Children.OfType<Label>().FirstOrDefault();
         var border = menu.Parent as Border;

         if (label != null && border != null)
         {
            label.TextColor = isActive ? Color.FromArgb("#3498db") : Color.FromArgb("#bdc3c7");
            border.BackgroundColor = isActive ? Color.FromArgb("#34495e") : Colors.Transparent;
         }
      }
      private Color GetStatusColor(Enums.ItemStatus status)
      {
         return status switch
         {
            Enums.ItemStatus.Draft => Color.FromArgb("#95a5a6"),
            Enums.ItemStatus.Open => Color.FromArgb("#2ecc71"),
            Enums.ItemStatus.Pending => Color.FromArgb("#f39c12"),
            Enums.ItemStatus.Canceled => Color.FromArgb("#e74c3c"),
            Enums.ItemStatus.Closed => Color.FromArgb("#3498db"),
            _ => Color.FromArgb("#95a5a6")
         };
      }

      private string GetStatusText(Enums.ItemStatus status)
      {
         return status switch
         {
            Enums.ItemStatus.Draft => "Brouillon",
            Enums.ItemStatus.Open => "Ouvert",
            Enums.ItemStatus.Pending => "En attente",
            Enums.ItemStatus.Canceled => "Annulé",
            Enums.ItemStatus.Closed => "Fermé",
            _ => "Inconnu"
         };
      }

      private string GetCategoryText(int categoryId)
      {
         return categoryId switch
         {
            1 => "📅 Réunion",
            2 => "📊 Projet",
            3 => "🔧 Maintenance",
            4 => "🎓 Formation",
            5 => "📋 Reporting",
            6 => "❓ Autre",
            _ => "Inconnue"
         };
      }
      private async void OnCreateNewItemClicked(object sender, EventArgs e)
      {
         try
         {
            var createPage = new CreateItemPage();

            // Ouvrir directement en modal (sans NavigationPage)
            await Navigation.PushModalAsync(createPage);
         }
         catch (Exception ex)
         {
            await DisplayAlert("Erreur", $"Impossible d'ouvrir le formulaire: {ex.Message}", "OK");
         }
      }
   }
}