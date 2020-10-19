
Partial Class CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_NuovoAssest
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("BP_GENERALE") <> 1 Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        '##### CARICAMENTO PAGINA #####
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        If IsPostBack = False Then
            Carica()

        End If

    End Sub
    Private Sub Carica()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim idEsercizio As String = par.RicavaEsercizioCorrente
            par.cmd.CommandText = "select id from siscom_mi.pf_main where id_esercizio_finanziario = " & idEsercizio & " and id_stato = 5"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                idPfMain.Value = par.IfNull(lettore("id"), 0)
            End If
            lettore.Close()

            If idPfMain.Value > 0 Then
                par.cmd.CommandText = "SELECT pf_assestamento.id_stato,PF_ASSESTAMENTO.ID,(TO_CHAR(TO_DATE(inizio,'yyyymmdd'),'dd/mm/yyyy') ||' - '||TO_CHAR(TO_DATE(fine,'yyyymmdd'),'dd/mm/yyyy')) AS esercizio, " _
                                    & "TO_CHAR(TO_DATE(PF_ASSESTAMENTO.data_inserimento,'yyyymmdd'),'dd/mm/yyyy') as data_inserimento,PF_STATI.descrizione AS STATO " _
                                    & "FROM siscom_mi.PF_STATI, siscom_mi.PF_ASSESTAMENTO , " _
                                    & "siscom_mi.PF_MAIN, siscom_mi.T_ESERCIZIO_FINANZIARIO " _
                                    & "WHERE id_pf_main = " & idPfMain.Value _
                                    & " AND PF_MAIN.ID = id_pf_main " _
                                    & "AND T_ESERCIZIO_FINANZIARIO.ID = PF_MAIN.id_esercizio_finanziario " _
                                    & "AND PF_STATI.ID = PF_ASSESTAMENTO.ID_STATO "

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                Dim dt As New Data.DataTable()
                da.Fill(dt)


                DataGridAssestamenti.DataSource = dt
                DataGridAssestamenti.DataBind()

                For Each row In dt.Rows
                    If row("id_stato") = 1 Or row("id_stato") = 3 Then
                        ImgProcedi.Visible = False
                    End If
                Next



            Else
                Response.Write("<script>alert('Impossibile procedere!Nessun Esercizio Finanziario trovato');</script>")
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()



        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Visible = True
            lblErrore.Text = "Carica - " & ex.Message

        End Try
    End Sub
    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        Try
            If prosegui.Value = 1 Then

                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                Dim IdNewAssestamento As Integer = 0
                par.cmd.CommandText = "select siscom_mi.seq_pf_assestamento.nextval from dual"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    IdNewAssestamento = par.IfNull(lettore(0), 0)
                End If
                lettore.Close()

                newAssest.Value = IdNewAssestamento
                par.cmd.CommandText = "insert into siscom_mi.pf_assestamento (id,id_pf_main,data_inserimento,id_stato) " _
                                    & "values(" & IdNewAssestamento & " ," & idPfMain.Value & ",'" & Format(Now, "yyyyMMdd") & "',1)"

                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "insert into siscom_mi.pf_assestamento_voci (id_assestamento,id_voce,id_struttura) " _
                                    & "select " & IdNewAssestamento & ",id_voce,id_struttura from siscom_mi.pf_voci_struttura " _
                                    & "where id_voce in (select id from siscom_mi.pf_voci where id_piano_finanziario = " & idPfMain.Value & " )"

                par.cmd.ExecuteNonQuery()
                WriteEvent("F55", "CREAZIONE NUOVO ASSESTAMENTO")

                par.myTrans.Commit()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Nuovo Assestamento creato con successo!');</script>")
                Response.Write("<script>alert('Lo stato dell\'assestamento appena creato viene settato su: CARICAMENTO IMPORTI.');</script>")

                Carica()

            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
            lblErrore.Text = "Procedi - " & ex.Message

        End Try
    End Sub
    Protected Sub WriteEvent(ByVal CodEvento As String, Optional ByVal Motivazione As String = "")
        Dim idPadre As String = "null"
        Dim ConnOpenNow As Boolean = False
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ConnOpenNow = True
            End If

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_ASSESTAMENTO (ID_ASSESTAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_VOCE,ID_STRUTTURA,IMPORTO) VALUES " _
                                & "(" & newAssest.Value & ", " & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', '" & CodEvento & "','" & par.PulisciStrSql(Motivazione) & "',NULL,NULL,'0' )"
            par.cmd.ExecuteNonQuery()


            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "WriteEvent - " & ex.Message
            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
        End Try
    End Sub
End Class
