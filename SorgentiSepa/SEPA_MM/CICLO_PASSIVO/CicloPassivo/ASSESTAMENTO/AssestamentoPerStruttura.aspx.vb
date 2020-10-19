
Partial Class CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_AssestamentoPerStruttura
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or (Session.Item("BP_GENERALE") <> 1 And Session.Item("MOD_ASS_CONV_COMU") <> 1) Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Session.Item("MOD_ASS_COMPILA") <> 1 Then
            Response.Write("<script>alert('Operatore non abilitato alla compilazione dell\'assestamento!');location.href='../../pagina_home.aspx';</script>")
            Exit Sub
        End If
        '##### CARICAMENTO PAGINA #####
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        If Not IsPostBack Then
            ControlloAssestamento()
            CaricaStrutture()
        End If

    End Sub
    Protected Sub ControlloAssestamento()
        'CONTROLLO CHE CI SIA UN ASSESTAMENTO IN FASE DI CARICAMENTO IMPORTI o DA CONVALIDARE
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT ID,ID_STATO FROM SISCOM_MI.PF_ASSESTAMENTO WHERE ID_STATO=1 OR ID_STATO=2"
            Dim READER As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If READER.Read Then
                idstato.Value = par.IfNull(READER(1), "0")
                idassestamento.Value = par.IfNull(READER(0), "0")
            End If
            READER.Close()
            If idstato.Value = 2 Then
                completa.Visible = False
                convalida.Visible = True
            ElseIf idstato.Value = 1 Then
                convalida.Visible = False
            End If


            '### date ####
            par.cmd.CommandText = "SELECT PF_ASSESTAMENTO.ID_STATO, TO_CHAR(TO_DATE(INIZIO,'yyyyMMdd'),'dd/MM/yyyy') AS INIZIO, TO_CHAR(TO_DATE(FINE,'yyyyMMdd'),'dd/MM/yyyy') AS FINE FROM SISCOM_MI.PF_ASSESTAMENTO,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE PF_ASSESTAMENTO.ID=" & idassestamento.Value & " AND PF_ASSESTAMENTO.ID_PF_MAIN=PF_MAIN.ID AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID"
            Dim lettoreEsercizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreEsercizio.Read Then
                esercizio.Text = par.IfNull(lettoreEsercizio("inizio"), "") & " - " & par.IfNull(lettoreEsercizio("fine"), "")
            End If
            lettoreEsercizio.Close()

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Response.Redirect("Assestamento.aspx?IDs=" & ddlStrutture.SelectedValue)
    End Sub

    Protected Sub CaricaStrutture()

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader

        Try
            If idassestamento.Value <> 0 Then
                '*****************APERTURA CONNESSIONE***************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                'CARICO TUTTE LE STRUTTURE
                par.cmd.CommandText = "SELECT DISTINCT ID, NOME FROM SISCOM_MI.TAB_FILIALI ORDER BY NOME ASC"
                myReader = par.cmd.ExecuteReader
                ddlStrutture.Items.Clear()

                While myReader.Read
                    ddlStrutture.Items.Add(New ListItem(par.IfNull(myReader("NOME"), ""), par.IfNull(myReader("ID"), "")))
                End While
                myReader.Close()

                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Else
                Response.Write("<script>alert('Attenzione! Non esiste nessun Assestamento con stato Caricamento Importi o Da Convalidare!');parent.main.location.replace('../../pagina_home.aspx');</script>")
            End If
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Response.Redirect("../../pagina_home.aspx")
    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Response.Redirect("ScegliAssestamento.aspx")
    End Sub

    Protected Sub completa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles completa.Click
        Response.Redirect("CompletaAssestamento.aspx?IDassestamento=" & idassestamento.Value & "&IDstato=" & idstato.Value)
    End Sub

    Protected Sub convalida_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles convalida.Click
        If ConfAlerCompleto.Value = 1 Then
            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "update siscom_mi.pf_assestamento_voci set importo_approvato = nvl(importo,0) where (importo_approvato is null or importo_approvato = importo)"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "update siscom_mi.pf_assestamento set id_stato = 3 , data_app_aler = '" & Format(Now, "yyyyMMdd") & "' where id = " & idassestamento.Value
                par.cmd.ExecuteNonQuery()
                WriteEvent("F86", "IL GESTORE HA CONVALIDATO L'ASSESTAMENTO")
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Operazione eseguita correttamente!L\'assestamento ha ora stato: APPROVATO DAL GESTORE!');location.href='GestioneAssestamento.aspx?id=" & idassestamento.Value & "'</script>")
            Catch ex As Exception
                lblErrore.Visible = True
                lblErrore.Text = "Convalida - " & ex.Message
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try
        End If
    End Sub

    Protected Sub WriteEvent(ByVal CodEvento As String, Optional ByVal Motivazione As String = "")
        Dim ConnOpenNow As Boolean = False
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ConnOpenNow = True
            End If

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_ASSESTAMENTO (ID_ASSESTAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_VOCE,ID_STRUTTURA,IMPORTO) VALUES " _
                                & "(" & idassestamento.Value & ", " & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', '" & CodEvento & "','" & par.PulisciStrSql(Motivazione) & "',NULL,NULL,'0' )"
            par.cmd.ExecuteNonQuery()


            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
        Catch ex As Exception
            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
        End Try
    End Sub

End Class
