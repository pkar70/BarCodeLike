' TODO
' 1) guzik Save
' 2) odczyt/zapis danych do pliku
' 3) dodawanie obrazków (fotek)
' 4) wyszukiwanie danych (na razie: z ktorego kraju)


Public NotInheritable Class Details
    Inherits Page

    Private msItemBarCode As String = ""
    Private Shared mPrzedmiot As Przedmiot
    Private mbEdit As Boolean = False

    Protected Overrides Sub onNavigatedTo(e As NavigationEventArgs)
        msItemBarCode = ""
        mPrzedmiot = Nothing
        If e?.Parameter Is Nothing Then Return

        msItemBarCode = e?.Parameter.ToString
        For Each oItem As Przedmiot In App.gItemsList.lLista
            If oItem.sBarCode = msItemBarCode Then
                mPrzedmiot = oItem
                mbEdit = True
                Exit For
            End If
        Next

        ' If mPrzedmiot Is Nothing Then COSNIETAKNIEZNALAZLEM
    End Sub

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        'If Not mbEdit Then
        '    Dim oRes As ZXing.Result = Await Skanowanie()
        '    If oRes IsNot Nothing Then msItemBarCode = oRes.Text
        'End If

        '' dalej nie mamy kodu, to nie pokazujemy, bo nie mamy co pokazac
        'If msItemBarCode = "" Then Me.Frame.GoBack()

        If mPrzedmiot Is Nothing Then
            mPrzedmiot = New Przedmiot
            mPrzedmiot.sBarCode = msItemBarCode
            mPrzedmiot.sNazwa = msItemBarCode
            mPrzedmiot.sAddedAt = Date.Now.ToString("yyyy.MM.dd")
            uiEAN.IsReadOnly = False

            uiDetailsScan.Visibility = Visibility.Visible
        Else
            uiDetailsScan.Visibility = Visibility.Collapsed
            uiEAN.IsReadOnly = True
        End If

        ' pokaz dane
        PrzedmiotNaEkran()
    End Sub

    Private Sub PrzedmiotNaEkran()
        uiEAN.Text = mPrzedmiot.sBarCode
        uiNazwa.Text = mPrzedmiot.sNazwa
        uiAdded.Text = GetLangString("msgAddedAt") & " " & mPrzedmiot.sAddedAt

        Dim oReq As ComboBoxItem = Nothing
        For Each oItem As ComboBoxItem In uiOcena.Items
            If oItem.Content.ToString.Contains("+") AndAlso mPrzedmiot.iLikeDislike > 0 Then oReq = oItem
            If oItem.Content.ToString.Contains("-") AndAlso mPrzedmiot.iLikeDislike < 0 Then oReq = oItem
            If oItem.Content.ToString.Contains("0") AndAlso mPrzedmiot.iLikeDislike = 0 Then oReq = oItem
        Next
        uiOcena.SelectedItem = oReq

        uiCountry.Text = GetLangString("msgRegisteredKraj") & " " & BarCodeRegistrarEN(mPrzedmiot.sBarCode)
    End Sub


    Private Async Sub uiSave_Click(sender As Object, e As RoutedEventArgs)
        ' replace or save

        Dim oReq As ComboBoxItem = uiOcena.SelectedItem
        If oReq.Content.ToString.Contains("+") Then mPrzedmiot.iLikeDislike = 1
        If oReq.Content.ToString.Contains("-") Then mPrzedmiot.iLikeDislike = -1
        If oReq.Content.ToString.Contains("0") Then mPrzedmiot.iLikeDislike = 0

        mPrzedmiot.sBarCode = uiEAN.Text
        mPrzedmiot.sNazwa = uiNazwa.Text
        mPrzedmiot.sAddedAt = uiAdded.Text


        If Not mbEdit Then App.gItemsList.lLista.Add(mPrzedmiot)
        Await App.gItemsList.SaveItemsAsync

        Me.Frame.GoBack()
    End Sub

    Private Async Function Skanowanie() As Task(Of ZXing.Result)
        ' kod paskowy do fotografowania
        Dim oRes As ZXing.Result = Await TryScanBarCode(Me.Dispatcher, False)

        ' ominiecie bledu? ale wczesniej (WezPigulka) bylo dobrze? Teraz jest 0:MainPage 1:Details
        If Me.Frame.BackStack.Count > 0 Then
            If Me.Frame.BackStack.ElementAt(Me.Frame.BackStack.Count - 1).GetType Is Me.GetType Then
                Me.Frame.BackStack.RemoveAt(Me.Frame.BackStack.Count - 1)
            End If
        End If

        Return oRes

    End Function
    Private Async Sub uiRunScan_Clicked(sender As Object, e As RoutedEventArgs)
        If mbEdit Then Return

        Dim oRes As ZXing.Result = Await Skanowanie()
        If oRes.Text = "" Then Return

        mPrzedmiot = New Przedmiot
        mPrzedmiot.sAddedAt = Date.Now.ToString("yyyy.MM.dd")

        mPrzedmiot.sBarCode = oRes.Text
        mPrzedmiot.sNazwa = oRes.Text
        uiEAN.IsReadOnly = False

        PrzedmiotNaEkran() ' nie potrzeba, bo i tak sie zrobi (mamy PageLoaded tutaj!)

    End Sub
End Class
