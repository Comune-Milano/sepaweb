
Partial Class Contabilita_CicloPassivo_Plan_ConvalidaComune
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

        If Not IsPostBack Then
            idPianoF.Value = Request.QueryString("IDP")
            per.Value = ""
            CaricaStato()
            CaricaTabella()
            'CaricaValori()

        End If
    End Sub

    Function CaricaTabella()
        Try
            Dim TestoPagina As String = ""
            Dim Buono As Boolean = True
            Dim TotaleGenerale As Double = 0
            Dim Totale As Double = 0

            par.OracleConn.Open()
            par.SettaCommand(par)

            TestoPagina = TestoPagina & "<table style='width: 900px;' cellpadding=0 cellspacing = 0'>"
            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='100px'>COD.</td><td width='300px' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE</td><td width='100px' align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO</td><td width='100px' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;CAPITOLO</td><td width='300px' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>DESCRIZIONE</td></tr>"
            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 8pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>1</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per il property management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"

            buono = True
            par.cmd.CommandText = "select PF_VOCI.*,PF_CAPITOLI.COD,PF_CAPITOLI.DESCRIZIONE as dcap from SISCOM_MI.PF_CAPITOLI,siscom_mi.pf_voci where PF_VOCI.ID_CAPITOLO=PF_CAPITOLI.ID (+) AND id_piano_finanziario=" & idPianoF.Value & " order by codice asc,indice asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader1.Read
                'If myReader1("indice") = "21" Then
                If myReader1("codice") = "2.01" Then
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per il property management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                    Totale = 0
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 9pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>2</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per il facility management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                End If
                'If myReader1("indice") = "41" Then
                If myReader1("codice") = "3.01" Then
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per il facility management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                    Totale = 0
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 8pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>3</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per contributi per sostegno agli inquilini</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                End If
                'If myReader1("indice") = "46" Then
                If myReader1("codice") = "4.01" Then
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per contributi per sostegno agli inquilini</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                    Totale = 0
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border: 2px dashed #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 8pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>4</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese diverse</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                End If




                'If par.IfNull(myReader1("CODice"), "--") <> "1.01" And par.IfNull(myReader1("CODice"), "--") <> "1.02" And par.IfNull(myReader1("CODice"), "--") <> "1.03" And par.IfNull(myReader1("CODice"), "--") <> "2.01" And par.IfNull(myReader1("CODice"), "--") <> "2.02" And par.IfNull(myReader1("CODice"), "--") <> "2.03" And par.IfNull(myReader1("CODice"), "--") <> "2.04" And par.IfNull(myReader1("CODice"), "--") <> "3.01" And par.IfNull(myReader1("CODice"), "--") <> "4.01" Then
                If Len(par.IfNull(myReader1("CODice"), "--")) <> 4 Then
                    par.cmd.CommandText = "select sum(valore_lordo) from SISCOM_MI.PF_voci_struttura where PF_VOCI_struttura.id_voce=" & myReader1("id")
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader3.Read Then
                        If Len(par.IfNull(myReader1("CODice"), "--")) = 7 Then
                            Totale = Totale + CDbl(par.IfNull(myReader3(0), "0"))
                            TotaleGenerale = TotaleGenerale + CDbl(par.IfNull(myReader3(0), "0"))
                        End If

                        If Len(par.IfNull(myReader1("CODice"), "--")) = 7 Then
                            If par.IfNull(myReader1("COD"), "--") = "--" Then
                                Buono = False
                            End If
                            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'><a href='DettagliSpesaAler.aspx?IDV=" & par.IfNull(myReader1("id"), "") & "&P=" & per.Value & "' target='_blank'>" & Format(CDbl(par.IfNull(myReader3(0), 0)), "##,##0.00") & "</a></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;&nbsp;" & par.IfNull(myReader1("COD"), "---") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("dcap"), "---") & "</td></tr>"
                        Else
                            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'><a href='DettagliSpesaAler.aspx?IDV=" & par.IfNull(myReader1("id"), "") & "&P=" & per.Value & "' target='_blank'>" & Format(CDbl(par.IfNull(myReader3(0), 0)), "##,##0.00") & "</a></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td></tr>"
                        End If
                    End If
                    myReader3.Close()
                    Else
                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td></tr>"

                    End If

            End While
            myReader1.Close()

            If Buono = False Then
                convalidaok.Value = "0"
            Else
                convalidaok.Value = "1"
            End If
            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE GENERALE</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(TotaleGenerale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"

            TestoPagina = TestoPagina & "</table><br/><br/><p style='font-family: arial; font-size: 10pt; font-weight: bold'>Importi per Capitolo</p>"


            TestoPagina = TestoPagina & "<table style='width: 600px;' cellpadding=0 cellspacing = 0'>"
            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='100px'>CAPITOLO</td><td width='400px' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>DESCRIZIONE</td><td width='100px' align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO</td></tr>"
            Totale = 0


            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_CAPITOLI ORDER BY COD ASC"
            myReader1 = par.cmd.ExecuteReader()
            Dim TotCapitoli As Double = 0

            While myReader1.Read

                par.cmd.CommandText = "SELECT SUM(VALORE_LORDO) FROM SISCOM_MI.PF_VOCI_STRUTTURA,SISCOM_MI.PF_VOCI WHERE LENGTH(CODICE)=7 AND PF_VOCI_STRUTTURA.ID_VOCE=PF_VOCI.ID AND ID_CAPITOLO=" & myReader1("ID") & " AND id_piano_finanziario=" & idPianoF.Value & " AND PF_VOCI_STRUTTURA.ID_VOCE=PF_VOCI.ID"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("cod"), "---") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "---") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(CDbl(par.IfNull(myReader2(0), "0")), "##,##0.00") & "</td></tr>"
                    TotCapitoli = TotCapitoli + CDbl(par.IfNull(myReader2(0), 0))
                End If
                myReader2.Close()
            End While
            myReader1.Close()

            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>TOTALE PER CAPITOLI</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(TotCapitoli, "##,##0.00") & "</td></tr>"
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
                If stato.Value <> "3" Then
                    btnConvalida.Visible = False
                    btnConvalida.Visible = False
                End If
            End If
            myReader5.Close()


            par.cmd.CommandText = "select * from operatori where id=" & Session.Item("ID_OPERATORE")
            myReader5 = par.cmd.ExecuteReader()
            If myReader5.Read Then
                If par.IfNull(myReader5("BP_CONV_COMUNE_L"), "1") = "1" Then
                    imgConvalida.Visible = False
                End If
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


    

    Protected Sub imgConferma_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgConferma.Click
        Try

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            If salvaok.Value = "1" Then

                par.cmd.CommandText = "update siscom_mi.pf_main set id_stato=1 where id=" & idPianoF.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "update siscom_mi.pf_voci_struttura set completo_aler='0',COMPLETO_COMUNE=1 where id_voce in (select id from siscom_mi.pf_voci where id_piano_finanziario=" & idPianoF.Value & ")"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_EVENTI (ID_PIANO_FINANZIARIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_STRUTTURA) " _
                                    & "VALUES (" & idPianoF.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F87','Piano Finanziario non Approvato dal Comune di Milano - " & par.PulisciStrSql(TxtNote.Text) & "',-1)"
                par.cmd.ExecuteNonQuery()

                TxtNote.Enabled = False
                btnConvalida.Visible = False


                Response.Write("<script>alert('Operazione Effettuata!');</script>")
                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                CaricaStato()

            Else
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If




        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnConvalida_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnConvalida.Click
        If salvaok.Value = "1" Then

            Try
                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans


                par.cmd.CommandText = "select * from siscom_mi.pf_voci_STRUTTURA where ID_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_VOCE_MADRE IS NOT NULL AND LENGTH(CODICE)<10) AND ((valore_lordo-valore_lordo_aler)>1 or (valore_lordo_aler-valore_lordo)>1) and valore_lordo<>valore_lordo_aler and (valore_netto<>0 or valore_netto is not null) and (valore_lordo<>0 or valore_lordo is not null) AND id_voce IN (SELECT ID FROM siscom_mi.PF_VOCI WHERE id_piano_finanziario=" & idPianoF.Value & ")  " '"select * from siscom_mi.pf_voci_STRUTTURA where (valore_lordo_aler<>0 and valore_netto<>0) and valore_lordo<>valore_lordo_aler "
                Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader5.HasRows = True Then
                    Response.Write("<script>alert('Il piano non può essere covalidato ora. Gli importi richiesti potrebbero non coincidere con quelli approvati dal Gestore!');</script>")
                    par.myTrans.Rollback()
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If

                par.cmd.CommandText = "update siscom_mi.pf_main set id_stato=5 where id=" & idPianoF.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "update siscom_mi.pf_voci_struttura set COMPLETO_COMUNE=2 where id_voce in (select id from siscom_mi.pf_voci where id_piano_finanziario=" & idPianoF.Value & ")"
                par.cmd.ExecuteNonQuery()

                stato.Value = "5"

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_EVENTI (ID_PIANO_FINANZIARIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_STRUTTURA) " _
                                    & "VALUES (" & idPianoF.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F86','Piano Finanziario Approvato dal Comune di Milano',-1)"
                par.cmd.ExecuteNonQuery()

                Dim Ev As Boolean = True
                AllineaVociCondomini(Ev)

                Response.Write("<script>alert('Operazione Effettuata!');</script>")
                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                CaricaStato()


                TxtNote.Enabled = False
                btnConvalida.Visible = False

            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            End Try

        End If
    End Sub
