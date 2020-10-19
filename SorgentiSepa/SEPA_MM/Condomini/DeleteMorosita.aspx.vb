
Partial Class Condomini_DeleteMorosita
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public Property vIdConnModale() As String
        Get
            If Not (ViewState("par_vIdConnModale") Is Nothing) Then
                Return CStr(ViewState("par_vIdConnModale"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdConnModale") = value
        End Set

    End Property
    Public Property vIdMorosita() As String
        Get
            If Not (ViewState("par_vIdMorosita") Is Nothing) Then
                Return CStr(ViewState("par_vIdMorosita"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdMorosita") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Not IsPostBack Then
            Dim pagato As Boolean = False
            Dim TestoTabella As String = ""

            TestoTabella = "<table cellpadding='1' cellspacing='2' width='100%'>" _
                & "<tr bgcolor = '#CCCCFF'>" _
                & "<td style='height: 15; width : 50%'>" _
                & "<span style='font-size: 8pt; font-family:Courier New'><strong>ID CONTRATTO</strong></span></td>" _
                & "<td style='height: 15px;text-align:center; width : 20%'>" _
                & "<span style='font-size: 8pt; font-family: Courier New'><strong>ID BOLLETTA</strong></span></td>" _
                & "<td style='height: 15px;text-align:center; width : 30%'>" _
                & "<span style='font-size: 8pt; font-family: Courier New'><strong>DATA PAGAMENTO</strong></span></td>" _
                & "</tr>"

            Try
                If Session.Item("OPERATORE") = "" Then
                    Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
                End If
                If Request.QueryString("IDCON") <> "" Then
                    vIdConnModale = Request.QueryString("IDCON")
                End If
                vIdMorosita = Request.QueryString("IDMOROSITA")



                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "SELECT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE ID = (SELECT ID_PRENOTAZIONE FROM SISCOM_MI.COND_MOROSITA WHERE ID = " & vIdMorosita & ")"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    If par.IfNull(myReader("id_pagamento"), 0) > 0 Then
                        Response.Write("<script>alert('Impossibile eliminare i dati della morosità, perchè è stato già emesso il pagamento!');self.close();</script>")
                        Exit Sub
                    End If
                End If
                myReader.Close()




                'CONTROLLARE SE BOLLETTE SONO STATE GIA' STAMPATE
                par.cmd.CommandText = "select RAPPORTI_UTENZA.COD_CONTRATTO,cond_morosita_lettere.Bollettino,bol_bollette.* from siscom_mi.cond_morosita_lettere,siscom_mi.bol_bollette,SISCOM_MI.RAPPORTI_UTENZA where RAPPORTI_UTENZA.ID = BOL_BOLLETTE.ID_CONTRATTO AND cond_morosita_lettere.id_morosita =" & vIdMorosita & "  and rif_bollettino = bollettino and bol_bollette.fl_stampato = 1"

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                'CONTROLLO SE BOLLETTE SONO STATE PAGATE
                For Each row As Data.DataRow In dt.Rows
                    Me.lblAttention.Visible = True
                    If par.IfNull(row.Item("DATA_PAGAMENTO"), "") <> "" Then
                        Dim NumBolletta As String = ""

                        Select Case par.IfNull(row.Item("N_RATA"), "")
                            Case "99" 'bolletta manuale
                                NumBolletta = "MA"
                            Case "999" 'bolletta automatica
                                NumBolletta = "AU"
                            Case "99999" 'bolletta di conguaglio
                                NumBolletta = "CO"
                            Case Else
                                NumBolletta = Format(par.IfNull(row.Item("N_RATA"), "??"), "00")
                        End Select

                        TestoTabella = TestoTabella & "<tr>" _
                                    & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; text-align:left; border-bottom-color: #ACACAC'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>" & row.Item("COD_CONTRATTO") & "</strong></span></td>" _
                                    & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin;text-align:center; border-bottom-color: #ACACAC'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>" & NumBolletta & "/" & row.Item("ANNO") & "</strong></span></td>" _
                                    & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin;text-align:center; border-bottom-color: #ACACAC'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>" & par.FormattaData(row.Item("DATA_PAGAMENTO")) & "</strong></span></td>" _
                                    & "</tr>"
                        pagato = True
                    End If

                Next
                TestoTabella = TestoTabella & "</table>"

                If pagato = True Then
                    Me.lblAttention.Text = "ATTENZIONE!<br/>Impossibile eliminare la morosità, perchè alcune bollette sono state pagate!"
                    Me.lblTabella.Text = TestoTabella
                    btnConferma.Visible = False
                    Exit Sub
                Else
                    Me.lblTabella.Visible = False
                    If dt.Rows.Count > 0 Then
                        Me.lblAttention.Visible = True
                    Else
                        lblAttention.Visible = False
                        btnConferma.Visible = False
                        DelSoloMorosita()
                    End If

                End If




            Catch ex As Exception
                Me.lblerrore.Visible = True
                lblerrore.Text = ex.Message
            End Try
        End If

    End Sub
    Private Sub DelSoloMorosita()
        '*******************RICHIAMO LA CONNESSIONE*********************
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        '*******************RICHIAMO LA TRANSAZIONE*********************
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET " _
                            & "id_stato = -3 WHERE ID = (Select id_prenotazione from siscom_mi.cond_morosita where id = " & vIdMorosita & ")"
        par.cmd.ExecuteNonQuery()

        par.cmd.ExecuteNonQuery()
        par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_MOROSITA_INQUILINI_DET WHERE ID_MOROSITA = " & vIdMorosita
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_MOROSITA_INQUILINI WHERE ID_MOROSITA = " & vIdMorosita
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_MOROSITA WHERE ID = " & vIdMorosita
        par.cmd.ExecuteNonQuery()
        '****************MYEVENT*****************
        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCONDOMINIO") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','CANCELLATA MOROSITA CONDOMINIO')"
        par.cmd.ExecuteNonQuery()

        Response.Write("<script>alert('Operazione eseguita correttamente!');window.close();</script>")

    End Sub
    Private Sub Delete()
        Try


            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans
            par.cmd.CommandText = "select cond_morosita_lettere.Bollettino,bol_bollette.* from siscom_mi.cond_morosita_lettere,siscom_mi.bol_bollette where cond_morosita_lettere.id_morosita =" & vIdMorosita & "  and rif_bollettino = bollettino and fl_stampato = 1"
            Dim myreader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While myreader.Read
                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET FL_ANNULLATA = 1 , DATA_ANNULLO = '" & Format(Now, "yyyyMMdd") & "' WHERE RIF_BOLLETTINO = '" & myreader("BOLLETTINO") & "' AND RIF_FILE = 'MOR'"
                par.cmd.ExecuteNonQuery()
            End While

            'SE SI AVVISARE E CHIEDERE CONFERMA = I mav per questa morosità sono stati emessi. Procedendo questi ultimi verranno annullati.Procedere?
            'Quindi annullare tutte le bollette emesse su questa morosità.in bol_bollette fl_annullata = '1'

            par.cmd.CommandText = ""

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_MOROSITA WHERE ID = " & vIdMorosita
            par.cmd.ExecuteNonQuery()
            '****************MYEVENT*****************
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCONDOMINIO") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','CANCELLATA MOROSITA CONDOMINIO')"
            par.cmd.ExecuteNonQuery()

            Response.Write("<script>alert('Operazione eseguita correttamente!');window.close();</script>")

        Catch ex As Exception
            Me.lblerrore.Visible = True
            lblerrore.Text = ex.Message

        End Try
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnConferma.Click
        Delete()
        Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>window.close();</script>")

    End Sub
End Class
