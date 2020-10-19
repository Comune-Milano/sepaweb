
Partial Class MANUTENZIONI_ConsistenzaUC
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String


    Private Property vobj() As String
        Get
            If Not (ViewState("par_vobj") Is Nothing) Then
                Return CStr(ViewState("par_vobj"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vobj") = value
        End Set

    End Property

    Private Property vId() As Long
        Get
            If Not (ViewState("par_id") Is Nothing) Then
                Return CLng(ViewState("par_id"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_id") = value
        End Set

    End Property
    Private Property vTipo() As String
        Get
            If Not (ViewState("par_Tipo") Is Nothing) Then
                Return CStr(ViewState("par_Tipo"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Tipo") = value
        End Set

    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            Exit Sub
        End If
        If Not IsPostBack Then
            vId = Session.Item("ID")
            vTipo = Session.Item("TIPO")
            'Session.Add("ID", vId)
            'Session.Add("TIPO", vTipo)

            sStringaSql = Session.Item("PED")
            sStringaSql = sStringaSql.ToUpper
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

            Try

                If vId <> 0 And vTipo = "UC" Then
                    '******PRIMA CONNESSIONE**********

                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)
                    par.myTrans = par.OracleConn.BeginTransaction()
                    ‘‘par.cmd.Transaction = par.myTrans
                    HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

                    'If par.OracleConn.State = Data.ConnectionState.Open Then
                    '    Exit Sub
                    'Else
                    '    par.OracleConn.Open()
                    '    par.SettaCommand(par)
                    'End If

                    Me.DrlSchede.Items.Add(New ListItem("- - - - - - - - - - - - - - - - - - ", -1))
                    Me.DrlSchede.Items.Add(New ListItem("Sc. A-RILIEVO STRUTTURE", 0))
                    Me.DrlSchede.Items.Add(New ListItem("Sc. B-SCHEDA RILIEVO CHIUSURE", 1))
                    Me.DrlSchede.Items.Add(New ListItem("Sc. C-SCHEDA RILIEVO PARTIZIONI INTERNE", 2))
                    Me.DrlSchede.Items.Add(New ListItem("Sc. D-SCHEDA RILIEVO PAVIMENTAZIONI INTERNE", 3))
                    Me.DrlSchede.Items.Add(New ListItem("Sc. E-SCHEDA RILIEVO PROTEZIONE E DELIMITAZIONI", 4))
                    Me.DrlSchede.Items.Add(New ListItem("Sc. F-SCHEDA RILIEVO ATTREZZATURE E SPAZI INTERNI", 5))
                    Me.DrlSchede.Items.Add(New ListItem("Sc. G-SCHEDA RILIEVO ATTREZZATURE ED ARREDI ESTERNI", 6))
                    Me.DrlSchede.Items.Add(New ListItem("Sc. H-SCHEDA RILIEVO IMPIANTI FISSI DI TRASPORTO", 7))
                    Me.DrlSchede.Items.Add(New ListItem("Sc. I-SCHEDA RILIEVO IMPIANTI  RISCALDAMENTO E PRODUZIONE H2O CENTRALIZZATA", 8))
                    Me.DrlSchede.Items.Add(New ListItem("Sc. L-SCHEDA RILIEVO IMPIANTI IDRICO SANITARI", 9))
                    Me.DrlSchede.Items.Add(New ListItem("Sc. M-SCHEDA RILIEVO IMPIANTI ANTINCENDIO", 10))
                    Me.DrlSchede.Items.Add(New ListItem("Sc. N-SCHEDA RILIEVO RETE SCARICO / FOGNARIA", 11))
                    Me.DrlSchede.Items.Add(New ListItem("Sc. O-SCHEDA RILIEVO IMPIANTI SMALTIMENTO AERIFORMI ", 12))
                    Me.DrlSchede.Items.Add(New ListItem("Sc. P-SCHEDA RILIEVO INPIANTO DI DISTRIBUZIONE GAS ", 13))
                    Me.DrlSchede.Items.Add(New ListItem("Sc. Q-SCHEDA RILIEVO IMPIANTI ELETTRICI", 14))
                    Me.DrlSchede.Items.Add(New ListItem("Sc. R-SCHEDA RILIEVO IMPIANTI TELEVISIVI", 15))
                    Me.DrlSchede.Items.Add(New ListItem("Sc. S-SCHEDA RILIEVO IMPIANTI CITOFONI", 16))
                    Me.DrlSchede.Items.Add(New ListItem("Sc. T-SCHEDA RILIEVO IMPIANTI DI TELECOMUNICAZIONE", 17))
                    Me.DrlSchede.Enabled = False

                    If sStringaSql.Contains("EDIFICI") Then

                        par.cmd.CommandText = "Select indirizzi.id, indirizzi.descrizione, indirizzi.civico, indirizzi.cap, indirizzi.localita, indirizzi.cod_comune, unita_comuni.cod_unita_comune, TIPO_UNITA_COMUNE.DESCRIZIONE  AS TIPO_UNITA  from SISCOM_MI.tipo_unita_comune, SISCOM_MI.indirizzi, SISCOM_MI.edifici, SISCOM_MI.unita_comuni where indirizzi.id = edifici.id_indirizzo_principale and unita_comuni.cod_tipologia = tipo_unita_comune.cod and unita_comuni.id_edificio = edifici.id and unita_comuni.id = " & vId
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            LblDescIndirizzo.Text = par.IfNull(myReader1("DESCRIZIONE"), "--")
                            LblCap.Text = par.IfNull(myReader1("CAP"), "--")
                            LblCivico.Text = par.IfNull(myReader1("CIVICO"), "--")
                            LblLocali.Text = par.IfNull(myReader1("LOCALITA"), "--")
                            'LblCodUnita.Text = par.IfNull(myReader1("COD_UNITA_COMUNE"), "--")
                            lblTipologia.Text = par.IfNull(myReader1("TIPO_UNITA"), "ND")
                            Me.lblAnno.Text = 0

                            'lblAnno.Text = par.IfNull(par.FormattaData(myReader1("DATA_COSTRUZIONE")), "0")
                            'lblfoglio.Text = par.IfNull(myReader1("FOGLIO"), "--")
                            'lblmap.Text = par.IfNull(myReader1("NUMERO"), "--")
                            vobj = par.IfNull(myReader1("COD_UNITA_COMUNE"), "--")
                        End If
                    ElseIf sStringaSql.Contains("COMPLESSI") Then
                        par.cmd.CommandText = "Select indirizzi.id, indirizzi.descrizione, indirizzi.civico, indirizzi.cap, indirizzi.localita, indirizzi.cod_comune, unita_comuni.cod_unita_comune, TIPO_UNITA_COMUNE.DESCRIZIONE  AS TIPO_UNITA  from SISCOM_MI.tipo_unita_comune, SISCOM_MI.indirizzi, SISCOM_MI.complessi_immobiliari, SISCOM_MI.unita_comuni where indirizzi.id = complessi_immobiliari.id_indirizzo_riferimento and unita_comuni.cod_tipologia = tipo_unita_comune.cod and unita_comuni.id_complesso = complessi_immobiliari.id and unita_comuni.id = " & vId
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            LblDescIndirizzo.Text = par.IfNull(myReader1("DESCRIZIONE"), "--")
                            LblCap.Text = par.IfNull(myReader1("CAP"), "--")
                            LblCivico.Text = par.IfNull(myReader1("CIVICO"), "--")
                            LblLocali.Text = par.IfNull(myReader1("LOCALITA"), "--")
                            'LblCodUnita.Text = par.IfNull(myReader1("COD_UNITA_COMUNE"), "--")
                            lblTipologia.Text = par.IfNull(myReader1("TIPO_UNITA"), "ND")

                            Me.lblAnno.Text = 0
                            vobj = par.IfNull(myReader1("COD_UNITA_COMUNE"), "--")

                        End If
                    End If
                    myReader1.Close()
                End If

                '************RIEMPIO LE COMBO CON I DATI DA PRENDERE DAL DB ******************

                par.cmd.CommandText = "Select * from SISCOM_MI.ubicazioni_uc"
                myReader1 = par.cmd.ExecuteReader()
                cmbUbicazione.Items.Add(New ListItem(" ", -1))
                While myReader1.Read
                    cmbUbicazione.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()

                par.cmd.CommandText = "Select * from SISCOM_MI.destinazioni_uso_uc"
                myReader1 = par.cmd.ExecuteReader()
                cmbDestUso.Items.Add(New ListItem(" ", -1))
                While myReader1.Read
                    cmbDestUso.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()

                par.cmd.CommandText = "Select * from SISCOM_MI.tipologia_uc"
                myReader1 = par.cmd.ExecuteReader()
                cmbTipologia.Items.Add(New ListItem(" ", -1))
                While myReader1.Read
                    cmbTipologia.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()

                par.cmd.CommandText = "Select * from SISCOM_MI.stato_uc"
                myReader1 = par.cmd.ExecuteReader()
                cmbStatoFisico.Items.Add(New ListItem(" ", -1))
                While myReader1.Read
                    cmbStatoFisico.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()




                '************FINE**********
                Session.Add("LAVORAZIONE", "1")

                'par.OracleConn.Close()
                ApriEsistente()


            Catch ex As Exception
                Me.LblErrore.Visible = True
                LblErrore.Text = ex.Message
                par.myTrans.Rollback()
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
            End Try

        End If
        Me.txtindietro.Text = txtindietro.Text - 1

        BindGrid()
        BindGrid2()

    End Sub
    Private Sub ApriEsistente()
        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        'par.OracleConn.Open()
        'par.SettaCommand(par)
        Try


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

            par.cmd.CommandText = "Select * from SISCOM_MI.SK_CONS_UNITA_COMUNI where id_unita_comune = " & vId
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.btnrilievo.Visible = True
                Me.DrlSchede.Enabled = True
                Me.cmbDestUso.SelectedValue = par.IfNull(myReader1("id_destinazione_uso"), "-1")
                Me.cmbUbicazione.SelectedValue = par.IfNull(myReader1("id_ubicazione"), "-1")
                Me.cmbStatoFisico.SelectedValue = par.IfNull(myReader1("id_stato_fisico"), "-1")
                Me.cmbTipologia.SelectedValue = par.IfNull(myReader1("id_tipologia"), "-1")
                Me.txtNote.Text = par.IfNull(myReader1("NOTE"), "")
                Me.btnrilievo.Visible = True
                Me.DrlSchede.Enabled = True
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        End Try
        'par.OracleConn.Close()

    End Sub
    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Session.Add("LAVORAZIONE", "0")
        par.myTrans.Rollback()
        par.OracleConn.Close()
        HttpContext.Current.Session.Remove("TRANSAZIONE")
        HttpContext.Current.Session.Remove("CONNESSIONE")
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Page.Dispose()
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub
    Private Sub BindGrid()

        'par.OracleConn.Open()
        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        '**********BINDGRID SOTTO TRANSAZIONE FUNZIONANTE CON IL DATASET

        Dim StringaSql As String = "select rownum, ID_UNITA_COMUNE, dotazioni_uc.id_tipologia, descrizione as TipoDotazione, quant from SISCOM_MI.dotazioni_uc, SISCOM_MI.tipologia_dotazioni where dotazioni_uc.id_UNITA_COMUNE =" & vId & " and dotazioni_uc.id_tipologia = tipologia_dotazioni.id"

        par.cmd.CommandText = StringaSql

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "DOMANDE_BANDO,COMP_NUCLEO")

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()

        'par.OracleConn.Close()
    End Sub
    Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        LBLDESCRIZIONE.Text = e.Item.Cells(1).Text
        LBLID.Text = e.Item.Cells(2).Text
        Label6.Text = "Hai selezionato la riga N°: " & e.Item.Cells(0).Text
    End Sub


    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(0).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(2).Text & "';document.getElementById('txtdesc').value='" & e.Item.Cells(1).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(0).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(2).Text & "';document.getElementById('txtdesc').value='" & e.Item.Cells(1).Text & "'")

        End If

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnAdd.Click
        Response.Write("<script>window.open('CarSingolaUC.aspx','VARIAZIONI', 'resizable=no, width=300, height=180');</script>")

        'If Me.txtQuantita.Text <> "" AndAlso Me.cmbTipoDotaz.SelectedValue <> -1 Then
        '    par.OracleConn.Open()
        '    par.SettaCommand(par)
        '    Dim i As Integer
        '    For i = 0 To DataGrid1.Items.Count - 1
        '        If cmbTipoDotaz.SelectedValue = DataGrid1.Items(i).Cells(0).Text Then
        '            Response.Write("<script>alert('Dotazione già iserita!')</script>")
        '            Exit Sub
        '            Me.txtQuantita.Text = ""
        '            Me.cmbTipoDotaz.SelectedValue = -1

        '        End If
        '    Next
        '    par.cmd.CommandText = "insert into SISCOM_MI.dotazioni_uc (ID_UNITA_COMUNE, ID_TIPOLOGIA, QUANT) values (" & vId & ", " & Me.cmbTipoDotaz.SelectedValue & ", " & Me.txtQuantita.Text & ")"
        '    par.cmd.ExecuteNonQuery()
        '    par.cmd.CommandText = ""
        '    Me.txtQuantita.Text = ""
        '    Me.cmbTipoDotaz.SelectedValue = -1
        '    par.OracleConn.Close()
        '    BindGrid()
        'End If

    End Sub

    Protected Sub BtnDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnDelete.Click
        Try

            If Me.txtmia.Text <> "" Then
                '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
                'par.OracleConn.Open()
                'par.SettaCommand(par)

                par.cmd.CommandText = "delete from SISCOM_MI.dotazioni_uc where id_unita_comune = " & vId & " and ID_TIPOLOGIA = " & txtdesc.Text
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                'par.OracleConn.Close()
                DataGrid1.CurrentPageIndex = 0
                BindGrid()
                txtmia.Text = ""
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        End Try

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Me.salva()
        'Me.txtindietro.Text = CInt(txtindietro.Text) - 1

    End Sub
    Private Function QualeAprire()

        Select Case Me.DrlSchede.SelectedValue
            Case 0
                Response.Write("<script>window.open('ScA.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 1

                Response.Write("<script>window.open('ScB.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 2
                Response.Write("<script>window.open('ScC.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 3
                Response.Write("<script>window.open('ScD.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 4
                Response.Write("<script>window.open('ScE.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 5
                Response.Write("<script>window.open('ScF.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 6
                Response.Write("<script>window.open('ScG.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 7
                Response.Write("<script>window.open('ScH.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 8
                Response.Write("<script>window.open('ScI.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 9
                Response.Write("<script>window.open('ScL.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 10
                Response.Write("<script>window.open('ScM.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 11
                Response.Write("<script>window.open('ScN.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 12
                Response.Write("<script>window.open('ScO.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 13
                Response.Write("<script>window.open('ScP.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 14
                Response.Write("<script>window.open('ScQ.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 15
                Response.Write("<script>window.open('ScR.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 16
                Response.Write("<script>window.open('ScS.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 17
                Response.Write("<script>window.open('ScT.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

        End Select
    End Function

    Private Sub salva()
        'par.OracleConn.Open()
        'par.SettaCommand(par)
        Try
            '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            If btnrilievo.Visible = False Then
                If Me.cmbStatoFisico.SelectedValue <> "-1" Then

                    'ORE 14.45 MODIFICA MARCO NON SONO VINCOLANTI UBICAZIONE, DEST_USO E TIPOLOGIA!
                    'If Me.cmbUbicazione.SelectedValue <> -1 AndAlso Me.cmbTipologia.SelectedValue <> -1 AndAlso Me.cmbUbicazione.SelectedValue <> -1 Then

                    par.cmd.CommandText = "insert into SISCOM_MI.sk_cons_unita_comuni (id_operatore,id_unita_comune, data_inserimento, id_destinazione_uso, id_ubicazione, id_tipologia, anno_costruzione, foglio, mappale, id_stato_fisico, note)" _
                    & "values(" & Session("ID_OPERATORE") & "," & vId & ", " & Format(Now, "yyyyMMdd") & ", " & RitornaNullSeMenoUno(Me.cmbDestUso.SelectedValue.ToString) & ", " & RitornaNullSeMenoUno(Me.cmbUbicazione.SelectedValue.ToString) & ", " & RitornaNullSeMenoUno(Me.cmbTipologia.SelectedValue.ToString) & ", '" & Me.lblAnno.Text & "','" & Me.lblfoglio.Text & "', '" & Me.lblmap.Text & "', " & Me.cmbStatoFisico.SelectedValue.ToString & ", '" & par.PulisciStrSql(Me.txtNote.Text) & "' )"
                    par.cmd.ExecuteNonQuery()
                    Me.btnrilievo.Visible = True
                    Me.DrlSchede.Enabled = True

                    Response.Write("<script>alert('Operazione eseguita correttamente!')</script>")
                    'Else
                    '    Response.Write("<script>alert('Riempire tutti i campi!')</script>")

                    'End If
                Else
                    Response.Write("<script>alert('Definire lo stato fisico del bene!')</script>")

                End If

            ElseIf btnrilievo.Visible = True Then

                'par.cmd.CommandText = "delete SISCOM_MI.sk_cons_unita_comuni where id_unita_comune=" & vId
                'par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE SISCOM_MI.sk_cons_unita_comuni SET id_operatore= " & Session("ID_OPERATORE") & ", data_inserimento = " & Format(Now, "yyyyMMdd") & ", id_destinazione_uso = " & RitornaNullSeMenoUno(Me.cmbDestUso.SelectedValue.ToString) & ", id_ubicazione = " & RitornaNullSeMenoUno(Me.cmbUbicazione.SelectedValue.ToString) & ", id_tipologia = " & RitornaNullSeMenoUno(Me.cmbTipologia.SelectedValue.ToString) & ",  foglio = '" & Me.lblfoglio.Text & "', mappale = '" & Me.lblmap.Text & "', id_stato_fisico = " & RitornaNullSeMenoUno(Me.cmbStatoFisico.SelectedValue.ToString) & ", note = '" & par.PulisciStrSql(Me.txtNote.Text) & "'" _
                & " WHERE ID_UNITA_COMUNE = " & vId & ""
                par.cmd.ExecuteNonQuery()
                Response.Write("<script>alert('Operazione eseguita correttamente!')</script>")

            End If
            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        Catch ex As Exception
            'par.OracleConn.Close()
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
        End Try

        'par.OracleConn.Close()

    End Sub
    Private Sub BindGrid2()

        'par.OracleConn.Open()
        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        Dim StringaSql As String = "select ROWNUM, ID_UNITA_COMUNE, Anomalie_uc.id_tipologia, descrizione as Tipo, valore from SISCOM_MI.anomalie_uc, SISCOM_MI.tipo_anomalie_uc where anomalie_uc.id_UNITA_COMUNE =" & vId & " and anomalie_uc.id_tipologia = tipo_anomalie_uc.id"
        par.cmd.CommandText = StringaSql

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "DOMANDE_BANDO,COMP_NUCLEO")
        DataGrid2.DataSource = ds
        DataGrid2.DataBind()

        'par.OracleConn.Close()
    End Sub

    Protected Sub BtnAddAnomalie_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnAddAnomalie.Click

        Response.Write("<script>window.open('AnomCantUC.aspx','VARIAZIONI', 'resizable=no, width=300, height=180');</script>")
        'Me.txtindietro.Text = txtindietro.Text - 1

        'If Me.TxtValore.Text <> "" AndAlso Me.cmbtipoanomalie.SelectedValue <> -1 Then
        '    par.OracleConn.Open()
        '    par.SettaCommand(par)
        '    Dim i As Integer
        '    For i = 0 To DataGrid2.Items.Count - 1
        '        If cmbtipoanomalie.SelectedValue = DataGrid2.Items(i).Cells(0).Text Then
        '            Response.Write("<script>alert('Anomalia già iserita!')</script>")
        '            Exit Sub
        '            Me.TxtValore.Text = ""
        '            Me.cmbtipoanomalie.SelectedValue = -1
        '        End If
        '    Next
        '    par.cmd.CommandText = "insert into SISCOM_MI.anomalie_uc (ID_UNITA_COMUNE, ID_TIPOLOGIA, VALORE) values (" & vId & ", " & Me.cmbtipoanomalie.SelectedValue & ", " & par.VirgoleInPunti(Me.TxtValore.Text) & ")"
        '    par.cmd.ExecuteNonQuery()
        '    par.cmd.CommandText = ""
        '    Me.TxtValore.Text = ""
        '    Me.cmbtipoanomalie.SelectedValue = -1
        '    par.OracleConn.Close()
        '    BindGrid2()
        'End If

    End Sub

    Protected Sub DataGrid2_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid2.EditCommand
        Lbltipo.Text = e.Item.Cells(1).Text
        LblId2.Text = e.Item.Cells(2).Text
        Label20.Text = "Hai selezionato la riga N°: " & e.Item.Cells(0).Text

    End Sub

    Protected Sub DataGrid2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmiaanom').value='Hai selezionato la riga n. " & e.Item.Cells(0).Text & "';document.getElementById('txtidanom').value='" & e.Item.Cells(2).Text & "';document.getElementById('txtdescanom').value='" & e.Item.Cells(1).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmiaanom').value='Hai selezionato la riga n. " & e.Item.Cells(0).Text & "';document.getElementById('txtidanom').value='" & e.Item.Cells(2).Text & "';document.getElementById('txtdescanom').value='" & e.Item.Cells(1).Text & "'")

        End If
    End Sub

    Protected Sub DataGrid2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid2.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid2()
        End If

    End Sub

    Protected Sub btnDelAnomalie_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDelAnomalie.Click
        Try

            If Me.txtmiaanom.Text <> "" Then
                'par.OracleConn.Open()
                'par.SettaCommand(par)

                '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                If Me.txtdescanom.Text <> "" Then

                    par.cmd.CommandText = "delete from SISCOM_MI.anomalie_uc where id_unita_comune = " & vId & " and ID_TIPOLOGIA = " & txtdescanom.Text
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                Else

                End If

                'par.OracleConn.Close()
                DataGrid2.CurrentPageIndex = 0
                BindGrid2()
                txtmiaanom.Text = ""
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
        End Try

    End Sub

    Protected Sub btnrilievo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnrilievo.Click
        QualeAprire()
        'Me.txtindietro.Text = txtindietro.Text - 1

    End Sub
    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String
        Try
            Dim a As String
            If valorepass = "-1" Then
                a = "Null"
            ElseIf valorepass <> "-1" Then
                a = "'" & valorepass & "'"
            End If
            Return a
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Function


    Protected Sub ImageButton4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton4.Click
        Session.Add("LAVORAZIONE", "0")
        par.myTrans.Rollback()
        par.OracleConn.Close()
        HttpContext.Current.Session.Remove("TRANSAZIONE")
        HttpContext.Current.Session.Remove("CONNESSIONE")
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Page.Dispose()
        Response.Write("<script>history.go(" & txtindietro.Text & ");</script>")

    End Sub
End Class