#Region "AllineaSospesi"
    Private Sub AllineaVociCondomini(ByRef Esito As Boolean)
        Esito = True
        Try
            '************ ESEGUO LE INSERT DELLE VOCI DI SPESA PER I CONDOMINI********************
            par.cmd.CommandText = "INSERT INTO siscom_mi.COND_VOCI_SPESA_PF ( ID_VOCE_COND, ID_PIANO_FINANZIARIO, ID_VOCE_PF," _
                                & "ID_VOCE_PF_IMPORTO ) VALUES ( " _
                                & "1, " & idPianoF.Value & ",(SELECT ID FROM siscom_mi.pf_voci " _
                                & " WHERE codice = '1.02.09.01' AND id_piano_finanziario = " & idPianoF.Value & ") , NULL)"
            par.cmd.ExecuteNonQuery()


            par.cmd.CommandText = "INSERT INTO siscom_mi.COND_VOCI_SPESA_PF ( ID_VOCE_COND, ID_PIANO_FINANZIARIO, ID_VOCE_PF," _
                                & "ID_VOCE_PF_IMPORTO ) VALUES ( " _
                                & "2, " & idPianoF.Value & ",(SELECT ID FROM siscom_mi.pf_voci WHERE codice = '1.02.09.02' AND id_piano_finanziario =" & idPianoF.Value & ") , NULL)"

            par.cmd.ExecuteNonQuery()



            par.cmd.CommandText = "INSERT INTO siscom_mi.COND_VOCI_SPESA_PF ( ID_VOCE_COND, ID_PIANO_FINANZIARIO, ID_VOCE_PF," _
                                & "ID_VOCE_PF_IMPORTO ) VALUES ( " _
                                & "3, " & idPianoF.Value & ",(SELECT ID FROM siscom_mi.pf_voci WHERE codice = '1.02.09.03' AND id_piano_finanziario =" & idPianoF.Value & ") , NULL) "

            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO siscom_mi.COND_VOCI_SPESA_PF ( ID_VOCE_COND, ID_PIANO_FINANZIARIO, ID_VOCE_PF," _
                                & "ID_VOCE_PF_IMPORTO ) VALUES ( " _
                                & "4, " & idPianoF.Value & ",NULL , NULL) "

            par.cmd.ExecuteNonQuery()

            '********************* ATTENZIONE: QUESTE VOCI DI SPESA SONO LEGATE AGLI IMPORTI STANZIATI PER LA FILIALE 54 (AMM.CONDOMINI)!
            '********************************* SE DOVESSE CAMBIARE L'ID FILIALE BISOGNA MODIFICARE MANUALMENTE LE QUERY!!!
            par.cmd.CommandText = "INSERT INTO siscom_mi.COND_VOCI_SPESA_PF ( ID_VOCE_COND, ID_PIANO_FINANZIARIO, ID_VOCE_PF," _
                                & "ID_VOCE_PF_IMPORTO ) VALUES ( " _
                                & "8, " & idPianoF.Value & ",NULL,(SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                                & "WHERE ID_VOCE =(SELECT ID FROM siscom_mi.pf_voci WHERE codice = '2.02.01' AND id_piano_finanziario =" & idPianoF.Value & ") " _
                                & "AND id_lotto = (SELECT ID FROM siscom_mi.lotti WHERE id_filiale = 54 AND id_esercizio_finanziario = " _
                                & "(SELECT id_esercizio_finanziario FROM siscom_mi.pf_main WHERE ID = " & idPianoF.Value & "))" _
                                & "AND descrizione LIKE '05.%')) "
            par.cmd.ExecuteNonQuery()


            par.cmd.CommandText = "INSERT INTO siscom_mi.COND_VOCI_SPESA_PF ( ID_VOCE_COND, ID_PIANO_FINANZIARIO, ID_VOCE_PF," _
                                & "ID_VOCE_PF_IMPORTO ) VALUES ( " _
                                & "9, " & idPianoF.Value & ",NULL,(SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                                & "WHERE ID_VOCE =(SELECT ID FROM siscom_mi.pf_voci WHERE codice = '2.02.02' AND id_piano_finanziario =" & idPianoF.Value & ") " _
                                & "AND id_lotto = (SELECT ID FROM siscom_mi.lotti WHERE id_filiale = 54 AND id_esercizio_finanziario = " _
                                & "(SELECT id_esercizio_finanziario FROM siscom_mi.pf_main WHERE ID = " & idPianoF.Value & "))" _
                                & "AND descrizione LIKE '13.%')) "
            par.cmd.ExecuteNonQuery()


            par.cmd.CommandText = "INSERT INTO siscom_mi.COND_VOCI_SPESA_PF ( ID_VOCE_COND, ID_PIANO_FINANZIARIO, ID_VOCE_PF," _
                                & "ID_VOCE_PF_IMPORTO ) VALUES ( " _
                                & "11, " & idPianoF.Value & ",NULL,(SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                                & "WHERE ID_VOCE =(SELECT ID FROM siscom_mi.pf_voci WHERE codice = '2.02.03' AND id_piano_finanziario =" & idPianoF.Value & ") " _
                                & "AND id_lotto = (SELECT ID FROM siscom_mi.lotti WHERE id_filiale = 54 AND id_esercizio_finanziario = " _
                                & "(SELECT id_esercizio_finanziario FROM siscom_mi.pf_main WHERE ID = " & idPianoF.Value & "))" _
                                & "AND descrizione LIKE '03.%')) "
            par.cmd.ExecuteNonQuery()


            par.cmd.CommandText = "SELECT inizio FROM siscom_mi.t_esercizio_finanziario where id = (select ID_ESERCIZIO_FINANZIARIO from siscom_mi.pf_main where id = " & idPianoF.Value & ")"
            Dim dataInizio As String = ""
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                dataInizio = par.IfNull(lettore("inizio"), "")
            End If
            lettore.Close()

            If dataInizio <> "" Then
                par.cmd.CommandText = "SELECT id_prenotazione, " _
                                    & "(CASE WHEN id_voce_pf_importo IS NOT NULL THEN " _
                                    & "(SELECT id_voce FROM siscom_mi.PF_VOCI_IMPORTO WHERE ID = id_voce_pf_importo) " _
                                    & "ELSE id_voce_pf END) AS id_voce_pf, " _
                                    & "id_voce_pf_importo " _
                                    & "FROM siscom_mi.COND_GESTIONE_DETT_SCAD, siscom_mi.COND_VOCI_SPESA_PF  " _
                                    & "WHERE  COND_GESTIONE_DETT_SCAD.id_voce = COND_VOCI_SPESA_PF.id_voce_cond " _
                                    & "AND COND_VOCI_SPESA_PF.id_piano_finanziario = " & idPianoF.Value & " " _
                                    & "AND rata_scad>= '" & dataInizio & "'AND id_prenotazione IS NOT NULL ORDER BY N_rata "

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable()
                da.Fill(dt)

                For Each row As Data.DataRow In dt.Rows
                    par.cmd.CommandText = "update siscom_mi.prenotazioni set DATA_PRENOTAZIONE = '" & dataInizio & "', id_voce_pf = " & par.IfNull(row.Item("id_voce_pf"), "null") _
                                        & ", id_voce_pf_importo =  " & par.IfNull(row.Item("id_voce_pf_importo"), "null") _
                                        & " where prenotazioni.id = " & par.IfNull(row.Item("id_prenotazione"), "null")
                    par.cmd.ExecuteNonQuery()
                Next
                da.Dispose()
                dt.Clear()
                dt.Dispose()

            Else
                Esito = False
            End If


            '*******************



        Catch ex As Exception
            Esito = False
        End Try


    End Sub



#End Region

End Class
