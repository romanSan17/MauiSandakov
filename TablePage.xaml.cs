using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace MauiSandakov;

public partial class TablePage : ContentPage
{
    public TablePage()
    {
        TableView tableview;

        tableview = new TableView
        {
            Intent = TableIntent.Form, // могут быть еще Menu, Data, Settings
            Root = new TableRoot("Andmete sisestamine")
            {
                new TableSection("P\u00f5hiandmed: ")
                {
                    new EntryCell
                    {
                        Label = "Nimi: ",
                        Placeholder = "Sisesta oma s\u00f5bra nimi",
                        Keyboard = Keyboard.Default
                    }
                },
                new TableSection("Kontaktandmed: ")
                {
                    new EntryCell
                    {
                        Label = "Telefon",
                        Placeholder = "Sisesta tel. number",
                        Keyboard = Keyboard.Telephone
                    },
                    new EntryCell
                    {
                        Label = "Email",
                        Placeholder = "Sisesta email",
                        Keyboard = Keyboard.Email
                    }
                }
            }
        };

        Content = tableview;
    }
}
