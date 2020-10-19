
Partial Class CICLO_PASSIVO_CicloPassivo_Plan_ElencoNonApprovate
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Property Tabella() As String
        Get
            If Not (ViewState("par_Tabella") Is Nothing) Then
                Return CStr(ViewState("par_Tabella"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Tabella") = value
        End Set

    End Property



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0

        'If Not IsPostBack Then
        idPianoF.Value = Request.QueryString("IDP")
        per.Value = ""
        CaricaStato()
        CaricaTabella()

        'End If

    End Sub

    Function CaricaTabella()
        Try
            Dim TestoPagina As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)

            TestoPagina = TestoPagina & "<table style='width: 95%;' cellpadding=0 cellspacing = 0'>"
            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>COD.</td><td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>RICHIESTO</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;&nbsp;COMP.&nbsp;&nbsp;</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>APPROVAZIONE</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>APPROVATO GESTORE</td></tr>"

            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 8pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>1</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per il property management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"

            Dim COMPLETO As String = ""
            Dim immagine As String = ""
            Dim importovoce As String = ""
            Dim Approvazione_aler As String = ""
            Dim Totale As Double = 0
            Dim TotaleGenerale As Double = 0
            Dim ImportoVoceRichiesto As String = "0,00"

            'If Session.Item("LIVELLO") = "1" Then
            '    par.cmd.CommandText = "select * from siscom_mi.pf_voci where id_piano_finanziario=" & idPianoF.Value & " and completo='0' and completo_aler='1'  order by codice asc,indice asc"
            'Else
            'par.cmd.CommandText = "select pf_voci.*,pf_voci_struttura.* from siscom_mi.pf_voci_struttura,siscom_mi.pf_voci where id_piano_finanziario=" & idPianoF.Value & " and completo='0' and completo_aler='1' and PF_VOCI.ID IN (SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_OPERATORI WHERE ID_OPERATORE=" & Session.Item("ID_OPERATORE") & ") order by codice asc,indice asc"
            par.cmd.CommandText = "select pf_voci.ID,pf_voci.ID_PIANO_FINANZIARIO,pf_voci.CODICE,pf_voci.DESCRIZIONE,PF_VOCI.ID_CAPITOLO,PF_VOCI.INDICE,PF_VOCI.ID_VOCE_MADRE,PF_VOCI_STRUTTURA.VALORE_NETTO,PF_VOCI_STRUTTURA.COMPLETO,PF_VOCI_STRUTTURA.IVA,PF_VOCI_STRUTTURA.VALORE_LORDO,PF_VOCI_STRUTTURA.COMPLETO_ALER,PF_VOCI_STRUTTURA.VALORE_LORDO_ALER from siscom_mi.pf_voci,SISCOM_MI.PF_VOCI_STRUTTURA where pf_voci.id_piano_finanziario=" & idPianoF.Value & " AND PF_VOCI_STRUTTURA.ID_VOCE=PF_VOCI.ID AND PF_VOCI_STRUTTURA.ID_STRUTTURA=(SELECT ID_UFFICIO FROM OPERATORI WHERE ID=" & Session.Item("ID_OPERATORE") & ") AND (PF_VOCI.ID IN (SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_OPERATORI WHERE ID_OPERATORE=" & Session.Item("ID_OPERATORE") & ") OR PF_VOCI.ID_VOCE_MADRE IN (SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_OPERATORI WHERE ID_OPERATORE=" & Session.Item("ID_OPERATORE") & ")) and completo='0' and completo_aler='1'  order by codice asc,indice asc"
            'End If

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            tuttocompleto.Value = "0"
            While myReader1.Read
                If myReader1("indice") = "21" Then
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per il property management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                    Totale = 0
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 9pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>2</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per il facility management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                End If
                If myReader1("indice") = "41" Then

                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per il facility management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                    Totale = 0
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 8pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>3</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per contributi per sostegno agli inquilini</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                End If
                If myReader1("indice") = "46" Then

                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per contributi per sostegno agli inquilini</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                    Totale = 0
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border: 2px dashed #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 8pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>4</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese diverse</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                End If

                If par.IfNull(myReader1("COMPLETO"), "0") = "0" Then
                    If myReader1("codice") <> "1.02" And myReader1("codice") <> "1.03" And myReader1("codice") <> "2.02" And myReader1("codice") <> "2.03" And myReader1("codice") <> "2.04" Then
                        COMPLETO = "NO"
                        tuttocompleto.Value = "1"
                    End If
                Else
                    COMPLETO = "SI"
                End If

                par.cmd.CommandText = "select * from siscom_mi.pf_voci where id_voce_madre=" & myReader1("id")
                Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader123.HasRows = False Then
                    ImportoVoceRichiesto = par.IfNull(myReader1("valore_lordo"), "0") ' Format(CDbl(par.IfNull(myReader1("valore_lordo"), "0")), "##,##0.00")
                    importovoce = "<a href='DettagliSpesa.aspx?IDV=" & par.IfNull(myReader1("id"), "") & "&P=" & per.Value & "' target='_blank'>" & Format(CDbl(par.IfNull(myReader1("valore_lordo"), "0")), "##,##0.00") & "</a>"
                    Totale = Totale + CDbl(par.IfNull(myReader1("valore_lordo"), "0"))
                    TotaleGenerale = TotaleGenerale + CDbl(par.IfNull(myReader1("valore_lordo"), "0"))
                Else
                    importovoce = "&nbsp;"
                End If
                myReader123.Close()

                If COMPLETO = "NO" And importovoce <> "&nbsp;" Then
                    Select Case par.IfNull(myReader1("codice"), "")
                        Case "2.02.01", "2.02.02", "2.02.03", "2.04.01", "2.04.02", "2.04.03", "2.04.04"
                            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>" & COMPLETO & "</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'><img alt='Divisione Automatica' src='Immagini/Automatico.png' style='cursor: pointer' onclick='ConfermaAutomatica(" & par.IfNull(myReader1("id"), "0") & ");'/>&nbsp;&nbsp;&nbsp;<img alt='Divisione manuale degli importi' src='Immagini/Manuale.png' style='cursor: pointer' onclick='ConfermaManuale(" & par.IfNull(myReader1("id"), "0") & ");'/></td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(CDbl(par.IfNull(myReader1("valore_lordo_aler"), "0")), "##,##0.00") & "</td></tr>"
                        Case Else
                            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>" & COMPLETO & "</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'><img alt='Divisione Automatica' src='Immagini/Automatico.png' style='cursor: pointer' onclick='ConfermaAutomatica(" & par.IfNull(myReader1("id"), "0") & ");'/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(CDbl(par.IfNull(myReader1("valore_lordo_aler"), "0")), "##,##0.00") & "</td></tr>"
                    End Select

                End If

            End While
            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per contributi per sostegno agli inquilini</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
            TestoPagina = TestoPagina & "<tr></tr><tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE GENERALE</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(TotaleGenerale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
            myReader1.Close()
            TestoPagina = TestoPagina & "</table>"

            Tabella = TestoPagina




            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try

    End Function

    Function CaricaStato()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE PF_MAIN.ID=" & idPianoF.Value & " and PF_STATI.ID=PF_MAIN.ID_STATO and t_esercizio_finanziario.id=pf_main.id_esercizio_finanziario"
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                Label1.Text = myReader5("inizio") & "-" & myReader5("fine")
                per.Value = Label1.Text
                lblStato.Text = "STATO:" & par.IfNull(myReader5("stato"), "")
                stato.Value = par.IfNull(myReader5("id_stato"), "")
            End If
            myReader5.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function

    Protected Sub imgConfermare_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgConfermare.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            Dim valore_netto As String = "0,00"
            Dim ValoreLordoAler As Double = 0
            Dim ValoreLordoRichiesto As Double = 0
            Dim DifferenzaLordo As Double = 0
            Dim PercentualeAbbattimento As Double = 0
            Dim ImportoVoceCanone As Double = 0
            Dim ImportoVoceConsumo As Double = 0
            Dim NuovoImportoLordo As Double = 0
            Dim ScartoImportoVoceCanone As Double = 0
            Dim ScartoImportoVoceConsumo As Double = 0

            par.cmd.CommandText = "select pf_voci.*,pf_voci_struttura.* from siscom_mi.pf_voci,siscom_mi.pf_voci_struttura where pf_voci_struttura.id_voce=pf_voci.id and pf_voci_struttura.id_struttura=" & Session.Item("id_struttura") & " and pf_voci.id=" & idvoce.Value
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then

                If myReader5("codice") <> "2.02.01" And myReader5("codice") <> "2.02.02" And myReader5("codice") <> "2.02.03" And myReader5("codice") <> "2.04.01" And myReader5("codice") <> "2.04.02" And myReader5("codice") <> "2.04.03" And myReader5("codice") <> "2.04.04" Then
                    'divizione singola, non legata a patrimonio
                    valore_netto = Format(par.IfNull(myReader5("valore_lordo_aler"), 0) / (1 + (par.IfNull(myReader5("iva"), 0) / 100)), "0.00")

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_EVENTI (ID_PIANO_FINANZIARIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,id_struttura) " _
                                        & "VALUES (" & idPianoF.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                        & "'F84','Voce " & par.PulisciStrSql(par.IfNull(myReader5("codice"), "")) & "-" _
                                        & par.IfNull(myReader5("descrizione"), "") & " - Adeguamento automatico a Euro " & Format(par.IfNull(myReader5("valore_lordo_aler"), 0), "##,##0.00") & "'," & Session.Item("id_struttura") & ")"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "update siscom_mi.pf_voci_struttura set completo='1',completo_aler='1',valore_lordo=valore_lordo_aler,valore_netto=" & par.VirgoleInPunti(valore_netto) & " where id_voce=" & idvoce.Value & " and id_struttura=" & Session.Item("id_struttura")
                    par.cmd.ExecuteNonQuery()
                Else
                    'divisione su patrimonio espresso in euro
                    ValoreLordoRichiesto = par.IfNull(myReader5("valore_lordo"), 0)
                    ValoreLordoAler = par.IfNull(myReader5("valore_lordo_aler"), 0)
                    DifferenzaLordo = ValoreLordoRichiesto - ValoreLordoAler
                    PercentualeAbbattimento = (DifferenzaLordo * 100) / ValoreLordoRichiesto


                    par.cmd.CommandText = "select * from siscom_mi.pf_voci_importo where id_voce=" & idvoce.Value & " and id_lotto in (select id from siscom_mi.lotti where id_filiale=" & Session.Item("id_struttura") & ")"
                    Dim myReader6 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReader6.Read
                        'scorporo iva
                        'prezzo comprensivo di IVA  x 100  diviso (100 + aliquota IVA)

                        If CDbl(par.IfNull(myReader6("valore_canone"), 0)) <> 0 Then
                            'calcolo valore lordo
                            ImportoVoceCanone = CDbl(par.IfNull(myReader6("valore_canone"), 0)) + ((CDbl(par.IfNull(myReader6("valore_canone"), 0)) * CDbl(par.IfNull(myReader6("iva_canone"), 0))) / 100)
                            'lordo meno percentuale
                            ImportoVoceCanone = ImportoVoceCanone - ((ImportoVoceCanone * PercentualeAbbattimento) / 100)
                            NuovoImportoLordo = NuovoImportoLordo + ImportoVoceCanone
                            'nuovo netto
                            ImportoVoceCanone = (ImportoVoceCanone * 100) / (100 + CDbl(par.IfNull(myReader6("iva_canone"), 0)))

                            ScartoImportoVoceCanone = ScartoImportoVoceCanone + (ImportoVoceCanone - Format(ImportoVoceCanone, "0.00"))
                        End If
                        If CDbl(par.IfNull(myReader6("valore_consumo"), 0)) <> 0 Then
                            'calcolo valore lordo
                            ImportoVoceConsumo = CDbl(par.IfNull(myReader6("valore_consumo"), 0)) + ((CDbl(par.IfNull(myReader6("valore_consumo"), 0)) * CDbl(par.IfNull(myReader6("iva_consumo"), 0))) / 100)
                            'lordo meno percentuale
                            ImportoVoceConsumo = ImportoVoceConsumo - ((ImportoVoceConsumo * PercentualeAbbattimento) / 100)
                            NuovoImportoLordo = NuovoImportoLordo + ImportoVoceConsumo
                            'nuovo netto
                            ImportoVoceConsumo = (ImportoVoceConsumo * 100) / (100 + CDbl(par.IfNull(myReader6("iva_consumo"), 0)))
                            ScartoImportoVoceConsumo = ScartoImportoVoceConsumo + (ImportoVoceConsumo - Format(ImportoVoceConsumo, "0.00"))
                        End If

                        If ScartoImportoVoceCanone >= 0.01 Then
                            ImportoVoceCanone = ImportoVoceCanone - 0.01
                            ScartoImportoVoceCanone = 0
                        End If
                        If ScartoImportoVoceCanone <= -0.01 Then
                            ImportoVoceCanone = ImportoVoceCanone + 0.01
                            ScartoImportoVoceCanone = 0
                        End If
                        If ScartoImportoVoceConsumo >= 0.01 Then
                            ImportoVoceConsumo = ImportoVoceConsumo - 0.01
                            ScartoImportoVoceConsumo = 0
                        End If
                        If ScartoImportoVoceConsumo <= -0.01 Then
                            ImportoVoceConsumo = ImportoVoceConsumo + 0.01
                            ScartoImportoVoceConsumo = 0
                        End If
                        par.cmd.CommandText = "update siscom_mi.pf_voci_importo set valore_canone=" & par.VirgoleInPunti(Format(ImportoVoceCanone, "0.00")) & ",valore_consumo=" & par.VirgoleInPunti(Format(ImportoVoceConsumo, "0.00")) & " where id=" & par.IfNull(myReader6("id"), 0)
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "update siscom_mi.lotti_patrimonio_importi set importo=round(importo-((importo*" & par.VirgoleInPunti(PercentualeAbbattimento) & ")/100),2) where id_voce_importo=" & par.IfNull(myReader6("id"), 0)
                        par.cmd.ExecuteNonQuery()

                        ImportoVoceCanone = 0
                        ImportoVoceConsumo = 0

                    Loop
                    myReader6.Close()
                    par.cmd.CommandText = "update siscom_mi.pf_voci_struttura set completo=1,valore_lordo=" & par.VirgoleInPunti(Format(NuovoImportoLordo, "0.00")) & " where id_voce=" & idvoce.Value & " and id_struttura=" & Session.Item("id_struttura")
                    par.cmd.ExecuteNonQuery()
                    NuovoImportoLordo = 0
                End If
            End If
            myReader5.Close()
            If PercentualeAbbattimento > 0 Then
                Response.Write("<script>alert('Operazione effettuata.\nGli importi sono stati ridotti di circa il " & Format(PercentualeAbbattimento, "0.00") & " %');</script>")
            Else
                Response.Write("<script>alert('Operazione effettuata.\nGli importi sono stati aumentati di circa il " & Format(PercentualeAbbattimento * -1, "0.00") & " %');</script>")
            End If
            'par.myTrans.Rollback()

            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()



        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
        CaricaTabella()
    End Sub
End Class
