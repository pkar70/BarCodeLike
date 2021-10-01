Public Class Przedmiot
    Public Property sAddedAt As String
    Public Property sBarCode As String
    Public Property iLikeDislike As Integer = 0 ' -1/0/+1
    Public Property sSklep As String
    Public Property sNazwa As String
    Public Property lNotatki As List(Of Notatka)
End Class
Public Class Notatka
    Public Property sAddedAt As String
    Public Property sText As String
    Public Property dKwota As Double
End Class

Public Class ListaItems
    Public lLista As ObservableCollection(Of Przedmiot)

    Public Async Function SaveItemsAsync() As Task(Of Boolean)
        If lLista.Count < 1 Then Return False

        Dim oFold As Windows.Storage.StorageFolder = Windows.Storage.ApplicationData.Current.RoamingFolder
        Dim sTxt As String = Newtonsoft.Json.JsonConvert.SerializeObject(lLista, Newtonsoft.Json.Formatting.Indented)

        Await oFold.WriteAllTextToFileAsync("items.json", sTxt, Windows.Storage.CreationCollisionOption.ReplaceExisting)

        Return True
    End Function

    Public Async Function LoadItemsAsync() As Task(Of Boolean)

        Dim sTxt As String = Await Windows.Storage.ApplicationData.Current.RoamingFolder.ReadAllTextFromFileAsync("items.json")
        If sTxt Is Nothing OrElse sTxt.Length < 5 Then
            lLista = New ObservableCollection(Of Przedmiot)
            Return False
        End If
        lLista = Newtonsoft.Json.JsonConvert.DeserializeObject(sTxt, GetType(ObservableCollection(Of Przedmiot)))
        Return True
    End Function

End Class