'MODULO DI GESTIONE TAB_TRIBUNALI_COMPETENTI

Partial Class MOROSITA_Legali_Tribunali
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If


        Try

            Dim Str As String

            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            Str = Str & "<" & "/div>"

            Response.Write(Str)

            If Not IsPostBack Then
                Response.Flush()

                Me.txtID.Value = "-1"

                BindGrid()

                CaricaComuni()

                If Session.Item("MOD_MOROSITA_SL") = "1" Then
                    FrmSoloLettura()
                End If



            End If

        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub
    Private Sub FrmSoloLettura()
        Me.btnAggiungi.Visible = False
        Me.btnModifica.Visible = False
        Me.btnElimina.Visible = False
    End Sub

    Private Sub BindGrid()
        Dim FlagConnessione As Boolean = False

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            Dim Str As String = "select TAB_TRIBUNALI_COMPETENTI.ID, T_COMUNI.NOME as COMUNE,TAB_TRIBUNALI_COMPETENTI.COMPETENZA  " _
                             & " from SISCOM_MI.TAB_TRIBUNALI_COMPETENTI,SEPA.T_COMUNI " _
                             & " where TAB_TRIBUNALI_COMPETENTI.COD_COMUNE=T_COMUNI.COD " _
                             & " order by COMUNE ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)
            Dim ds As New Data.DataTable

            da.Fill(ds)

            DataGrid1.DataSource = ds
            DataGrid1.DataBind()

            da.Dispose()
            ds.Dispose()

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    Private Sub CaricaComuni()
        Dim FlagConnessione As Boolean = False

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            Me.cmbComune.Items.Clear()
            Me.cmbComune.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "select T_COMUNI.COD,T_COMUNI.NOME   " _
                                 & " from SEPA.T_COMUNI " _
                                 & " order by NOME ASC"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader1.Read
                Me.cmbComune.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("COD"), -1)))
            End While
            myReader1.Close()
            '**************************

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtSelezione').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtID').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtSelezione').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtID').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If

    End Sub


    Protected Sub img_ChiudiSchema_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_ChiudiSchema.Click
        Me.txtCompetenza.Text = ""
        Me.cmbComune.SelectedValue = "-1"
        Me.txtID.Value = "-1"
    End Sub


    Protected Sub btn_Inserisci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Inserisci.Click
        If Me.txtID.Value <> "-1" Then
            Update()
        Else
            Salva()
        End If
    End Sub


    Private Sub Update()
        Dim FlagConnessione As Boolean = False

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            If par.IfEmpty(Me.cmbComune.Text, "") <> "" And par.IfEmpty(Me.txtCompetenza.Text, "") <> "" Then

                par.cmd.CommandText = "select * from SISCOM_MI.TAB_TRIBUNALI_COMPETENTI where COD_COMUNE='" & Me.cmbComune.SelectedValue & "' and COMPETENZA='" & par.PulisciStrSql(Me.txtCompetenza.Text) & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Response.Write("<script>alert('Attenzione, comune e competenza già inseriti')</script>")
                    Me.txtCompetenza.Text = ""

                    Me.cmbComune.SelectedValue = "-1"

                    Me.txtID.Value = "-1"

                    If FlagConnessione = True Then
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    End If

                    Exit Sub
                End If

                par.cmd.CommandText = "update SISCOM_MI.TAB_TRIBUNALI_COMPETENTI set COD_COMUNE='" & Me.cmbComune.SelectedValue & "',COMPETENZA='" & par.PulisciStrSql(Me.txtCompetenza.Text) & "'" _
                                   & " where ID=" & Me.txtID.Value
                par.cmd.ExecuteNonQuery()


                'LOG MODIFICA
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.LOG  " _
                                        & " (ID_OPERATORE,DATA_OPERAZIONE,TIPO_OPERAZIONE,TIPO_OGGETTO," _
                                        & " COD_OGGETTO,DESCR_OGGETTO,NOTE) " _
                                 & " values (:id_operatore,:data_operazione,:tipo_operazione,:tipo_oggetto," _
                                        & ":cod_oggetto,:descrizione,:note) "

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_operazione", Format(Now, "yyyyMMdd")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_operazione", "MODIFICA"))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_oggetto", "TAB_TRIBUNALI_COMPETENTI"))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_oggetto", "TAB_TRIBUNALI_COMPETENTI"))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", ""))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", "COMUNE=" & Me.txtComuneODL.Value & " in=" & Me.cmbComune.SelectedItem.Text & "   COMPETENZA=" & Me.txtCompetenzaODL.Value & " in: " & Me.txtCompetenza.Text))

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()
                '****************************************


                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

                Response.Write("<script>alert('Operazione eseguita correttamente!')</script>")

                BindGrid()
            Else
                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
                Response.Write("<script>alert('Riempire tutti i campi obbligatori prima di salvare! Nessun dato salvato')</script>")

            End If

            Me.txtCompetenza.Text = ""
            Me.cmbComune.SelectedValue = "-1"

            Me.TextBox1.Value = 0
            Me.txtSelezione.Text = ""
            Me.txtID.Value = "-1"


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub


    Private Sub Salva()
        Dim FlagConnessione As Boolean = False

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            If par.IfEmpty(Me.cmbComune.Text, "") <> "" And par.IfEmpty(Me.txtCompetenza.Text, "") <> "" Then

                par.cmd.CommandText = "select * from SISCOM_MI.TAB_TRIBUNALI_COMPETENTI where COD_COMUNE='" & Me.cmbComune.SelectedValue & "' and COMPETENZA='" & par.PulisciStrSql(Me.txtCompetenza.Text) & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader.Read Then
                    Response.Write("<script>alert('Attenzione, comune e competenza già inseriti')</script>")

                    Me.txtCompetenza.Text = ""
                    Me.cmbComune.SelectedValue = "-1"

                    Me.txtSelezione.Text = ""
                    Me.txtID.Value = "-1"
                    par.OracleConn.Close()

                    Exit Sub
                End If
                myReader.Close()

                par.cmd.CommandText = "insert INTO SISCOM_MI.TAB_TRIBUNALI_COMPETENTI (ID,COD_COMUNE,COMPETENZA)" _
                                   & " values (SISCOM_MI.SEQ_TAB_TRIBUNALI_COMPETENTI.NEXTVAL,'" & Me.cmbComune.SelectedValue & "','" & par.PulisciStrSql(Me.txtCompetenza.Text) & "')"
                par.cmd.ExecuteNonQuery()


                'LOG MODIFICA
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.LOG  " _
                                        & " (ID_OPERATORE,DATA_OPERAZIONE,TIPO_OPERAZIONE,TIPO_OGGETTO," _
                                        & " COD_OGGETTO,DESCR_OGGETTO,NOTE) " _
                                 & " values (:id_operatore,:data_operazione,:tipo_operazione,:tipo_oggetto," _
                                        & ":cod_oggetto,:descrizione,:note) "

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_operazione", Format(Now, "yyyyMMdd")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_operazione", "INSERIMENTO"))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_oggetto", "TAB_TRIBUNALI_COMPETENTI"))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_oggetto", "TAB_TRIBUNALI_COMPETENTI"))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", ""))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", "COMUNE=" & Me.cmbComune.SelectedItem.Text & " e COMPETENZA=" & Me.txtCompetenza.Text))

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()
                '****************************************

                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

                Response.Write("<script>alert('Operazione eseguita correttamente!')</script>")
                BindGrid()
            Else
                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
                Response.Write("<script>alert('Riempire tutti i campi obbligatori prima di salvare!')</script>")

            End If

            Me.txtCompetenza.Text = ""
            Me.cmbComune.SelectedValue = "-1"
            Me.txtSelezione.Text = ""
            Me.txtID.Value = "-1"

        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub btnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnElimina.Click
        Dim FlagConnessione As Boolean = False

        Try
            If txtannullo.Value = 1 Then

                ' APRO CONNESSIONE
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                If par.IfEmpty(Me.txtID.Value, "") <> "-1" Then


                    par.cmd.CommandText = "select * from SISCOM_MI.MOROSITA_LEGALI where ID_TRIBUNALI_COMPETENTI=" & Me.txtID.Value
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                    If myReader.Read Then
                        Response.Write("<script>alert('Attenzione, comune e competenza utilizzati. Operazione non eseguita!')</script>")

                        Me.txtCompetenza.Text = ""
                        Me.cmbComune.SelectedValue = "-1"

                        Me.txtCompetenzaODL.Value = ""
                        Me.txtComuneODL.Value = ""

                        Me.txtSelezione.Text = ""
                        Me.txtID.Value = "-1"
                        par.OracleConn.Close()

                        Exit Sub
                    End If
                    myReader.Close()


                    par.cmd.CommandText = "select TAB_TRIBUNALI_COMPETENTI.ID, T_COMUNI.NOME as COMUNE,TAB_TRIBUNALI_COMPETENTI.COMPETENZA  " _
                                       & " from SISCOM_MI.TAB_TRIBUNALI_COMPETENTI,SEPA.T_COMUNI " _
                                       & " where TAB_TRIBUNALI_COMPETENTI.COD_COMUNE=T_COMUNI.COD " _
                                       & "   and TAB_TRIBUNALI_COMPETENTI.ID=" & Me.txtID.Value

                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then

                        Me.txtComuneODL.Value = myReader("COMUNE").ToString
                        Me.txtCompetenzaODL.Value = myReader("COMPETENZA").ToString

                    End If
                    myReader.Close()


                    par.cmd.CommandText = "delete FROM SISCOM_MI.TAB_TRIBUNALI_COMPETENTI WHERE ID=" & Me.txtID.Value
                    par.cmd.ExecuteNonQuery()

                    'LOG CANCELLAZIONE
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "insert into SISCOM_MI.LOG  " _
                                            & " (ID_OPERATORE,DATA_OPERAZIONE,TIPO_OPERAZIONE,TIPO_OGGETTO," _
                                            & " COD_OGGETTO,DESCR_OGGETTO,NOTE) " _
                                     & " values (:id_operatore,:data_operazione,:tipo_operazione,:tipo_oggetto," _
                                            & ":cod_oggetto,:descrizione,:note) "

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_operazione", Format(Now, "yyyyMMdd")))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_operazione", "CANCELLAZIONE"))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_oggetto", "TAB_TRIBUNALI_COMPETENTI"))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_oggetto", "TAB_TRIBUNALI_COMPETENTI"))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", ""))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", "COMUNE=" & Me.txtComuneODL.Value & " e COMPETENZA=" & Me.txtCompetenzaODL.Value))

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.Parameters.Clear()
                    '****************************************

                    Me.txtComuneODL.Value = ""
                    Me.txtCompetenzaODL.Value = ""

                    If FlagConnessione = True Then
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    End If

                    BindGrid()
                    Me.txtSelezione.Text = ""
                    Me.txtID.Value = "-1"

                Else
                    Me.TextBox1.Value = 0

                    Response.Write("<script>alert('Nessuna voce selezionata!')</script>")

                    If FlagConnessione = True Then
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    End If

                End If
            End If

        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub

    Protected Sub btnModifica_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnModifica.Click
        Dim FlagConnessione As Boolean = False

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            If par.IfEmpty(Me.txtID.Value, "") <> "-1" Then
                par.cmd.CommandText = "select * from SISCOM_MI.TAB_TRIBUNALI_COMPETENTI where ID = " & Me.txtID.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then

                    Me.cmbComune.SelectedValue = myReader("COD_COMUNE").ToString
                    Me.txtCompetenza.Text = myReader("COMPETENZA").ToString

                    Me.txtComuneODL.Value = Me.cmbComune.SelectedItem.Text
                    Me.txtCompetenzaODL.Value = myReader("COMPETENZA").ToString

                End If


                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
                Me.TextBox1.Value = "1"

            Else
                Me.TextBox1.Value = 0

                Response.Write("<script>alert('Nessuna Voce selezionata!')</script>")

                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Page.Dispose()
        '**************************

        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub


End Class
