
' <list> barcode_num, ewentualnie picek itemu, nazwa, kolorek (zielony, czerwony, bez kolorku)
' DODAJ: Navigate(Details,"")
' SZUKAJ: Await TryScanBarCode(Me.Dispatcher, False)

Public NotInheritable Class MainPage
    Inherits Page

    Private Sub uiAdd_Click(sender As Object, e As RoutedEventArgs)
        Me.Frame.Navigate(GetType(Details))
    End Sub
    Private Async Sub uiSearch_Click(sender As Object, e As RoutedEventArgs)
        ' scan
        Dim oRes As ZXing.Result = Await TryScanBarCode(Me.Dispatcher, False)

        ' ominiecie bledu? ale wczesniej (WezPigulka) bylo dobrze? Teraz jest 0:MainPage 1:Details
        If Me.Frame.BackStack.Count > 0 Then
            If Me.Frame.BackStack.ElementAt(Me.Frame.BackStack.Count - 1).GetType Is Me.GetType Then
                Me.Frame.BackStack.RemoveAt(Me.Frame.BackStack.Count - 1)
            End If
        End If

        If oRes Is Nothing Then Return

        Dim sTxt As String = "not found"

        For Each oItem As Przedmiot In App.gItemsList.lLista
            If oItem.sBarCode = oRes.Text Then
                If oItem.iLikeDislike < 0 Then sTxt = GetLangString("resMsgDislike") & "! :("
                If oItem.iLikeDislike > 0 Then sTxt = GetLangString("resMsgLike") & "! :("
                If oItem.iLikeDislike > 0 Then sTxt = "??"
                Exit For
            End If
        Next

        DialogBox(sTxt)
    End Sub


    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Await App.gItemsList.LoadItemsAsync
        GetAppVers(Nothing) ' sam dodaje pole

        uiList.ItemsSource = From c In App.gItemsList.lLista Order By c.sBarCode

    End Sub

    Private Sub uiMenuEdit_Click(sender As Object, e As RoutedEventArgs)
        Dim oMFI As MenuFlyoutItem = TryCast(sender, MenuFlyoutItem)
        If oMFI Is Nothing Then Return

        Dim oItem As Przedmiot = TryCast(oMFI.DataContext, Przedmiot)
        If oItem Is Nothing Then Return

        Me.Frame.Navigate(GetType(Details), oItem.sBarCode)
    End Sub
End Class

Public Class KonwersjaTla
    Implements IValueConverter

    ' Define the Convert method to change a DateTime object to
    ' a month string.
    Public Function Convert(ByVal value As Object,
            ByVal targetType As Type, ByVal parameter As Object,
            ByVal language As System.String) As Object _
            Implements IValueConverter.Convert

        ' value is the data from the source object.

        Dim iTmp As Integer = CType(value, Integer)

        If iTmp > 0 Then Return New SolidColorBrush(Windows.UI.Colors.LightGreen)
        If iTmp < 0 Then Return New SolidColorBrush(Windows.UI.Colors.OrangeRed)

        If App.Current.RequestedTheme = ApplicationTheme.Dark Then Return New SolidColorBrush(Windows.UI.Colors.Black)
        Return New SolidColorBrush(Windows.UI.Colors.White)
    End Function

    ' ConvertBack is not implemented for a OneWay binding.
    Public Function ConvertBack(ByVal value As Object,
            ByVal targetType As Type, ByVal parameter As Object,
            ByVal language As System.String) As Object _
            Implements IValueConverter.ConvertBack

        Throw New NotImplementedException

    End Function
End Class