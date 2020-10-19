
Partial Class Contabilita_DetMorosita
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            If Not IsPostBack Then

                Try
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                    Dim COLORE As String = "#E6E6E6"
                    Dim bTrovato As Boolean = False
                    Dim TotDaPagare As Double = 0
                    Dim TotFinalePagato As Double = 0
                    Dim TotFinaleDaPagare As Double = 0
                    Dim testoTabella As String = ""
                    Dim testoTabellaVoci As String = ""
                    Dim sStringaSql As String = ""
                    Dim Nbolletta As String

                    Dim Contatore As Integer = 0


                    testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                                    & "<tr>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial;'><strong></strong></span></td>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial;'><strong>NUMERO BOLLETTA</strong></span></td>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial;'><strong>TIPO</strong></span></td>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial;'><strong>NUMERO RATA</strong></span></td>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA EMISSIONE</strong></span></td>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA SCADENZA</strong></span></td>" _
                                    & "<td style='height: 19px; text-align:center'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>VOCI DELLA BOLLETTA</strong></span></td>" _
                                    & "<td style='height: 19px;text-align:right'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>TOTALE BOLLETTA</strong></span></td>" _
                                    & "<td style='height: 19px;text-align:center'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA PAGAMENTO</strong></span></td>" _
                                    & "<td style='height: 19px;text-align:right'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>TOT PAGATO</strong></span></td>" _
                                    & "</tr>"
                    testoTabellaVoci = "<table cellpadding='0' cellspacing='0' width='100%'>" _
                        & "<tr>" _
                        & "<td style='height: 15'>" _
                        & "<span style='font-size: 6pt; font-family:Courier New'><strong>DESCRIZIONE</strong></span></td>" _
                        & "<td style='height: 15px;text-align:right'>" _
                        & "<span style='font-size: 6pt; font-family: Courier New'><strong>IMPORTO</strong></span></td>" _
                        & "</tr>"
                    '******APERTURA CONNESSIONE*****
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)
                    End If

                    Dim UltimoPagam As String = 0
                    UltimoPagam = par.FormattaData(Format(Now, "yyyyMMdd")) 'par.IfNull(myReaderTEMP("ULTIMA_BOLL"), 0)

                    If Request.QueryString("IDBOLL") <> "" Then

                        'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.INTESTATARI_RAPPORTO WHERE  " & sStringaSql & " INTESTATARI_RAPPORTO.ID_CONTRATTO= BOL_BOLLETTE.ID_CONTRATTO AND INTESTATARI_RAPPORTO.DATA_FINE>= TO_CHAR(TO_DATE(CURRENT_DATE,'dd/mm/yyyy'),'yyyymmdd')  AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA =  " & Request.QueryString("IDANA") & " ORDER BY BOL_BOLLETTE.ID DESC"
                        par.cmd.CommandText = "SELECT BOL_BOLLETTE.*, (CASE WHEN ID_BOLLETTA_RIC IS NULL THEN TIPO_BOLLETTE.ACRONIMO ELSE TIPO_BOLLETTE.ACRONIMO ||'/RIC'END) AS TIPO,ID_TIPO FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.TIPO_BOLLETTE " _
                                            & "WHERE  BOL_BOLLETTE.ID = " & Request.QueryString("IDBOLL") & " AND TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO ORDER BY BOL_BOLLETTE.ID DESC"

                        myReader = par.cmd.ExecuteReader

                        If myReader.Read Then
                            Nbolletta = ""
                            If par.IfNull(myReader("N_RATA"), "") = "99" Then
                                Nbolletta = "MA/" & par.IfNull(myReader("ANNO"), "")
                            ElseIf par.IfNull(myReader("N_RATA"), "") = "999" Then
                                Nbolletta = "AU/" & par.IfNull(myReader("ANNO"), "")
                            ElseIf par.IfNull(myReader("N_RATA"), "") = "99999" Then
                                Nbolletta = "CO/" & par.IfNull(myReader("ANNO"), "")
                            Else
                                Nbolletta = par.IfNull(myReader("N_RATA"), "") & "/" & par.IfNull(myReader("ANNO"), "")
                            End If

                            Contatore = Contatore + 1
                            If par.IfNull(myReader("FL_ANNULLATA"), "0") = "0" Then

                                testoTabella = testoTabella _
                                & "<tr>" _
                                & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & Contatore & ")</span></td>" _
                                & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & Nbolletta & "</span></td>" _
                                & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("TIPO"), "n.d.") & "</span></td>" _
                                & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & Format(par.IfNull(myReader("ID"), ""), "0000000000") & "</span></td>" _
                                & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(myReader("DATA_EMISSIONE"), "")) & "</span></td>" _
                                & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")) & "</span></td>" _
                                & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                & "<span style='font-size: 8pt; font-family: Courier New''>" & testoTabellaVoci & "</span>"

                                par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*, T_VOCI_BOLLETTA.DESCRIZIONE " _
                                                    & " FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), 0) _
                                                    & " AND ID_VOCE = T_VOCI_BOLLETTA.ID"
                                myReader2 = par.cmd.ExecuteReader

                                Do While myReader2.Read
                                    testoTabella = testoTabella _
                                    & "<tr bgcolor = '" & COLORE & "'>" _
                                    & "<td style='height: 15px'>" _
                                    & "<span style='font-size: 6pt; font-family: Courier New'>" & par.IfNull(myReader2("DESCRIZIONE"), "") & "</span></td>" _
                                    & "<td style='height: 15px;text-align:right'>" _
                                    & "<span style='font-size: 6pt; font-family: Courier New'>€." & Format((par.IfNull(myReader2("IMPORTO"), 0)), "##,##0.00") & "</span></td>"

                                    TotDaPagare = TotDaPagare + par.IfNull(myReader2("IMPORTO"), 0)
                                    If COLORE = "#FFFFFF" Then
                                        COLORE = "#E6E6E6"
                                    Else
                                        COLORE = "#FFFFFF"
                                    End If
                                Loop
                                myReader2.Close()
                                testoTabella = testoTabella & "</table></td>" _
                                & "<td style='height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format(TotDaPagare, "##,##0.00") & "</span></td>" _
                                & "<td style='height: 19px;text-align:center;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), "")) & "&nbsp;</span></td>" _
                                & "<td style='height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format(par.IfNull(myReader("IMPORTO_PAGATO"), 0), "##,##0.00") & "</span></td></tr>"
                                TotFinalePagato = TotFinalePagato + par.IfNull(myReader("IMPORTO_PAGATO"), 0)
                                TotFinaleDaPagare = TotFinaleDaPagare + TotDaPagare
                                TotDaPagare = 0

                                par.cmd.CommandText = "SELECT BOL_BOLLETTE.*, (CASE WHEN ID_BOLLETTA_RIC IS NULL THEN TIPO_BOLLETTE.ACRONIMO ELSE TIPO_BOLLETTE.ACRONIMO ||'/RIC'END) AS TIPO,ID_TIPO FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.TIPO_BOLLETTE " _
                                                    & "WHERE  BOL_BOLLETTE.ID_BOLLETTA_RIC = " & Request.QueryString("IDBOLL") & " AND TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO ORDER BY BOL_BOLLETTE.ID DESC"

                            Else
                                testoTabella = testoTabella _
                                & "<tr>" _
                                & "<td style='background-color: #CCCCFF;height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & Contatore & ")</span></td>" _
                                & "<td style='background-color: #CCCCFF;height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & Format(par.IfNull(myReader("ID"), ""), "0000000000") & "</span></td>" _
                                & "<td style='background-color: #CCCCFF;height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'>" & Nbolletta & "</span></td>" _
                                & "<td style='background-color: #CCCCFF;height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(myReader("DATA_EMISSIONE"), "")) & "</span></td>" _
                                & "<td style='background-color: #CCCCFF;height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")) & "</span></td>" _
                                & "<td style='background-color: #CCCCFF;height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                & "<span style='font-size: 8pt; font-family: Courier New''>" & testoTabellaVoci & "</span>"

                                par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*, T_VOCI_BOLLETTA.DESCRIZIONE FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), 0) & " AND ID_VOCE = T_VOCI_BOLLETTA.ID"
                                myReader2 = par.cmd.ExecuteReader

                                Do While myReader2.Read
                                    testoTabella = testoTabella _
                                    & "<tr bgcolor = '" & COLORE & "'>" _
                                    & "<td style='background-color: #CCCCFF;height: 15px'>" _
                                    & "<span style='font-size: 6pt; font-family: Courier New'>" & par.IfNull(myReader2("DESCRIZIONE"), "") & "</span></td>" _
                                    & "<td style='background-color: #CCCCFF;height: 15px;text-align:right'>" _
                                    & "<span style='font-size: 6pt; font-family: Courier New'>€." & Format((par.IfNull(myReader2("IMPORTO"), 0)), "##,##0.00") & "</span></td>"

                                    TotDaPagare = TotDaPagare + 0
                                    If COLORE = "#FFFFFF" Then
                                        COLORE = "#E6E6E6"
                                    Else
                                        COLORE = "#FFFFFF"
                                    End If
                                Loop
                                myReader2.Close()
                                testoTabella = testoTabella & "</table></td>" _
                                & "<td style='background-color: #CCCCFF;height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format(TotDaPagare, "##,##0.00") & "</span></td>" _
                                & "<td style='background-color: #CCCCFF;height: 19px;text-align:center;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>ANNULLATA IL " & par.FormattaData(par.IfNull(myReader("DATA_ANNULLO"), "")) & "&nbsp;</span></td>" _
                                & "<td style='background-color: #CCCCFF;height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format(par.IfNull(myReader("IMPORTO_PAGATO"), 0), "##,##0.00") & "</span></td></tr>"
                                TotFinalePagato = TotFinalePagato + par.IfNull(myReader("IMPORTO_PAGATO"), 0)
                                TotFinaleDaPagare = TotFinaleDaPagare + TotDaPagare
                                TotDaPagare = 0

                                '******************select di tutte le bollette riclassificate nella morosita appena mostrata

                                par.cmd.CommandText = "SELECT BOL_BOLLETTE.*, (CASE WHEN ID_BOLLETTA_RIC IS NULL THEN TIPO_BOLLETTE.ACRONIMO ELSE TIPO_BOLLETTE.ACRONIMO ||'/RIC'END) AS TIPO,ID_TIPO FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.TIPO_BOLLETTE " _
                                                    & "WHERE  BOL_BOLLETTE.ID_BOLLETTA_RIC = " & Request.QueryString("IDBOLL") & " AND TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO ORDER BY BOL_BOLLETTE.ID DESC"
                            End If
                            If myReader("TIPO") = "FIN" Then
                                Me.lblTitolo.Text = "Dettaglio della bolletta di Fine Contratto"
                            ElseIf myReader("TIPO") = "MOR" Then
                                Me.lblTitolo.Text = "Dettaglio della bolletta di Morosità"

                            End If
                        End If
                        myReader.Close()
                        'testoTabella = testoTabella _
                        '& "<tr>" _
                        '& "<td style='height: 19px'>" _
                        '& "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                        '& "<td style='height: 19px'>" _
                        '& "<span style='font-size: 8pt; font-family: Arial'><strong>TOTALE</strong></span></td>" _
                        '& "<td style='height: 19px'>" _
                        '& "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                        '& "<td style='height: 19px'>" _
                        '& "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                        '& "<td style='height: 19px'>" _
                        '& "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                        '& "<td style='height: 19px'>" _
                        '& "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                        '& "<td style='height: 19px'>" _
                        '& "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                        '& "<td style='height: 19px;text-align:right'>" _
                        '& "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotFinaleDaPagare, "##,##0.00") & "</strong></span></td>" _
                        '& "<td style='height: 19px'>" _
                        '& "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                        '& "<td style='height: 19px;text-align:right'>" _
                        '& "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotFinalePagato, "##,##0.00") & "</strong></span></td>" _
                        '& "<td style='height: 19px'>" _
                        '& "</td>" _
                        '& "<td style='height: 19px'>" _
                        '& "</td>" _
                        '& "</tr>"
                    End If
                    Me.TBL_DETTAGLIO_BOLLETTA.Text = testoTabella & "</table>"


                    '******resetto le variabili di appoggio per la costruzione della nuova tabella**********
                    testoTabella = ""
                    testoTabellaVoci = ""
                    Contatore = 0
                    TotFinaleDaPagare = 0
                    TotFinalePagato = 0
                    TotDaPagare = 0
                    '******costruzione nuova tabella

                    testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                                & "<tr>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial;'><strong></strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial;'><strong>NUMERO BOLLETTA</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial;'><strong>TIPO</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial;'><strong>NUMERO RATA</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA EMISSIONE</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA SCADENZA</strong></span></td>" _
                                & "<td style='height: 19px; text-align:center'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>VOCI DELLA BOLLETTA</strong></span></td>" _
                                & "<td style='height: 19px;text-align:right'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>TOTALE BOLLETTA</strong></span></td>" _
                                & "<td style='height: 19px;text-align:center'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA PAGAMENTO</strong></span></td>" _
                                & "<td style='height: 19px;text-align:right'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>TOT PAGATO</strong></span></td>" _
                                & "</tr>"

                    testoTabellaVoci = "<table cellpadding='0' cellspacing='0' width='100%'>" _
                                    & "<tr>" _
                                    & "<td style='height: 15'>" _
                                    & "<span style='font-size: 6pt; font-family:Courier New'><strong>DESCRIZIONE</strong></span></td>" _
                                    & "<td style='height: 15px;text-align:right'>" _
                                    & "<span style='font-size: 6pt; font-family: Courier New'><strong>IMPORTO</strong></span></td>" _
                                    & "</tr>"
                    '******lettura delle bollette che sono state riclassificate********
                    myReader = par.cmd.ExecuteReader

                    Do While myReader.Read
                        Nbolletta = ""
                        If par.IfNull(myReader("N_RATA"), "") = "99" Then
                            Nbolletta = "MA/" & par.IfNull(myReader("ANNO"), "")
                        ElseIf par.IfNull(myReader("N_RATA"), "") = "999" Then
                            Nbolletta = "AU/" & par.IfNull(myReader("ANNO"), "")
                        ElseIf par.IfNull(myReader("N_RATA"), "") = "99999" Then
                            Nbolletta = "CO/" & par.IfNull(myReader("ANNO"), "")
                        Else
                            Nbolletta = par.IfNull(myReader("N_RATA"), "") & "/" & par.IfNull(myReader("ANNO"), "")
                        End If

                        Contatore = Contatore + 1
                        If par.IfNull(myReader("FL_ANNULLATA"), "0") = "0" Then

                            testoTabella = testoTabella _
                            & "<tr>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & Contatore & ")</span></td>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & Nbolletta & "</span></td>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("TIPO"), "n.d.") & "</span></td>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & Format(par.IfNull(myReader("ID"), ""), "0000000000") & "</span></td>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(myReader("DATA_EMISSIONE"), "")) & "</span></td>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")) & "</span></td>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Courier New''>" & testoTabellaVoci & "</span>"

                            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*, T_VOCI_BOLLETTA.DESCRIZIONE FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), 0) & " AND ID_VOCE = T_VOCI_BOLLETTA.ID"
                            myReader2 = par.cmd.ExecuteReader

                            Do While myReader2.Read
                                testoTabella = testoTabella _
                                & "<tr bgcolor = '" & COLORE & "'>" _
                                & "<td style='height: 15px'>" _
                                & "<span style='font-size: 6pt; font-family: Courier New'>" & par.IfNull(myReader2("DESCRIZIONE"), "") & "</span></td>" _
                                & "<td style='height: 15px;text-align:right'>" _
                                & "<span style='font-size: 6pt; font-family: Courier New'>€." & Format((par.IfNull(myReader2("IMPORTO"), 0)), "##,##0.00") & "</span></td>"

                                TotDaPagare = TotDaPagare + par.IfNull(myReader2("IMPORTO"), 0)
                                If COLORE = "#FFFFFF" Then
                                    COLORE = "#E6E6E6"
                                Else
                                    COLORE = "#FFFFFF"
                                End If
                            Loop
                            myReader2.Close()
                            testoTabella = testoTabella & "</table></td>" _
                            & "<td style='height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format(TotDaPagare, "##,##0.00") & "</span></td>" _
                            & "<td style='height: 19px;text-align:center;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), "")) & "&nbsp;</span></td>" _
                            & "<td style='height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format(par.IfNull(myReader("IMPORTO_PAGATO"), 0), "##,##0.00") & "</span></td></tr>"
                            TotFinalePagato = TotFinalePagato + par.IfNull(myReader("IMPORTO_PAGATO"), 0)
                            TotFinaleDaPagare = TotFinaleDaPagare + TotDaPagare
                            TotDaPagare = 0

                        Else
                            testoTabella = testoTabella _
                            & "<tr>" _
                            & "<td style='background-color: #CCCCFF;height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & Contatore & ")</span></td>" _
                            & "<td style='background-color: #CCCCFF;height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & Format(par.IfNull(myReader("ID"), ""), "0000000000") & "</span></td>" _
                            & "<td style='background-color: #CCCCFF;height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & Nbolletta & "</span></td>" _
                            & "<td style='background-color: #CCCCFF;height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(myReader("DATA_EMISSIONE"), "")) & "</span></td>" _
                            & "<td style='background-color: #CCCCFF;height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")) & "</span></td>" _
                            & "<td style='background-color: #CCCCFF;height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Courier New''>" & testoTabellaVoci & "</span>"

                            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*, T_VOCI_BOLLETTA.DESCRIZIONE FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), 0) & " AND ID_VOCE = T_VOCI_BOLLETTA.ID"
                            myReader2 = par.cmd.ExecuteReader

                            Do While myReader2.Read
                                testoTabella = testoTabella _
                                & "<tr bgcolor = '" & COLORE & "'>" _
                                & "<td style='background-color: #CCCCFF;height: 15px'>" _
                                & "<span style='font-size: 6pt; font-family: Courier New'>" & par.IfNull(myReader2("DESCRIZIONE"), "") & "</span></td>" _
                                & "<td style='background-color: #CCCCFF;height: 15px;text-align:right'>" _
                                & "<span style='font-size: 6pt; font-family: Courier New'>€." & Format((par.IfNull(myReader2("IMPORTO"), 0)), "##,##0.00") & "</span></td>"

                                TotDaPagare = TotDaPagare + 0
                                If COLORE = "#FFFFFF" Then
                                    COLORE = "#E6E6E6"
                                Else
                                    COLORE = "#FFFFFF"
                                End If
                            Loop
                            myReader2.Close()
                            testoTabella = testoTabella & "</table></td>" _
                            & "<td style='background-color: #CCCCFF;height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format(TotDaPagare, "##,##0.00") & "</span></td>" _
                            & "<td style='background-color: #CCCCFF;height: 19px;text-align:center;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>ANNULLATA IL " & par.FormattaData(par.IfNull(myReader("DATA_ANNULLO"), "")) & "&nbsp;</span></td>" _
                            & "<td style='background-color: #CCCCFF;height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format(par.IfNull(myReader("IMPORTO_PAGATO"), 0), "##,##0.00") & "</span></td></tr>"
                            TotFinalePagato = TotFinalePagato + par.IfNull(myReader("IMPORTO_PAGATO"), 0)
                            TotFinaleDaPagare = TotFinaleDaPagare + TotDaPagare
                            TotDaPagare = 0

                        End If
                    Loop
                    myReader.Close()
                    testoTabella = testoTabella _
                    & "<tr>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong>TOTALE</strong></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                    & "<td style='height: 19px;text-align:right'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotFinaleDaPagare, "##,##0.00") & "</strong></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                    & "<td style='height: 19px;text-align:right'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotFinalePagato, "##,##0.00") & "</strong></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "</td>" _
                    & "<td style='height: 19px'>" _
                    & "</td>" _
                    & "</tr>"

                    Me.TBL_BOLLETTE_RIC.Text = testoTabella & "</table>"


                    If Request.QueryString("IDANA") <> "" Then

                        'par.cmd.CommandText = "SELECT CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"" FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID = " & Request.QueryString("IDANA")
                        'myReader = par.cmd.ExecuteReader
                        'If myReader.Read Then
                        '    Me.LblIntestazione.Text = myReader("INTESTATARIO") & " - informazioni aggiornate al: " & UltimoPagam
                        'End If
                        'myReader.Close()
                    End If

                    '*********************CHIUSURA CONNESSIONE**********************
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Catch ex As Exception
                    Me.lblErrore.Visible = True
                    lblErrore.Text = ex.Message
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End Try
            End If
        End If
    End Sub
End Class
