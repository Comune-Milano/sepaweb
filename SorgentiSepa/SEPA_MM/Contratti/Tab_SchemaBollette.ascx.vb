
Partial Class Contratti_Tab_SchemaBollette
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        'lstSchemaBollette.Attributes.Add("OnClick", "javascript:obj1=document.getElementById('Tab_SchemaBollette1_lstSchemaBollette');if (obj1.selectedIndex!=-1) {document.getElementById('Tab_SchemaBollette1_V1').value=obj1.options[obj1.selectedIndex].text;}")
        If Not IsPostBack Then
            cmbAnnoSchema.Items.Add(New ListItem(Year(Now), Year(Now)))
            cmbAnnoSchema.Items.Add(New ListItem(Year(Now) + 1, Year(Now) + 1))
            cmbAnnoSchema.Items.Add(New ListItem(Year(Now) + 2, Year(Now) + 2))
            cmbAnnoSchema.ClearSelection()
            If Month(Now) = 11 Or Month(Now) = 12 Then
                cmbAnnoSchema.Items.FindByValue(Year(Now) + 1).Selected = True
            Else
                cmbAnnoSchema.Items.FindByValue(Year(Now)).Selected = True
            End If
        End If
    End Sub

    Protected Sub img_EliminaOspite_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_EliminaOspite.Click
       CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"

        If V1.Value <> "" Then
            Try
                If txtIdContratto.Value = "-1" Then
                    txtIdContratto.Value = CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value
                End If

                PAR.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
                PAR.SettaCommand(PAR)
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
                '‘'‘par.cmd.Transaction = par.myTrans

                PAR.cmd.CommandText = "DELETE FROM SISCOM_MI.BOL_SCHEMA WHERE ID=" & V1.Value
                PAR.cmd.ExecuteNonQuery()

                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                & "VALUES (" & txtIdContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                & "'F06','" & PAR.PulisciStrSql(VDescr.Value) & "')"
                PAR.cmd.ExecuteNonQuery()
                CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"
                Response.Write("<SCRIPT>alert('Operazione effettuata. Premere il pulsante SALVA per confermare!');</SCRIPT>")
                CaricaSchema()

            Catch ex As Exception
                CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
                Session.Item("LAVORAZIONE") = "0"
                PAR.myTrans.Rollback()
                PAR.OracleConn.Close()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End Try
        Else
            Response.Write("<SCRIPT>alert('Selezionare un elemento della lista!');</SCRIPT>")
        End If

    End Sub

    Protected Sub img_InserisciSchema_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_InserisciSchema.Click

        Try
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Dim numero_rate As Integer = 12

            PAR.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
            PAR.SettaCommand(PAR)
            PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)

            PAR.cmd.CommandText = "select nro_rate from SISCOM_MI.rapporti_utenza where id=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                numero_rate = PAR.IfNull(myReader("nro_rate"), 0)
            End If
            myReader.Close()


            If PAR.IfEmpty(txtImportoVoce.Text, "") <> "" And PAR.IfEmpty(txtDaRata.Text, "") <> "" And PAR.IfEmpty(txtPerRate.Text, "") <> "" And (Val(txtDaRata.Text) + Val(txtPerRate.Text)) <= numero_rate + 1 Then

                PAR.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                            & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) " _
                            & "Values " _
                            & "(SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL," & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value _
                            & "," & CType(Me.Page.FindControl("txtIdUnita"), HiddenField).Value & "," & PAR.RicavaEsercizioCorrente _
                            & "," & cmbVoceSchema.SelectedValue & " ," _
                            & PAR.VirgoleInPunti(txtImportoVoce.Text.Replace(".", "") * txtPerRate.Text) _
                            & "," & txtDaRata.Text & "," & txtPerRate.Text & "," & PAR.VirgoleInPunti(txtImportoVoce.Text.Replace(".", "")) _
                            & "," & cmbAnnoSchema.SelectedItem.Value & ")"
                PAR.cmd.ExecuteNonQuery()
                'PAR.cmd.CommandText = "select SEQ_BOL_SCHEMA.CURRVAL FROM DUAL"
                'Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                'If myReaderS.Read Then
                '    If cmbAnnoSchema.SelectedItem.Value = CType(Me.Page.FindControl("annoschema"), HiddenField).Value Then
                '        lstSchemaBollette.Items.Add(New ListItem(PAR.MiaFormat(cmbVoceSchema.SelectedItem.Text, 50) & " " _
                '                                                 & PAR.MiaFormat(txtImportoVoce.Text, 15) & " " _
                '                                                 & PAR.MiaFormat(txtDaRata.Text, 10) & " " _
                '                                                 & PAR.MiaFormat(txtPerRate.Text, 10), myReaderS(0)))
                '    End If
                'End If
                'myReaderS.Close()
                If txtIdContratto.Value = "-1" Then
                    txtIdContratto.Value = CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value
                End If
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                & "VALUES (" & txtIdContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                & "'F05','" & PAR.PulisciStrSql(cmbVoceSchema.SelectedItem.Text) & "')"
                PAR.cmd.ExecuteNonQuery()

                CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"

                txtImportoVoce.Text = ""
                txtPerRate.Text = ""
                txtDaRata.Text = ""
                txtAppare.Value = ""
                img_InserisciSchema.Visible = True
                img_SalvaSchema.Visible = False
                CaricaSchema()
                Response.Write("<script>alert('Operazione effettuata! Premere il pulante SALVA per rendere effettive le modifiche.');</script>")
            Else
                Response.Write("<script>alert('Valori assenti, non corretti o intervallo date non valido!');</script>")
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Sub DisattivaTutto()
        'lstSchemaBollette.Enabled = False
        img_EliminaOspite.Visible = False
        img_InserisciSchema.Visible = False
        cmbAnnoSchema.Enabled = False
    End Sub



   
    Protected Sub img_ModificaSchema_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles img_ModificaSchema.Click
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        If V1.Value <> "" Then
            Try

                IdBolSchema.Value = V1.Value
                inserimento.Value = "0"
                If txtIdContratto.Value = "-1" Then
                    txtIdContratto.Value = CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value
                End If
                PAR.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
                PAR.cmd = PAR.OracleConn.CreateCommand()
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)

                PAR.cmd.CommandText = "SELECT BOL_SCHEMA.*,T_VOCI_BOLLETTA.SELEZIONABILE FROM SISCOM_MI.T_VOCI_BOLLETTA,SISCOM_MI.BOL_SCHEMA WHERE T_VOCI_BOLLETTA.ID=BOL_SCHEMA.ID_VOCE AND BOL_SCHEMA.ID=" & V1.Value
                Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReaderS.Read Then
                    If PAR.IfNull(myReaderS("SELEZIONABILE"), "0") = "1" Then
                        txtImportoVoce.Text = Format(PAR.IfNull(myReaderS("IMPORTO_SINGOLA_RATA"), "0,00"), "0.00")
                        txtDaRata.Text = PAR.IfNull(myReaderS("DA_RATA"), "1")
                        txtPerRate.Text = PAR.IfNull(myReaderS("PER_RATE"), "1")
                        cmbVoceSchema.SelectedIndex = -1
                        cmbVoceSchema.Items.FindByValue(PAR.IfNull(myReaderS("ID_VOCE"), "1")).Selected = True
                        cmbAnnoSchema.SelectedIndex = -1
                        cmbAnnoSchema.Items.FindByValue(PAR.IfNull(myReaderS("ANNO"), Year(Now))).Selected = True
                        img_InserisciSchema.Visible = False
                        img_SalvaSchema.Visible = True
                        txtAppare.Value = "2"
                    Else
                        Response.Write("<SCRIPT>alert('ATTENZIONE...questa voce non può essere modificata!');</SCRIPT>")
                        txtAppare.Value = ""
                    End If
                End If
                myReaderS.Close()
                CaricaSchema()

            Catch ex As Exception
                CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
                Session.Item("LAVORAZIONE") = "0"
                PAR.myTrans.Rollback()
                PAR.OracleConn.Close()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End Try
        Else
            Response.Write("<SCRIPT>alert('Selezionare un elemento della lista!');</SCRIPT>")
            txtAppare.Value = ""
        End If
    End Sub



    Protected Sub img_SalvaSchema_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles img_SalvaSchema.Click
        Try
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"

            If inserimento.Value = "1" Then
                img_InserisciSchema.Visible = True
                img_SalvaSchema.Visible = False
                Response.Write("<script>alert('Operazione non effettuata, Riprovare.');</script>")
                Exit Sub
            End If
            PAR.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
            PAR.cmd = PAR.OracleConn.CreateCommand()
            PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)

            Dim numero_rate As Integer = 12

            PAR.cmd.CommandText = "select nro_rate from SISCOM_MI.rapporti_utenza where id=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                numero_rate = PAR.IfNull(myReader("nro_rate"), 0)
            End If
            myReader.Close()
            Dim sEvento As String = ""
            If PAR.IfEmpty(txtImportoVoce.Text, "") <> "" And PAR.IfEmpty(txtDaRata.Text, "") <> "" And PAR.IfEmpty(txtPerRate.Text, "") <> "" And (Val(txtDaRata.Text) + Val(txtPerRate.Text)) <= numero_rate + 1 Then
                PAR.cmd.CommandText = "SELECT BOL_SCHEMA.*,t_voci_bolletta.descrizione as voce FROM SISCOM_MI.t_voci_bolletta,SISCOM_MI.BOL_SCHEMA WHERE t_voci_bolletta.id=bol_schema.id_voce and bol_schema.ID=" & IdBolSchema.Value
                Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReaderS.Read Then
                    sEvento = "La voce " & PAR.IfNull(myReaderS("VOCE"), "") & " - Anno " & PAR.IfNull(myReaderS("ANNO"), "----") & " - Importo " & PAR.IfNull(myReaderS("importo_singola_rata"), "") & " - da rata " & PAR.IfNull(myReaderS("DA_RATA"), "1") & " e per rate " & PAR.IfNull(myReaderS("PER_RATE"), "1") & " è stata modificata in "
                End If
                myReaderS.Close()

                PAR.cmd.CommandText = "UPDATE SISCOM_MI.BOL_SCHEMA SET ID_VOCE=" & cmbVoceSchema.SelectedValue & ",IMPORTO=" & PAR.VirgoleInPunti(txtImportoVoce.Text * txtPerRate.Text) & ",DA_RATA=" & txtDaRata.Text & ",PER_RATE=" & txtPerRate.Text & ",IMPORTO_SINGOLA_RATA=" & PAR.VirgoleInPunti(txtImportoVoce.Text) & ",ANNO=" & cmbAnnoSchema.SelectedItem.Value & "  WHERE ID=" & IdBolSchema.Value & " AND ID_CONTRATTO=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value
                PAR.cmd.ExecuteNonQuery()
                sEvento = sEvento & PAR.PulisciStrSql(cmbVoceSchema.SelectedItem.Text) & " - Anno " & cmbAnnoSchema.SelectedItem.Value & " - Importo " & txtImportoVoce.Text & " - da rata " & txtDaRata.Text & " e per rate " & txtPerRate.Text

                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                & "VALUES (" & txtIdContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                & "'F184','" & PAR.PulisciStrSql(sEvento) & "')"
                PAR.cmd.ExecuteNonQuery()

                CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"

                txtImportoVoce.Text = ""
                txtPerRate.Text = ""
                txtDaRata.Text = ""
                txtAppare.Value = ""
                IdBolSchema.Value = ""

                img_InserisciSchema.Visible = True
                img_SalvaSchema.Visible = False


                CaricaSchema()

                Response.Write("<script>alert('Operazione effettuata! Premere il pulante SALVA per rendere effettive le modifiche.');</script>")
            Else
                Response.Write("<script>alert('Valori assenti, non corretti o intervallo rate non valido!');</script>")
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
        'Try
        '    CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"

        '    PAR.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
        '    PAR.SettaCommand(PAR)
        '    PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
        '    '‘par.cmd.Transaction = par.myTrans

        '    Dim numero_rate As Integer = 12

        '    PAR.cmd.CommandText = "select nro_rate from siscom_mi.rapporti_utenza where id=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value
        '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        '    If myReader.Read Then
        '        numero_rate = PAR.IfNull(myReader("nro_rate"), 0)
        '    End If
        '    myReader.Close()
        '    Dim sEvento As String = ""
        '    If PAR.IfEmpty(txtImportoVoce.Text, "") <> "" And PAR.IfEmpty(txtDaRata.Text, "") <> "" And PAR.IfEmpty(txtPerRate.Text, "") <> "" And (Val(txtDaRata.Text) + Val(txtPerRate.Text)) <= numero_rate + 1 Then
        '        PAR.cmd.CommandText = "SELECT BOL_SCHEMA.*,t_voci_bolletta.descrizione as voce FROM siscom_mi.t_voci_bolletta,SISCOM_MI.BOL_SCHEMA WHERE t_voci_bolletta.id=bol_schema.id_voce and bol_schema.ID=" & IdBolSchema.Value
        '        Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        '        If myReaderS.Read Then
        '            sEvento = "La voce " & PAR.IfNull(myReaderS("VOCE"), "") & " - Anno " & PAR.IfNull(myReaderS("ANNO"), "----") & " - Importo " & PAR.IfNull(myReaderS("importo_singola_rata"), "") & " - da rata " & PAR.IfNull(myReaderS("DA_RATA"), "1") & " e per rate " & PAR.IfNull(myReaderS("PER_RATE"), "1") & " è stata modificata in "
        '        End If
        '        myReaderS.Close()

        '        PAR.cmd.CommandText = "UPDATE SISCOM_MI.BOL_SCHEMA SET ID_VOCE=" & cmbVoceSchema.SelectedValue & ",IMPORTO=" & PAR.VirgoleInPunti(txtImportoVoce.Text * txtPerRate.Text) & ",DA_RATA=" & txtDaRata.Text & ",PER_RATE=" & txtPerRate.Text & ",IMPORTO_SINGOLA_RATA=" & PAR.VirgoleInPunti(txtImportoVoce.Text) & ",ANNO=" & cmbAnnoSchema.SelectedItem.Value & "  WHERE ID=" & IdBolSchema.Value & " AND ID_CONTRATTO=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value
        '        PAR.cmd.ExecuteNonQuery()
        '        sEvento = sEvento & PAR.PulisciStrSql(cmbVoceSchema.SelectedItem.Text) & " - Anno " & cmbAnnoSchema.SelectedItem.Value & " - Importo " & txtImportoVoce.Text & " - da rata " & txtDaRata.Text & " e per rate " & txtPerRate.Text

        '        PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
        '        & "VALUES (" & txtIdContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
        '        & "'F184','" & PAR.PulisciStrSql(sEvento) & "')"
        '        PAR.cmd.ExecuteNonQuery()

        '        CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"

        '        txtImportoVoce.Text = ""
        '        txtPerRate.Text = ""
        '        txtDaRata.Text = ""
        '        txtAppare.Value = ""
        '        IdBolSchema.Value = ""

        '        lstSchemaBollette.Items.Clear()
        '        Dim kk As Integer = 0
        '        'CARICAMENTO schema bollette
        '        ' Label4 = "SCHEMA VOCI BOLLETTA" & " " & sAnnoSchema & "  -  <a href='SchemaAltriAnni.aspx?CN=" & lIdConnessione & "&ID=" & lIdContratto & "&A=" & sAnnoSchema & "' target='_blank'>Clicca qui per visualizzare lo schema di altri anni</a>"
        '        PAR.cmd.CommandText = "select bol_schema.*,t_voci_bolletta.descrizione from siscom_mi.t_voci_bolletta, siscom_mi.bol_schema where t_voci_bolletta.id=bol_schema.id_voce and bol_schema.id_contratto=" & txtIdContratto.Value & " and anno=" & CType(Me.Page, Object).sAnnoSchema & " order by t_voci_bolletta.descrizione,(-importo) asc"
        '        Dim myReader22 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        '        Do While myReader22.Read
        '            lstSchemaBollette.Items.Add(New ListItem(PAR.MiaFormat(PAR.IfNull(myReader22("descrizione"), ""), 50) & " " & PAR.MiaFormat(Format(PAR.IfNull(myReader22("importo_singola_rata"), "0"), "0.00"), 15) & " " & PAR.MiaFormat(PAR.IfNull(myReader22("da_rata"), ""), 10) & " " & PAR.MiaFormat(PAR.IfNull(myReader22("per_rate"), ""), 10), myReader22("ID")))
        '            If kk Mod 2 <> 0 Then
        '                lstSchemaBollette.Items(kk).Attributes.CssStyle.Add("background-color", "#dcdada")
        '            Else
        '                lstSchemaBollette.Items(kk).Attributes.CssStyle.Add("background-color", "white")
        '            End If
        '            kk = kk + 1
        '        Loop
        '        myReader22.Close()

        '        Response.Write("<script>alert('Operazione effettuata! Premere il pulante SALVA per rendere effettive le modifiche.');</script>")
        '    Else
        '        Response.Write("<script>alert('Valori assenti, non corretti o intervallo rate non valido!');</script>")
        '    End If

        'Catch ex As Exception
        '    CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        '    Session.Item("LAVORAZIONE") = "0"
        '    PAR.myTrans.Rollback()
        '    PAR.OracleConn.Close()
        '    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
        '    Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        'End Try
    End Sub

    Private Sub CaricaSchema()
        Try
            Dim LL As Integer = 0
            'CARICAMENTO schema bollette

            'MODIFICATO ORDINE DI VISUALIZZAZIONE VOCI (PER IMPORTO ASC)
            PAR.cmd.CommandText = "select bol_schema.*,t_voci_bolletta.descrizione from SISCOM_MI.t_voci_bolletta, SISCOM_MI.bol_schema where t_voci_bolletta.id=bol_schema.id_voce and bol_schema.id_contratto=" & txtIdContratto.Value & " and anno=" & CType(Me.Page, Object).sAnnoSchema & " order by t_voci_bolletta.descrizione,(-importo) ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "BOL_SCHEMA")
            DataGridSchema.DataSource = ds
            DataGridSchema.DataBind()


        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub DataGridSchema_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridSchema.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFD784';};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='lightsteelblue';" _
                                & "document.getElementById('Tab_SchemaBollette1_V1').value=" & e.Item.Cells(0).Text & ";document.getElementById('Tab_SchemaBollette1_VDescr').value='" & Replace(e.Item.Cells(1).Text, "'", "\'") & "';")
        End If
    End Sub

    Protected Sub DataGridSchema_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DataGridSchema.SelectedIndexChanged

    End Sub

    Private Function MESE(ByVal TESTO As String) As String
        Select Case TESTO
            Case "01"
                MESE = "Gennaio"
            Case "02"
                MESE = "Febbraio"
            Case "03"
                MESE = "Marzo"
            Case "04"
                MESE = "Aprile"
            Case "05"
                MESE = "Maggio"
            Case "06"
                MESE = "Giugno"
            Case "07"
                MESE = "Luglio"
            Case "08"
                MESE = "Agosto"
            Case "09"
                MESE = "Settembre"
            Case "10"
                MESE = "Ottobre"
            Case "11"
                MESE = "Novembre"
            Case "12"
                MESE = "Dicembre"
        End Select

    End Function

    Protected Sub imgModificaPS_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgModificaPS.Click
        PAR.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
        PAR.cmd = PAR.OracleConn.CreateCommand()
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
        'par.cmd.Transaction = par.myTrans

        PAR.cmd.CommandText = "SELECT * from siscom_mi.RAPPORTI_UTENZA_PROSSIMA_BOL where id_contratto=" & txtIdContratto.Value
        Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        If myReaderB.Read Then
            lblProssimaEmissione.Text = "Prossima Emissione: " & Mid(PAR.IfNull(myReaderB("PROSSIMA_BOLLETTA"), "XXXXXX"), 1, 4) & "/" & MESE(Mid(PAR.IfNull(myReaderB("PROSSIMA_BOLLETTA"), "XXXXXX"), 5, 2))
        End If
        myReaderB.Close()
    End Sub
End Class
