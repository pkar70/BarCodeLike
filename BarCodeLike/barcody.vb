Module barcody

    Public Async Function TryScanBarCode(oDispatch As Windows.UI.Core.CoreDispatcher, bAllFormats As Boolean) As Task(Of ZXing.Result)

        Dim oScanner As ZXing.Mobile.MobileBarcodeScanner
        oScanner = New ZXing.Mobile.MobileBarcodeScanner(oDispatch)
        'Tell our scanner to use the default overlay 
        oScanner.UseCustomOverlay = False
        ' //We can customize the top And bottom text of our default overlay 
        oScanner.TopText = GetLangString("resMsgScannerTop") ' "Ustaw barcode w polu widzenia" ' "Hold camera up to barcode"
        oScanner.BottomText = GetLangString("resMsgScannerBottom").Replace("\n", vbCrLf) ' "Kod zostanie rozpoznany automatycznie" & vbCrLf & "Użyj 'back' by anulować" ' "Camera will automatically scan barcode" & vbCrLf & "Press the 'Back' button to Cancel"
        Dim oRes As ZXing.Result = Await oScanner.Scan()

        If oRes Is Nothing Then Return Nothing

        If oRes.BarcodeFormat = ZXing.BarcodeFormat.EAN_13 Then Return oRes
        If oRes.BarcodeFormat = ZXing.BarcodeFormat.EAN_8 Then Return oRes

        If bAllFormats Then Return oRes

        Await DialogBoxResAsync("msgNoEAN13")
        Return Nothing
    End Function

    Public Function BarCodeRegistrarEN(sBarCode As String) As String
        ' https://www.gs1.org/standards/id-keys/company-prefix
        If sBarCode.Length <> 13 Then Return ""

        If sBarCode.StartsWith("0000000") Then Return "internal"
        If sBarCode.StartsWith("000000") Then Return "unused" ' Unused to avoid collision with GTIN-8
        If sBarCode.StartsWith("00000") Then Return "unused" ' Unused to avoid collision with GTIN-8
        If sBarCode.StartsWith("00") Then Return "US"
        If sBarCode.StartsWith("01") Then Return "US"
        If sBarCode.StartsWith("02") Then Return "Restricted Circulation Numbers"
        If sBarCode.StartsWith("03") Then Return "US"
        If sBarCode.StartsWith("04") Then Return "internal"
        If sBarCode.StartsWith("05") Then Return "reserved"

        If sBarCode.StartsWith("0") Then Return "US"   ' od 06 do 09, ale wczesniejsze juz wczesniej byl return
        If sBarCode.StartsWith("10") Then Return "US"
        If sBarCode.StartsWith("11") Then Return "US"
        If sBarCode.StartsWith("12") Then Return "US"
        If sBarCode.StartsWith("13") Then Return "US"

        If sBarCode.StartsWith("2") Then Return "Restricted Circulation Numbers"


        If sBarCode.StartsWith("380") Then Return "Bulgaria"
        If sBarCode.StartsWith("383") Then Return "Slovenija"
        If sBarCode.StartsWith("385") Then Return "Croatia"
        If sBarCode.StartsWith("387") Then Return "BIH(Bosnia - Herzegovina)Then"
        If sBarCode.StartsWith("389") Then Return "Montenegro"
        If sBarCode.StartsWith("38") Then Return "*unknown*"    ' niektore 38x są zdefiniowane wyżej
        If sBarCode.StartsWith("39") Then Return "*unknown*"
        If sBarCode.StartsWith("3") Then Return "France"    ' 30-37



        If sBarCode.StartsWith("40") Then Return "Germany"
        If sBarCode.StartsWith("41") Then Return "Germany"
        If sBarCode.StartsWith("42") Then Return "Germany"
        If sBarCode.StartsWith("43") Then Return "Germany"
        If sBarCode.StartsWith("44") Then Return "Germany"
        If sBarCode.StartsWith("45") Then Return "Japan"
        If sBarCode.StartsWith("49") Then Return "Japan"
        If sBarCode.StartsWith("46") Then Return "Russia"
        If sBarCode.StartsWith("470") Then Return "Kyrgyzstan"
        If sBarCode.StartsWith("471") Then Return "Chinese Taipei"
        If sBarCode.StartsWith("474") Then Return "Estonia"
        If sBarCode.StartsWith("475") Then Return "Latvia"
        If sBarCode.StartsWith("476") Then Return "Azerbaijan"
        If sBarCode.StartsWith("477") Then Return "Lithuania"
        If sBarCode.StartsWith("478") Then Return "Uzbekistan"
        If sBarCode.StartsWith("479") Then Return "Sri Lanka"
        If sBarCode.StartsWith("480") Then Return "Philippines"
        If sBarCode.StartsWith("481") Then Return "Belarus"
        If sBarCode.StartsWith("482") Then Return "Ukraine"
        If sBarCode.StartsWith("483") Then Return "Turkmenistan"
        If sBarCode.StartsWith("484") Then Return "Moldova"
        If sBarCode.StartsWith("485") Then Return "Armenia"
        If sBarCode.StartsWith("486") Then Return "Georgia"
        If sBarCode.StartsWith("487") Then Return "Kazakstan"
        If sBarCode.StartsWith("488") Then Return "Tajikistan"
        If sBarCode.StartsWith("489") Then Return "Hong Kong"

        If sBarCode.StartsWith("50") Then Return "UK"
        If sBarCode.StartsWith("520") Then Return "Greece"
        If sBarCode.StartsWith("521") Then Return "Greece"
        If sBarCode.StartsWith("528") Then Return "Lebanon"
        If sBarCode.StartsWith("529") Then Return "Cyprus"
        If sBarCode.StartsWith("530") Then Return "Albania"
        If sBarCode.StartsWith("531") Then Return "Macedonia"
        If sBarCode.StartsWith("535") Then Return "Malta"
        If sBarCode.StartsWith("539") Then Return "Ireland"
        If sBarCode.StartsWith("54") Then Return "Belgium & Luxembourg"
        If sBarCode.StartsWith("560") Then Return "Portugal"
        If sBarCode.StartsWith("569") Then Return "Iceland"
        If sBarCode.StartsWith("57") Then Return "Denmark"
        If sBarCode.StartsWith("590") Then Return GetLangString("msgCountryPoland")

        If sBarCode.StartsWith("594") Then Return "Romania"
        If sBarCode.StartsWith("599") Then Return "Hungary"

        If sBarCode.StartsWith("600") Then Return "South Africa"
        If sBarCode.StartsWith("601") Then Return "South Africa"
        If sBarCode.StartsWith("603") Then Return "Ghana"
        If sBarCode.StartsWith("604") Then Return "Senegal"
        If sBarCode.StartsWith("608") Then Return "Bahrain"
        If sBarCode.StartsWith("609") Then Return "Mauritius"
        If sBarCode.StartsWith("611") Then Return "Morocco"
        If sBarCode.StartsWith("613") Then Return "Algeria"
        If sBarCode.StartsWith("615") Then Return "Nigeria"
        If sBarCode.StartsWith("616") Then Return "Kenya"
        If sBarCode.StartsWith("617") Then Return "Cameroon"
        If sBarCode.StartsWith("618") Then Return "Côte d'Ivoire"
        If sBarCode.StartsWith("619") Then Return "Tunisia"
        If sBarCode.StartsWith("620") Then Return "Tanzania"
        If sBarCode.StartsWith("621") Then Return "Syria"
        If sBarCode.StartsWith("622") Then Return "Egypt"
        If sBarCode.StartsWith("623") Then Return "Brunei"
        If sBarCode.StartsWith("624") Then Return "Libya"
        If sBarCode.StartsWith("625") Then Return "Jordan"
        If sBarCode.StartsWith("626") Then Return "Iran"
        If sBarCode.StartsWith("627") Then Return "Kuwait"
        If sBarCode.StartsWith("628") Then Return "Saudi Arabia"
        If sBarCode.StartsWith("629") Then Return "Emirates"
        If sBarCode.StartsWith("64") Then Return "Finland"
        If sBarCode.StartsWith("69") Then Return "China"

        If sBarCode.StartsWith("70") Then Return "Norway"
        If sBarCode.StartsWith("729") Then Return "Israel"
        If sBarCode.StartsWith("73") Then Return "Sweden"
        If sBarCode.StartsWith("740") Then Return "Guatemala"
        If sBarCode.StartsWith("741") Then Return "El Salvador"
        If sBarCode.StartsWith("742") Then Return "Honduras"
        If sBarCode.StartsWith("743") Then Return "Nicaragua"
        If sBarCode.StartsWith("744") Then Return "Costa Rica"
        If sBarCode.StartsWith("745") Then Return "Panama"
        If sBarCode.StartsWith("746") Then Return "Republica Dominicana"
        If sBarCode.StartsWith("750") Then Return "Mexico"
        If sBarCode.StartsWith("754") Then Return "Canada"
        If sBarCode.StartsWith("755") Then Return "Canada"
        If sBarCode.StartsWith("759") Then Return "Venezuela"
        If sBarCode.StartsWith("76") Then Return "Schweiz, Suisse, Svizzera"
        If sBarCode.StartsWith("770") Then Return "Colombia"
        If sBarCode.StartsWith("771") Then Return "Colombia"
        If sBarCode.StartsWith("773") Then Return "Uruguay"
        If sBarCode.StartsWith("775") Then Return "Peru"
        If sBarCode.StartsWith("777") Then Return "Bolivia"
        If sBarCode.StartsWith("778") Then Return "Argentina"
        If sBarCode.StartsWith("779") Then Return "Argentina"
        If sBarCode.StartsWith("780") Then Return "Chile"
        If sBarCode.StartsWith("784") Then Return "Paraguay"
        If sBarCode.StartsWith("786") Then Return "Ecuador"
        If sBarCode.StartsWith("789") Then Return "Brasil"
        If sBarCode.StartsWith("790") Then Return "Brasil"

        If sBarCode.StartsWith("80") Then Return "Italy"
        If sBarCode.StartsWith("81") Then Return "Italy"
        If sBarCode.StartsWith("82") Then Return "Italy"
        If sBarCode.StartsWith("83") Then Return "Italy"
        If sBarCode.StartsWith("84") Then Return "Spain"
        If sBarCode.StartsWith("850") Then Return "Cuba"
        If sBarCode.StartsWith("858") Then Return "Slovakia"
        If sBarCode.StartsWith("859") Then Return "Czech"
        If sBarCode.StartsWith("860") Then Return "Serbia"
        If sBarCode.StartsWith("865") Then Return "Mongolia"
        If sBarCode.StartsWith("867") Then Return "North Korea"
        If sBarCode.StartsWith("868") Then Return "Turkey"
        If sBarCode.StartsWith("869") Then Return "Turkey"
        If sBarCode.StartsWith("87") Then Return "Netherlands"
        If sBarCode.StartsWith("880") Then Return "South Korea"
        If sBarCode.StartsWith("883") Then Return "Myanmar"
        If sBarCode.StartsWith("884") Then Return "Cambodia"
        If sBarCode.StartsWith("885") Then Return "Thailand"
        If sBarCode.StartsWith("888") Then Return "Singapore"
        If sBarCode.StartsWith("890") Then Return "India"
        If sBarCode.StartsWith("893") Then Return "Vietnam"
        If sBarCode.StartsWith("896") Then Return "Pakistan"
        If sBarCode.StartsWith("899") Then Return "Indonesia"

        If sBarCode.StartsWith("90") Then Return "Austria"
        If sBarCode.StartsWith("91") Then Return "Austria"
        If sBarCode.StartsWith("93") Then Return "Australia"
        If sBarCode.StartsWith("94") Then Return "New Zealand"

        If sBarCode.StartsWith("950") Then Return "Global Office"
        If sBarCode.StartsWith("951") Then Return "Global Office"
        If sBarCode.StartsWith("952") Then Return "demonstration"

        If sBarCode.StartsWith("955") Then Return "Malaysia"
        If sBarCode.StartsWith("958") Then Return "Macau, China"

        If sBarCode.StartsWith("96") Then Return "Global Office - GTIN-8"
        If sBarCode.StartsWith("977") Then Return "ISSN"
        If sBarCode.StartsWith("978") Then Return "ISBN"
        If sBarCode.StartsWith("979") Then Return "ISBN"

        If sBarCode.StartsWith("980") Then Return "Refund receipts"

        If sBarCode.StartsWith("981") Then Return "coupon identification"
        If sBarCode.StartsWith("982") Then Return "coupon identification"
        If sBarCode.StartsWith("983") Then Return "coupon identification"
        If sBarCode.StartsWith("984") Then Return "coupon identification"

        If sBarCode.StartsWith("99") Then Return "coupon identification"

        Return "*unknown*"
    End Function

    Public Sub GetData()
        ' https://www.ean-search.org/?q=8711327398283
        ' https://kodyean.pl/ - uwaga na token
        ' https://gepir.gs1.org/index.php/search-by-gtin - captcha
        ' zwykle szukanie w Web (gogiel)
    End Sub

End Module
