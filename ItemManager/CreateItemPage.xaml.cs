using ItemManagerClient.Models;
using ItemManagerClient.Enums;

namespace ItemManagerClient
{
   public partial class CreateItemPage : ContentPage
   {
      public CreateItemPage()
      {
         InitializeComponent();

         // Initialiser les valeurs par défaut
         InitializeForm();
      }

      private void InitializeForm()
      {
         // Date par défaut = aujourd'hui
         DatePicker.Date = DateTime.Today;

         // Heures par défaut
         StartTimePicker.Time = new TimeSpan(9, 0, 0); // 09:00
         EndTimePicker.Time = new TimeSpan(17, 0, 0);  // 17:00

         // Statut par défaut = Brouillon
         StatusPicker.SelectedIndex = 0;

         // Catégorie par défaut = Réunion
         CategoryPicker.SelectedIndex = 0;
      }

      private async void OnSaveClicked(object sender, EventArgs e)
      {
         // Validation
         if (string.IsNullOrWhiteSpace(TitleEntry.Text))
         {
            await DisplayAlert("Erreur", "Le titre est obligatoire", "OK");
            TitleEntry.Focus();
            return;
         }

         // Créer le nouvel item
         var newItem = new Item
         {
            Title = TitleEntry.Text.Trim(),
            Description = DescriptionEditor.Text?.Trim(),
            Date = DatePicker.Date,
            IsAllDay = false, // Plus de "Toute la journée"
            StartTime = StartTimePicker.Time,
            EndTime = EndTimePicker.Time,
            Status = GetSelectedStatus(),
            CategoryId = CategoryPicker.SelectedIndex + 1,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
         };

         try
         {
            // Sauvegarder l'item
            // await itemService.CreateItemAsync(newItem);

            await DisplayAlert("Succès", "Item créé avec succès", "OK");

            // Fermer le modal
            await Navigation.PopModalAsync();
         }
         catch (Exception ex)
         {
            await DisplayAlert("Erreur", $"Erreur lors de la création: {ex.Message}", "OK");
         }
      }

      private async void OnCancelButtonClicked(object sender, EventArgs e)
      {
         // Demander confirmation si des données ont été saisies
         if (!string.IsNullOrWhiteSpace(TitleEntry.Text) ||
             !string.IsNullOrWhiteSpace(DescriptionEditor.Text))
         {
            bool answer = await DisplayAlert(
                "Confirmation",
                "Voulez-vous vraiment annuler ? Les modifications seront perdues.",
                "Oui", "Non");

            if (!answer)
               return;
         }

         // Fermer le modal
         await Navigation.PopModalAsync();
      }

      private void OnCloseClicked(object sender, EventArgs e)
      {
         // Appeler directement la méthode d'annulation
         OnCancelButtonClicked(sender, e);
      }

      private ItemStatus GetSelectedStatus()
      {
         return StatusPicker.SelectedIndex switch
         {
            0 => ItemStatus.Draft,     // Brouillon
            1 => ItemStatus.Open,      // Ouvert
            2 => ItemStatus.Pending,   // En attente
            3 => ItemStatus.Canceled,  // Annulé
            4 => ItemStatus.Closed,    // Fermé
            _ => ItemStatus.Draft
         };
      }

      private async void OnCreateNewItemClicked(object sender, EventArgs e)
      {
         try
         {
            var createPage = new CreateItemPage();
            await Navigation.PushModalAsync(createPage);
         }
         catch (Exception ex)
         {
            await DisplayAlert("Erreur", $"Impossible d'ouvrir le formulaire: {ex.Message}", "OK");
         }
      }

   }
}