
Partial Class NEW_CENSIMENTO_ElencoCensimenti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:500; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='IMMCENSIMENTO/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)

        If Not IsPostBack Then
            'Me.txtDataInizio.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            'Me.txtDataFine.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            Me.txtDataInizio.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtDataFine.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            Cerca()
            Response.Flush()

        End If

    End Sub

    Private Sub Cerca()
        Try

            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim Query As String = "SELECT ID, to_char(to_date(DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,to_char(to_date(DATA_FINE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_FINE,(CASE WHEN DATA_FINE IS NULL THEN 'APERTO' ELSE 'CHIUSO'END) AS STATO FROM SISCOM_MI.CENSIMENTI_STATO_MANU ORDER BY DATA_INIZIO DESC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Query, par.OracleConn)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "CENSIMENTI_ST_MANUTENTIVO")
            DataGridElenco.DataSource = ds
            DataGridElenco.DataBind()
            'LnlNumeroRisultati.Text = "  - " & DataGridCondom.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()




        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnApriCens_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriCens.Click
        Try

            par.OracleConn.Open()
            par.SettaCommand(par)
            Me.txtDataInizio.Text = ""
            Me.txtDataFine.Text = ""
            par.cmd.CommandText = "SELECT ID, to_char(to_date(DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,to_char(to_date(DATA_FINE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_FINE,(CASE WHEN DATA_FINE IS NULL OR DATA_FINE>(to_char(SYSDATE,'yyyymmdd')) THEN 'APERTO' ELSE 'CHIUSO'END) AS STATO FROM SISCOM_MI.CENSIMENTI_STATO_MANU WHERE DATA_FINE IS NULL"

            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If MyReader.Read Then
                Response.Write("<script>alert('Chiudere il censimento attualmente aperto, per crearne uno nuovo!');</script>")
                Me.txtVisible.Value = 0
            Else
                Me.txtVisible.Value = 1
                Me.txtDataFine.Visible = False
                Me.lblDataFine.Visible = False
            End If

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub



    Protected Sub DataGridElenco_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridElenco.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la gestione con data inizio : " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtId').value='" & e.Item.Cells(0).Text & "';")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la gestione con data inizio : " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtId').value='" & e.Item.Cells(0).Text & "';")

        End If

    End Sub

    Protected Sub BtnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnSalva.Click
        If Me.txtdatainizio.enabled = True Then
            SalvaNuova()
        Else
            ChiudiCensimento()
        End If
    End Sub
    Private Sub SalvaNuova()

        Try
            If Not String.IsNullOrEmpty(Me.txtDataInizio.Text) Then

                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "SELECT MAX(DATA_FINE) FROM SISCOM_MI.CENSIMENTI_STATO_MANU"
                Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If MyReader.Read Then
                    If par.AggiustaData(Me.txtDataInizio.Text) > MyReader(0) Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.CENSIMENTI_STATO_MANU (ID,DATA_INIZIO) VALUES (SISCOM_MI.SEQ_CENSIMENTI_STATO_MANU.NEXTVAL,'" & par.AggiustaData(Me.txtDataInizio.Text) & "')"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.MATERIALI_ST SELECT ('" & par.AggiustaData(Me.txtDataInizio.Text) & "'), materiali.* FROM siscom_mi.materiali"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTERVENTI_ST SELECT ('" & par.AggiustaData(Me.txtDataInizio.Text) & "'), INTERVENTI.* FROM siscom_mi.INTERVENTI"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.ANALISI_PRESTAZIONALE_ST SELECT ('" & par.AggiustaData(Me.txtDataInizio.Text) & "'), ANALISI_PRESTAZIONALE.* FROM siscom_mi.ANALISI_PRESTAZIONALE"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.ANOMALIE_ST SELECT ('" & par.AggiustaData(Me.txtDataInizio.Text) & "'), ANOMALIE.* FROM siscom_mi.ANOMALIE"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.NOTE_ST SELECT ('" & par.AggiustaData(Me.txtDataInizio.Text) & "'), NOTE.* FROM siscom_mi.NOTE"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.STATO_DEGRADO_ST SELECT ('" & par.AggiustaData(Me.txtDataInizio.Text) & "'), STATO_DEGRADO.* FROM siscom_mi.STATO_DEGRADO"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.DIMENSIONI_ELEMENTI_ST SELECT ('" & par.AggiustaData(Me.txtDataInizio.Text) & "'), DIMENSIONI_ELEMENTI.* FROM siscom_mi.DIMENSIONI_ELEMENTI"
                        par.cmd.ExecuteNonQuery()
                        Me.txtVisible.Value = 0
                    Else
                        Response.Write("<script>alert('La Data Inizio del nuovo censimento, deve essere successiva alla Data Fine del precedente!');</script>")

                    End If
                End If
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Cerca()

            End If


        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnChiudiCens_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnChiudiCens.Click
        Try

            If Me.txtId.Value <> "" Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                par.cmd.CommandText = "SELECT DATA_INIZIO,DATA_FINE from SISCOM_MI.CENSIMENTI_STATO_MANU WHERE ID = " & Me.txtId.Value

                Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If MyReader.Read Then
                    If par.IfNull(MyReader("DATA_FINE"), "") = "" Then

                        Me.txtDataInizio.Text = par.FormattaData(par.IfNull(MyReader(0), ""))
                        Me.txtDataInizio.Enabled = False
                        Me.txtDataFine.Visible = True
                        Me.lblDataFine.Visible = True
                    Else
                        Response.Write("<script>alert('Il Censimento risulta già chiuso!');</script>")
                        Me.txtVisible.Value = 0
                    End If

                End If
            Else
                Response.Write("<script>alert('Selezionare una riga!');</script>")
                Me.txtVisible.Value = 0
            End If

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub ChiudiCensimento()
        Try
            If par.AggiustaData(Me.txtDataInizio.Text) < par.AggiustaData(Me.txtDataFine.Text) Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "UPDATE SISCOM_MI.CENSIMENTI_STATO_MANU SET DATA_FINE = '" & par.AggiustaData(Me.txtDataFine.Text) & "' WHERE ID = " & Me.txtId.Value & ""
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.MATERIALI_ST SELECT ('" & par.AggiustaData(Me.txtDataFine.Text) & "'), materiali.* FROM siscom_mi.materiali"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTERVENTI_ST SELECT ('" & par.AggiustaData(Me.txtDataFine.Text) & "'), INTERVENTI.* FROM siscom_mi.INTERVENTI"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.ANALISI_PRESTAZIONALE_ST SELECT ('" & par.AggiustaData(Me.txtDataFine.Text) & "'), ANALISI_PRESTAZIONALE.* FROM siscom_mi.ANALISI_PRESTAZIONALE"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.ANOMALIE_ST SELECT ('" & par.AggiustaData(Me.txtDataFine.Text) & "'), ANOMALIE.* FROM siscom_mi.ANOMALIE"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.NOTE_ST SELECT ('" & par.AggiustaData(Me.txtDataFine.Text) & "'), NOTE.* FROM siscom_mi.NOTE"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.STATO_DEGRADO_ST SELECT ('" & par.AggiustaData(Me.txtDataFine.Text) & "'), STATO_DEGRADO.* FROM siscom_mi.STATO_DEGRADO"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.DIMENSIONI_ELEMENTI_ST SELECT ('" & par.AggiustaData(Me.txtDataFine.Text) & "'), DIMENSIONI_ELEMENTI.* FROM siscom_mi.DIMENSIONI_ELEMENTI"
                par.cmd.ExecuteNonQuery()

                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Me.txtVisible.Value = 0
            Else
                Response.Write("<script>alert('Deifnire correttamente la data fine!Deve essere successiva alla data di inizio!');</script>")

            End If
            Cerca()

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message

        End Try
    End Sub
End Class
