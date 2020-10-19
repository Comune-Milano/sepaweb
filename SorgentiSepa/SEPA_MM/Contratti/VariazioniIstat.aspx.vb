
Partial Class Contratti_VariazioniIstat
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Dim Str As String

            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            Str = Str & "<" & "/div>"

            Response.Write(Str)
            Response.Flush()


            BindGrid()

            TxtData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataGazz.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub
    Private Property vData() As String
        Get
            If Not (ViewState("par_vData") Is Nothing) Then
                Return CStr(ViewState("par_vData"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vData") = value
        End Set

    End Property

    Private Sub BindGrid()
        Try



            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim Str As String = "select id, to_char(to_date(data_validita,'yyyymmdd'),'dd/mm/yyyy') as data_validita, indice_nazionale, base_indice,var_100_annuale, var_100_biennale,var_75_annuale, nro_gazzetta, to_char(to_date(data_gazzetta,'yyyymmdd'),'dd/mm/yyyy')as data_gazzetta from siscom_mi.variazioni_istat order by id desc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)

            Dim ds As New Data.DataSet()

            da.Fill(ds, "SISCOM_MI.VARIAZIONI_ISTAT")

            DataGridVarIstat.DataSource = ds
            DataGridVarIstat.DataBind()

            par.OracleConn.Close()

        Catch ex As Exception
            par.OracleConn.Close()
        End Try

    End Sub

    Protected Sub DataGridVarIstat_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridVarIstat.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub

    Protected Sub DataGridVarIstat_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridVarIstat.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridVarIstat.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If

    End Sub


    Protected Sub img_ChiudiSchema_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_ChiudiSchema.Click
        Me.txtBaseIndice.Text = ""
        Me.TxtData.Text = ""
        Me.txtDataGazz.Text = ""
        Me.TxtIndice.Text = ""
        Me.txtNumGazzetta.Text = ""
        Me.TxtVarAnn100.Text = ""
        Me.TxtVarAnn75.Text = ""
        Me.TxtVarBienn.Text = ""
        Me.txtmia.Text = ""
        Me.txtid.Value = ""
    End Sub
    Protected Sub img_InserisciSchema_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_InserisciSchema.Click
        If Me.TextBox1.Value = 2 Then
            Update()
        ElseIf Me.TextBox1.Value = 1 Then
            Salva()

        End If
    End Sub
    Private Sub Update()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If par.IfEmpty(Me.TxtData.Text, "") <> "" And par.IfEmpty(Me.TxtIndice.Text, "") <> "" And par.IfEmpty(Me.txtBaseIndice.Text, "") <> "" And par.IfEmpty(Me.TxtVarAnn100.Text, "") <> "" Then
                If Me.vData <> Me.TxtData.Text Then

                    par.cmd.CommandText = "select * FROM SISCOM_MI.VARIAZIONI_ISTAT WHERE DATA_VALIDITA = " & par.AggiustaData(Me.TxtData.Text)
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        Response.Write("<script>alert('Per questo mese sono già stati inseriti i dati della variazione ISTAT!Nessun dato salvato')</script>")
                        Me.txtBaseIndice.Text = ""
                        Me.TxtData.Text = ""
                        Me.txtDataGazz.Text = ""
                        Me.TxtIndice.Text = ""
                        Me.txtNumGazzetta.Text = ""
                        Me.TxtVarAnn100.Text = ""
                        Me.TxtVarAnn75.Text = ""
                        Me.TxtVarBienn.Text = ""
                        Me.TextBox1.Value = 0
                        Me.txtmia.Text = ""
                        Me.txtid.Value = ""
                        par.OracleConn.Close()
                        Exit Sub
                    End If
                End If

                par.cmd.CommandText = "UPDATE SISCOM_MI.VARIAZIONI_ISTAT SET DATA_VALIDITA= " & par.AggiustaData(Me.TxtData.Text) & ",INDICE_NAZIONALE = " & par.VirgoleInPunti(Me.TxtIndice.Text) & "," _
                & "BASE_INDICE =" & par.VirgoleInPunti(Me.txtBaseIndice.Text) & ",VAR_100_ANNUALE =" & par.VirgoleInPunti(Me.TxtVarAnn100.Text) & ",VAR_100_BIENNALE =" & par.IfEmpty(par.VirgoleInPunti(Me.TxtVarBienn.Text), "NULL") & "," _
                & "VAR_75_ANNUALE =" & par.IfEmpty(par.VirgoleInPunti(Me.TxtVarAnn75.Text), "NULL") & ",NRO_GAZZETTA =" & par.IfEmpty(par.VirgoleInPunti(Me.txtNumGazzetta.Text), "NULL") & ",DATA_GAZZETTA = " & par.IfEmpty(par.AggiustaData(Me.txtDataGazz.Text), "NULL") & " WHERE ID = " & Me.txtid.Value
                par.cmd.ExecuteNonQuery()
                par.OracleConn.Close()
                Response.Write("<script>alert('Operazione eseguita correttamente!')</script>")
     
                BindGrid()
            Else
                par.OracleConn.Close()
                Response.Write("<script>alert('Riempire tutti i campi obbligatori prima di salvare!Nessun dato salvato')</script>")

            End If
            Me.txtBaseIndice.Text = ""
            Me.TxtData.Text = ""
            Me.txtDataGazz.Text = ""
            Me.TxtIndice.Text = ""
            Me.txtNumGazzetta.Text = ""
            Me.TxtVarAnn100.Text = ""
            Me.TxtVarAnn75.Text = ""
            Me.TxtVarBienn.Text = ""
            Me.TextBox1.Value = 0
            Me.txtmia.Text = ""
            Me.txtid.Value = ""
        Catch ex As Exception
            par.OracleConn.Close()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub Salva()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If par.IfEmpty(Me.TxtData.Text, "") <> "" And par.IfEmpty(Me.TxtIndice.Text, "") <> "" And par.IfEmpty(Me.txtBaseIndice.Text, "") <> "" And par.IfEmpty(Me.TxtVarAnn100.Text, "") <> "" Then

                par.cmd.CommandText = "select * FROM SISCOM_MI.VARIAZIONI_ISTAT WHERE DATA_VALIDITA = " & par.AggiustaData(Me.TxtData.Text)
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Response.Write("<script>alert('Per questo mese sono già stati inseriti i dati della variazione ISTAT!')</script>")
                    Me.txtBaseIndice.Text = ""
                    Me.TxtData.Text = ""
                    Me.txtDataGazz.Text = ""
                    Me.TxtIndice.Text = ""
                    Me.txtNumGazzetta.Text = ""
                    Me.TxtVarAnn100.Text = ""
                    Me.TxtVarAnn75.Text = ""
                    Me.TxtVarBienn.Text = ""
                    Me.txtmia.Text = ""
                    Me.txtid.Value = ""
                    par.OracleConn.Close()
                    Exit Sub
                End If
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.VARIAZIONI_ISTAT (ID,DATA_VALIDITA,INDICE_NAZIONALE,BASE_INDICE,VAR_100_ANNUALE,VAR_100_BIENNALE,VAR_75_ANNUALE,NRO_GAZZETTA,DATA_GAZZETTA)" _
               & " VALUES (SISCOM_MI.SEQ_VARIAZIONI_ISTAT.NEXTVAL," & par.AggiustaData(Me.TxtData.Text) & "," & par.VirgoleInPunti(Me.TxtIndice.Text) & "," & par.VirgoleInPunti(Me.txtBaseIndice.Text) & "," & par.VirgoleInPunti(Me.TxtVarAnn100.Text) & "," _
               & " " & par.IfEmpty(par.VirgoleInPunti(Me.TxtVarBienn.Text), "NULL") & "," & par.IfEmpty(par.VirgoleInPunti(Me.TxtVarAnn75.Text), "NULL") & "," & par.IfEmpty(par.VirgoleInPunti(Me.txtNumGazzetta.Text), "NULL") & "," & par.IfEmpty(par.AggiustaData(Me.txtDataGazz.Text), "NULL") & ")"
                par.cmd.ExecuteNonQuery()
                par.OracleConn.Close()
                Response.Write("<script>alert('Operazione eseguita correttamente!')</script>")
                BindGrid()
            Else
                par.OracleConn.Close()
                Response.Write("<script>alert('Riempire tutti i campi obbligatori prima di salvare!')</script>")

            End If
            Me.txtBaseIndice.Text = ""
            Me.TxtData.Text = ""
            Me.txtDataGazz.Text = ""
            Me.TxtIndice.Text = ""
            Me.txtNumGazzetta.Text = ""
            Me.TxtVarAnn100.Text = ""
            Me.TxtVarAnn75.Text = ""
            Me.TxtVarBienn.Text = ""
            Me.txtmia.Text = ""
            Me.txtid.Value = ""

        Catch ex As Exception
            par.OracleConn.Close()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub ImgBtnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnElimina.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If par.IfEmpty(Me.txtid.Value, "") <> "" Then

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.VARIAZIONI_ISTAT WHERE ID = " & Me.txtid.Value
                par.cmd.ExecuteNonQuery()
                par.OracleConn.Close()

                BindGrid()
                Me.txtmia.Text = ""
                Me.txtid.Value = ""

            Else
                Me.TextBox1.Value = 0

                Response.Write("<script>alert('Nessuna voce selezionata!')</script>")

                par.OracleConn.Close()

            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try


    End Sub

    Protected Sub ImgModifica_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgModifica.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            If par.IfEmpty(Me.txtid.Value, "") <> "" Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.VARIAZIONI_ISTAT WHERE ID = " & Me.txtid.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Me.TxtData.Text = par.FormattaData(myReader("DATA_VALIDITA").ToString)
                    Me.txtBaseIndice.Text = myReader("BASE_INDICE").ToString
                    Me.txtDataGazz.Text = par.FormattaData(myReader("DATA_GAZZETTA").ToString)
                    Me.txtNumGazzetta.Text = myReader("NRO_GAZZETTA").ToString
                    Me.TxtIndice.Text = myReader("INDICE_NAZIONALE").ToString
                    Me.TxtVarAnn100.Text = myReader("VAR_100_ANNUALE").ToString
                    Me.TxtVarAnn75.Text = myReader("VAR_75_ANNUALE").ToString
                    Me.TxtVarBienn.Text = myReader("VAR_100_BIENNALE").ToString
                    Me.vData = Me.TxtData.Text
                End If
                par.OracleConn.Close()
                Me.TextBox1.Value = "2"
            Else
                Me.TextBox1.Value = 0

                Response.Write("<script>alert('Nessuna Voce selezionata!')</script>")
                par.OracleConn.Close()

            End If

            'Me.ImgModifica.OnClientClick = "document.getElementById('TextBox1').value='2';document.getElementById('InsVariazione').style.visibility='visible';"
        Catch ex As Exception
            par.OracleConn.Close()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub txtmia_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtmia.TextChanged

    End Sub
End Class
