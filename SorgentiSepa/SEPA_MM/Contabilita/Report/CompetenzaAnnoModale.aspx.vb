
Partial Class Contabilita_Report_CompetenzaAnnoModale
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            caricaElencoVoci()
            caricaCompetenze()
            reimpostaNumerieDate()
        End If
    End Sub
    Private Sub caricaElencoVoci()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select * from siscom_mi.t_voci_bolletta where id=" & Request.QueryString("ID")
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                lblComp.Text = "Competenze voce " & par.IfNull(lettore("descrizione"), 0)
            End If
            lettore.Close()

            par.cmd.CommandText = "SELECT T_VOCI_BOLLETTA_COMP_ANNO.ID,T_VOCI_BOLLETTA_COMPETENZA.DESCRIZIONE AS COMPETENZA," _
                & " TO_CHAR(TO_DATE(INIZIO,'yyyyMMdd'),'dd/MM/yyyy') AS INIZIO, TO_CHAR(TO_DATE(FINE,'yyyyMMdd'),'dd/MM/yyyy') AS FINE," _
                & " 1 AS ELIMINA,'' AS ELIMINA_IMM " _
                & " FROM SISCOM_MI.T_VOCI_BOLLETTA_COMP_ANNO,SISCOM_MI.T_VOCI_BOLLETTA_COMPETENZA " _
                & " WHERE ID_VOCE=" & Request.QueryString("ID") _
                & " AND T_VOCI_BOLLETTA_COMP_ANNO.ID_COMPETENZA=T_VOCI_BOLLETTA_COMPETENZA.ID " _
                & " ORDER BY FINE "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            If dt.Rows.Count > 0 Then
                DataGridVoci.DataSource = dt
                DataGridVoci.DataBind()
                DataGridVoci.Visible = True
                nessuna.Visible = False

            Else
                DataGridVoci.Visible = False
                nessuna.Visible = True

            End If
            HiddenFieldConteggio.Value = dt.Rows.Count
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore durante il caricamento delle voci!');", True)
        End Try
        controllaEliminazioni()
    End Sub

    Private Sub caricaCompetenze()
        Try
            par.caricaComboBox("SELECT * FROM SISCOM_MI.T_VOCI_BOLLETTA_COMPETENZA", ddlCompetenza, "ID", "DESCRIZIONE", False)
        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.ClearAllPools()
        End Try
    End Sub

    Protected Sub ImageButtonAggiungi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonAggiungi.Click

        If dataDal.Text <> "" Then
            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    par.myTrans = par.OracleConn.BeginTransaction()
                    ‘‘par.cmd.Transaction = par.myTrans

                    Dim DataInizioValidita As Date = dataDal.Text

                    'CONTROLLO DATA SUPERIORE ALL'INIZIO VALIDITà DELL'ULTIMA COMPETENZA
                    par.cmd.CommandText = "SELECT ID,TO_CHAR(TO_DATE(INIZIO,'yyyyMMdd'),'dd/MM/yyyy') AS INIZIO " _
                        & " FROM SISCOM_MI.T_VOCI_BOLLETTA_COMP_ANNO " _
                        & " WHERE ID_VOCE=" & Request.QueryString("ID") _
                        & " AND FINE=20501231"
                    Dim dataInizioPrecedente As Date = Nothing
                    Dim idComp As Integer = 0
                    Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If Lettore.Read Then
                        dataInizioPrecedente = par.IfNull(Lettore("INIZIO"), "")
                        idComp = par.IfNull(Lettore("ID"), "")
                    End If
                    Lettore.Close()


                    If DataInizioValidita > dataInizioPrecedente Then
                        If dataInizioPrecedente = Nothing Then
                            'NON ESISTE UNA COMPETENZA PRECEDENTE
                            'INSERT DELLA NUOVA COMPETENZA
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.T_VOCI_BOLLETTA_COMP_ANNO " _
                                & "(ID,ID_VOCE,ID_COMPETENZA,INIZIO,FINE) " _
                                & "VALUES (" _
                                & "siscom_mi.SEQ_T_VOCI_BOLLETTA_COMP_ANNO.NEXTVAL," _
                                & Request.QueryString("ID") & "," _
                                & ddlCompetenza.SelectedValue & "," _
                                & par.FormatoDataDB(dataDal.Text) & "," _
                                & "20501231)"
                            par.cmd.ExecuteNonQuery()

                            'UPDATE BOL_BOLLETTE_VOCI_PAGAMENTI
                            Dim QUERY As String = ""
                            QUERY = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI SET ID_COMPETENZA=" _
                                & ddlCompetenza.SelectedValue & ",COMPETENZA=INITCAP('" & ddlCompetenza.SelectedItem.Text & "') " _
                                & " WHERE ID_T_VOCE_BOLLETTA=" & Request.QueryString("ID") _
                                & " AND DATA_EMISSIONE_BOL_BOLLETTE BETWEEN " & par.FormatoDataDB(dataDal.Text) & " AND 20501231 "
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.UPDATE_COMPETENZE (DESCRIZIONE,DATA_ORA) VALUES('" & Replace(QUERY, "'", "''") & "'," & Format(Now, "yyyyMMddHHmmss") & ")"
                            par.cmd.ExecuteNonQuery()

                            'UPDATE BOL_BOLLETTE_VOCI_EMISSIONI
                            QUERY = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI_EMISSIONI SET FL_NO_TRG=1,COMPETENZA_T_VOCI_BOLLETTA=" _
                               & ddlCompetenza.SelectedValue & ",COMPETENZA=INITCAP('" & ddlCompetenza.SelectedItem.Text & "') " _
                               & " WHERE ID_VOCE_BOL_BOLLETTE_VOCI=" & Request.QueryString("ID") _
                               & " AND DATA_EMISSIONE_BOL_BOLLETTE BETWEEN " & par.FormatoDataDB(dataDal.Text) & " AND 20501231 "
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.UPDATE_COMPETENZE (DESCRIZIONE,DATA_ORA) VALUES('" & Replace(QUERY, "'", "''") & "'," & Format(Now, "yyyyMMddHHmmss") & ")"
                            par.cmd.ExecuteNonQuery()


                        Else
                            'ESISTE UNA COMPETENZA PRECEDENTE   
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.T_VOCI_BOLLETTA_COMP_ANNO " _
                                & "(ID,ID_VOCE,ID_COMPETENZA,INIZIO,FINE) " _
                                & "VALUES (" _
                                & "siscom_mi.SEQ_T_VOCI_BOLLETTA_COMP_ANNO.NEXTVAL," _
                                & Request.QueryString("ID") & "," _
                                & ddlCompetenza.SelectedValue & "," _
                                & par.FormatoDataDB(dataDal.Text) & "," _
                                & "20501231)"
                            par.cmd.ExecuteNonQuery()


                            'UPDATE DELLA COMPETENZA PRECEDENTE
                            Dim dataAlPrecedente As Date = DataInizioValidita.AddDays(-1)
                            par.cmd.CommandText = "UPDATE SISCOM_MI.T_VOCI_BOLLETTA_COMP_ANNO " _
                                & " SET FINE=" & dataAlPrecedente.Year & Format(dataAlPrecedente.Month, "00") & Format(dataAlPrecedente.Day, "00") _
                                & " WHERE ID=" & idComp
                            par.cmd.ExecuteNonQuery()

                            'UPDATE BOL_BOLLETTE_VOCI_PAGAMENTI
                            Dim QUERY As String = ""
                            QUERY = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI SET ID_COMPETENZA=" _
                                & ddlCompetenza.SelectedValue & ",COMPETENZA=INITCAP('" & ddlCompetenza.SelectedItem.Text & "') " _
                                & " WHERE ID_T_VOCE_BOLLETTA=" & Request.QueryString("ID") _
                                & " AND DATA_EMISSIONE_BOL_BOLLETTE BETWEEN " & par.FormatoDataDB(dataDal.Text) & " AND 20501231 "
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.UPDATE_COMPETENZE (DESCRIZIONE,DATA_ORA) VALUES('" & Replace(QUERY, "'", "''") & "'," & Format(Now, "yyyyMMddHHmmss") & ")"
                            par.cmd.ExecuteNonQuery()

                            'UPDATE BOL_BOLLETTE_VOCI_EMISSIONI
                            QUERY = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI_EMISSIONI SET FL_NO_TRG=1,COMPETENZA_T_VOCI_BOLLETTA=" _
                               & ddlCompetenza.SelectedValue & ",COMPETENZA=INITCAP('" & ddlCompetenza.SelectedItem.Text & "') " _
                               & " WHERE ID_VOCE_BOL_BOLLETTE_VOCI=" & Request.QueryString("ID") _
                               & " AND DATA_EMISSIONE_BOL_BOLLETTE BETWEEN " & par.FormatoDataDB(dataDal.Text) & " AND 20501231 "
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.UPDATE_COMPETENZE (DESCRIZIONE,DATA_ORA) VALUES('" & Replace(QUERY, "'", "''") & "'," & Format(Now, "yyyyMMddHHmmss") & ")"
                            par.cmd.ExecuteNonQuery()

                        End If
                    Else
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('La data di inizio validità deve essere superiore\nalla data di inizio validità della precedente competenza!');", True)
                    End If

                    par.myTrans.Commit()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore durante l\'inserimento della competenza!');", True)
            End Try
        Else
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Inserire una data corretta di inizio validità!');", True)
        End If

        caricaElencoVoci()
    End Sub
    Protected Sub ImageButtonEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsci.Click
        Response.Write("<script>self.close();</script>")
    End Sub
    Private Sub reimpostaNumerieDate()
        dataDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub
    Private Sub controllaEliminazioni()
        For Each item As DataGridItem In DataGridVoci.Items
            'If item.Cells(4).Text = 1 And item.Cells(2).Text <> "01/01/1990" Then
            If item.Cells(4).Text = 1 Then
                item.Cells(5).Text = "<img src=""../../NuoveImm/Elimina.png"" style=""cursor: pointer"" onclick=""javascript:elimina(" & item.Cells(0).Text & ");void(0);"" alt=""elimina"" />"
            End If
        Next
    End Sub

    Protected Sub btnElimina_Click(sender As Object, e As System.EventArgs) Handles btnElimina.Click
        If IDelimina.Value <> "0" Then
            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    par.myTrans = par.OracleConn.BeginTransaction()
                    ‘‘par.cmd.Transaction = par.myTrans

                    'CONTROLLO DELLA RIGA DA LIMINARE
                    par.cmd.CommandText = "SELECT NVL(MAX(ID),0) " _
                        & " FROM SISCOM_MI.T_VOCI_BOLLETTA_COMP_ANNO " _
                        & " WHERE ID_VOCE = " & Request.QueryString("ID") _
                        & " AND ID<" & IDelimina.Value
                    Dim LettorePrecedente As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If LettorePrecedente.Read Then
                        If par.IfNull(LettorePrecedente(0), 0) <> 0 Then

                            'LA COMPETENZA PRECEDENTE DEVE ESSERE ESTESA FINO AL GIORNO PRECEDENTE
                            par.cmd.CommandText = "UPDATE SISCOM_MI.T_VOCI_BOLLETTA_COMP_ANNO SET FINE=" _
                                & " (SELECT A.FINE FROM SISCOM_MI.T_VOCI_BOLLETTA_COMP_ANNO A WHERE A.ID=" & IDelimina.Value & ") " _
                                & " WHERE ID=" & par.IfNull(LettorePrecedente(0), 0)
                            par.cmd.ExecuteNonQuery()

                            'RECUPERIAMO LA COMPETENZA DA AGGIORNARE
                            par.cmd.CommandText = "SELECT ID_COMPETENZA AS ID,T_VOCI_BOLLETTA_COMPETENZA.DESCRIZIONE AS DESCRIZIONE " _
                                & " FROM SISCOM_MI.T_VOCI_BOLLETTA_COMP_ANNO, SISCOM_MI.T_VOCI_BOLLETTA_COMPETENZA " _
                                & " WHERE T_VOCI_BOLLETTA_COMP_ANNO.ID_COMPETENZA=T_VOCI_BOLLETTA_COMPETENZA.ID " _
                                & " AND T_VOCI_BOLLETTA_COMP_ANNO.ID=" & par.IfNull(LettorePrecedente(0), 0)
                            Dim LettoreCompetenza As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            Dim competenza As Integer = 0
                            Dim DescrizioneCompetenza As String = 0
                            If LettoreCompetenza.Read Then
                                competenza = par.IfNull(LettoreCompetenza("ID"), 0)
                                DescrizioneCompetenza = par.IfNull(LettoreCompetenza("DESCRIZIONE"), 0)
                            End If
                            LettoreCompetenza.Close()

                            'UPDATE BOL_BOLLETTE_VOCI_PAGAMENTI
                            Dim UpdateDataDal As String = ""
                            Dim UpdateDataAl As String = ""
                            par.cmd.CommandText = "SELECT INIZIO FROM SISCOM_MI.T_VOCI_BOLLETTA_COMP_ANNO WHERE ID=" & par.IfNull(LettorePrecedente(0), 0)
                            Dim LettoreDate As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If LettoreDate.Read Then
                                UpdateDataDal = par.IfNull(LettoreDate("INIZIO"), "")
                            End If
                            LettoreDate.Close()

                            par.cmd.CommandText = "SELECT FINE FROM SISCOM_MI.T_VOCI_BOLLETTA_COMP_ANNO WHERE ID=" & IDelimina.Value
                            LettoreDate = par.cmd.ExecuteReader
                            If LettoreDate.Read Then
                                UpdateDataAl = par.IfNull(LettoreDate("FINE"), "")
                            End If
                            LettoreDate.Close()

                            Dim QUERY As String = ""
                            QUERY = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI SET ID_COMPETENZA=" _
                                & competenza & ",COMPETENZA=INITCAP('" & DescrizioneCompetenza & "') " _
                                & " WHERE ID_T_VOCE_BOLLETTA=" & Request.QueryString("ID") _
                                & " AND DATA_EMISSIONE_BOL_BOLLETTE BETWEEN " & UpdateDataDal _
                                & " AND " & UpdateDataAl
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.UPDATE_COMPETENZE (DESCRIZIONE,DATA_ORA) VALUES('" & Replace(QUERY, "'", "''") & "'," & Format(Now, "yyyyMMddHHmmss") & ")"
                            par.cmd.ExecuteNonQuery()

                            'UPDATE BOL_BOLLETTE_VOCI_EMISSIONI
                            QUERY = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI_EMISSIONI SET FL_NO_TRG=1,COMPETENZA_T_VOCI_BOLLETTA=" _
                               & competenza & ",COMPETENZA=INITCAP('" & DescrizioneCompetenza & "') " _
                               & " WHERE ID_VOCE_BOL_BOLLETTE_VOCI=" & Request.QueryString("ID") _
                               & " AND DATA_EMISSIONE_BOL_BOLLETTE BETWEEN " & UpdateDataDal _
                               & " AND " & UpdateDataAl
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.UPDATE_COMPETENZE (DESCRIZIONE,DATA_ORA) VALUES('" & Replace(QUERY, "'", "''") & "'," & Format(Now, "yyyyMMddHHmmss") & ")"
                            par.cmd.ExecuteNonQuery()

                        Else

                            'CONTROLLO DELLA RIGA DA LIMINARE
                            par.cmd.CommandText = "SELECT MIN(ID) " _
                                & " FROM SISCOM_MI.T_VOCI_BOLLETTA_COMP_ANNO " _
                                & " WHERE ID_VOCE = " & Request.QueryString("ID") _
                                & " AND ID>" & IDelimina.Value
                            Dim LettoreSuccessivo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

                            If LettoreSuccessivo.Read Then

                                If par.IfNull(LettoreSuccessivo(0), 0) <> 0 Then
                                    'LA COMPETENZA SUCCESSIVA DEVE ESSERE AGGIORNATA ALLA DATA INIZIALE DI QUELLA CHE STIAMO ELIMINANDO
                                    par.cmd.CommandText = "UPDATE SISCOM_MI.T_VOCI_BOLLETTA_COMP_ANNO SET INIZIO=" _
                                        & " (SELECT A.INIZIO FROM SISCOM_MI.T_VOCI_BOLLETTA_COMP_ANNO A WHERE A.ID=" & IDelimina.Value & ") " _
                                        & " WHERE ID=" & par.IfNull(LettoreSuccessivo(0), 0)
                                    par.cmd.ExecuteNonQuery()

                                    'RECUPERIAMO LA COMPETENZA DA AGGIORNARE
                                    par.cmd.CommandText = "SELECT ID_COMPETENZA AS ID,T_VOCI_BOLLETTA_COMPETENZA.DESCRIZIONE AS DESCRIZIONE " _
                                        & " FROM SISCOM_MI.T_VOCI_BOLLETTA_COMP_ANNO, SISCOM_MI.T_VOCI_BOLLETTA_COMPETENZA " _
                                        & " WHERE T_VOCI_BOLLETTA_COMP_ANNO.ID_COMPETENZA=T_VOCI_BOLLETTA_COMPETENZA.ID " _
                                        & " AND T_VOCI_BOLLETTA_COMP_ANNO.ID=" & par.IfNull(LettoreSuccessivo(0), 0)
                                    Dim LettoreCompetenza As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                    Dim competenza As Integer = 0
                                    Dim DescrizioneCompetenza As String = 0
                                    If LettoreCompetenza.Read Then
                                        competenza = par.IfNull(LettoreCompetenza("ID"), 0)
                                        DescrizioneCompetenza = par.IfNull(LettoreCompetenza("DESCRIZIONE"), 0)
                                    End If
                                    LettoreCompetenza.Close()

                                    'UPDATE BOL_BOLLETTE_VOCI_PAGAMENTI
                                    Dim UpdateDataDal As String = ""
                                    Dim UpdateDataAl As String = ""
                                    par.cmd.CommandText = "SELECT INIZIO FROM SISCOM_MI.T_VOCI_BOLLETTA_COMP_ANNO WHERE ID=" & IDelimina.Value
                                    Dim LettoreDate As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                    If LettoreDate.Read Then
                                        UpdateDataDal = par.IfNull(LettoreDate("INIZIO"), "")
                                    End If
                                    LettoreDate.Close()

                                    par.cmd.CommandText = "SELECT FINE FROM SISCOM_MI.T_VOCI_BOLLETTA_COMP_ANNO WHERE ID=" & par.IfNull(LettoreSuccessivo(0), 0)
                                    LettoreDate = par.cmd.ExecuteReader
                                    If LettoreDate.Read Then
                                        UpdateDataAl = par.IfNull(LettoreDate("FINE"), "")
                                    End If
                                    LettoreDate.Close()

                                    Dim QUERY As String = ""
                                    QUERY = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI SET ID_COMPETENZA=" _
                                        & competenza & ",COMPETENZA=INITCAP('" & DescrizioneCompetenza & "') " _
                                        & " WHERE ID_T_VOCE_BOLLETTA=" & Request.QueryString("ID") _
                                        & " AND DATA_EMISSIONE_BOL_BOLLETTE BETWEEN " & UpdateDataDal _
                                        & " AND " & UpdateDataAl
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.UPDATE_COMPETENZE (DESCRIZIONE,DATA_ORA) VALUES('" & Replace(QUERY, "'", "''") & "'," & Format(Now, "yyyyMMddHHmmss") & ")"
                                    par.cmd.ExecuteNonQuery()

                                    'UPDATE BOL_BOLLETTE_VOCI_EMISSIONI
                                    QUERY = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI_EMISSIONI SET FL_NO_TRG=1,COMPETENZA_T_VOCI_BOLLETTA=" _
                                       & competenza & ",COMPETENZA=INITCAP('" & DescrizioneCompetenza & "') " _
                                       & " WHERE ID_VOCE_BOL_BOLLETTE_VOCI=" & Request.QueryString("ID") _
                                       & " AND DATA_EMISSIONE_BOL_BOLLETTE BETWEEN " & UpdateDataDal _
                                       & " AND " & UpdateDataAl
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.UPDATE_COMPETENZE (DESCRIZIONE,DATA_ORA) VALUES('" & Replace(QUERY, "'", "''") & "'," & Format(Now, "yyyyMMddHHmmss") & ")"
                                    par.cmd.ExecuteNonQuery()
                                Else
                                    'LA RIGA CHE STIAMO ELIMINANDO è L'UNICA COMPETENZA
                                    'UPDATE BOL_BOLLETTE_VOCI_PAGAMENTI
                                    Dim QUERY As String = ""
                                    QUERY = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI SET ID_COMPETENZA=-1," _
                                        & " COMPETENZA=INITCAP('NESSUNA') " _
                                        & " WHERE ID_T_VOCE_BOLLETTA=" & Request.QueryString("ID")
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.UPDATE_COMPETENZE (DESCRIZIONE,DATA_ORA) VALUES('" & Replace(QUERY, "'", "''") & "'," & Format(Now, "yyyyMMddHHmmss") & ")"
                                    par.cmd.ExecuteNonQuery()

                                    'UPDATE BOL_BOLLETTE_VOCI_EMISSIONI
                                    QUERY = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI_EMISSIONI SET FL_NO_TRG=1,COMPETENZA_T_VOCI_BOLLETTA=-1," _
                                       & " COMPETENZA=INITCAP('NESSUNA') " _
                                       & " WHERE ID_VOCE_BOL_BOLLETTE_VOCI=" & Request.QueryString("ID")
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.UPDATE_COMPETENZE (DESCRIZIONE,DATA_ORA) VALUES('" & Replace(QUERY, "'", "''") & "'," & Format(Now, "yyyyMMddHHmmss") & ")"
                                    par.cmd.ExecuteNonQuery()
                                End If
                            End If
                            LettoreSuccessivo.Close()
                        End If
                    End If
                    LettorePrecedente.Close()

                    'Dim idUltimo As Integer = 0
                    'Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    'If Lettore.Read Then
                    '    idUltimo = par.IfNull(Lettore(0), 0)
                    'End If
                    'Lettore.Close()

                    ''UPDATE DELLA COMPETENZA PRECEDENTE
                    'par.cmd.CommandText = "UPDATE SISCOM_MI.T_VOCI_BOLLETTA_COMP_ANNO " _
                    '    & " SET FINE=20501231 " _
                    '    & " WHERE ID=" & idUltimo
                    'par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "delete from SISCOM_MI.T_VOCI_BOLLETTA_COMP_ANNO where id=" & IDelimina.Value
                    par.cmd.ExecuteNonQuery()

                    par.myTrans.Commit()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore durante la cancellazione della competenza!');", True)
            End Try
            caricaElencoVoci()
        End If
    End Sub
End Class
