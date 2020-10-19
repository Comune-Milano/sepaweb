Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb
Imports System.Math

Partial Class SATISFACTION_RisultatoSchede
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sValoreVAL As String
    Dim sValoreSERV As String
    Dim sValoreDOM As String
    Dim sValoreRISP As String
    Dim sValoreCODUI As String
    Dim sValoreIndirizzo As String
    Dim sValoreCivico As String
    Dim sValoreIDUN As Long
    Dim sValoreOP As String
    Dim sValoreDATA1 As String
    Dim sValoreDATA2 As String
    Dim sValoreGiudizio As String
    Dim controlloDate As String = ""
    Dim controlloOperatore As String = ""
    Dim controlloGiudizio As String = ""


    Public Property stringaSQL() As String

        Get
            If Not (ViewState("par_stringaSQL") Is Nothing) Then
                Return CStr(ViewState("par_stringaSQL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_stringaSQL") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)

        sValoreVAL = UCase(Request.QueryString("VAL"))
        sValoreDOM = UCase(Request.QueryString("DOM"))
        sValoreRISP = UCase(Request.QueryString("RISP"))
        sValoreSERV = UCase(Request.QueryString("SERV"))
        'sValoreIDUN = Request.QueryString("IdUN")
        sValoreCODUI = UCase(Request.QueryString("CODUI"))
        sValoreIndirizzo = UCase(Replace(Request.QueryString("IND"), "@@@", "''"))
        sValoreCivico = UCase(Request.QueryString("CIV"))
        sValoreGiudizio = Request.QueryString("GIU")
        sValoreOP = UCase(Request.QueryString("OP"))
        sValoreDATA1 = UCase(Request.QueryString("DATAI"))
        sValoreDATA2 = UCase(Request.QueryString("DATAF"))

        sValoreDATA1 = Mid(sValoreDATA1, 7, 4) & Mid(sValoreDATA1, 4, 2) & Mid(sValoreDATA1, 1, 2)
        sValoreDATA2 = Mid(sValoreDATA2, 7, 4) & Mid(sValoreDATA2, 4, 2) & Mid(sValoreDATA2, 1, 2)


        If Trim(sValoreDATA1) = "" And Trim(sValoreDATA2) = "" Then
            controlloDate = ""
        ElseIf Trim(sValoreDATA1) <> "" And Trim(sValoreDATA2) = "" Then
            controlloDate = " SISCOM_MI.CUSTOMER_SATISFACTION.DATA_INSERIMENTO_OP >='" & sValoreDATA1 & "' AND "
        ElseIf Trim(sValoreDATA1) = "" And Trim(sValoreDATA2) <> "" Then
            controlloDate = " SISCOM_MI.CUSTOMER_SATISFACTION.DATA_INSERIMENTO_OP <='" & sValoreDATA2 & "' AND "
        Else
            controlloDate = " (SISCOM_MI.CUSTOMER_SATISFACTION.DATA_INSERIMENTO_OP BETWEEN '" & sValoreDATA1 & "' AND '" & sValoreDATA2 & "' ) AND "
        End If


        If Not sValoreOP = "---" Then
            controlloOperatore = controlloOperatore & " SISCOM_MI.CUSTOMER_SATISFACTION.ID_OPERATORE=SEPA.OPERATORI.ID AND SEPA.OPERATORI.ID='" & sValoreOP & "' AND "
        Else
            controlloOperatore = controlloOperatore & " SISCOM_MI.CUSTOMER_SATISFACTION.ID_OPERATORE=SEPA.OPERATORI.ID AND "
        End If

        If Not sValoreGiudizio = "---" Then
            controlloGiudizio = controlloGiudizio & " SISCOM_MI.CUSTOMER_SATISFACTION.GIUDIZIO_COMPLESSIVO='" & sValoreGiudizio & "' AND "
        Else
            controlloGiudizio = ""
        End If


        If Not IsPostBack Then
            Response.Flush()

            'inserire la condizione all'interno
            Dim controlloIndirizzo As String = ""
            Dim controlloCivico As String = ""


            

            If Not sValoreIndirizzo = "---" Then
                controlloIndirizzo = controlloIndirizzo & " SISCOM_MI.INDIRIZZI.DESCRIZIONE= '" & sValoreIndirizzo & "' AND "
            End If
            If Not sValoreCivico = "---" Then
                controlloCivico = controlloCivico & " SISCOM_MI.INDIRIZZI.CIVICO='" & sValoreCivico & "' AND "
            End If
            If sValoreRISP = "---" And sValoreVAL = "---" Then
                If sValoreCODUI <> "" Then
                    If Right(sValoreCODUI, 1) = "*" Then
                        Dim CODUIModificato As String = Mid(sValoreCODUI, 1, Len(sValoreCODUI) - 1)
                        stringaSQL = "SELECT SISCOM_MI.INDIRIZZI.*,SISCOM_MI.CUSTOMER_SATISFACTION.*,SISCOM_MI.UNITA_IMMOBILIARI.*,TO_CHAR(TO_DATE(DATA_COMPILAZIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_C,SISCOM_MI.CUSTOMER_SATISFACTION.ID AS ID_CUST,SEPA.OPERATORI.* FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.CUSTOMER_SATISFACTION,SISCOM_MI.UNITA_IMMOBILIARI,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloIndirizzo & controlloCivico & " SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO=SISCOM_MI.INDIRIZZI.ID AND SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA=SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & CODUIModificato & "%' ORDER BY SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA ASC"
                    Else
                        stringaSQL = "SELECT SISCOM_MI.INDIRIZZI.*,SISCOM_MI.CUSTOMER_SATISFACTION.*,SISCOM_MI.UNITA_IMMOBILIARI.*,TO_CHAR(TO_DATE(DATA_COMPILAZIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_C,SISCOM_MI.CUSTOMER_SATISFACTION.ID AS ID_CUST,SEPA.OPERATORI.* FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.CUSTOMER_SATISFACTION,SISCOM_MI.UNITA_IMMOBILIARI,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloIndirizzo & controlloCivico & " SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO=SISCOM_MI.INDIRIZZI.ID AND SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA=SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ='" & sValoreCODUI & "' ORDER BY SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA ASC"
                    End If
                    'MsgBox(stringaSQL)
                Else
                    stringaSQL = "SELECT SISCOM_MI.INDIRIZZI.*,SISCOM_MI.CUSTOMER_SATISFACTION.*,SISCOM_MI.UNITA_IMMOBILIARI.*,TO_CHAR(TO_DATE(DATA_COMPILAZIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_C,SISCOM_MI.CUSTOMER_SATISFACTION.ID AS ID_CUST,SEPA.OPERATORI.* FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.CUSTOMER_SATISFACTION,SISCOM_MI.UNITA_IMMOBILIARI,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloIndirizzo & controlloCivico & " SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO=SISCOM_MI.INDIRIZZI.ID AND SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA=SISCOM_MI.UNITA_IMMOBILIARI.ID ORDER BY SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA ASC"

                End If


            Else
                If sValoreCODUI <> "" Then
                    If Right(sValoreCODUI, 1) = "*" Then
                        Dim CODUIModificato As String = Mid(sValoreCODUI, 1, Len(sValoreCODUI) - 1)
                        stringaSQL = "SELECT SISCOM_MI.INDIRIZZI.*,SISCOM_MI.CUSTOMER_SATISFACTION.*,SISCOM_MI.UNITA_IMMOBILIARI.*,TO_CHAR(TO_DATE(DATA_COMPILAZIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_C,SISCOM_MI.CUSTOMER_SATISFACTION.ID AS ID_CUST,SEPA.OPERATORI.* FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.CUSTOMER_SATISFACTION,SISCOM_MI.UNITA_IMMOBILIARI,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloIndirizzo & controlloCivico & " SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO=SISCOM_MI.INDIRIZZI.ID AND SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA=SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & CODUIModificato & "%' AND ( "
                    Else
                        stringaSQL = "SELECT SISCOM_MI.INDIRIZZI.*,SISCOM_MI.CUSTOMER_SATISFACTION.*,SISCOM_MI.UNITA_IMMOBILIARI.*,TO_CHAR(TO_DATE(DATA_COMPILAZIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_C, SISCOM_MI.CUSTOMER_SATISFACTION.ID AS ID_CUST,SEPA.OPERATORI.* FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.CUSTOMER_SATISFACTION,SISCOM_MI.UNITA_IMMOBILIARI,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloIndirizzo & controlloCivico & " SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO=SISCOM_MI.INDIRIZZI.ID AND SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA=SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ='" & sValoreCODUI & "' AND ( "
                    End If

                Else
                    stringaSQL = "SELECT SISCOM_MI.INDIRIZZI.*,SISCOM_MI.CUSTOMER_SATISFACTION.*,SISCOM_MI.UNITA_IMMOBILIARI.*,TO_CHAR(TO_DATE(DATA_COMPILAZIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_C, SISCOM_MI.CUSTOMER_SATISFACTION.ID AS ID_CUST,SEPA.OPERATORI.* FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.CUSTOMER_SATISFACTION,SISCOM_MI.UNITA_IMMOBILIARI,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloIndirizzo & controlloCivico & " SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO=SISCOM_MI.INDIRIZZI.ID AND SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA=SISCOM_MI.UNITA_IMMOBILIARI.ID AND ( "
                End If
                Select Case sValoreDOM
                    'Nessuna domanda selezionata
                    Case "25"

                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")

                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "PU_IGIENE_VAL='" & sValoreVAL & "' OR PU_PARTI_COMUNI_VAL='" & sValoreVAL & "' OR PU_RIF_INGOMBRANTI_VAL='" _
                                & sValoreVAL & "' OR PO_INF_COMPLETE_VAL='" & sValoreVAL & "' OR PO_POSTA_VAL='" & sValoreVAL & "' OR RI_TEMPERATURA_VAL='" _
                                & sValoreVAL & "' OR RI_GUASTI_VAL='" & sValoreVAL & "' OR RI_RIS_GUASTI_VAL='" & sValoreVAL & "' OR VE_TEMPESTIVITA_VAL='" _
                                & sValoreVAL & "' OR VE_RUMORE_VAL='" & sValoreVAL & "' OR VE_SMALTIMENTO_RIF_VAL='" & sValoreVAL & "' OR PU_REGOLARITA_VAL='" _
                                & sValoreVAL & "' OR PU_QUALITA_VAL='" & sValoreVAL & "' OR PU_CORTESIA_VAL='" & sValoreVAL & "' OR PO_REGOLARITA_VAL='" _
                                & sValoreVAL & "' OR PO_QUALITA_VAL='" & sValoreVAL & "' OR PO_CORTESIA_VAL='" & sValoreVAL & "' OR RI_REGOLARITA_VAL='" _
                                & sValoreVAL & "' OR RI_QUALITA_VAL='" & sValoreVAL & "' OR RI_CORTESIA_VAL='" & sValoreVAL & "' OR VE_REGOLARITA_VAL='" _
                                & sValoreVAL & "' OR VE_QUALITA_VAL='" & sValoreVAL & "' OR VE_CORTESIA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "PU_IGIENE='" & sValoreRISP & "' OR PU_PARTI_COMUNI='" & sValoreRISP & "' OR PU_RIF_INGOMBRANTI='" _
                                & sValoreRISP & "' OR PO_INF_COMPLETE='" & sValoreRISP & "' OR PO_POSTA='" & sValoreRISP & "' OR RI_TEMPERATURA='" _
                                & sValoreRISP & "' OR RI_GUASTI='" & sValoreRISP & "' OR RI_RIS_GUASTI='" & sValoreRISP & "' OR VE_TEMPESTIVITA='" _
                                & sValoreRISP & "' OR VE_RUMORE='" & sValoreRISP & "' OR VE_SMALTIMENTO_RIF='" & sValoreRISP & "' OR PU_REGOLARITA='" _
                                & sValoreRISP & "' OR PU_QUALITA='" & sValoreRISP & "' OR PU_CORTESIA='" & sValoreRISP & "' OR PO_REGOLARITA='" _
                                & sValoreRISP & "' OR PO_QUALITA='" & sValoreRISP & "' OR PO_CORTESIA='" & sValoreRISP & "' OR RI_REGOLARITA='" _
                                & sValoreRISP & "' OR RI_QUALITA='" & sValoreRISP & "' OR RI_CORTESIA='" & sValoreRISP & "' OR VE_REGOLARITA='" _
                                & sValoreRISP & "' OR VE_QUALITA='" & sValoreRISP & "' OR VE_CORTESIA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "(PU_IGIENE='" & sValoreRISP & "' AND " & "PU_IGIENE_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PU_PARTI_COMUNI='" & sValoreRISP & "' AND " & "PU_PARTI_COMUNI_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PU_RIF_INGOMBRANTI='" & sValoreRISP & "' AND " & "PU_RIF_INGOMBRANTI_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PO_INF_COMPLETE='" & sValoreRISP & "' AND " & "PO_INF_COMPLETE_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PO_POSTA='" & sValoreRISP & "' AND " & "PO_POSTA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(RI_TEMPERATURA='" & sValoreRISP & "' AND " & "RI_TEMPERATURA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(RI_GUASTI='" & sValoreRISP & "' AND " & "RI_GUASTI_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(RI_RIS_GUASTI='" & sValoreRISP & "' AND " & "RI_RIS_GUASTI_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(VE_TEMPESTIVITA='" & sValoreRISP & "' AND " & "VE_TEMPESTIVITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(VE_RUMORE='" & sValoreRISP & "' AND " & "VE_RUMORE_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(VE_SMALTIMENTO_RIF='" & sValoreRISP & "' AND " & "VE_SMALTIMENTO_RIF_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PU_REGOLARITA='" & sValoreRISP & "' AND " & "PU_REGOLARITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PO_REGOLARITA='" & sValoreRISP & "' AND " & "PO_REGOLARITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(RI_REGOLARITA='" & sValoreRISP & "' AND " & "RI_REGOLARITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(VE_REGOLARITA='" & sValoreRISP & "' AND " & "VE_REGOLARITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PU_QUALITA='" & sValoreRISP & "' AND " & "PU_QUALITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PO_QUALITA='" & sValoreRISP & "' AND " & "PO_QUALITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(RI_QUALITA='" & sValoreRISP & "' AND " & "RI_QUALITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(VE_QUALITA='" & sValoreRISP & "' AND " & "VE_QUALITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PU_CORTESIA='" & sValoreRISP & "' AND " & "PU_CORTESIA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PO_CORTESIA='" & sValoreRISP & "' AND " & "PO_CORTESIA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(RI_CORTESIA='" & sValoreRISP & "' AND " & "RI_CORTESIA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(VE_CORTESIA='" & sValoreRISP & "' AND " & "VE_CORTESIA_VAL='" & sValoreVAL & "')"
                        End If

                    Case "26"
                        'Generico servizi di pulizia

                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "PU_IGIENE_VAL='" & sValoreVAL & "' OR PU_PARTI_COMUNI_VAL='" & sValoreVAL & _
                                "' OR PU_RIF_INGOMBRANTI_VAL='" & sValoreVAL & "' OR PU_REGOLARITA_VAL='" & sValoreVAL & _
                                "' OR PU_QUALITA_VAL='" & sValoreVAL & "' OR PU_CORTESIA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "PU_IGIENE='" & sValoreRISP & "' OR PU_PARTI_COMUNI='" & sValoreRISP & "' OR PU_RIF_INGOMBRANTI='" _
                                & sValoreRISP & "' OR PU_REGOLARITA='" & sValoreRISP & "' OR PU_QUALITA='" & sValoreRISP & "' OR PU_CORTESIA='" & sValoreRISP & "' "
                        Else
                            stringaSQL = stringaSQL & "(PU_IGIENE='" & sValoreRISP & "' AND " & "PU_IGIENE_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PU_PARTI_COMUNI='" & sValoreRISP & "' AND " & "PU_PARTI_COMUNI_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PU_RIF_INGOMBRANTI='" & sValoreRISP & "' AND " & "PU_RIF_INGOMBRANTI_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PU_REGOLARITA='" & sValoreRISP & "' AND " & "PU_REGOLARITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PU_QUALITA='" & sValoreRISP & "' AND " & "PU_QUALITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PU_CORTESIA='" & sValoreRISP & "' AND " & "PU_CORTESIA_VAL='" & sValoreVAL & "') "

                        End If

                    Case "27"
                        'Generico servizi di portierato
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "PO_INF_COMPLETE_VAL='" & sValoreVAL & "' OR PO_POSTA_VAL='" & sValoreVAL & "' OR PO_REGOLARITA_VAL='" _
                                & sValoreVAL & "' OR PO_QUALITA_VAL='" & sValoreVAL & "' OR PO_CORTESIA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "PO_INF_COMPLETE='" & sValoreRISP & "' OR PO_POSTA='" & sValoreRISP & _
                                "' OR PO_REGOLARITA='" & sValoreRISP & "' OR PO_QUALITA='" & sValoreRISP & "' OR PO_CORTESIA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "(PO_INF_COMPLETE='" & sValoreRISP & "' AND " & "PO_INF_COMPLETE_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PO_POSTA='" & sValoreRISP & "' AND " & "PO_POSTA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PO_REGOLARITA='" & sValoreRISP & "' AND " & "PO_REGOLARITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PO_QUALITA='" & sValoreRISP & "' AND " & "PO_QUALITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PO_CORTESIA='" & sValoreRISP & "' AND " & "PO_CORTESIA_VAL='" & sValoreVAL & "') "

                        End If


                    Case "28"
                        'Generico servizi di riscaldamento
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "RI_TEMPERATURA_VAL='" & sValoreVAL & "' OR RI_GUASTI_VAL='" & sValoreVAL & "' OR RI_RIS_GUASTI_VAL='" _
                                & sValoreVAL & "' OR RI_REGOLARITA_VAL='" & sValoreVAL & "' OR RI_QUALITA_VAL='" & sValoreVAL & "' OR RI_CORTESIA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "RI_TEMPERATURA='" & sValoreRISP & "' OR RI_GUASTI='" & sValoreRISP & "' OR RI_RIS_GUASTI='" _
                                & sValoreRISP & "' OR RI_REGOLARITA='" & sValoreRISP & "' OR RI_QUALITA='" & sValoreRISP & "' OR RI_CORTESIA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "(RI_TEMPERATURA='" & sValoreRISP & "' AND " & "RI_TEMPERATURA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(RI_GUASTI='" & sValoreRISP & "' AND " & "RI_GUASTI_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(RI_RIS_GUASTI='" & sValoreRISP & "' AND " & "RI_RIS_GUASTI_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(RI_REGOLARITA='" & sValoreRISP & "' AND " & "RI_REGOLARITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(RI_QUALITA='" & sValoreRISP & "' AND " & "RI_QUALITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(RI_CORTESIA='" & sValoreRISP & "' AND " & "RI_CORTESIA_VAL='" & sValoreVAL & "') "
                        End If

                    Case "29"
                        'Generico servizi di manutenzione del verde
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "VE_TEMPESTIVITA_VAL='" & sValoreVAL & "' OR VE_RUMORE_VAL='" & sValoreVAL & "' OR VE_SMALTIMENTO_RIF_VAL='" _
                                & sValoreVAL & "' OR VE_REGOLARITA_VAL='" & sValoreVAL & "' OR VE_QUALITA_VAL='" & sValoreVAL & "' OR VE_CORTESIA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "VE_TEMPESTIVITA='" & sValoreRISP & "' OR VE_RUMORE='" & sValoreRISP & "' OR VE_SMALTIMENTO_RIF='" & sValoreRISP & _
                                "' OR VE_REGOLARITA='" & sValoreRISP & "' OR VE_QUALITA='" & sValoreRISP & "' OR VE_CORTESIA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "(VE_TEMPESTIVITA='" & sValoreRISP & "' AND " & "VE_TEMPESTIVITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(VE_RUMORE='" & sValoreRISP & "' AND " & "VE_RUMORE_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(VE_SMALTIMENTO_RIF='" & sValoreRISP & "' AND " & "VE_SMALTIMENTO_RIF_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(VE_REGOLARITA='" & sValoreRISP & "' AND " & "VE_REGOLARITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(VE_QUALITA='" & sValoreRISP & "' AND " & "VE_QUALITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(VE_CORTESIA='" & sValoreRISP & "' AND " & "VE_CORTESIA_VAL='" & sValoreVAL & "') "

                        End If

                    Case "0"
                        'Regolarità generica
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "PU_REGOLARITA_VAL='" & sValoreVAL & "' OR PO_REGOLARITA_VAL='" & sValoreVAL & "' OR RI_REGOLARITA_VAL='" _
                                & sValoreVAL & "' OR VE_REGOLARITA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "PU_REGOLARITA='" & sValoreRISP & "' OR PO_REGOLARITA='" & sValoreRISP & "' OR RI_REGOLARITA='" _
                                & sValoreRISP & "' OR VE_REGOLARITA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "(PU_REGOLARITA='" & sValoreRISP & "' AND " & "PU_REGOLARITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PO_REGOLARITA='" & sValoreRISP & "' AND " & "PO_REGOLARITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(RI_REGOLARITA='" & sValoreRISP & "' AND " & "RI_REGOLARITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(VE_REGOLARITA='" & sValoreRISP & "' AND " & "VE_REGOLARITA_VAL='" & sValoreVAL & "')"
                        End If


                    Case "1"
                        'Qualità generica
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "PU_QUALITA_VAL='" & sValoreVAL & "' OR PO_QUALITA_VAL='" & sValoreVAL & "' OR RI_QUALITA_VAL='" _
                                & sValoreVAL & "' OR VE_QUALITA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "PU_QUALITA='" & sValoreRISP & "' OR PO_QUALITA='" & sValoreRISP & "' OR RI_QUALITA='" & sValoreRISP & _
                                "' OR VE_QUALITA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "(PU_QUALITA='" & sValoreRISP & "' AND " & "PU_QUALITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PO_QUALITA='" & sValoreRISP & "' AND " & "PO_QUALITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(RI_QUALITA='" & sValoreRISP & "' AND " & "RI_QUALITA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(VE_QUALITA='" & sValoreRISP & "' AND " & "VE_QUALITA_VAL='" & sValoreVAL & "')"
                        End If

                    Case "2"
                        'Cortesia generica
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "PU_CORTESIA_VAL='" & sValoreVAL & "' OR PO_CORTESIA_VAL='" & sValoreVAL & "' OR RI_CORTESIA_VAL='" _
                                & sValoreVAL & "' OR VE_CORTESIA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "PU_CORTESIA='" & sValoreRISP & "' OR PO_CORTESIA='" & sValoreRISP & "' OR RI_CORTESIA='" _
                                & sValoreRISP & "' OR VE_CORTESIA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "(PU_CORTESIA='" & sValoreRISP & "' AND " & "PU_CORTESIA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(PO_CORTESIA='" & sValoreRISP & "' AND " & "PO_CORTESIA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(RI_CORTESIA='" & sValoreRISP & "' AND " & "RI_CORTESIA_VAL='" & sValoreVAL & "') OR "
                            stringaSQL = stringaSQL & "(VE_CORTESIA='" & sValoreRISP & "' AND " & "VE_CORTESIA_VAL='" & sValoreVAL & "')"
                        End If

                    Case "3"
                        'Regolarità pulizia
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "PU_REGOLARITA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "PU_REGOLARITA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "PU_REGOLARITA='" & sValoreRISP & "' AND " & "PU_REGOLARITA_VAL='" & sValoreVAL & "'"
                        End If

                    Case "4"
                        'Qualità pulizia
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "PU_QUALITA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "PU_QUALITA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "PU_QUALITA='" & sValoreRISP & "' AND " & "PU_QUALITA_VAL='" & sValoreVAL & "'"
                        End If

                    Case "5"
                        'Cortesia pulizia
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "PU_CORTESIA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "PU_CORTESIA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "PU_CORTESIA='" & sValoreRISP & "' AND " & "PU_CORTESIA_VAL='" & sValoreVAL & "'"
                        End If

                    Case "6"
                        'Regolarità portierato
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "PO_REGOLARITA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "PO_REGOLARITA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "PO_REGOLARITA='" & sValoreRISP & "' AND " & "PO_REGOLARITA_VAL='" & sValoreVAL & "'"
                        End If

                    Case "7"
                        'Qualità 

                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "PO_QUALITA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "PO_QUALITA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "PO_QUALITA='" & sValoreRISP & "' AND " & "PO_QUALITA_VAL='" & sValoreVAL & "'"
                        End If

                    Case "8"
                        'Cortesia portierato
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "PO_CORTESIA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "PO_CORTESIA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "PO_CORTESIA='" & sValoreRISP & "' AND " & "PO_CORTESIA_VAL='" & sValoreVAL & "'"
                        End If

                    Case "9"
                        'Regolarità riscaldamento
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "RI_REGOLARITA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "RI_REGOLARITA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "RI_REGOLARITA='" & sValoreRISP & "' AND " & "RI_REGOLARITA_VAL='" & sValoreVAL & "'"
                        End If

                    Case "10"
                        'Qualità riscaldamento
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "RI_QUALITA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "RI_QUALITA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "RI_QUALITA='" & sValoreRISP & "' AND " & "RI_QUALITA_VAL='" & sValoreVAL & "'"
                        End If

                    Case "11"
                        'Cortesia riscaldamento
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "RI_CORTESIA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "RI_CORTESIA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "RI_CORTESIA='" & sValoreRISP & "' AND " & "RI_CORTESIA_VAL='" & sValoreVAL & "'"
                        End If

                    Case "12"
                        'Regolarità manutenzione del verde
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "VE_REGOLARITA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "VE_REGOLARITA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "VE_REGOLARITA='" & sValoreRISP & "' AND " & "VE_REGOLARITA_VAL='" & sValoreVAL & "'"
                        End If

                    Case "13"
                        'Qualità manutenzione del verde
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "VE_QUALITA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "VE_QUALITA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "VE_QUALITA='" & sValoreRISP & "' AND " & "VE_QUALITA_VAL='" & sValoreVAL & "'"
                        End If

                    Case "14"
                        'Cortesia manutenzione del verde
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "VE_CORTESIA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "VE_CORTESIA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "VE_CORTESIA='" & sValoreRISP & "' AND " & "VE_CORTESIA_VAL='" & sValoreVAL & "'"
                        End If

                    Case "32"
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "PU_IGIENE_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "PU_IGIENE='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "PU_IGIENE='" & sValoreRISP & "' AND " & "PU_IGIENE_VAL='" & sValoreVAL & "'"
                        End If

                    Case "33"
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "PU_PARTI_COMUNI_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "PU_PARTI_COMUNI='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "PU_PARTI_COMUNI='" & sValoreRISP & "' AND " & "PU_PARTI_COMUNI_VAL='" & sValoreVAL & "'"
                        End If

                    Case "34"
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "PU_RIF_INGOMBRANTI_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "PU_RIF_INGOMBRANTI='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "PU_RIF_INGOMBRANTI='" & sValoreRISP & "' AND " & "PU_RIF_INGOMBRANTI_VAL='" & sValoreVAL & "'"
                        End If

                    Case "17"
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "PO_INF_COMPLETE_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "PO_INF_COMPLETE='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "PO_INF_COMPLETE='" & sValoreRISP & "' AND " & "PO_INF_COMPLETE_VAL='" & sValoreVAL & "'"
                        End If

                    Case "18"
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "PO_POSTA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "PO_POSTA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "PO_POSTA='" & sValoreRISP & "' AND " & "PO_POSTA_VAL='" & sValoreVAL & "'"
                        End If

                    Case "19"
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "RI_TEMPERATURA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "RI_TEMPERATURA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "RI_TEMPERATURA='" & sValoreRISP & "' AND " & "RI_TEMPERATURA_VAL='" & sValoreVAL & "'"
                        End If

                    Case "20"
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "RI_GUASTI_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "RI_GUASTI='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "RI_GUASTI='" & sValoreRISP & "' AND " & "RI_GUASTI_VAL='" & sValoreVAL & "'"
                        End If

                    Case "21"
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "RI_RIS_GUASTI_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "RI_RIS_GUASTI='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "RI_RIS_GUASTI='" & sValoreRISP & "' AND " & "RI_RIS_GUASTI_VAL='" & sValoreVAL & "'"
                        End If

                    Case "22"
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "VE_TEMPESTIVITA_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "VE_TEMPESTIVITA='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "VE_TEMPESTIVITA='" & sValoreRISP & "' AND " & "VE_TEMPESTIVITA_VAL='" & sValoreVAL & "'"
                        End If

                    Case "23"
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "VE_RUMORE_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "VE_RUMORE='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "VE_RUMORE='" & sValoreRISP & "' AND " & "VE_RUMORE_VAL='" & sValoreVAL & "'"
                        End If

                    Case "24"
                        If sValoreRISP = "---" And sValoreVAL = "---" Then
                            'Response.Write("<script>alert('Selezionare almeno uno dei campi ""Risposta"" o ""Valore""');</script>")
                            'Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
                        ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                            stringaSQL = stringaSQL & "VE_SMALTIMENTO_RIF_VAL='" & sValoreVAL & "'"
                        ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                            stringaSQL = stringaSQL & "VE_SMALTIMENTO_RIF='" & sValoreRISP & "'"
                        Else
                            stringaSQL = stringaSQL & "VE_SMALTIMENTO_RIF='" & sValoreRISP & "' AND " & "VE_SMALTIMENTO_RIF_VAL='" & sValoreVAL & "'"
                        End If

                End Select
                stringaSQL = stringaSQL & ") ORDER BY SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA ASC "

            End If

            lblNumRis.Text = ""
            'Label6.Text = stringaSQL
            'btnVisualizza.Attributes.Add("onclick", "this.style.visibility='hidden'")
            'LBLID.Value = "-1"

            BindGrid(sValoreDOM)
            If contaris.Value <> 0 Then
                disegnaGrafici()
            End If

            'If Session.Item("LIVELLO") = "1" Then
            '    btnExport.Visible = True
            'End If

            If Session.Item("CUSTOM_SAT_L") = 1 Then
                btnElimina.Enabled = False
            End If

        End If
    End Sub

    Protected Sub apriConnessione()
        Try
            If par.OracleConn.State = 0 Then
                par.OracleConn.Open()
            End If
            par.cmd = par.OracleConn.CreateCommand
        Catch ex As Exception
            Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
        End Try

    End Sub

    Protected Sub chiudiConnessione()
        Try
            If par.OracleConn.State = 1 Then

                par.cmd.Dispose()
                par.OracleConn.Close()

            End If
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
        End Try

    End Sub

    Private Sub BindGrid(ByVal domanda As String)
        Try

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(stringaSQL, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Datagrid2.DataSource = dt
            Dim conta As Integer = 0
            conta = dt.Rows.Count
            'For Each riga As Data.DataRow In dt.Rows
            '    conta = conta + 1
            'Next
            If conta = 0 Then
                Label4.Text = "La ricerca non ha prodotto risultati"
                Datagrid2.Visible = False
                ImmagineStatistiche.Visible = False
                btnExport.Visible = False
                btnElimina.Visible = False
                btnVisualizza.Visible = False
                txtmia.Text = ""
            Else
                Datagrid2.Visible = True
                If conta = 1 Then
                    lblNumRis.Text = "Trovato un questionario"
                Else
                    lblNumRis.Text = "Trovati " & conta & " questionari"
                End If
                Datagrid2.DataBind()
                domandaSel.Value = sValoreDOM

                Session.Item("QUERY_ESPORTAZIONE_QUESTIONARI") = stringaSQL
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            contaris.Value = conta
        Catch ex As Exception
            par.OracleConn.Close()
            TextBox3.Text = ex.Message
        End Try
    End Sub

    Protected Sub Datagrid2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid2.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white';}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la scheda con Codice UI: " & e.Item.Cells(0).Text & "';document.getElementById('LBL_CODUI').value='" & e.Item.Cells(0).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(110).Text & "';document.getElementById('LBL_idU').value='" & e.Item.Cells(111).Text & "';document.getElementById('LBL_INDIR').value='" & e.Item.Cells(112).Text & "';document.getElementById('LBL_DATA').value='" & e.Item.Cells(6).Text & "';")
        End If

        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro';}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la scheda con Codice UI: " & e.Item.Cells(0).Text & "';document.getElementById('LBL_CODUI').value='" & e.Item.Cells(0).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(110).Text & "';document.getElementById('LBL_idU').value='" & e.Item.Cells(111).Text & "';document.getElementById('LBL_INDIR').value='" & e.Item.Cells(112).Text & "';document.getElementById('LBL_DATA').value='" & e.Item.Cells(6).Text & "';")
        End If

    End Sub

    Protected Sub Datagrid2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid2.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            Datagrid2.CurrentPageIndex = e.NewPageIndex
            BindGrid(domandaSel.Value)
            If contaris.Value <> 0 Then
                disegnaGrafici()
            End If

        End If

    End Sub

    Protected Sub disegnaGrafici()
        'MsgBox(Session.Item("ID_UNITA_IMMOBILIARE"))
        Chart1.Series.Clear()
        Chart2.Series.Clear()
        Chart1.Legends.Clear()
        Chart2.Legends.Clear()

        Select Case sValoreDOM
            Case "3"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("PU_REGOLARITA", 1)
                    creaGrafico("PU_REGOLARITA_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("PU_REGOLARITA", 1)
                    creaGraficoConValore("PU_REGOLARITA_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("PU_REGOLARITA", 1)
                    creaGraficoConRisposta("PU_REGOLARITA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PU_REGOLARITA", 1)
                    creaGraficoConValoreRisposta("PU_REGOLARITA_VAL", 2)
                End If

            Case "4"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("PU_QUALITA", 1)
                    creaGrafico("PU_QUALITA_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("PU_QUALITA", 1)
                    creaGraficoConValore("PU_QUALITA_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("PU_QUALITA", 1)
                    creaGraficoConRisposta("PU_QUALITA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PU_QUALITA", 1)
                    creaGraficoConValoreRisposta("PU_QUALITA_VAL", 2)
                End If

            Case "5"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("PU_CORTESIA", 1)
                    creaGrafico("PU_CORTESIA_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("PU_CORTESIA", 1)
                    creaGraficoConValore("PU_CORTESIA_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("PU_CORTESIA", 1)
                    creaGraficoConRisposta("PU_CORTESIA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PU_CORTESIA", 1)
                    creaGraficoConValoreRisposta("PU_CORTESIA_VAL", 2)
                End If

            Case "6"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("PO_REGOLARITA", 1)
                    creaGrafico("PO_REGOLARITA_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("PO_REGOLARITA", 1)
                    creaGraficoConValore("PO_REGOLARITA_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("PO_REGOLARITA", 1)
                    creaGraficoConRisposta("PO_REGOLARITA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PO_REGOLARITA", 1)
                    creaGraficoConValoreRisposta("PO_REGOLARITA_VAL", 2)
                End If

            Case "7"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("PO_QUALITA", 1)
                    creaGrafico("PO_QUALITA_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("PO_QUALITA", 1)
                    creaGraficoConValore("PO_QUALITA_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("PO_QUALITA", 1)
                    creaGraficoConRisposta("PO_QUALITA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PO_QUALITA", 1)
                    creaGraficoConValoreRisposta("PO_QUALITA_VAL", 2)
                End If

            Case "8"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("PO_CORTESIA", 1)
                    creaGrafico("PO_CORTESIA_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("PO_CORTESIA", 1)
                    creaGraficoConValore("PO_CORTESIA_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("PO_CORTESIA", 1)
                    creaGraficoConRisposta("PO_CORTESIA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PO_CORTESIA", 1)
                    creaGraficoConValoreRisposta("PO_CORTESIA_VAL", 2)
                End If

            Case "9"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("RI_REGOLARITA", 1)
                    creaGrafico("RI_REGOLARITA_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("RI_REGOLARITA", 1)
                    creaGraficoConValore("RI_REGOLARITA_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("RI_REGOLARITA", 1)
                    creaGraficoConRisposta("RI_REGOLARITA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("RI_REGOLARITA", 1)
                    creaGraficoConValoreRisposta("RI_REGOLARITA_VAL", 2)
                End If

            Case "10"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("RI_QUALITA", 1)
                    creaGrafico("RI_QUALITA_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("RI_QUALITA", 1)
                    creaGraficoConValore("RI_QUALITA_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("RI_QUALITA", 1)
                    creaGraficoConRisposta("RI_QUALITA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("RI_QUALITA", 1)
                    creaGraficoConValoreRisposta("RI_QUALITA_VAL", 2)
                End If

            Case "11"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("RI_CORTESIA", 1)
                    creaGrafico("RI_CORTESIA_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("RI_CORTESIA", 1)
                    creaGraficoConValore("RI_CORTESIA_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("RI_CORTESIA", 1)
                    creaGraficoConRisposta("RI_CORTESIA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("RI_CORTESIA", 1)
                    creaGraficoConValoreRisposta("RI_CORTESIA_VAL", 2)
                End If

            Case "12"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("VE_REGOLARITA", 1)
                    creaGrafico("VE_REGOLARITA_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("VE_REGOLARITA", 1)
                    creaGraficoConValore("VE_REGOLARITA_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("VE_REGOLARITA", 1)
                    creaGraficoConRisposta("VE_REGOLARITA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("VE_REGOLARITA", 1)
                    creaGraficoConValoreRisposta("VE_REGOLARITA_VAL", 2)
                End If

            Case "13"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("VE_QUALITA", 1)
                    creaGrafico("VE_QUALITA_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("VE_QUALITA", 1)
                    creaGraficoConValore("VE_QUALITA_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("VE_QUALITA", 1)
                    creaGraficoConRisposta("VE_QUALITA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("VE_QUALITA", 1)
                    creaGraficoConValoreRisposta("VE_QUALITA_VAL", 2)
                End If

            Case "14"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("VE_CORTESIA", 1)
                    creaGrafico("VE_CORTESIA_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("VE_CORTESIA", 1)
                    creaGraficoConValore("VE_CORTESIA_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("VE_CORTESIA", 1)
                    creaGraficoConRisposta("VE_CORTESIA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("VE_CORTESIA", 1)
                    creaGraficoConValoreRisposta("VE_CORTESIA_VAL", 2)
                End If

            Case "17"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("PO_INF_COMPLETE", 1)
                    creaGrafico("PO_INF_COMPLETE_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("PO_INF_COMPLETE", 1)
                    creaGraficoConValore("PO_INF_COMPLETE_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("PO_INF_COMPLETE", 1)
                    creaGraficoConRisposta("PO_INF_COMPLETE_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PO_INF_COMPLETE", 1)
                    creaGraficoConValoreRisposta("PO_INF_COMPLETE_VAL", 2)
                End If

            Case "18"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("PO_POSTA", 1)
                    creaGrafico("PO_POSTA_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("PO_POSTA", 1)
                    creaGraficoConValore("PO_POSTA_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("PO_POSTA", 1)
                    creaGraficoConRisposta("PO_POSTA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PO_POSTA", 1)
                    creaGraficoConValoreRisposta("PO_POSTA_VAL", 2)
                End If

            Case "19"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("RI_TEMPERATURA", 1)
                    creaGrafico("RI_TEMPERATURA_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("RI_TEMPERATURA", 1)
                    creaGraficoConValore("RI_TEMPERATURA_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("RI_TEMPERATURA", 1)
                    creaGraficoConRisposta("RI_TEMPERATURA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("RI_TEMPERATURA", 1)
                    creaGraficoConValoreRisposta("RI_TEMPERATURA_VAL", 2)
                End If

            Case "20"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("RI_GUASTI", 1)
                    creaGrafico("RI_GUASTI_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("RI_GUASTI", 1)
                    creaGraficoConValore("RI_GUASTI_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("RI_GUASTI", 1)
                    creaGraficoConRisposta("RI_GUASTI_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("RI_GUASTI", 1)
                    creaGraficoConValoreRisposta("RI_GUASTI_VAL", 2)
                End If

            Case "21"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("RI_RIS_GUASTI", 1)
                    creaGrafico("RI_RIS_GUASTI_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("RI_RIS_GUASTI", 1)
                    creaGraficoConValore("RI_RIS_GUASTI_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("RI_RIS_GUASTI", 1)
                    creaGraficoConRisposta("RI_RIS_GUASTI_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("RI_RIS_GUASTI", 1)
                    creaGraficoConValoreRisposta("RI_RIS_GUASTI_VAL", 2)
                End If

            Case "22"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("VE_TEMPESTIVITA", 1)
                    creaGrafico("VE_TEMPESTIVITA_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("VE_TEMPESTIVITA", 1)
                    creaGraficoConValore("VE_TEMPESTIVITA_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("VE_TEMPESTIVITA", 1)
                    creaGraficoConRisposta("VE_TEMPESTIVITA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("VE_TEMPESTIVITA", 1)
                    creaGraficoConValoreRisposta("VE_TEMPESTIVITA_VAL", 2)
                End If

            Case "23"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("VE_RUMORE", 1)
                    creaGrafico("VE_RUMORE_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("VE_RUMORE", 1)
                    creaGraficoConValore("VE_RUMORE_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("VE_RUMORE", 1)
                    creaGraficoConRisposta("VE_RUMORE_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("VE_RUMORE", 1)
                    creaGraficoConValoreRisposta("VE_RUMORE_VAL", 2)
                End If

            Case "24"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("VE_SMALTIMENTO_RIF", 1)
                    creaGrafico("VE_SMALTIMENTO_RIF_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("VE_SMALTIMENTO_RIF", 1)
                    creaGraficoConValore("VE_SMALTIMENTO_RIF_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("VE_SMALTIMENTO_RIF", 1)
                    creaGraficoConRisposta("VE_SMALTIMENTO_RIF_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("VE_SMALTIMENTO_RIF", 1)
                    creaGraficoConValoreRisposta("VE_SMALTIMENTO_RIF_VAL", 2)
                End If

            Case "32"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("PU_IGIENE", 1)
                    creaGrafico("PU_IGIENE_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("PU_IGIENE", 1)
                    creaGraficoConValore("PU_IGIENE_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("PU_IGIENE", 1)
                    creaGraficoConRisposta("PU_IGIENE_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PU_IGIENE", 1)
                    creaGraficoConValoreRisposta("PU_IGIENE_VAL", 2)
                End If

            Case "33"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("PU_PARTI_COMUNI", 1)
                    creaGrafico("PU_PARTI_COMUNI_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("PU_PARTI_COMUNI", 1)
                    creaGraficoConValore("PU_PARTI_COMUNI_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("PU_PARTI_COMUNI", 1)
                    creaGraficoConRisposta("PU_PARTI_COMUNI_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PU_PARTI_COMUNI", 1)
                    creaGraficoConValoreRisposta("PU_PARTI_COMUNI_VAL", 2)
                End If

            Case "34"

                If sValoreRISP = "---" And sValoreVAL = "---" Then
                    creaGrafico("PU_RIF_INGOMBRANTI", 1)
                    creaGrafico("PU_RIF_INGOMBRANTI_VAL", 2)
                ElseIf sValoreRISP = "---" And sValoreVAL <> "---" Then
                    creaGraficoConValore("PU_RIF_INGOMBRANTI", 1)
                    creaGraficoConValore("PU_RIF_INGOMBRANTI_VAL", 2)
                ElseIf sValoreRISP <> "---" And sValoreVAL = "---" Then
                    creaGraficoConRisposta("PU_RIF_INGOMBRANTI", 1)
                    creaGraficoConRisposta("PU_RIF_INGOMBRANTI_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PU_RIF_INGOMBRANTI", 1)
                    creaGraficoConValoreRisposta("PU_RIF_INGOMBRANTI_VAL", 2)
                End If

            Case "26"

                'servizi di pulizia generici
                creaGraficoServiziPulizia(sValoreRISP, sValoreVAL)

            Case "27"

                'servizi di portierato generici
                creaGraficoServiziPortierato(sValoreRISP, sValoreVAL)

            Case "28"

                'servizi di riscaldamento generici
                creaGraficoServiziRiscaldamento(sValoreRISP, sValoreVAL)

            Case "29"

                'servizi di manutenzione generici
                creaGraficoServiziManutenzione(sValoreRISP, sValoreVAL)

            Case "0"

                'regolarità generica
                creaGraficoRegolarita(sValoreRISP, sValoreVAL)

            Case "1"

                'qualità generica
                creaGraficoQualita(sValoreRISP, sValoreVAL)

            Case "2"

                'cortesia generica
                creaGraficoCortesia(sValoreRISP, sValoreVAL)

            Case "25"

                'Nessuna domanda selezionata
                creaGraficoNessunaDomanda(sValoreRISP, sValoreVAL)

            Case Else
        End Select
    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click


        Dim myExcelFile As New CM.ExcelFile
        Dim i As Long
        Dim K As Long
        Dim row As System.Data.DataRow
        Dim dt As New Data.DataTable
        Dim par As New CM.Global
        Dim FileXLS As String = ""
        stringaSQL = Session.Item("QUERY_ESPORTAZIONE_QUESTIONARI")
        Try
            par.OracleConn.Open()
            FileXLS = "Questionario" & Format(Now, "yyyyMMddHHmm")
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            da = New Oracle.DataAccess.Client.OracleDataAdapter(stringaSQL, par.OracleConn)
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                i = 0
                With myExcelFile
                    .CreateFile(Server.MapPath("..\FileTemp\" & FileXLS & ".xls"))
                    .PrintGridLines = False
                    .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                    .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                    .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                    .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                    .SetDefaultRowHeight(14)
                    .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                    .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                    .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                    .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)
                    .SetColumnWidth(1, 1, 64)
                    .SetColumnWidth(2, 2, 64)
                    .SetColumnWidth(3, 3, 17)
                    .SetColumnWidth(4, 53, 64)
                    .SetColumnWidth(54, 55, 73)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "CODICE UNITA' IMMOBILIARE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "INDIRIZZO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "CIVICO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "DATA DI COMPILAZIONE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "REGOLARITA' PULIZIA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "REGOLARITA' PULIZIA (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "QUALITA' PULIZIA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "QUALITA' PULIZIA (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "CORTESIA PERSONALE DI PULIZIA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "CORTESIA PERSONALE DI PULIZIA (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "IGIENE PULIZIA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "IGIENE PULIZIA (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "ADEGUATEZZA PULIZIA PIAZZALI", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "ADEGUATEZZA PULIZIA PIAZZALI (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "RIMOZIONE RIFIUTI INGOMBRANTI", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "RIMOZIONE RIFIUTI INGOMBRANTI (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 17, "SUGGERIMENTI PER MIGLIORARE IL SERVIZIO DI PULIZIA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 18, "REGOLARITA' PORTIERATO ", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 19, "REGOLARITA' PORTIERATO (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 20, "QUALITA' PORTIERATO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 21, "QUALITA' PORTIERATO (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 22, "CORTESIA PERSONALE DI PORTIERATO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 23, "CORTESIA PERSONALE DI PORTIERATO (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 24, "INFORMAZIONI COMPLETE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 25, "INFORMAZIONI COMPLETE (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 26, "GESTIONE DELLA POSTA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 27, "GESTIONE DELLA POSTA (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 28, "SUGGERIMENTI PER MIGLIORARE IL SERVIZIO DI PORTIERATO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 29, "REGOLARITA' RISCALDAMENTO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 30, "REGOLARITA' RISCALDAMENTO (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 31, "QUALITA' RISCALDAMENTO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 32, "QUALITA' RISCALDAMENTO (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 33, "CORTESIA PERSONALE DI RISCALDAMENTO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 34, "CORTESIA PERSONALE DI RISCALDAMENTO (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 35, "TEMPERATURA RISCALDAMENTO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 36, "TEMPERATURA RISCALDAMENTO (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 37, "PRONTO INTERVENTO AI GUASTI", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 38, "PRONTO INTERVENTO AI GUASTI (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 39, "RISOLUZIONE TEMPESTIVA GUASTI", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 40, "RISOLUZIONE TEMPESTIVA GUASTI (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 41, "SUGGERIMENTI PER MIGLIORARE IL SERVIZIO DI RISCALDAMENTO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 42, "REGOLARITA' MANUTENZIONE DEL VERDE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 43, "REGOLARITA' MANUTENZIONE DEL VERDE (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 44, "QUALITA' MANUTENZIONE DEL VERDE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 45, "QUALITA' MANUTENZIONE DEL VERDE (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 46, "CORTESIA PERSONALE DI MANUTENZIONE DEL VERDE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 47, "CORTESIA PERSONALE DI MANUTENZIONE DEL VERDE (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 48, "TEMPESTIVITA' MANUTENZIONE DEL VERDE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 49, "TEMPESTIVITA' MANUTENZIONE DEL VERDE (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 50, "MACCHINARI RUMOROSI", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 51, "MACCHINARI RUMOROSI (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 52, "SMALTIMENTO RIFIUTI", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 53, "SMALTIMENTO RIFIUTI (VAL)", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 54, "SUGGERIMENTI PER MIGLIORARE IL SERVIZIO DI MANUTENZIONE DEL VERDE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 55, "GIUDIZIO COMPLESSIVO", 12)

                    K = 2
                    For Each row In dt.Rows
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_UNITA_IMMOBILIARE"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DESCRIZIONE"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CIVICO"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_C"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PU_REGOLARITA"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PU_REGOLARITA_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PU_QUALITA"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PU_QUALITA_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PU_CORTESIA"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PU_CORTESIA_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PU_IGIENE"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PU_IGIENE_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PU_PARTI_COMUNI"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PU_PARTI_COMUNI_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PU_RIF_INGOMBRANTI"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PU_RIF_INGOMBRANTI_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PU_SUGGERIMENTI"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PO_REGOLARITA"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PO_REGOLARITA_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PO_QUALITA"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PO_QUALITA_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 22, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PO_CORTESIA"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 23, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PO_CORTESIA_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 24, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PO_INF_COMPLETE"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 25, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PO_INF_COMPLETE_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 26, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PO_POSTA"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 27, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PO_POSTA_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 28, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PO_SUGGERIMENTI"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 29, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RI_REGOLARITA"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 30, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RI_REGOLARITA_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 31, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RI_QUALITA"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 32, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RI_QUALITA_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 33, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RI_CORTESIA"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 34, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RI_CORTESIA_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 35, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RI_TEMPERATURA"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 36, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RI_TEMPERATURA_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 37, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RI_GUASTI"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 38, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RI_GUASTI_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 39, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RI_RIS_GUASTI"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 40, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RI_RIS_GUASTI_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 41, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RI_SUGGERIMENTI"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 42, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("VE_REGOLARITA"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 43, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("VE_REGOLARITA_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 44, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("VE_QUALITA"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 45, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("VE_QUALITA_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 46, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("VE_CORTESIA"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 47, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("VE_CORTESIA_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 48, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("VE_TEMPESTIVITA"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 49, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("VE_TEMPESTIVITA_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 50, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("VE_RUMORE"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 51, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("VE_RUMORE_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 52, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("VE_SMALTIMENTO_RIF"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 53, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("VE_SMALTIMENTO_RIF_VAL"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 54, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("VE_SUGGERIMENTI"), " ")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 55, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("GIUDIZIO_COMPLESSIVO"), " ")))
                        i = i + 1
                        K = K + 1
                    Next
                    .CloseFile()
                End With

                Dim objCrc32 As New Crc32()
                Dim strmZipOutputStream As ZipOutputStream
                Dim zipfic As String
                zipfic = Server.MapPath("..\FileTemp\" & FileXLS & ".zip")
                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)
                Dim strFile As String
                strFile = Server.MapPath("..\FileTemp\" & FileXLS & ".xls")
                Dim strmFile As FileStream = File.OpenRead(strFile)
                Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
                strmFile.Read(abyBuffer, 0, abyBuffer.Length)
                Dim sFile As String = Path.GetFileName(strFile)
                Dim theEntry As ZipEntry = New ZipEntry(sFile)
                Dim fi As New FileInfo(strFile)
                theEntry.DateTime = fi.LastWriteTime
                theEntry.Size = strmFile.Length
                strmFile.Close()
                objCrc32.Reset()
                objCrc32.Update(abyBuffer)
                theEntry.Crc = objCrc32.Value
                strmZipOutputStream.PutNextEntry(theEntry)
                strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()
                File.Delete(strFile)
                Response.Redirect("..\FileTemp\" & FileXLS & ".zip")
            End If
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click

        If LBLID.Value = "" Then
            Response.Write("<script>alert('Nessuna scheda selezionata!');</script>")
        Else
            Response.Redirect("SchedaQuestionario.aspx?id=" & LBLID.Value & "&idU=" & LBL_idU.Value & "&codUI=" & LBL_CODUI.Value & "&data=" & LBL_DATA.Value & "&idInd=" & LBL_INDIR.Value)
        End If
        If contaris.Value <> 0 Then
            disegnaGrafici()
        End If


    End Sub

    Protected Sub creaGraficoNessunaDomanda(ByVal risposta As String, ByVal valore As String)
        apriConnessione()
        Dim codiceUnitaImmobiliare As String = Trim(sValoreCODUI)
        Dim controlloIndirizzo As String = ""
        Dim controlloCivico As String = ""

        If Not sValoreIndirizzo = "---" Then
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE='" & Replace(sValoreIndirizzo, "'", "''") & "' AND "
        Else
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE LIKE '%' AND "
        End If
        If Not sValoreCivico = "---" Then
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO='" & Replace(sValoreCivico, "'", "''") & "' AND "
        Else
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO LIKE '%' AND "
        End If

        Dim query As String = ""

        If risposta = "---" And valore = "---" Then

            If codiceUnitaImmobiliare = "" Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA "
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' "
                Else
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' "
                End If
            End If

        ElseIf risposta <> "---" And valore = "---" Then

            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If


            query = query & "PU_REGOLARITA='" & risposta & "' OR "
            query = query & "PU_QUALITA='" & risposta & "' OR "
            query = query & "PU_CORTESIA='" & risposta & "' OR "
            query = query & "PU_IGIENE='" & risposta & "' OR "
            query = query & "PU_PARTI_COMUNI='" & risposta & "' OR "
            query = query & "PU_RIF_INGOMBRANTI='" & risposta & "' OR "
            query = query & "PO_REGOLARITA='" & risposta & "' OR "
            query = query & "PO_QUALITA='" & risposta & "' OR "
            query = query & "PO_CORTESIA='" & risposta & "' OR "
            query = query & "PO_POSTA='" & risposta & "' OR "
            query = query & "PO_INF_COMPLETE='" & risposta & "' OR "
            query = query & "RI_REGOLARITA='" & risposta & "' OR "
            query = query & "RI_QUALITA='" & risposta & "' OR "
            query = query & "RI_CORTESIA='" & risposta & "' OR "
            query = query & "RI_TEMPERATURA='" & risposta & "' OR "
            query = query & "RI_GUASTI='" & risposta & "' OR "
            query = query & "RI_RIS_GUASTI='" & risposta & "' OR "
            query = query & "VE_REGOLARITA='" & risposta & "' OR "
            query = query & "VE_QUALITA='" & risposta & "' OR "
            query = query & "VE_CORTESIA='" & risposta & "' OR "
            query = query & "VE_TEMPESTIVITA='" & risposta & "' OR "
            query = query & "VE_SMALTIMENTO_RIF='" & risposta & "' OR "
            query = query & "VE_RUMORE='" & risposta & "') "
            'MsgBox(query)

            par.cmd.CommandText = query

        ElseIf risposta = "---" And valore <> "---" Then

            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION WHERE,SEPA.OPERATORI " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If

            query = query & "PU_REGOLARITA_VAL='" & valore & "' OR "
            query = query & "PU_QUALITA_VAL='" & valore & "' OR "
            query = query & "PU_CORTESIA_VAL='" & valore & "' OR "
            query = query & "PU_IGIENE_VAL='" & valore & "' OR "
            query = query & "PU_PARTI_COMUNI_VAL='" & valore & "' OR "
            query = query & "PU_RIF_INGOMBRANTI_VAL='" & valore & "' OR "
            query = query & "PO_REGOLARITA_VAL='" & valore & "' OR "
            query = query & "PO_QUALITA_VAL='" & valore & "' OR "
            query = query & "PO_CORTESIA_VAL='" & valore & "' OR "
            query = query & "PO_POSTA_VAL='" & valore & "' OR "
            query = query & "PO_INF_COMPLETE_VAL='" & valore & "' OR "
            query = query & "RI_REGOLARITA_VAL='" & valore & "' OR "
            query = query & "RI_QUALITA_VAL='" & valore & "' OR "
            query = query & "RI_CORTESIA_VAL='" & valore & "' OR "
            query = query & "RI_TEMPERATURA_VAL='" & valore & "' OR "
            query = query & "RI_GUASTI_VAL='" & valore & "' OR "
            query = query & "RI_RIS_GUASTI_VAL='" & valore & "' OR "
            query = query & "VE_REGOLARITA_VAL='" & valore & "' OR "
            query = query & "VE_QUALITA_VAL='" & valore & "' OR "
            query = query & "VE_CORTESIA_VAL='" & valore & "' OR "
            query = query & "VE_TEMPESTIVITA_VAL='" & valore & "' OR "
            query = query & "VE_SMALTIMENTO_RIF_VAL='" & valore & "' OR "
            query = query & "VE_RUMORE_VAL='" & valore & "') "

            par.cmd.CommandText = query

        Else

            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If

            query = query & "(PU_REGOLARITA='" & risposta & "' AND PU_REGOLARITA_VAL='" & valore & "') OR "
            query = query & "(PU_QUALITA='" & risposta & "' AND PU_QUALITA_VAL='" & valore & "') OR "
            query = query & "(PU_CORTESIA='" & risposta & "' AND PU_CORTESIA_VAL='" & valore & "') OR "
            query = query & "(PU_IGIENE='" & risposta & "' AND PU_IGIENE_VAL='" & valore & "') OR "
            query = query & "(PU_PARTI_COMUNI='" & risposta & "' AND PU_PARTI_COMUNI_VAL='" & valore & "') OR "
            query = query & "(PU_RIF_INGOMBRANTI='" & risposta & "' AND PU_RIF_INGOMBRANTI_VAL='" & valore & "') OR "
            query = query & "(PO_REGOLARITA='" & risposta & "' AND PO_REGOLARITA_VAL='" & valore & "') OR "
            query = query & "(PO_QUALITA='" & risposta & "' AND PO_QUALITA_VAL='" & valore & "') OR "
            query = query & "(PO_CORTESIA='" & risposta & "' AND PO_CORTESIA_VAL='" & valore & "') OR "
            query = query & "(PO_POSTA='" & risposta & "' AND PO_CORTESIA_VAL='" & valore & "') OR "
            query = query & "(PO_INF_COMPLETE='" & risposta & "' AND PO_INF_COMPLETE_VAL='" & valore & "') OR "
            query = query & "(RI_REGOLARITA='" & risposta & "' AND RI_REGOLARITA_VAL='" & valore & "') OR "
            query = query & "(RI_QUALITA='" & risposta & "' AND RI_QUALITA_VAL='" & valore & "') OR "
            query = query & "(RI_CORTESIA='" & risposta & "' AND RI_CORTESIA_VAL='" & valore & "') OR "
            query = query & "(RI_TEMPERATURA='" & risposta & "' AND RI_TEMPERATURA_VAL='" & valore & "') OR "
            query = query & "(RI_GUASTI='" & risposta & "' AND RI_GUASTI_VAL='" & valore & "') OR "
            query = query & "(RI_RIS_GUASTI='" & risposta & "' AND RI_RIS_GUASTI_VAL='" & valore & "') OR "
            query = query & "(VE_REGOLARITA='" & risposta & "' AND VE_REGOLARITA_VAL='" & valore & "') OR "
            query = query & "(VE_QUALITA='" & risposta & "' AND VE_QUALITA_VAL='" & valore & "') OR "
            query = query & "(VE_CORTESIA='" & risposta & "' AND VE_CORTESIA_VAL='" & valore & "') OR "
            query = query & "(VE_TEMPESTIVITA='" & risposta & "' AND VE_TEMPESTIVITA_VAL='" & valore & "') OR "
            query = query & "(VE_SMALTIMENTO_RIF='" & risposta & "' AND VE_SMALTIMENTO_RIF_VAL='" & valore & "') OR "
            query = query & "(VE_RUMORE='" & risposta & "' AND VE_RUMORE_VAL='" & valore & "')) "
            par.cmd.CommandText = query

        End If




        Dim areaGrafico As String
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()
        Dim arraySI As Integer = 0
        Dim arrayAB As Integer = 0
        Dim arrayPC As Integer = 0
        Dim arrayNO As Integer = 0
        Dim array1 As Integer = 0
        Dim array2 As Integer = 0
        Dim array3 As Integer = 0
        Dim array4 As Integer = 0
        Dim i As Integer = 0

        While myReader.Read

            Select Case par.IfNull(myReader("PU_REGOLARITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PU_QUALITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PU_CORTESIA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PU_IGIENE"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PU_PARTI_COMUNI"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PU_RIF_INGOMBRANTI"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("RI_REGOLARITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("RI_QUALITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("RI_CORTESIA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("RI_TEMPERATURA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("RI_GUASTI"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("RI_RIS_GUASTI"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PO_REGOLARITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PO_QUALITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PO_CORTESIA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PO_INF_COMPLETE"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PO_POSTA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("VE_REGOLARITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("VE_QUALITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("VE_CORTESIA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("VE_RUMORE"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("VE_TEMPESTIVITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("VE_SMALTIMENTO_RIF"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            '---------------------------
            'VALORI
            '---------------------------

            Select Case par.IfNull(myReader("PU_REGOLARITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("PU_QUALITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("PU_CORTESIA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("PU_IGIENE_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("PU_PARTI_COMUNI_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("PU_RIF_INGOMBRANTI_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("RI_REGOLARITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("RI_QUALITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("RI_CORTESIA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("RI_TEMPERATURA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("RI_GUASTI_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("RI_RIS_GUASTI_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("PO_REGOLARITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("PO_QUALITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("PO_CORTESIA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("PO_INF_COMPLETE_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("PO_POSTA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("VE_REGOLARITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("VE_QUALITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("VE_CORTESIA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("VE_RUMORE_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("VE_TEMPESTIVITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("VE_SMALTIMENTO_RIF_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


        End While
        chiudiConnessione()

        Dim campo As String = "Servizi di manutenzione del verde"
        areaGrafico = "ChartArea1"
        Chart1.Series.Add(campo)
        Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart1.Series(campo).LabelForeColor = Drawing.Color.White
        Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart1.Series(campo)("BarLabelStyle") = "Center"


        Dim somma As Integer

        somma = arrayAB + arrayNO + arrayPC + arraySI

        Dim vettPerc As ArrayList = CalcolaPercentuali(arraySI, arrayAB, arrayPC, arrayNO)

        If arraySI <> 0 Then
            Chart1.Series(campo).Points.Add(vettPerc(0)).AxisLabel = "SI=" & CStr(vettPerc(0)) & "%"
        End If
        If arrayAB <> 0 Then
            Chart1.Series(campo).Points.Add(vettPerc(1)).AxisLabel = "AB=" & CStr(vettPerc(1)) & "%"
        End If
        If arrayPC <> 0 Then
            Chart1.Series(campo).Points.Add(vettPerc(2)).AxisLabel = "PC=" & CStr(vettPerc(2)) & "%"
        End If
        If arrayNO <> 0 Then
            Chart1.Series(campo).Points.Add(vettPerc(3)).AxisLabel = "NO=" & CStr(vettPerc(3)) & "%"
        End If

        'Chart1.Titles.Add("TITOLO")
        Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart1.BackColor = Drawing.Color.White
        Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
        Chart1.Series(campo)("DrawingStyle") = "Cylinder"
        Chart1.Series(campo).ChartArea = areaGrafico
        Chart1.Legends.Add("Legenda")
        Chart1.Visible = True

        areaGrafico = "ChartArea2"
        Chart2.Series.Add(campo)
        Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart2.Series(campo).LabelForeColor = Drawing.Color.White
        Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart2.Series(campo)("BarLabelStyle") = "Center"

        somma = array1 + array2 + array3 + array4
        vettPerc.Clear()
        vettPerc = CalcolaPercentuali(array1, array2, array3, array4)

        If array1 <> 0 Then
            Chart2.Series(campo).Points.Add(vettPerc(0)).AxisLabel = "1=" & CStr(Round(CDbl(vettPerc(0)), 2)) & "%"
        End If
        If array2 <> 0 Then
            Chart2.Series(campo).Points.Add(vettPerc(1)).AxisLabel = "2=" & CStr(Round(CDbl(vettPerc(1)), 2)) & "%"
        End If
        If array3 <> 0 Then
            Chart2.Series(campo).Points.Add(vettPerc(2)).AxisLabel = "3=" & CStr(Round(CDbl(vettPerc(2)), 2)) & "%"
        End If
        If array4 <> 0 Then
            Chart2.Series(campo).Points.Add(vettPerc(3)).AxisLabel = "4=" & CStr(Round(CDbl(vettPerc(3)), 2)) & "%"
        End If

        'Chart2.Titles.Add("TITOLO")
        Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart2.BackColor = Drawing.Color.White
        Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
        Chart2.Series(campo)("DrawingStyle") = "Cylinder"
        Chart2.Series(campo).ChartArea = areaGrafico
        Chart2.Legends.Add("Legenda")
        Chart2.Visible = True
        'Label5.Text = "Numero di questionari coinvolti: " & CStr(Round(somma / 23))

    End Sub

    Protected Sub creaGraficoRegolarita(ByVal risposta As String, ByVal valore As String)

        apriConnessione()

        Dim codiceUnitaImmobiliare As String = Trim(sValoreCODUI)
        Dim controlloIndirizzo As String = ""
        Dim controlloCivico As String = ""



        If Not sValoreIndirizzo = "---" Then
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE='" & Replace(sValoreIndirizzo, "'", "''") & "' AND "
        Else
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE LIKE '%' AND "
        End If
        If Not sValoreCivico = "---" Then
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO='" & Replace(sValoreCivico, "'", "''") & "' AND "
        Else
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO LIKE '%' AND "
        End If

        Dim query As String = ""


        If risposta = "---" And valore = "---" Then

            If codiceUnitaImmobiliare = "" Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA "

            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' "
                Else
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' "
                End If
            End If

        ElseIf risposta <> "---" And valore = "---" Then

            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If


            query = query & "PU_REGOLARITA='" & risposta & "' OR "
            query = query & "PO_REGOLARITA='" & risposta & "' OR "
            query = query & "RI_REGOLARITA='" & risposta & "' OR "
            query = query & "VE_REGOLARITA='" & risposta & "')"

            par.cmd.CommandText = query


        ElseIf risposta = "---" And valore <> "---" Then

            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If

            query = query & "PU_REGOLARITA_VAL='" & valore & "' OR "
            query = query & "PO_REGOLARITA_VAL='" & valore & "' OR "
            query = query & "RI_REGOLARITA_VAL='" & valore & "' OR "
            query = query & "VE_REGOLARITA_VAL='" & valore & "')"
            par.cmd.CommandText = query

        Else
            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If

            query = query & "(PU_REGOLARITA='" & risposta & "' AND PU_REGOLARITA_VAL='" & valore & "') OR "
            query = query & "(PO_REGOLARITA='" & risposta & "' AND PO_REGOLARITA_VAL='" & valore & "') OR "
            query = query & "(RI_REGOLARITA='" & risposta & "' AND RI_REGOLARITA_VAL='" & valore & "') OR "
            query = query & "(VE_REGOLARITA='" & risposta & "' AND VE_REGOLARITA_VAL='" & valore & "')) "
            par.cmd.CommandText = query

        End If

        Dim areaGrafico As String

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()

        Dim arraySI As Integer = 0
        Dim arrayAB As Integer = 0
        Dim arrayPC As Integer = 0
        Dim arrayNO As Integer = 0
        Dim array1 As Integer = 0
        Dim array2 As Integer = 0
        Dim array3 As Integer = 0
        Dim array4 As Integer = 0
        Dim i As Integer = 0

        While myReader.Read

            Select Case par.IfNull(myReader("PU_REGOLARITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("RI_REGOLARITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PO_REGOLARITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("VE_REGOLARITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            '---------------------------
            'VALORI
            '---------------------------

            Select Case par.IfNull(myReader("PU_REGOLARITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("RI_REGOLARITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


            Select Case par.IfNull(myReader("PO_REGOLARITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


            Select Case par.IfNull(myReader("VE_REGOLARITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


        End While

        chiudiConnessione()

        Dim campo As String = "Servizi di manutenzione del verde"
        areaGrafico = "ChartArea1"
        Chart1.Series.Add(campo)
        Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart1.Series(campo).LabelForeColor = Drawing.Color.White
        Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart1.Series(campo)("BarLabelStyle") = "Center"

        Dim somma As Integer
        somma = arrayAB + arrayNO + arrayPC + arraySI
        Dim vettPerc As ArrayList = CalcolaPercentuali(arraySI, arrayAB, arrayPC, arrayNO)

        If arraySI <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(0)).AxisLabel = "SI=" & CStr(vettPerc(0)) & "%"
        End If
        If arrayAB <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(1)).AxisLabel = "AB=" & CStr(vettPerc(1)) & "%"
        End If
        If arrayPC <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(2)).AxisLabel = "PC=" & CStr(vettPerc(2)) & "%"
        End If
        If arrayNO <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(3)).AxisLabel = "NO=" & CStr(vettPerc(3)) & "%"
        End If

        'Chart1.Titles.Add("TITOLO")
        Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart1.BackColor = Drawing.Color.White
        Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
        Chart1.Series(campo)("DrawingStyle") = "Cylinder"
        Chart1.Series(campo).ChartArea = areaGrafico
        Chart1.Legends.Add("Legenda")
        Chart1.Visible = True

        areaGrafico = "ChartArea2"
        Chart2.Series.Add(campo)
        Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart2.Series(campo).LabelForeColor = Drawing.Color.White
        Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart2.Series(campo)("BarLabelStyle") = "Center"

        somma = array1 + array2 + array3 + array4
        vettPerc.Clear()
        vettPerc = CalcolaPercentuali(array1, array2, array3, array4)
        If array1 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(0)).AxisLabel = "1=" & CStr(vettPerc(0)) & "%"
        End If
        If array2 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(1)).AxisLabel = "2=" & CStr(vettPerc(1)) & "%"
        End If
        If array3 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(2)).AxisLabel = "3=" & CStr(vettPerc(2)) & "%"
        End If
        If array4 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(3)).AxisLabel = "4=" & CStr(vettPerc(3)) & "%"
        End If

        'Chart2.Titles.Add("TITOLO")
        Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart2.BackColor = Drawing.Color.White
        Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
        Chart2.Series(campo)("DrawingStyle") = "Cylinder"
        Chart2.Series(campo).ChartArea = areaGrafico
        Chart2.Legends.Add("Legenda")
        Chart2.Visible = True

        'Label5.Text = "Numero di questionari coinvolti: " & CStr(Round(somma / 4))

    End Sub

    Protected Sub creaGraficoQualita(ByVal risposta As String, ByVal valore As String)

        apriConnessione()
        Dim codiceUnitaImmobiliare As String = Trim(sValoreCODUI)
        Dim controlloIndirizzo As String = ""
        Dim controlloCivico As String = ""



        If Not sValoreIndirizzo = "---" Then
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE='" & Replace(sValoreIndirizzo, "'", "''") & "' AND "
        Else
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE LIKE '%' AND "
        End If
        If Not sValoreCivico = "---" Then
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO='" & Replace(sValoreCivico, "'", "''") & "' AND "
        Else
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO LIKE '%' AND "
        End If
        Dim query As String = ""

        If risposta = "---" And valore = "---" Then

            If codiceUnitaImmobiliare = "" Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA "

            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' "
                Else
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' "
                End If
            End If

        ElseIf risposta <> "---" And valore = "---" Then

            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If

            query = query & "PU_QUALITA='" & risposta & "' OR "
            query = query & "PO_QUALITA='" & risposta & "' OR "
            query = query & "RI_QUALITA='" & risposta & "' OR "
            query = query & "VE_QUALITA='" & risposta & "')"
            par.cmd.CommandText = query


        ElseIf risposta = "---" And valore <> "---" Then

            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If
            query = query & "PU_QUALITA_VAL='" & valore & "' OR "
            query = query & "PO_QUALITA_VAL='" & valore & "' OR "
            query = query & "RI_QUALITA_VAL='" & valore & "' OR "
            query = query & "VE_QUALITA_VAL='" & valore & "')"


            par.cmd.CommandText = query

        Else

            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If


            query = query & "(PU_QUALITA='" & risposta & "' AND PU_QUALITA_VAL='" & valore & "') OR "
            query = query & "(PO_QUALITA='" & risposta & "' AND PO_QUALITA_VAL='" & valore & "') OR "
            query = query & "(RI_QUALITA='" & risposta & "' AND RI_QUALITA_VAL='" & valore & "') OR "
            query = query & "(VE_QUALITA='" & risposta & "' AND VE_QUALITA_VAL='" & valore & "')) "


            par.cmd.CommandText = query
            'MsgBox(query)

        End If

        Dim areaGrafico As String

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()

        Dim arraySI As Integer = 0
        Dim arrayAB As Integer = 0
        Dim arrayPC As Integer = 0
        Dim arrayNO As Integer = 0
        Dim array1 As Integer = 0
        Dim array2 As Integer = 0
        Dim array3 As Integer = 0
        Dim array4 As Integer = 0
        Dim i As Integer = 0

        While myReader.Read

            Select Case par.IfNull(myReader("PU_QUALITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("RI_QUALITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PO_QUALITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("VE_QUALITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            '---------------------------
            'VALORI
            '---------------------------

            Select Case par.IfNull(myReader("PU_QUALITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("RI_QUALITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


            Select Case par.IfNull(myReader("PO_QUALITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


            Select Case par.IfNull(myReader("VE_QUALITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

        End While

        chiudiConnessione()

        Dim campo As String = "Servizi di manutenzione del verde"
        areaGrafico = "ChartArea1"
        Chart1.Series.Add(campo)
        Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart1.Series(campo).LabelForeColor = Drawing.Color.White
        Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart1.Series(campo)("BarLabelStyle") = "Center"


        Dim somma As Integer
        somma = arrayAB + arrayNO + arrayPC + arraySI
        Dim vettPerc As ArrayList = CalcolaPercentuali(arraySI, arrayAB, arrayPC, arrayNO)
        If arraySI <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(0)).AxisLabel = "SI=" & CStr(vettPerc(0)) & "%"
        End If
        If arrayAB <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(1)).AxisLabel = "AB=" & CStr(vettPerc(1)) & "%"
        End If
        If arrayPC <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(2)).AxisLabel = "PC=" & CStr(vettPerc(2)) & "%"
        End If
        If arrayNO <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(3)).AxisLabel = "NO=" & CStr(vettPerc(3)) & "%"
        End If

        'Chart1.Titles.Add("TITOLO")
        Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart1.BackColor = Drawing.Color.White
        Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
        Chart1.Series(campo)("DrawingStyle") = "Cylinder"
        Chart1.Series(campo).ChartArea = areaGrafico
        Chart1.Legends.Add("Legenda")
        Chart1.Visible = True

        areaGrafico = "ChartArea2"
        Chart2.Series.Add(campo)
        Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart2.Series(campo).LabelForeColor = Drawing.Color.White
        Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart2.Series(campo)("BarLabelStyle") = "Center"

        somma = array1 + array2 + array3 + array4
        vettPerc.Clear()
        vettPerc = CalcolaPercentuali(array1, array2, array3, array4)
        If array1 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(0)).AxisLabel = "1=" & CStr(vettPerc(0)) & "%"
        End If
        If array2 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(1)).AxisLabel = "2=" & CStr(vettPerc(1)) & "%"
        End If
        If array3 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(2)).AxisLabel = "3=" & CStr(vettPerc(2)) & "%"
        End If
        If array4 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(3)).AxisLabel = "4=" & CStr(vettPerc(3)) & "%"
        End If

        'Chart2.Titles.Add("TITOLO")
        Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart2.BackColor = Drawing.Color.White
        Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
        Chart2.Series(campo)("DrawingStyle") = "Cylinder"
        Chart2.Series(campo).ChartArea = areaGrafico
        Chart2.Legends.Add("Legenda")
        Chart2.Visible = True

        'Label5.Text = "Numero di questionari coinvolti: " & CStr(Round(somma / 4))

    End Sub

    Protected Sub creaGraficoCortesia(ByVal risposta As String, ByVal valore As String)

        apriConnessione()
        Dim codiceUnitaImmobiliare As String = Trim(sValoreCODUI)
        Dim controlloIndirizzo As String = ""
        Dim controlloCivico As String = ""



        If Not sValoreIndirizzo = "---" Then
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE='" & Replace(sValoreIndirizzo, "'", "''") & "' AND "
        Else
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE LIKE '%' AND "
        End If
        If Not sValoreCivico = "---" Then
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO='" & Replace(sValoreCivico, "'", "''") & "' AND "
        Else
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO LIKE '%' AND "
        End If
        Dim query As String = ""

        If risposta = "---" And valore = "---" Then

            If codiceUnitaImmobiliare = "" Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA "

            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' "
                Else
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' "
                End If
            End If

        ElseIf risposta <> "---" And valore = "---" Then

            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If


            query = query & "PU_CORTESIA='" & risposta & "' OR "
            query = query & "PO_CORTESIA='" & risposta & "' OR "
            query = query & "RI_CORTESIA='" & risposta & "' OR "
            query = query & "VE_CORTESIA='" & risposta & "')"



            par.cmd.CommandText = query
        ElseIf risposta = "---" And valore <> "---" Then

            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If


            query = query & "PU_CORTESIA_VAL='" & valore & "' OR "
            query = query & "PO_CORTESIA_VAL='" & valore & "' OR "
            query = query & "RI_CORTESIA_VAL='" & valore & "' OR "
            query = query & "VE_CORTESIA_VAL='" & valore & "')"



            par.cmd.CommandText = query
        Else

            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If


            query = query & "(PU_CORTESIA='" & risposta & "' AND PU_CORTESIA_VAL='" & valore & "') OR "
            query = query & "(PO_CORTESIA='" & risposta & "' AND PO_CORTESIA_VAL='" & valore & "') OR "
            query = query & "(RI_CORTESIA='" & risposta & "' AND RI_CORTESIA_VAL='" & valore & "') OR "
            query = query & "(VE_CORTESIA='" & risposta & "' AND VE_CORTESIA_VAL='" & valore & "')) "



            par.cmd.CommandText = query
        End If

        Dim areaGrafico As String

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()

        Dim arraySI As Integer = 0
        Dim arrayAB As Integer = 0
        Dim arrayPC As Integer = 0
        Dim arrayNO As Integer = 0
        Dim array1 As Integer = 0
        Dim array2 As Integer = 0
        Dim array3 As Integer = 0
        Dim array4 As Integer = 0
        Dim i As Integer = 0

        While myReader.Read

            Select Case par.IfNull(myReader("PU_CORTESIA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("RI_CORTESIA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PO_CORTESIA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("VE_CORTESIA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            '---------------------------
            'VALORI
            '---------------------------

            Select Case par.IfNull(myReader("PU_CORTESIA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("RI_CORTESIA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


            Select Case par.IfNull(myReader("PO_CORTESIA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("VE_CORTESIA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

        End While

        chiudiConnessione()

        Dim campo As String = "Servizi di manutenzione del verde"
        areaGrafico = "ChartArea1"
        Chart1.Series.Add(campo)
        Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart1.Series(campo).LabelForeColor = Drawing.Color.White
        Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart1.Series(campo)("BarLabelStyle") = "Center"

        Dim somma As Integer

        somma = arrayAB + arrayNO + arrayPC + arraySI
        Dim vettPerc As ArrayList = CalcolaPercentuali(arraySI, arrayAB, arrayPC, arrayNO)
        If arraySI <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(0)).AxisLabel = "SI=" & CStr(vettPerc(0)) & "%"
        End If
        If arrayAB <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(1)).AxisLabel = "AB=" & CStr(vettPerc(1)) & "%"
        End If
        If arrayPC <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(2)).AxisLabel = "PC=" & CStr(vettPerc(2)) & "%"
        End If
        If arrayNO <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(3)).AxisLabel = "NO=" & CStr(vettPerc(3)) & "%"
        End If

        'Chart1.Titles.Add("TITOLO")
        Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart1.BackColor = Drawing.Color.White
        Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
        Chart1.Series(campo)("DrawingStyle") = "Cylinder"
        Chart1.Series(campo).ChartArea = areaGrafico
        Chart1.Legends.Add("Legenda")
        Chart1.Visible = True

        areaGrafico = "ChartArea2"
        Chart2.Series.Add(campo)
        Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart2.Series(campo).LabelForeColor = Drawing.Color.White
        Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart2.Series(campo)("BarLabelStyle") = "Center"

        somma = array1 + array2 + array3 + array4
        vettPerc.Clear()
        vettPerc = CalcolaPercentuali(array1, array2, array3, array4)
        If array1 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(0)).AxisLabel = "1=" & CStr(vettPerc(0)) & "%"
        End If
        If array2 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(1)).AxisLabel = "2=" & CStr(vettPerc(1)) & "%"
        End If
        If array3 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(2)).AxisLabel = "3=" & CStr(vettPerc(2)) & "%"
        End If
        If array4 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(3)).AxisLabel = "4=" & CStr(vettPerc(3)) & "%"
        End If

        'Chart2.Titles.Add("TITOLO")
        Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart2.BackColor = Drawing.Color.White
        Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
        Chart2.Series(campo)("DrawingStyle") = "Cylinder"
        Chart2.Series(campo).ChartArea = areaGrafico
        Chart2.Legends.Add("Legenda")
        Chart2.Visible = True

        'Label5.Text = "Numero di questionari coinvolti: " & CStr(Round(somma / 4))

    End Sub

    Protected Sub creaGraficoConRisposta(ByVal campoRic As String, ByVal num As Integer)
        Dim areaGrafico As String

        Dim campo As String = campoRic

        Dim campoQuery As String
        If Right(campo, 4) = "_VAL" Then
            campoQuery = Mid(campo, 1, Len(campo) - 4)
        Else
            campoQuery = campo
        End If
        apriConnessione()

        Dim codiceUnitaImmobiliare As String = Trim(sValoreCODUI)
        Dim controlloIndirizzo As String = ""
        Dim controlloCivico As String = ""



        If Not sValoreIndirizzo = "---" Then
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE='" & Replace(sValoreIndirizzo, "'", "''") & "' AND "
        Else
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE LIKE '%' AND "
        End If
        If Not sValoreCivico = "---" Then
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO='" & Replace(sValoreCivico, "'", "''") & "' AND "
        Else
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO LIKE '%' AND "
        End If

        If codiceUnitaImmobiliare = "" Then
            par.cmd.CommandText = "SELECT (" & campo & "),COUNT(" & campo & ") FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND " & controlloGiudizio & controlloOperatore & controlloDate & controlloIndirizzo & controlloCivico & campoQuery & "='" & sValoreRISP & "' GROUP BY " & campo & " ORDER BY " & campo & " ASC"
        Else
            If Right(codiceUnitaImmobiliare, 1) = "*" Then
                codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                par.cmd.CommandText = "SELECT (" & campo & "),COUNT(" & campo & ") FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND " & controlloGiudizio & controlloOperatore & controlloDate & controlloIndirizzo & controlloCivico & campoQuery & "='" & sValoreRISP & "' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' GROUP BY " & campo & " ORDER BY " & campo & " ASC"
            Else
                par.cmd.CommandText = "SELECT (" & campo & "),COUNT(" & campo & ") FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND " & controlloGiudizio & controlloOperatore & controlloDate & controlloIndirizzo & controlloCivico & campoQuery & "='" & sValoreRISP & "' AND UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "' GROUP BY " & campo & " ORDER BY " & campo & " ASC"
            End If

        End If



        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()
        Dim i As Integer = 1
        Dim vettoreNum As New System.Collections.ArrayList
        Dim vettoreNum2 As New System.Collections.ArrayList
        Dim vettoreLbl As New System.Collections.ArrayList
        vettoreNum.Clear()
        vettoreLbl.Clear()
        Dim somma As Double = 0
        While myReader.Read
            vettoreLbl.Add(myReader(0))
            vettoreNum.Add(myReader(1))

            somma = somma + myReader(1)
            i = i + 1
        End While
        chiudiConnessione()

        Dim k As Integer = 1
        For k = 1 To 4 - vettoreNum.Count
            vettoreNum.Add(0)
        Next


        vettoreNum = CalcolaPercentuali(vettoreNum(0), vettoreNum(1), vettoreNum(2), vettoreNum(3))

        For k = 0 To vettoreNum.Count - 1
            If vettoreNum(k) <> 0 Then
                vettoreNum2.Add(vettoreNum(k))
            End If
        Next




        If num = 1 Then
            areaGrafico = "ChartArea1"
            Chart1.Series.Add(campo)
            Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
            Chart1.Series(campo).LabelForeColor = Drawing.Color.White
            Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
            Chart1.Series(campo)("BarLabelStyle") = "Center"

            For j As Integer = 0 To vettoreNum2.Count - 1
                Chart1.Series(campo).Points.Add(vettoreNum2(j)).AxisLabel = vettoreLbl(j) & "=" & CStr(vettoreNum2(j)) & "%"
            Next
            'Chart1.Titles.Add("TITOLO")
            Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
            Chart1.BackColor = Drawing.Color.White
            Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
            Chart1.Series(campo)("DrawingStyle") = "Cylinder"
            Chart1.Series(campo).ChartArea = areaGrafico
            Chart1.Legends.Add("Legenda")
            Chart1.Visible = True

        Else
            areaGrafico = "ChartArea2"
            Chart2.Series.Add(campo)
            Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
            Chart2.Series(campo).LabelForeColor = Drawing.Color.White
            Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
            Chart2.Series(campo)("BarLabelStyle") = "Center"

            For j As Integer = 0 To vettoreNum2.Count - 1
                Chart2.Series(campo).Points.Add(vettoreNum2(j)).AxisLabel = vettoreLbl(j) & "=" & CStr(vettoreNum2(j)) & "%"
            Next
            'Chart2.Titles.Add("TITOLO")
            Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
            Chart2.BackColor = Drawing.Color.White
            Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
            Chart2.Series(campo)("DrawingStyle") = "Cylinder"
            Chart2.Series(campo).ChartArea = areaGrafico
            Chart2.Legends.Add("Legenda")
            Chart2.Visible = True

        End If

        'Label5.Text = "Numero di questionari coinvolti: " & CStr(somma)

    End Sub

    Protected Sub creaGraficoConValore(ByVal campoRic As String, ByVal num As Integer)

        Dim areaGrafico As String
        Dim campo As String = campoRic
        Dim campoQuery As String
        If Not Right(campo, 4) = "_VAL" Then
            campoQuery = campo & "_VAL"
        Else
            campoQuery = campo
        End If
        apriConnessione()
        Dim codiceUnitaImmobiliare As String = Trim(sValoreCODUI)
        Dim controlloIndirizzo As String = ""
        Dim controlloCivico As String = ""



        If Not sValoreIndirizzo = "---" Then
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE='" & Replace(sValoreIndirizzo, "'", "''") & "' AND "
        Else
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE LIKE '%' AND "
        End If
        If Not sValoreCivico = "---" Then
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO='" & Replace(sValoreCivico, "'", "''") & "' AND "
        Else
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO LIKE '%' AND "
        End If

        If codiceUnitaImmobiliare = "" Then
            par.cmd.CommandText = "SELECT (" & campo & "),COUNT(" & campo & ") FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND " & controlloGiudizio & controlloOperatore & controlloDate & controlloIndirizzo & controlloCivico & campoQuery & "='" & sValoreVAL & "' GROUP BY " & campo & " ORDER BY " & campo & " ASC"
        Else
            If Right(codiceUnitaImmobiliare, 1) = "*" Then
                codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                par.cmd.CommandText = "SELECT (" & campo & "),COUNT(" & campo & ") FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND " & controlloGiudizio & controlloOperatore & controlloDate & controlloIndirizzo & controlloCivico & campoQuery & "='" & sValoreVAL & "' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' GROUP BY " & campo & " ORDER BY " & campo & " ASC"
            Else
                par.cmd.CommandText = "SELECT (" & campo & "),COUNT(" & campo & ") FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND " & controlloGiudizio & controlloOperatore & controlloDate & controlloIndirizzo & controlloCivico & campoQuery & "='" & sValoreVAL & "' AND UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "' GROUP BY " & campo & " ORDER BY " & campo & " ASC"
            End If

        End If





        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()
        Dim i As Integer = 1
        Dim vettoreNum As New System.Collections.ArrayList
        Dim vettoreLbl As New System.Collections.ArrayList
        Dim vettoreNum2 As New System.Collections.ArrayList
        vettoreNum.Clear()
        vettoreLbl.Clear()
        Dim somma As Double = 0

        While myReader.Read
            vettoreLbl.Add(myReader(0))
            vettoreNum.Add(myReader(1))
            somma = somma + myReader(1)
            i = i + 1
        End While
        chiudiConnessione()


        Dim k As Integer = 1
        For k = 1 To 4 - vettoreNum.Count
            vettoreNum.Add(0)
        Next


        vettoreNum = CalcolaPercentuali(vettoreNum(0), vettoreNum(1), vettoreNum(2), vettoreNum(3))

        For k = 0 To vettoreNum.Count - 1
            If vettoreNum(k) <> 0 Then
                vettoreNum2.Add(vettoreNum(k))
            End If
        Next


        If num = 1 Then
            areaGrafico = "ChartArea1"
            Chart1.Series.Add(campo)
            Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
            Chart1.Series(campo).LabelForeColor = Drawing.Color.White
            Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
            Chart1.Series(campo)("BarLabelStyle") = "Center"
            For j As Integer = 0 To vettoreNum2.Count - 1
                Chart1.Series(campo).Points.Add(vettoreNum2(j)).AxisLabel = vettoreLbl(j) & "=" & CStr(vettoreNum2(j)) & "%"
            Next
            'Chart1.Titles.Add("TITOLO")
            Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
            Chart1.BackColor = Drawing.Color.White
            Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
            Chart1.Series(campo)("DrawingStyle") = "Cylinder"
            Chart1.Series(campo).ChartArea = areaGrafico
            Chart1.Legends.Add("Legenda")
            Chart1.Visible = True

        Else
            areaGrafico = "ChartArea2"
            Chart2.Series.Add(campo)
            Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
            Chart2.Series(campo).LabelForeColor = Drawing.Color.White
            Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
            Chart2.Series(campo)("BarLabelStyle") = "Center"
            For j As Integer = 0 To vettoreNum2.Count - 1
                Chart2.Series(campo).Points.Add(vettoreNum2(j)).AxisLabel = vettoreLbl(j) & "=" & CStr(vettoreNum2(j)) & "%"
            Next
            'Chart2.Titles.Add("TITOLO")
            Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
            Chart2.BackColor = Drawing.Color.White
            Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
            Chart2.Series(campo)("DrawingStyle") = "Cylinder"
            Chart2.Series(campo).ChartArea = areaGrafico
            Chart2.Legends.Add("Legenda")
            Chart2.Visible = True

        End If

        'Label5.Text = "Numero di questionari coinvolti: " & CStr(somma)

    End Sub

    Protected Sub creaGraficoConValoreRisposta(ByVal campoRic As String, ByVal num As Integer)
        Dim areaGrafico As String
        Dim campo As String = campoRic
        Dim campoQueryValore As String
        Dim campoQueryRisposta As String
        If Not Right(campo, 4) = "_VAL" Then
            campoQueryValore = campo & "_VAL"
        Else
            campoQueryValore = campo
        End If

        If Right(campo, 4) = "_VAL" Then
            campoQueryRisposta = Mid(campo, 1, Len(campo) - 4)
        Else
            campoQueryRisposta = campo
        End If

        apriConnessione()

        Dim codiceUnitaImmobiliare As String = Trim(sValoreCODUI)
        Dim controlloIndirizzo As String = ""
        Dim controlloCivico As String = ""



        If Not sValoreIndirizzo = "---" Then
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE='" & Replace(sValoreIndirizzo, "'", "''") & "' AND "
        Else
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE LIKE '%' AND "
        End If
        If Not sValoreCivico = "---" Then
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO='" & Replace(sValoreCivico, "'", "''") & "' AND "
        Else
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO LIKE '%' AND "
        End If

        If codiceUnitaImmobiliare = "" Then
            par.cmd.CommandText = "SELECT (" & campo & "),COUNT(" & campo & ") FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND " & controlloGiudizio & controlloOperatore & controlloDate & controlloIndirizzo & controlloCivico & campoQueryValore & "='" & sValoreVAL & "' AND " & campoQueryRisposta & "='" & sValoreRISP & "' GROUP BY " & campo & " ORDER BY " & campo & " ASC"
        Else
            If Right(codiceUnitaImmobiliare, 1) = "*" Then
                codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                par.cmd.CommandText = "SELECT (" & campo & "),COUNT(" & campo & ") FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND " & controlloGiudizio & controlloOperatore & controlloDate & controlloIndirizzo & controlloCivico & campoQueryValore & "='" & sValoreVAL & "' AND " & campoQueryRisposta & "='" & sValoreRISP & "' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' GROUP BY " & campo & " ORDER BY " & campo & " ASC"
            Else
                par.cmd.CommandText = "SELECT (" & campo & "),COUNT(" & campo & ") FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND " & controlloGiudizio & controlloOperatore & controlloDate & controlloIndirizzo & controlloCivico & campoQueryValore & "='" & sValoreVAL & "' AND " & campoQueryRisposta & "='" & sValoreRISP & "' AND UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "' GROUP BY " & campo & " ORDER BY " & campo & " ASC"
            End If

        End If

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()
        Dim i As Integer = 1
        Dim vettoreNum As New System.Collections.ArrayList
        Dim vettoreLbl As New System.Collections.ArrayList
        Dim vettoreNum2 As New System.Collections.ArrayList
        vettoreNum.Clear()
        vettoreLbl.Clear()
        Dim somma As Double = 0
        While myReader.Read
            vettoreLbl.Add(myReader(0))
            vettoreNum.Add(myReader(1))

            somma = somma + myReader(1)
            i = i + 1
        End While
        chiudiConnessione()

        Dim k As Integer = 1
        For k = 1 To 4 - vettoreNum.Count
            vettoreNum.Add(0)
        Next


        vettoreNum = CalcolaPercentuali(vettoreNum(0), vettoreNum(1), vettoreNum(2), vettoreNum(3))

        For k = 0 To vettoreNum.Count - 1
            If vettoreNum(k) <> 0 Then
                vettoreNum2.Add(vettoreNum(k))
            End If
        Next



        If num = 1 Then
            areaGrafico = "ChartArea1"
            Chart1.Series.Add(campo)
            Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
            Chart1.Series(campo).LabelForeColor = Drawing.Color.White
            Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
            Chart1.Series(campo)("BarLabelStyle") = "Center"

            For j As Integer = 0 To vettoreNum2.Count - 1
                Chart1.Series(campo).Points.Add(vettoreNum2(j)).AxisLabel = vettoreLbl(j) & "=" & CStr(vettoreNum2(j)) & "%"
            Next
            'Chart1.Titles.Add("TITOLO")
            Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
            Chart1.BackColor = Drawing.Color.White
            Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
            Chart1.Series(campo)("DrawingStyle") = "Cylinder"
            Chart1.Series(campo).ChartArea = areaGrafico
            Chart1.Legends.Add("Legenda")
            Chart1.Visible = True

        Else

            areaGrafico = "ChartArea2"
            Chart2.Series.Add(campo)
            Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
            Chart2.Series(campo).LabelForeColor = Drawing.Color.White
            Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
            Chart2.Series(campo)("BarLabelStyle") = "Center"

            For j As Integer = 0 To vettoreNum2.Count - 1
                Chart2.Series(campo).Points.Add(vettoreNum2(j)).AxisLabel = vettoreLbl(j) & "=" & CStr(vettoreNum2(j)) & "%"
            Next
            'Chart2.Titles.Add("TITOLO")
            Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
            Chart2.BackColor = Drawing.Color.White
            Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
            Chart2.Series(campo)("DrawingStyle") = "Cylinder"
            Chart2.Series(campo).ChartArea = areaGrafico
            Chart2.Legends.Add("Legenda")
            Chart2.Visible = True

        End If

        'Label5.Text = "Numero di questionari coinvolti: " & CStr(somma)

    End Sub

    Protected Sub creaGrafico(ByVal campoRic As String, ByVal num As Integer)
        Dim areaGrafico As String
        Dim campo As String = campoRic

        apriConnessione()

        Dim codiceUnitaImmobiliare As String = Trim(sValoreCODUI)
        Dim controlloIndirizzo As String = ""
        Dim controlloCivico As String = ""



        If Not sValoreIndirizzo = "---" Then
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE='" & Replace(sValoreIndirizzo, "'", "''") & "' AND "
        Else
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE LIKE '%' AND "
        End If
        If Not sValoreCivico = "---" Then
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO='" & Replace(sValoreCivico, "'", "''") & "' AND "
        Else
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO LIKE '%' AND "
        End If

        If codiceUnitaImmobiliare = "" Then
            par.cmd.CommandText = "SELECT (" & campo & "),COUNT(" & campo & ") FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloIndirizzo & controlloCivico & " SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO GROUP BY " & campo & " ORDER BY " & campo & " ASC"
        Else
            If Right(codiceUnitaImmobiliare, 1) = "*" Then
                codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                par.cmd.CommandText = "SELECT (" & campo & "),COUNT(" & campo & ") FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND " & controlloGiudizio & controlloOperatore & controlloDate & controlloIndirizzo & controlloCivico & " SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' GROUP BY " & campo & " ORDER BY " & campo & " ASC"
            Else
                par.cmd.CommandText = "SELECT (" & campo & "),COUNT(" & campo & ") FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND " & controlloGiudizio & controlloOperatore & controlloDate & controlloIndirizzo & controlloCivico & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "' GROUP BY " & campo & " ORDER BY " & campo & " ASC"
            End If

        End If

        'Label6.Text = par.cmd.CommandText

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()
        Dim i As Integer = 1
        Dim vettoreNum As New System.Collections.ArrayList
        Dim vettoreLbl As New System.Collections.ArrayList
        Dim vettoreNum2 As New System.Collections.ArrayList
        vettoreNum.Clear()
        vettoreLbl.Clear()
        Dim somma As Double = 0
        While myReader.Read
            vettoreLbl.Add(myReader(0))
            vettoreNum.Add(myReader(1))

            somma = somma + myReader(1)
            i = i + 1
        End While
        chiudiConnessione()

        Dim k As Integer = 1
        For k = 1 To 4 - vettoreNum.Count
            vettoreNum.Add(0)
        Next


        vettoreNum = CalcolaPercentuali(vettoreNum(0), vettoreNum(1), vettoreNum(2), vettoreNum(3))

        For k = 0 To vettoreNum.Count - 1
            If vettoreNum(k) <> 0 Then
                vettoreNum2.Add(vettoreNum(k))
            End If
        Next

        If num = 1 Then
            areaGrafico = "ChartArea1"
            Chart1.Series.Add(campo)
            Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
            Chart1.Series(campo).LabelForeColor = Drawing.Color.White
            Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
            Chart1.Series(campo)("BarLabelStyle") = "Center"
            For j As Integer = 0 To vettoreNum2.Count - 1
                Chart1.Series(campo).Points.Add(vettoreNum2(j)).AxisLabel = vettoreLbl(j) & "=" & CStr(vettoreNum2(j)) & "%"
            Next
            'Chart1.Titles.Add("TITOLO")
            Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
            Chart1.BackColor = Drawing.Color.White
            Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
            Chart1.Series(campo)("DrawingStyle") = "Cylinder"
            Chart1.Series(campo).ChartArea = areaGrafico
            Chart1.Legends.Add("Legenda")
            Chart1.Visible = True
        Else
            areaGrafico = "ChartArea2"
            Chart2.Series.Add(campo)
            Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
            Chart2.Series(campo).LabelForeColor = Drawing.Color.White
            Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
            Chart2.Series(campo)("BarLabelStyle") = "Center"
            For j As Integer = 0 To vettoreNum2.Count - 1
                Chart2.Series(campo).Points.Add(vettoreNum2(j)).AxisLabel = vettoreLbl(j) & "=" & CStr(vettoreNum2(j)) & "%"
            Next
            'Chart2.Titles.Add("TITOLO")
            Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
            Chart2.BackColor = Drawing.Color.White
            Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
            Chart2.Series(campo)("DrawingStyle") = "Cylinder"
            Chart2.Series(campo).ChartArea = areaGrafico
            Chart2.Legends.Add("Legenda")
            Chart2.Visible = True

        End If

        'Label5.Text = "Numero di questionari coinvolti: " & CStr(somma)


    End Sub

    Protected Sub creaGraficoServiziPulizia(ByVal risposta As String, ByVal valore As String)
        apriConnessione()
        Dim codiceUnitaImmobiliare As String = Trim(sValoreCODUI)
        Dim controlloIndirizzo As String = ""
        Dim controlloCivico As String = ""

        If Not sValoreIndirizzo = "---" Then
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE='" & Replace(sValoreIndirizzo, "'", "''") & "' AND "
        Else
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE LIKE '%' AND "
        End If
        If Not sValoreCivico = "---" Then
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO='" & Replace(sValoreCivico, "'", "''") & "' AND "
        Else
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO LIKE '%' AND "
        End If

        Dim query As String = ""

        If risposta = "---" And valore = "---" Then

            If codiceUnitaImmobiliare = "" Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA "

            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' "
                Else
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' "
                End If
            End If

        ElseIf risposta <> "---" And valore = "---" Then
            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If

            par.cmd.CommandText = query & " PU_QUALITA='" & risposta & "' OR PU_CORTESIA='" & risposta & "' OR PU_PARTI_COMUNI='" & risposta & "' OR PU_IGIENE='" & risposta & "' OR PU_RIF_INGOMBRANTI='" & risposta & "')"


        ElseIf risposta = "---" And valore <> "---" Then

            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If

            par.cmd.CommandText = query & " PU_REGOLARITA_VAL='" & valore & "' OR PU_QUALITA_VAL='" & valore & "' OR PU_CORTESIA_VAL='" & valore & "' OR PU_PARTI_COMUNI_VAL='" & valore & "' OR PU_IGIENE_VAL='" & valore & "' OR PU_RIF_INGOMBRANTI_VAL='" & valore & "')"

        Else

            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If

            par.cmd.CommandText = query & " (PU_REGOLARITA_VAL='" & valore & "' AND PU_REGOLARITA='" & risposta & "') OR (PU_QUALITA_VAL='" & valore & "' AND PU_QUALITA='" & risposta & "') OR (PU_CORTESIA_VAL='" & valore & "' AND PU_CORTESIA='" & risposta & "') OR (PU_IGIENE_VAL='" & valore & "' AND PU_IGIENE='" & risposta & "') OR (PU_PARTI_COMUNI_VAL='" & valore & "' AND PU_PARTI_COMUNI='" & risposta & "') OR (PU_RIF_INGOMBRANTI_VAL='" & valore & "' AND PU_RIF_INGOMBRANTI='" & risposta & "'))"

        End If

        Dim areaGrafico As String
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()

        Dim arraySI As Integer = 0
        Dim arrayAB As Integer = 0
        Dim arrayPC As Integer = 0
        Dim arrayNO As Integer = 0
        Dim array1 As Integer = 0
        Dim array2 As Integer = 0
        Dim array3 As Integer = 0
        Dim array4 As Integer = 0
        Dim i As Integer = 0

        While myReader.Read

            Select Case par.IfNull(myReader("PU_REGOLARITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PU_QUALITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PU_CORTESIA"), "")

                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PU_IGIENE"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PU_PARTI_COMUNI"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PU_RIF_INGOMBRANTI"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select


            '---------------------------
            'VALORI
            '---------------------------

            Select Case par.IfNull(myReader("PU_REGOLARITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("PU_QUALITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("PU_CORTESIA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("PU_IGIENE_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("PU_PARTI_COMUNI_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("PU_RIF_INGOMBRANTI_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


        End While
        chiudiConnessione()

        Dim campo As String = "Servizi di pulizia"


        areaGrafico = "ChartArea1"
        Chart1.Series.Add(campo)
        Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart1.Series(campo).LabelForeColor = Drawing.Color.White
        Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart1.Series(campo)("BarLabelStyle") = "Center"


        Dim somma As Integer
        somma = arrayAB + arrayNO + arrayPC + arraySI
        Dim vettPerc As ArrayList = CalcolaPercentuali(arraySI, arrayAB, arrayPC, arrayNO)
        If arraySI <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(0)).AxisLabel = "SI=" & CStr(vettPerc(0)) & "%"
        End If
        If arrayAB <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(1)).AxisLabel = "AB=" & CStr(vettPerc(1)) & "%"
        End If
        If arrayPC <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(2)).AxisLabel = "PC=" & CStr(vettPerc(2)) & "%"
        End If
        If arrayNO <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(3)).AxisLabel = "NO=" & CStr(vettPerc(3)) & "%"
        End If

        'Chart1.Titles.Add("TITOLO")
        Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart1.BackColor = Drawing.Color.White
        Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
        Chart1.Series(campo)("DrawingStyle") = "Cylinder"
        Chart1.Series(campo).ChartArea = areaGrafico
        Chart1.Legends.Add("Legenda")
        Chart1.Visible = True
        areaGrafico = "ChartArea2"
        Chart2.Series.Add(campo)
        Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart2.Series(campo).LabelForeColor = Drawing.Color.White
        Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart2.Series(campo)("BarLabelStyle") = "Center"

        somma = array1 + array2 + array3 + array4
        vettPerc.Clear()
        vettPerc = CalcolaPercentuali(array1, array2, array3, array4)
        If array1 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(0)).AxisLabel = "1=" & CStr(vettPerc(0)) & "%"
        End If
        If array2 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(1)).AxisLabel = "2=" & CStr(vettPerc(1)) & "%"
        End If
        If array3 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(2)).AxisLabel = "3=" & CStr(vettPerc(2)) & "%"
        End If
        If array4 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(3)).AxisLabel = "4=" & CStr(vettPerc(3)) & "%"
        End If

        'Chart2.Titles.Add("TITOLO")
        Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart2.BackColor = Drawing.Color.White
        Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
        Chart2.Series(campo)("DrawingStyle") = "Cylinder"
        Chart2.Series(campo).ChartArea = areaGrafico
        Chart2.Legends.Add("Legenda")
        Chart2.Visible = True

        'Label5.Text = "Numero di questionari coinvolti: " & CStr(Round(somma / 6))
    End Sub

    Protected Sub creaGraficoServiziRiscaldamento(ByVal risposta As String, ByVal valore As String)
        apriConnessione()
        Dim codiceUnitaImmobiliare As String = Trim(sValoreCODUI)
        Dim controlloIndirizzo As String = ""
        Dim controlloCivico As String = ""



        If Not sValoreIndirizzo = "---" Then
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE='" & Replace(sValoreIndirizzo, "'", "''") & "' AND "
        Else
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE LIKE '%' AND "
        End If
        If Not sValoreCivico = "---" Then
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO='" & Replace(sValoreCivico, "'", "''") & "' AND "
        Else
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO LIKE '%' AND "
        End If

        Dim query As String = ""

        If risposta = "---" And valore = "---" Then

            If codiceUnitaImmobiliare = "" Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA "

            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' "
                Else
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' "
                End If
            End If

        ElseIf risposta <> "---" And valore = "---" Then

            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If

            par.cmd.CommandText = query & " RI_REGOLARITA='" & risposta & "' OR RI_QUALITA='" & risposta & "' OR RI_CORTESIA='" & risposta & "' OR RI_TEMPERATURA='" & risposta & "' OR RI_GUASTI='" & risposta & "' OR RI_RIS_GUASTI='" & risposta & "')"

        ElseIf risposta = "---" And valore <> "---" Then

            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If

            par.cmd.CommandText = query & " RI_REGOLARITA_VAL='" & valore & "' OR RI_QUALITA_VAL='" & valore & "' OR RI_CORTESIA_VAL='" & valore & "' OR RI_TEMPERATURA_VAL='" & valore & "' OR RI_GUASTI_VAL='" & valore & "' OR RI_RIS_GUASTI_VAL='" & valore & "')"

        Else

            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If

            par.cmd.CommandText = query & " (RI_REGOLARITA_VAL='" & valore & "' AND RI_REGOLARITA='" & risposta & "') OR (RI_QUALITA_VAL='" & valore & "' AND RI_QUALITA='" & risposta & "') OR (RI_CORTESIA_VAL='" & valore & "' AND RI_CORTESIA='" & risposta & "') OR (RI_TEMPERATURA_VAL='" & valore & "' AND RI_TEMPERATURA='" & risposta & "') OR (RI_GUASTI_VAL='" & valore & "' AND RI_GUASTI='" & risposta & "') OR (RI_RIS_GUASTI_VAL='" & valore & "' AND RI_RIS_GUASTI='" & risposta & "'))"

        End If

        Dim areaGrafico As String

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()

        Dim arraySI As Integer = 0
        Dim arrayAB As Integer = 0
        Dim arrayPC As Integer = 0
        Dim arrayNO As Integer = 0
        Dim array1 As Integer = 0
        Dim array2 As Integer = 0
        Dim array3 As Integer = 0
        Dim array4 As Integer = 0
        Dim i As Integer = 0

        While myReader.Read

            Select Case par.IfNull(myReader("RI_REGOLARITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("RI_QUALITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("RI_CORTESIA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("RI_TEMPERATURA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("RI_GUASTI"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("RI_RIS_GUASTI"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select


            '---------------------------
            'VALORI
            '---------------------------

            Select Case par.IfNull(myReader("RI_REGOLARITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("RI_QUALITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("RI_CORTESIA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("RI_TEMPERATURA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("RI_GUASTI_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("RI_RIS_GUASTI_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


        End While
        chiudiConnessione()

        Dim campo As String = "Servizi di riscaldamento"


        areaGrafico = "ChartArea1"
        Chart1.Series.Add(campo)
        Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart1.Series(campo).LabelForeColor = Drawing.Color.White
        Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart1.Series(campo)("BarLabelStyle") = "Center"


        Dim somma As Integer

        somma = arrayAB + arrayNO + arrayPC + arraySI
        Dim vettPerc As ArrayList = CalcolaPercentuali(arraySI, arrayAB, arrayPC, arrayNO)
        If arraySI <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(0)).AxisLabel = "SI=" & CStr(vettPerc(0)) & "%"
        End If
        If arrayAB <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(1)).AxisLabel = "AB=" & CStr(vettPerc(1)) & "%"
        End If
        If arrayPC <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(2)).AxisLabel = "PC=" & CStr(vettPerc(2)) & "%"
        End If
        If arrayNO <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(3)).AxisLabel = "NO=" & CStr(vettPerc(3)) & "%"
        End If

        'Chart1.Titles.Add("TITOLO")
        Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart1.BackColor = Drawing.Color.White
        Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
        Chart1.Series(campo)("DrawingStyle") = "Cylinder"
        Chart1.Series(campo).ChartArea = areaGrafico
        Chart1.Legends.Add("Legenda")
        Chart1.Visible = True

        areaGrafico = "ChartArea2"
        Chart2.Series.Add(campo)
        Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart2.Series(campo).LabelForeColor = Drawing.Color.White
        Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart2.Series(campo)("BarLabelStyle") = "Center"

        somma = array1 + array2 + array3 + array4
        vettPerc.Clear()
        vettPerc = CalcolaPercentuali(array1, array2, array3, array4)
        If array1 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(0)).AxisLabel = "1=" & CStr(vettPerc(0)) & "%"
        End If
        If array2 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(1)).AxisLabel = "2=" & CStr(vettPerc(1)) & "%"
        End If
        If array3 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(2)).AxisLabel = "3=" & CStr(vettPerc(2)) & "%"
        End If
        If array4 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(3)).AxisLabel = "4=" & CStr(vettPerc(3)) & "%"
        End If

        'Chart2.Titles.Add("TITOLO")
        Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart2.BackColor = Drawing.Color.White
        Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
        Chart2.Series(campo)("DrawingStyle") = "Cylinder"
        Chart2.Series(campo).ChartArea = areaGrafico
        Chart2.Legends.Add("Legenda")
        Chart2.Visible = True

        'Label5.Text = "Numero di questionari coinvolti: " & CStr(Round(somma / 6))

    End Sub

    Protected Sub creaGraficoServiziPortierato(ByVal risposta As String, ByVal valore As String)
        apriConnessione()
        Dim codiceUnitaImmobiliare As String = Trim(sValoreCODUI)
        Dim controlloIndirizzo As String = ""
        Dim controlloCivico As String = ""



        If Not sValoreIndirizzo = "---" Then
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE='" & Replace(sValoreIndirizzo, "'", "''") & "' AND "
        Else
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE LIKE '%' AND "
        End If
        If Not sValoreCivico = "---" Then
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO='" & Replace(sValoreCivico, "'", "''") & "' AND "
        Else
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO LIKE '%' AND "
        End If

        Dim query As String = ""

        If risposta = "---" And valore = "---" Then

            If codiceUnitaImmobiliare = "" Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA "

            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' "
                Else
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' "
                End If
            End If

        ElseIf risposta <> "---" And valore = "---" Then

            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If

            par.cmd.CommandText = query & " PO_REGOLARITA='" & risposta & "' OR PO_QUALITA='" & risposta & "' OR PO_CORTESIA='" & risposta & "' OR PO_INF_COMPLETE='" & risposta & "' OR PO_POSTA='" & risposta & "')"

        ElseIf risposta = "---" And valore <> "---" Then

            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If

            par.cmd.CommandText = query & " PO_REGOLARITA_VAL='" & valore & "' OR PO_QUALITA_VAL='" & valore & "' OR PO_CORTESIA_VAL='" & valore & "' OR PO_INF_COMPLETE_VAL='" & valore & "' OR PO_POSTA_VAL='" & valore & "')"


        Else

            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If

            par.cmd.CommandText = query & " (PO_REGOLARITA_VAL='" & valore & "' AND PO_REGOLARITA='" & risposta & "') OR (PO_QUALITA_VAL='" & valore & "' AND PO_QUALITA='" & risposta & "') OR (PO_CORTESIA_VAL='" & valore & "' AND PO_CORTESIA='" & risposta & "') OR (PO_INF_COMPLETE_VAL='" & valore & "' AND PO_INF_COMPLETE='" & risposta & "') OR (PO_POSTA_VAL='" & valore & "' AND PO_POSTA='" & risposta & "'))"

        End If

        Dim areaGrafico As String

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()

        Dim arraySI As Integer = 0
        Dim arrayAB As Integer = 0
        Dim arrayPC As Integer = 0
        Dim arrayNO As Integer = 0
        Dim array1 As Integer = 0
        Dim array2 As Integer = 0
        Dim array3 As Integer = 0
        Dim array4 As Integer = 0
        Dim i As Integer = 0

        While myReader.Read

            Select Case par.IfNull(myReader("PO_REGOLARITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PO_QUALITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PO_CORTESIA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PO_INF_COMPLETE"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("PO_POSTA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            '---------------------------
            'VALORI
            '---------------------------

            Select Case par.IfNull(myReader("PO_REGOLARITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("PO_QUALITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("PO_CORTESIA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("PO_INF_COMPLETE_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("PO_POSTA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


        End While
        chiudiConnessione()

        Dim campo As String = "Servizi di portierato"


        areaGrafico = "ChartArea1"
        Chart1.Series.Add(campo)
        Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart1.Series(campo).LabelForeColor = Drawing.Color.White
        Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart1.Series(campo)("BarLabelStyle") = "Center"


        Dim somma As Integer

        somma = arrayAB + arrayNO + arrayPC + arraySI
        Dim vettPerc As ArrayList = CalcolaPercentuali(arraySI, arrayAB, arrayPC, arrayNO)
        If arraySI <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(0)).AxisLabel = "SI=" & CStr(vettPerc(0)) & "%"
        End If
        If arrayAB <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(1)).AxisLabel = "AB=" & CStr(vettPerc(1)) & "%"
        End If
        If arrayPC <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(2)).AxisLabel = "PC=" & CStr(vettPerc(2)) & "%"
        End If
        If arrayNO <> 0 Then

            Chart1.Series(campo).Points.Add(vettPerc(3)).AxisLabel = "NO=" & CStr(vettPerc(3)) & "%"
        End If

        'Chart1.Titles.Add("TITOLO")
        Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart1.BackColor = Drawing.Color.White
        Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
        Chart1.Series(campo)("DrawingStyle") = "Cylinder"
        Chart1.Series(campo).ChartArea = areaGrafico
        Chart1.Legends.Add("Legenda")
        Chart1.Visible = True


        areaGrafico = "ChartArea2"
        Chart2.Series.Add(campo)
        Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart2.Series(campo).LabelForeColor = Drawing.Color.White
        Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart2.Series(campo)("BarLabelStyle") = "Center"

        somma = array1 + array2 + array3 + array4
        vettPerc.Clear()
        vettPerc = CalcolaPercentuali(array1, array2, array3, array4)
        If array1 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(0)).AxisLabel = "1=" & CStr(vettPerc(0)) & "%"
        End If
        If array2 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(1)).AxisLabel = "2=" & CStr(vettPerc(1)) & "%"
        End If
        If array3 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(2)).AxisLabel = "3=" & CStr(vettPerc(2)) & "%"
        End If
        If array4 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(3)).AxisLabel = "4=" & CStr(vettPerc(3)) & "%"
        End If

        'Chart2.Titles.Add("TITOLO")
        Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart2.BackColor = Drawing.Color.White
        Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
        Chart2.Series(campo)("DrawingStyle") = "Cylinder"
        Chart2.Series(campo).ChartArea = areaGrafico
        Chart2.Legends.Add("Legenda")
        Chart2.Visible = True

        'Label5.Text = "Numero di questionari coinvolti: " & CStr(Round(somma / 5))
    End Sub

    Protected Sub creaGraficoServiziManutenzione(ByVal risposta As String, ByVal valore As String)
        apriConnessione()
        Dim codiceUnitaImmobiliare As String = Trim(sValoreCODUI)
        Dim controlloIndirizzo As String = ""
        Dim controlloCivico As String = ""



        If Not sValoreIndirizzo = "---" Then
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE='" & Replace(sValoreIndirizzo, "'", "''") & "' AND "
        Else
            controlloIndirizzo = controlloIndirizzo & "SISCOM_MI.INDIRIZZI.DESCRIZIONE LIKE '%' AND "
        End If
        If Not sValoreCivico = "---" Then
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO='" & Replace(sValoreCivico, "'", "''") & "' AND "
        Else
            controlloCivico = controlloCivico & "SISCOM_MI.INDIRIZZI.CIVICO LIKE '%' AND "
        End If

        Dim query As String = ""

        If risposta = "---" And valore = "---" Then

            If codiceUnitaImmobiliare = "" Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA "

            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' "
                Else
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' "
                End If
            End If

        ElseIf risposta <> "---" And valore = "---" Then
            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If

            par.cmd.CommandText = query & " VE_REGOLARITA='" & risposta & "' OR VE_QUALITA='" & risposta & "' OR VE_CORTESIA='" & risposta & "' OR VE_SMALTIMENTO_RIF='" & risposta & "' OR VE_RUMORE='" & risposta & "' OR VE_TEMPESTIVITA='" & risposta & "')"


        ElseIf risposta = "---" And valore <> "---" Then
            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If

            par.cmd.CommandText = query & " VE_REGOLARITA_VAL='" & valore & "' OR VE_QUALITA_VAL='" & valore & "' OR VE_CORTESIA_VAL='" & valore & "' OR VE_SMALTIMENTO_RIF_VAL='" & valore & "' OR VE_RUMORE_VAL='" & valore & "' OR VE_TEMPESTIVITA_VAL='" & valore & "')"


        Else
            If codiceUnitaImmobiliare = "" Then
                query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND ("
            Else
                If Right(codiceUnitaImmobiliare, 1) = "*" Then
                    codiceUnitaImmobiliare = Replace(codiceUnitaImmobiliare, "*", "%")
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & codiceUnitaImmobiliare & "' AND ("
                Else
                    query = "SELECT * FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CUSTOMER_SATISFACTION,SEPA.OPERATORI WHERE " & controlloGiudizio & controlloDate & controlloOperatore & controlloCivico & controlloIndirizzo & " SISCOM_MI.INDIRIZZI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO AND SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codiceUnitaImmobiliare & "' AND ("
                End If
            End If

            par.cmd.CommandText = query & " (VE_REGOLARITA_VAL='" & valore & "' AND VE_REGOLARITA='" & risposta & "') OR (VE_QUALITA_VAL='" & valore & "' AND VE_QUALITA='" & risposta & "') OR (VE_CORTESIA_VAL='" & valore & "' AND VE_CORTESIA='" & risposta & "') OR (VE_SMALTIMENTO_RIF_VAL='" & valore & "' AND VE_SMALTIMENTO_RIF='" & risposta & "') OR (VE_RUMORE_VAL='" & valore & "' AND VE_RUMORE='" & risposta & "') OR (VE_TEMPESTIVITA_VAL='" & valore & "' AND VE_TEMPESTIVITA='" & risposta & "'))"

        End If

        Dim areaGrafico As String

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()

        Dim arraySI As Integer = 0
        Dim arrayAB As Integer = 0
        Dim arrayPC As Integer = 0
        Dim arrayNO As Integer = 0
        Dim array1 As Integer = 0
        Dim array2 As Integer = 0
        Dim array3 As Integer = 0
        Dim array4 As Integer = 0
        Dim i As Integer = 0

        While myReader.Read

            Select Case par.IfNull(myReader("VE_REGOLARITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("VE_QUALITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("VE_CORTESIA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("VE_RUMORE"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("VE_TEMPESTIVITA"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case par.IfNull(myReader("VE_SMALTIMENTO_RIF"), "")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            '---------------------------
            'VALORI
            '---------------------------

            Select Case par.IfNull(myReader("VE_REGOLARITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("VE_QUALITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("VE_CORTESIA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("VE_SMALTIMENTO_RIF_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("VE_RUMORE_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case par.IfNull(myReader("VE_TEMPESTIVITA_VAL"), "")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


        End While
        chiudiConnessione()

        Dim campo As String = "Servizi di manutenzione del verde"


        areaGrafico = "ChartArea1"
        Chart1.Series.Add(campo)
        Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart1.Series(campo).LabelForeColor = Drawing.Color.White
        Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart1.Series(campo)("BarLabelStyle") = "Center"


        Dim somma As Integer
        somma = arrayAB + arrayNO + arrayPC + arraySI
        Dim vettPerc As ArrayList = CalcolaPercentuali(arraySI, arrayAB, arrayPC, arrayNO)






        If arraySI <> 0 Then
            Chart1.Series(campo).Points.Add(vettPerc(0)).AxisLabel = "SI=" & CStr(Round(CDbl(vettPerc(0)), 2)) & "%"
        End If
        If arrayAB <> 0 Then
            Chart1.Series(campo).Points.Add(vettPerc(1)).AxisLabel = "AB=" & CStr(Round(CDbl(vettPerc(1)), 2)) & "%"
        End If
        If arrayPC <> 0 Then
            Chart1.Series(campo).Points.Add(vettPerc(2)).AxisLabel = "PC=" & CStr(Round(CDbl(vettPerc(2)), 2)) & "%"
        End If
        If arrayNO <> 0 Then
            Chart1.Series(campo).Points.Add(vettPerc(3)).AxisLabel = "NO=" & CStr(Round(CDbl(vettPerc(3)), 2)) & "%"
        End If

        'Chart1.Titles.Add("TITOLO")
        Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart1.BackColor = Drawing.Color.White
        Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
        Chart1.Series(campo)("DrawingStyle") = "Cylinder"
        Chart1.Series(campo).ChartArea = areaGrafico
        Chart1.Legends.Add("Legenda")
        Chart1.Visible = True


        areaGrafico = "ChartArea2"
        Chart2.Series.Add(campo)
        Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart2.Series(campo).LabelForeColor = Drawing.Color.White
        Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart2.Series(campo)("BarLabelStyle") = "Center"

        somma = array1 + array2 + array3 + array4
        vettPerc.Clear()
        vettPerc = CalcolaPercentuali(array1, array2, array3, array4)
        If array1 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(0)).AxisLabel = "1=" & CStr(vettPerc(0)) & "%"
        End If
        If array2 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(1)).AxisLabel = "2=" & CStr(vettPerc(1)) & "%"
        End If
        If array3 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(2)).AxisLabel = "3=" & CStr(vettPerc(2)) & "%"
        End If
        If array4 <> 0 Then

            Chart2.Series(campo).Points.Add(vettPerc(3)).AxisLabel = "4=" & CStr(vettPerc(3)) & "%"
        End If



        'Chart2.Titles.Add("TITOLO")
        Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart2.BackColor = Drawing.Color.White
        Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.White
        Chart2.Series(campo)("DrawingStyle") = "Cylinder"
        Chart2.Series(campo).ChartArea = areaGrafico
        Chart2.Legends.Add("Legenda")
        Chart2.Visible = True

        'Label5.Text = "Numero di questionari coinvolti: " & CStr(Round(somma / 6))
    End Sub

    Protected Function CalcolaPercentuali(ByVal x1 As Double, ByVal x2 As Double, ByVal x3 As Double, ByVal x4 As Double) As ArrayList
        Dim sommax = x1 + x2 + x3 + x4
        Dim percx1 As Double = Round(x1 / sommax * 100, 2)
        Dim percx2 As Double = Round(x2 / sommax * 100, 2)
        Dim percx3 As Double = Round(x3 / sommax * 100, 2)
        Dim percx4 As Double = Round(x4 / sommax * 100, 2)
        Dim sommaPerc = percx1 + percx2 + percx3 + percx4
        Dim vettorePerc As New System.Collections.ArrayList

        If sommaPerc <> 100 Then
            If percx1 <> 0 Then
                percx1 = 100 - percx2 - percx3 - percx4
            ElseIf percx2 <> 0 Then
                percx2 = 100 - percx1 - percx3 - percx4
            ElseIf percx3 <> 0 Then
                percx3 = 100 - percx2 - percx1 - percx4
            Else
                percx4 = 100 - percx2 - percx1 - percx3
            End If

        End If
        vettorePerc.Add(percx1)
        vettorePerc.Add(percx2)
        vettorePerc.Add(percx3)
        vettorePerc.Add(percx4)
        Return vettorePerc
    End Function

    Protected Sub btnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnElimina.Click

        apriConnessione()
        par.cmd.CommandText = "DELETE FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID=" & LBLID.Value

        If LBLok.Value = "1" Then
            par.cmd.ExecuteNonQuery()
            Response.Write("<script>alert('Eliminazione avvenuta correttamente');</script>")
            LBLok.Value = "0"
        End If

        LBLID.Value = ""
        BindGrid(domandaSel.Value)
        If contaris.Value <> 0 Then
            disegnaGrafici()
        End If
        chiudiConnessione()
    End Sub

End Class