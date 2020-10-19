
Partial Class Contratti_ContCalore_Parametri
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        If Not IsPostBack Then
            txtAnno.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")

            txtValore.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);return false;")
            txtValore.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")

            txtNDecimali.Attributes.Add("onBlur", "javascript:valid(this,'onlynumber');")
            txtMensilita.Attributes.Add("onBlur", "javascript:valid(this,'onlynumber');")

            par.caricaComboBox("SELECT id,descrizione FROM siscom_mi.AREA_ECONOMICA", cmbArea, "ID", "DESCRIZIONE", False)
            par.caricaComboBox("SELECT cod, descrizione from SISCOM_MI.TIPOLOGIA_DIMENSIONI", cmbTipoDim, "cod", "DESCRIZIONE", False)
            'par.caricaComboBox("select id,anno_isee from utenza_bandi order by anno_isee desc", cmbAnnoReddito, "id", "anno_isee", False)
            DefaultValue()

            CaricaContCalEsistenti()

        End If
    End Sub
    Private Sub DefaultValue()
        Try
            Me.txtMensilita.Text = ""
            Me.txtNDecimali.Text = "2"
            Me.txtValore.Text = "5,16"
            Me.cmbArea.SelectedValue = "1"
            Me.cmbTipoDim.SelectedValue = "SUP_NETTA"
            Me.txtNote.Text = ""

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- btnConfirm_Click" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")

        End Try
    End Sub
    Private Sub CaricaContCalEsistenti()
        Try

            par.cmd.CommandText = "select cont_calore_parametri.id," _
                                & " anno,valore,arrotondamento,note," _
                                & "tipologia_dimensioni.descrizione as tipo_dimensioni,area_economica.descrizione AS area " _
                                & "from siscom_mi.cont_calore_parametri,siscom_mi.tipologia_dimensioni, siscom_mi.area_economica " _
                                & "where tipologia_dimensioni.cod = cod_tipo_dimensione AND area_economica.ID = cont_calore_parametri.area_limite "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Me.dgvContCalore.DataSource = dt
            Me.dgvContCalore.DataBind()

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")

        End Try
    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            If Me.ConfNuovo.Value = 1 Then
                If ControlDati("insert") = True Then

                    par.cmd.CommandText = "insert into siscom_mi.cont_calore_parametri(id,anno," _
                        & "valore,cod_tipo_dimensione,area_limite,arrotondamento,note,mensilita,id_stato) values (siscom_mi.seq_cont_calore_parametri.nextval," _
                        & "" & Me.txtAnno.Text & "," & par.VirgoleInPunti(Me.txtValore.Text.Replace(".", "")) & ", '" & Me.cmbTipoDim.SelectedValue _
                        & "', " & Me.cmbArea.SelectedValue _
                        & "," & Me.txtNDecimali.Text & ",'" & par.PulisciStrSql(Me.txtNote.Text) & "', " & Me.txtMensilita.Text & ",0)"
                    par.cmd.ExecuteNonQuery()

                    Response.Write("<script>alert('Operazione eseguita correttamente!')</script>")

                    Me.DivVisibile.Value = 0
                    CaricaContCalEsistenti()
                    DefaultValue()
                End If


            ElseIf txtidContCalore.Value <> "0" Then
                If ControlDati("update") = True Then
                    par.cmd.CommandText = "select id from siscom_mi.cont_calore_anno where anno = " & Me.txtAnno.Text & " and id_stato <=1"
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.HasRows = False Then
                        par.cmd.CommandText = "update siscom_mi.cont_calore_parametri set " _
                                            & " anno = " & Me.txtAnno.Text & " " _
                                            & ",valore = " & par.VirgoleInPunti(Me.txtValore.Text.Replace(".", "")) & "" _
                                            & ",cod_tipo_dimensione = '" & Me.cmbTipoDim.SelectedValue & "'" _
                                            & ",area_limite = " & Me.cmbArea.SelectedValue & "" _
                                            & ",arrotondamento = " & Me.txtNDecimali.Text & "" _
                                            & ",note ='" & par.PulisciStrSql(Me.txtNote.Text) & "'" _
                                            & ",mensilita = " & Me.txtMensilita.Text & "" _
                                            & " where id = " & txtidContCalore.Value
                        par.cmd.ExecuteNonQuery()
                        Response.Write("<script>alert('Operazione eseguita correttamente!')</script>")
                    Else
                        Response.Write("<script>alert('Eiste già un contributo calore per questo anno e non può essere modificato!!')</script>")

                    End If



                    Me.DivVisibile.Value = 0
                    txtidContCalore.Value = 0
                    CaricaContCalEsistenti()
                    DefaultValue()

                End If
            End If

            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- btnSalva_Click" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")

        End Try
    End Sub

    Protected Sub dgvContCalore_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvContCalore.ItemDataBound



        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtidContCalore').value='" & e.Item.Cells(0).Text & "'")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtidContCalore').value='" & e.Item.Cells(0).Text & "'")
        End If


    End Sub

    Private Function ControlDati(ByVal tipo As String) As Boolean

        ControlDati = True
        Try
            If String.IsNullOrEmpty(Me.txtAnno.Text) Then
                Response.Write("<script>alert('Definire l\'anno di validità dei parametri per il calcolo del contributo!')</script>")
                ControlDati = False
                Exit Function

            End If
            If tipo = "insert" Then
                par.cmd.CommandText = "select * from siscom_mi.cont_calore_parametri where anno =" & Me.txtAnno.Text
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.HasRows = True Then
                    Response.Write("<script>alert('Parametri già definiti per questo anno!')</script>")
                    ControlDati = False
                    Exit Function

                End If
                lettore.Close()
            End If

            If String.IsNullOrEmpty(Me.txtNDecimali.Text) Then
                Response.Write("<script>alert('Definire il numero di decimali per arrotondare gli importi!')</script>")
                ControlDati = False
                Exit Function

            End If
            If String.IsNullOrEmpty(Me.txtNDecimali.Text) Then
                Response.Write("<script>alert('Definire il numero di decimali per arrotondare gli importi!')</script>")
                ControlDati = False
                Exit Function

            End If
            If String.IsNullOrEmpty(Me.txtValore.Text) Then
                Response.Write("<script>alert('Definire il valore per il calcolo del contributo calore!')</script>")
                ControlDati = False
                Exit Function

            End If

            If Not String.IsNullOrEmpty(Me.txtValore.Text) Then
                If Me.txtValore.Text.Replace(".", "") <= 0 Then
                    Response.Write("<script>alert('Il campo valore deve essere maggiore di zero!')</script>")
                    ControlDati = False
                    Exit Function

                End If
            End If

            If String.IsNullOrEmpty(Me.txtMensilita.Text) Then
                Response.Write("<script>alert('Definire il numero di mensilità!')</script>")
                ControlDati = False
                Exit Function

            End If
            If Not String.IsNullOrEmpty(Me.txtMensilita.Text) Then
                If Me.txtMensilita.Text <= 0 Then
                    Response.Write("<script>alert('Il numero di mensilità deve essere maggiore di zero!')</script>")
                    ControlDati = False
                    Exit Function
                End If
            End If

            'par.cmd.CommandText = "select * from siscom_mi.cont_calore_parametri where data_inizio <='" & par.AggiustaData(Me.txtDataInizio.Text) & "' and data_fine>='" & par.AggiustaData(Me.txtDataInizio.Text) & "' and id <> " & txtidContCalore.Value
            'Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'If lettore.HasRows Then
            '    Response.Write("<script>alert('Data inizio presente in un altro calcolo del contributo calore!')</script>")
            '    ControlDati = False
            'End If
            'lettore.Close()

            'par.cmd.CommandText = "select * from siscom_mi.cont_calore_parametri where data_inizio <='" & par.AggiustaData(Me.txtDataFine.Text) & "' and data_fine>='" & par.AggiustaData(Me.txtDataFine.Text) & "' and id <> " & txtidContCalore.Value
            'lettore = par.cmd.ExecuteReader
            'If lettore.HasRows Then
            '    Response.Write("<script>alert('Data fine presente in un altro calcolo del contributo calore!')</script>")
            '    ControlDati = False
            'End If
            'lettore.Close()


            'par.cmd.CommandText = "select * from siscom_mi.cont_calore_parametri where data_inizio "

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- btnConfirm_Click" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")

        End Try
    End Function

    Protected Sub btnModifica_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnModifica.Click
        Try

            If txtidContCalore.Value <> "0" Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                'par.cmd.CommandText = "select DISTINCT (cont_calore_parametri.id) from siscom_mi.cont_calore_parametri, siscom_mi.cont_calore_parametri_elaborazione where cont_calore_parametri.id = cont_calore_parametri_elaborazione.id_cont_calore_parametri and cont_calore_parametri.id = " & txtidContCalore.Value
                par.cmd.CommandText = "select cont_calore_anno.id from siscom_mi.cont_calore_anno, siscom_mi.cont_calore_parametri where cont_calore_anno.anno = cont_calore_parametri.anno and cont_calore_parametri.id = " & txtidContCalore.Value & " and cont_calore_anno.id_stato >=1"

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    Response.Write("<script>alert('Impossibile effettuare la modifica!\nPer questo parametro è stato già generato l\'elenco degli aventi diritto!');</script>")
                    DivVisibile.Value = 0
                Else
                    RiempiCampi()
                End If

                myReader1.Close()

                '****************MYEVENT*****************
                'par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & CType(Me.Page, Object).vIdCondominio & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F36','CANCELLATA CONVOCAZIONE')"
                'par.cmd.ExecuteNonQuery()

            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- btnModifica_Click" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")

        End Try
    End Sub


    Private Sub RiempiCampi()
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select * from siscom_mi.cont_calore_parametri where cont_calore_parametri.id = " & txtidContCalore.Value
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If myReader1.Read Then

                'Me.txtDataInizio.Text = par.IfNull(par.FormattaData(myReader1("DATA_INIZIO")), "")
                'Me.txtDataFine.Text = par.IfNull(par.FormattaData(myReader1("DATA_FINE")), "")
                Me.txtAnno.Text = myReader1("anno")

                Me.txtValore.Text = par.IfNull(myReader1("VALORE"), "")
                Me.cmbTipoDim.SelectedValue = myReader1("COD_TIPO_DIMENSIONE")
                Me.cmbArea.SelectedValue = myReader1("AREA_LIMITE")
                Me.txtNDecimali.Text = par.IfNull(myReader1("ARROTONDAMENTO"), "")
                Me.txtNote.Text = par.IfNull(myReader1("NOTE"), "")
                Me.txtMensilita.Text = par.IfNull(myReader1("MENSILITA"), "")

            End If

            myReader1.Close()

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- RiempiCampi" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")

        End Try
    End Sub


    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Try

            DefaultValue()
            txtidContCalore.Value = 0
            ConfNuovo.Value = 0

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- btnAnnulla_Click" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")

        End Try
    End Sub

End Class
